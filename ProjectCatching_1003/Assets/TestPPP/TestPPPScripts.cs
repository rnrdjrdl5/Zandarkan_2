using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class TestPPPScripts : MonoBehaviour {

    public PostProcessingProfile ppp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {

            gameObject.GetComponent<PostProcessingBehaviour>().profile = ppp;
        }
	}
}
