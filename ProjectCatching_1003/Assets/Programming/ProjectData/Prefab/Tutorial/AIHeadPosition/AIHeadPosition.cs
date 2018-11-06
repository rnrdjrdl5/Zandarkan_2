using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHeadPosition : MonoBehaviour {

    private void Awake()
    {
        Transform UpHeadTr = gameObject.transform.Find("UpHeadPosition");
    }
}
