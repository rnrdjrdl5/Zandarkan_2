﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGuide : MonoBehaviour {


    // 기본설정
    public GameObject tutorialCanvas;


    public TutorialElement[] mouseTutorialElements;
    public int maxMouseTutorialCount;

    public TutorialElement[] catTutorialElements;
    public int maxCatTutorialCount;



    private int nowTutorialCount;

    public enum EunmTutorialPlayer { CAT, MOUSE };

    public EunmTutorialPlayer tutorialPlayerType;
    public GameObject playerObject;

    // 메세지 오브젝트, 스크립트
    public GameObject messageObject;
    MessageImageScript messageImageScript;

   


    void InitMessageData()
    {
        tutorialCanvas = GameObject.Find("TutorialCanvas");

        if (tutorialCanvas == null) return;
        Transform tr = tutorialCanvas.transform.Find("TutorialMsgImage");

        if (tr == null) return;
        messageObject = tr.gameObject;

        messageImageScript = messageObject.GetComponent<MessageImageScript>();
    }


    // 레이캐스트 유틸리티
    private PointToLocation pointToLocation;




    private void Awake()
    {
        InitMessageData();

        pointToLocation = new PointToLocation();
    }


    // 하위 속성들 설정
    // 수정생각하기
    // 모든 Elements에게 데이터를 주지 말고 if문을 써서 하는게 나을 수도 있다.
    void SettingElements()
    {
        int nowCount = mouseTutorialElements.Length;

        for (int i = 0; i < nowCount; i++)
        {

            // 액션 속성들 설정
            int actionCount = mouseTutorialElements[i].tutorialActions.Length;


           // 수정생각하기
            // 모든 Elements에게 데이터를 주지 말고 if문을 써서 하는게 나을 수도 있다.
            for (int mte = 0; mte < actionCount; mte++) {


                mouseTutorialElements[i].tutorialActions[mte].messageImageScript = messageImageScript;
                mouseTutorialElements[i].tutorialActions[mte].messageObject = messageObject;
                mouseTutorialElements[i].tutorialActions[mte].tutorialCanvas = tutorialCanvas;


            }

            // 조건 설정들 설정
            int conditionCount = mouseTutorialElements[i].tutorialConditions.Length;

            for (int mtc = 0; mtc < conditionCount; mtc++)
            {

                mouseTutorialElements[i].tutorialConditions[mtc].pointToLocation = pointToLocation;
                
            }


            
        }
    }

    public void StartTutorial()
    {
        SettingElements();

        StartCoroutine("TutorialLoop");

        
    }

    IEnumerator TutorialLoop()
    {


        while (true)
        {

            // 1. 컨디션 모두 확인
            if (mouseTutorialElements[nowTutorialCount].CheckCondition())
            {

                TutorialAction[] tutorialActions =
                    mouseTutorialElements[nowTutorialCount].tutorialActions;

                int nowActionCount = tutorialActions.Length;



                for (int i = 0; i < nowActionCount; i++)
                {
                    float waitTime = tutorialActions[i].UseAction();
                    float tempTime = 0.0f;

                    while (true)
                    {
                        if (tempTime >= waitTime) break;

                        else {
                            tempTime += Time.deltaTime;
                            yield return null;
                        }

                    }
                }


                nowTutorialCount++;

                if (nowTutorialCount >= mouseTutorialElements.Length) break;

            }

            yield return null;
            
        }

        yield break;
    }
}
