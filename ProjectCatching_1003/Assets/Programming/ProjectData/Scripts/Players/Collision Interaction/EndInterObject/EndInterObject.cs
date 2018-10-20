using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndInterObject : MonoBehaviour
{


    private PhotonView pv;



    public float FindRad;
    private void Awake()
    {
        gameObject.GetComponent<SphereCollider>().radius = FindRad;
        pv = GetComponentInParent<PhotonView>();
        if (!pv.isMine)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
         if (other.tag == "Interaction")
         {


             // 해당 스크립트를 받아옵니다.
             InteractiveState IS = other.gameObject.GetComponent<InteractiveState>();

            if (IS != null)
            {
                // 사용 불가능하면
                if (!IS.GetCanUseObject())
                {
                    Debug.Log(IS.gameObject.name);
                    Debug.Log("a2sd");
                    int mrCount;
                    if (IS.InterMeshRenderer.Length != 0)
                    {
                        Debug.Log("a3sd");
                        mrCount = IS.InterMeshRenderer.Length;

                        int eMCount = IS.EndInterMaterials.Length;

                        for (int i = 0; i < mrCount; i++)
                        {
                            Debug.Log("as4d");

                            // 해당되는 것과 같은 이름 찾기
                            Material tempMaterials = null;
                            for (int j = 0; j < eMCount; j++)
                            {

                                if (IS.InterMeshRenderer[i].material.name.Split(' ')[0] + "_End" == IS.EndInterMaterials[j].name)
                                {
                                    Debug.Log("as5d");

                                    tempMaterials = IS.EndInterMaterials[j];

                                }
                            }

                            if(tempMaterials != null) IS.InterMeshRenderer[i].material = tempMaterials;
                        }
                    }

                    else if (IS.InterSkinnedMeshRanderer.Length != 0)
                    {

                        mrCount = IS.InterSkinnedMeshRanderer.Length;

                        for (int i = 0; i < mrCount; i++)
                        {
                            int eMCount = IS.EndInterMaterials.Length;
                            Material tempMaterials = null;
                            for (int j = 0; j < eMCount; j++)
                            {
                                if (IS.InterSkinnedMeshRanderer[i].material.name.Split(' ')[0] + "_End" == IS.EndInterMaterials[j].name)
                                {
                                    
                                    tempMaterials = IS.EndInterMaterials[j];
                                }
                            }


                           if(tempMaterials != null) IS.InterSkinnedMeshRanderer[i].material = tempMaterials;
                        }
                    }

                }

                else
                {
                    Debug.Log("qwer33");
                }
            }
         }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interaction")
        {

            // 해당 스크립트를 받아옵니다.
            InteractiveState IS = other.gameObject.GetComponent<InteractiveState>();

            if (IS != null)
            {

                if (!IS.GetCanUseObject())
                {

                    int mrCount;
                    if (IS.InterMeshRenderer.Length != 0)
                    {

                        mrCount = IS.InterMeshRenderer.Length;
                        for (int i = 0; i < mrCount; i++)
                        {
                            IS.InterMeshRenderer[i].material = IS.OriginalMaterials[i];
                        }
                    }

                    else if (IS.InterSkinnedMeshRanderer.Length != 0)
                    {

                        mrCount = IS.InterSkinnedMeshRanderer.Length;
                        for (int i = 0; i < mrCount; i++)
                        {
                            IS.InterSkinnedMeshRanderer[i].material = IS.OriginalMaterials[i];
                        }
                    }
                }

            }
        }
    }
}



