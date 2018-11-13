﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//다른 클라이언트에서 오브젝트를 찾기 위해서 사용 , 
// 점수를 확인하기 위해서도 사용
public class ObjectManager : MonoBehaviour {

    private const int MAX_OBJECT_TYPE = 15 + 1;  // 1 이유 : 시작점이 1이라서 한칸 더 할당함



    private static ObjectManager objectManager;
    public static ObjectManager GetInstance() { return objectManager; }

    public int MaxInterObj { get; set; }
    public int NowInterObj { get; set; }
    public List<GameObject> InterObj;
    public void AddInterObj(GameObject go) { InterObj.Add(go); }
    

    public delegate void RemoveDele();
    public event RemoveDele RemoveEvent;

    private PhotonManager photonManager;
    private PhotonManager GetPhotonManager()
    {
        if (photonManager == null)
        {
            GameObject go = GameObject.Find("PhotonManager");

            if (go == null) Debug.Log("PhotonManager 없습니다.");

            photonManager = go.GetComponent<PhotonManager>();
        }

        return photonManager;
    }

    public NotificationManager notificationManager;


    // 아래부터는 오브젝트 카운팅 용 변수 입니다.

    private int[] mountObjects;     //오브젝트의 총 가중치를 의미함.


    // 아래부터는 오브젝트를 표기하기 위한 변수입니다.

    private int[] maxGUIObject;
    private int[] nowGUIObject;
    
    

    private void Awake()
    {
        objectManager = this;

        mountObjects = new int[MAX_OBJECT_TYPE];

        maxGUIObject = new int[MAX_OBJECT_TYPE];
        nowGUIObject = new int[MAX_OBJECT_TYPE];

        for (int i = 0; i < MAX_OBJECT_TYPE; i++)
        {
            mountObjects[i] = 0;
            maxGUIObject[i] = 0;
            nowGUIObject[i] = 0;
        }
    }

    private void Start()
    {
        MaxInterObj = InterObj.Count;
        NowInterObj = MaxInterObj;
        notificationManager = NotificationManager.GetInstance();
    }

    public GameObject FindObject(int vID)
    {
        for (int i = 0; i < InterObj.Count; i++)
        {


            if (InterObj[i].GetPhotonView().viewID == vID)
            {
                return InterObj[i];
            }
        }
        return null;
    }


    bool isUseDestroyMessage = false;
    public void RemoveObject(int vID)
    {

        for (int i = 0; i < InterObj.Count; i++)
        {
            if (InterObj[i].GetPhotonView().viewID == vID)
            {

                // 점수 받아오기
                float NowCatScore = (float)PhotonNetwork.player.CustomProperties["StoreScore"];

                // 물체정보
                InteractiveState IS = InterObj[i].GetComponent<InteractiveState>();

                // 물체 점수
                float Score;
                if (IS.ObjectHeight == 0 || IS.InterObjectMag == 0) Score = 0;

                // 가중치를 통한 점수설정
                else Score = IS.InterObjectMag * IS.ObjectHeight;

                // 점수 감소
                float NextCatScore = NowCatScore - Score;
                if (NextCatScore <= 0)
                    NextCatScore = 0;


                // 실질적 점수 설정
                PhotonNetwork.player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "StoreScore", NextCatScore } });

                float Persent = NextCatScore / photonManager.MaxCatScore * 100;

                if (!isUseDestroyMessage)
                {
                    if (photonManager.GameTimeOutCondition >= Persent)
                    {
                        notificationManager.NotificationMessage(NotificationManager.EnumNotification.FINISH_DESTROY);
                        isUseDestroyMessage = true;
                    }
                }

                /*
                                float restPersent = NextCatScore / photonManager.MaxCatScore * 100;
                                int restCount = notificationManager.maxRestNotification;




                                // 사용하지 않음.
                                for (int rc = 0; rc < restCount; rc++)
                                {
                                    if (notificationManager.AtLeastRestCount[rc] >= restPersent &&
                                        !notificationManager.isUseRestMessage[rc])
                                    {
                                        notificationManager.NotificationMessage(NotificationManager.EnumNotification.REST,rc);
                                        notificationManager.isUseRestMessage[rc] = true;
                                    }

                                }*/

                // GUI 카운트
                nowGUIObject[(int)IS.interactiveObjectType]--;
                if (nowGUIObject[(int)IS.interactiveObjectType] < 0) {

                    nowGUIObject[(int)IS.interactiveObjectType] = 0;
                }

                NowInterObj -= 1;

                UIManager.GetInstance().interObjectGUIPanelScript.SetText(maxGUIObject, nowGUIObject);
                UIManager.GetInstance().interObjectGUIPanelScript.SetGUICheck(maxGUIObject, nowGUIObject);
                UIManager.GetInstance().interObjectGUIPanelScript.SetNowMaxText(NowInterObj, MaxInterObj);


                // }
                // 오브젝트에서 삭제
                InterObj.Remove(InterObj[i]);

                if (RemoveEvent == null)
                {
                    Debug.LogWarning("비었음. 에러");
                    return;
                }
                RemoveEvent();


                

            }
        }

    }

    public void RegisterObjectMount()
    {

        // 초기화
        for (int i = InterObj.Count - 1; i >= 0; i--)
        {

            InteractiveState IS = InterObj[i].GetComponent<InteractiveState>();


            if (IS != null)
            {

                mountObjects[(int)IS.interactiveObjectType] +=
                    IS.ObjectHeight;

                maxGUIObject[(int)IS.interactiveObjectType]++;
            }
            else { Debug.Log(" Inter 참조 에러, RegisterObjectMount()"); }
        }

        // 현재 GUI 등록
        for (int i = 1; i < MAX_OBJECT_TYPE; i++)
        {
            nowGUIObject[i] = maxGUIObject[i];
        }

        // GUI와 Text 일치화
        UIManager.GetInstance().interObjectGUIPanelScript.SetText(maxGUIObject, nowGUIObject);
        UIManager.GetInstance().interObjectGUIPanelScript.SetNowMaxText(NowInterObj, MaxInterObj);
    }

    public void CalcObjectMag()
    {
        int TotalHeightObjectMount = 0;

        for (int i = 0; i < MAX_OBJECT_TYPE; i++)
        {
            TotalHeightObjectMount += mountObjects[i];
        }


        float Mag = GetPhotonManager().MaxCatScore / TotalHeightObjectMount;        // 배율 구하기
        if (TotalHeightObjectMount == 0)
        {
            Debug.Log(" 총 가중치 0임. 확인 바람. ");
            Mag = 0;
        }
        else { Mag = GetPhotonManager().MaxCatScore / TotalHeightObjectMount; }


        // 비중치 x 배율이다.

        for (int i = 0; i < InterObj.Count; i++)
        {
            InteractiveState IS = InterObj[i].GetComponent<InteractiveState>();

            if (IS != null)
            {
                IS.InterObjectMag = Mag;
            }
        }

        Debug.Log(" 배율 :" + Mag);

    }

    public void DeleteObjPropPlayer()
    {

        for (int i = InterObj.Count-1; i >= 0; i--)
        
        {
            
            InteractiveState IS = InterObj[i].GetComponent<InteractiveState>();


            if(IS != null)
            {
                if (GetPhotonManager().MousePlayerListOneSort.Count <
                    IS.MinPlayerMount && 
                    IS.MinPlayerMount != 0)
                {
                    InterObj.Remove(IS.gameObject);
                    IS.gameObject.SetActive(false);

                    Debug.Log("삭***제완료");
                }


                
                
            }
        }
        Debug.Log("수행중");
    }

    
    
}
