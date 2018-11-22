using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestS : MonoBehaviour {

    public float waitTime;

    public Vector2 firstVec;
    public Vector2 lastVec;

    public GameObject moveObject;

    public KeyCode keyType;

    UIEffect UIs;
    // Use this for initialization
    void Start () {
        UIs = new UIEffect();

        StartCoroutine("WaitCoro");

    }





    // Update is called once per frame
    void Update () {
        /*if (Input.GetKeyDown(keyType))
        {
            UIManager.GetInstance().UpdateEvent -= UIs.EffectEvent;
        }*/
	}

    /* IEnumerator WaitCoro()
     {
         yield return new WaitForSeconds(waitTime);

         UIs.AddMoveEffectNode(moveObject,
          firstVec, lastVec, 0.5f);

         UIs.AddWaitEffectNode(2.0f);

         UIs.AddMoveEffectNode(moveObject,
            lastVec, firstVec, 0.5f);

         UIManager.GetInstance().UpdateEvent += UIs.EffectEvent;
     }
     */
    
}
