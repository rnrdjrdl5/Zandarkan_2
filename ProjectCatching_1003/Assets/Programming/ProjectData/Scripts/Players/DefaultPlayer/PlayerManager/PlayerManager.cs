using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerManager : MonoBehaviour {


    public UIManager uIManager;
    public PhotonView pv;

    // Use this for initialization
    private void Awake()
    {
        SetFallowCamera();


    }
    private void Start()
    {

        // player들 나오면 PhotonManager에 추가한다. 
        PhotonManager photonManager = PhotonManager.GetInstance();

        if(photonManager != null)
            photonManager.AllPlayers.Add(gameObject);


        SetSpringArm();


        uIManager = UIManager.GetInstance();
        pv = gameObject.GetPhotonView();



    }


    
    private void Update()
    {
        //UseNextPage();

        UseExplain();


    }
}
