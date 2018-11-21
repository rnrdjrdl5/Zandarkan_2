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

        pointToLocation = new PointToLocation();

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

        UsePlayerName();

    }

    private PointToLocation pointToLocation;
    public float showPlayerNameDis = 5.0f;
    void UsePlayerName()
    {
        GameObject go = pointToLocation.FindObject(showPlayerNameDis, "OtherPlayer", SpringArmObject.GetInstance().armCamera);

        if (go == null)
        {
            uIManager.playerNamePanelScript.SetActive(false);
            return;
        }


        // 해당 플레이어가 가리키는 phootn을 받아와야 한다.



        PlayerHideBuff phb = go.GetComponent<PlayerHideBuff>();
        if (phb != null) return;

        PhotonView pv = go.GetComponent<PhotonView>();
        if (pv == null) return;

        string CurrentPlayerName = pv.owner.NickName;

        uIManager.playerNamePanelScript.SetActive(true);
        uIManager.playerNamePanelScript.PlayerNameTextText.text = CurrentPlayerName;
        
    }
}
