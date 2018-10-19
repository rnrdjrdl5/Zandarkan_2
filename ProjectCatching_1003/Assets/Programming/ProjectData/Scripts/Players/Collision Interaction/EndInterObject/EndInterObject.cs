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

                    int mrCount = IS.InterMeshRenderer.Length;
                    for (int i = 0; i < mrCount; i++)
                    {
                        IS.InterMeshRenderer[i].material = IS.EndInterMaterials;
                        Debug.Log("반투명 킨다");
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

                    int mrCount = IS.InterMeshRenderer.Length;
                    for (int i = 0; i < mrCount; i++)
                    {
                        IS.InterMeshRenderer[i].material = IS.OriginalMaterials;
                        Debug.Log("반투명 돌린다");
                    }
                }

            }
        }
    }
}



