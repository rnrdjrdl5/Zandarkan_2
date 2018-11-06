using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
public class LobbyUIManager : MonoBehaviour {

    public const int MAXPLAYER = 6;

    public float UIFadeTime = 0.2f;

    static private LobbyUIManager lobbyUIManager;
    static public LobbyUIManager GetInstance() {
        if (lobbyUIManager == null)
            return null;

        return lobbyUIManager;
    }

    public delegate void DeleUpdate();
    public event DeleUpdate UpdateEvent;

    public GameObject UICanvas { get; set; }


    public LobbyPanelScript lobbyPanelScript;
    public RoomPanelScript roomPanelScript;
    public WaitingRoomPanelScript waitingRoomPanelScript;

    public FadePanelScript fadePanelScript;

    public TitlePanelScript titlePanelScript;

    public HelpWindowScript helpWindowScript;


    // 공용 용도의 라인
    public GameObject BookLineLeft;
    public GameObject BookLineRight;

    public UIEffect BookLineLeftEffect;
    public UIEffect BookLineRightEffect;

    public SystemPanelScript systemPanelScript;

    public TutorialPanelScript tutorialPanelScript;

    private void Awake()
    {
        if (lobbyUIManager == null)
            lobbyUIManager = this;

        UICanvas = GameObject.Find("UICanvas").gameObject;

        BookLineLeft = UICanvas.transform.Find("BookLineLeft").gameObject;
        BookLineRight = UICanvas.transform.Find("BookLineRight").gameObject;

        BookLineLeftEffect = new UIEffect();
        BookLineRightEffect = new UIEffect();

        InitScripts();
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        if (UpdateEvent != null) { UpdateEvent(); }
    }

    void InitScripts()
    {

        lobbyPanelScript = new LobbyPanelScript();
        lobbyPanelScript.InitData();

        roomPanelScript = new RoomPanelScript();
        roomPanelScript.InitData();

        waitingRoomPanelScript = new WaitingRoomPanelScript();
        waitingRoomPanelScript.InitData();

        fadePanelScript = new FadePanelScript();
        fadePanelScript.InitData();

        titlePanelScript = new TitlePanelScript();
        titlePanelScript.InitData();

        systemPanelScript = new SystemPanelScript();
        systemPanelScript.InitData();

        helpWindowScript = new HelpWindowScript();
        helpWindowScript.InitData();

        tutorialPanelScript = new TutorialPanelScript();
        tutorialPanelScript.InitData();

        
    }


    public void LineFadeInEffect()
    {
        BookLineLeftEffect.AddFadeEffectNode(BookLineLeft, UIFadeTime, UIEffectNode.EnumFade.IN);
        UpdateEvent += BookLineLeftEffect.EffectEventLobby;

        BookLineRightEffect.AddFadeEffectNode(BookLineRight, UIFadeTime, UIEffectNode.EnumFade.IN);
        UpdateEvent += BookLineRightEffect.EffectEventLobby;
    }

    public void LineFadeOutEffect()
    {
        BookLineLeftEffect.AddFadeEffectNode(BookLineLeft, UIFadeTime, UIEffectNode.EnumFade.OUT);
        UpdateEvent += BookLineLeftEffect.EffectEventLobby;

        BookLineRightEffect.AddFadeEffectNode(BookLineRight, UIFadeTime, UIEffectNode.EnumFade.OUT);
        UpdateEvent += BookLineRightEffect.EffectEventLobby;
    }

    public void LineSetActive(bool isActive)
    {
        BookLineLeft.SetActive(isActive);
        BookLineRight.SetActive(isActive);
    }


}
