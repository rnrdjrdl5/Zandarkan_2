using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public partial class NewLobbyRoomPhoton : Photon.PunBehaviour
{

    private enum EnumGameState { LOBBY, FINDROOM, ROOM, TITLE , NONE}
    private EnumGameState gameStateType;


    private delegate void FadeDelegate();
    private delegate void FadeDelegate_One(bool b);

    private FadeDelegate DeleFadeOut;
    private FadeDelegate_One DeleSetOff;

    private FadeDelegate DeleFadeIn;
    private FadeDelegate_One DeleSetOn;

    private FadeDelegate FinishFadeEvent;

    private LobbyUIManager lobbyUIManager;
    private SoundManager soundManager;



    public float AnimationTime = 1.5f;

    public string gameVersion;

    private bool isUseEvent;
    private Animator MenuBookAnimator;


    private void Awake()
    {
        TitleAwake();
        RoomAwake();
        FindRoomAwake();

        gameStateType = EnumGameState.NONE;


        isUseEvent = false;
        GameObject MenuBook = GameObject.Find("MenuBook").gameObject;
        MenuBookAnimator = MenuBook.GetComponent<Animator>();

        GameObject OpeningObject = GameObject.Find("OpeningObjects").gameObject;

        PhotonNetwork.ConnectUsingSettings(gameVersion);

        PhotonNetwork.automaticallySyncScene = true;

        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        
    }

    // Use this for initialization
    void Start() {

        lobbyUIManager = LobbyUIManager.GetInstance();

        LobbyRoomMouseSetting();

        // soundManager.PlayBGSound(SoundManager.EnumBGSound.BG_LOBBY_SOUND);

        TitleStart();
    }

    // Update is called once per frame
    void Update() {

        RoomUpdate();

        TitleUpdate();

        FindRoomUpdate();

    }

    private void LobbyRoomMouseSetting()
    {

        Cursor.lockState = CursorLockMode.Confined;

        Cursor.visible = true;
    }

    /*** 해당 기능 FSM으로 바꿔서 사용할 수 있음. ****/
    IEnumerator Finish_FadeOut_Start_Animation()
    {

        isUseEvent = false;

        if(DeleFadeOut != null)
        DeleFadeOut();

        yield return new WaitForSeconds(lobbyUIManager.UIFadeTime);
        
        if(DeleSetOff != null)
        DeleSetOff(false);
        // 모두 꺼버린다.

        MenuBookAnimator.SetTrigger("NextTrigger");

        yield return new WaitForSeconds(AnimationTime);

        // 모두 킨다.
        if(DeleSetOn != null)
        DeleSetOn(true);

        if(DeleFadeIn != null)
        DeleFadeIn();

        yield return new WaitForSeconds(lobbyUIManager.UIFadeTime);

        isUseEvent = true;

        if(FinishFadeEvent != null)
            FinishFadeEvent();
    }

    /***** Photon 함수들 *****/
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        FindRoomEnter();
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        base.OnPhotonPlayerConnected(newPlayer);


        soundManager.PlayRandomEffectSound
            (SoundManager.EnumEffectSound.UI_LOBBYENTER_1,
            SoundManager.EnumEffectSound.UI_LOBBYENTER_2);
            
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        base.OnPhotonPlayerDisconnected(otherPlayer);

        soundManager.PlayEffectSound(SoundManager.EnumEffectSound.UI_LOBBYLEFT_1);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        RoomEnter();
    }


    /**** Fade 이벤트 ****/

    public void WaitingRoomEvent()
    {
        FinishFadeEvent = null;
    }

    public void LobbyRoomEvent()
    {
        gameStateType = EnumGameState.LOBBY;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Loading");
    }

}
