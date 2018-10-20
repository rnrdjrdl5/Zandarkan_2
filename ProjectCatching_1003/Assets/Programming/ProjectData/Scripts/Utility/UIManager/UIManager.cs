using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class UIManager : Photon.PunBehaviour {

    public float SelectChar_MaxRouletteTick;
    public float SelectChar_DelayRouletteTick;
    public float SelectChar_ExitRouletteTick;
    public float SelectChar_DelayRouletteTickWeight;


    public TimeBarPanelScript timerBarPanelScript { get; set; }

    public EndStatePanelScript endStatePanelScript { get; set; }


    public GradePanelScript gradePanelScript { get; set; }
    public int GradePanel_ShakeFrequency;
    public int GradePanel_ShakePower;


    public SkillPanelScript skillPanelScript { get; set; }

    public MouseImagePanelScript mouseImagePanelScript { get; set; }


    public HPPanelScript hpPanelScript { get; set; }
    public int HPPanel_ShakeFrequency;
    public int HPPanel_ShakePower;


    public LimitTimePanelScript limitTimePanelScript { get; set; }      // 남은시간, 시계 아이콘

    public GameResultPanelScript gameResultPanelScript { get; set; }

    public PressImagePanelScript pressImagePanelScript { get; set; }

    public RescueBarPanelScript rescueBarPanelScript { get; set; }

    public HidePanelScript hidePanelScript { get; set; }

    public GameStartCountPanelScript gameStartCountPanelScript { get; set; }

    public DeadOutLinePanelScript deadOutLinePanelScript { get; set; }

    public FadeImageScript fadeImageScript { get; set; }

    public RescueIconPanelScript rescueIconPanelScript { get; set; }

    // 스킬 관련 UI
    public TurnOffPanelScript turnOffPanelScript { get; set; }
    public SaucePanelScript saucePanelScript { get; set; }

    public delegate void DeleUpdate();
    public event DeleUpdate UpdateEvent;


    // 상호작용 물체 GUI
    public InterObjectGUIPanelScript interObjectGUIPanelScript { get; set; }


    IEnumerator EnumCoro;





    static private UIManager uIManager;

    static public UIManager GetInstance()
    {
        return uIManager;
    }

    /**** 접근자 private ****/

    public List<PhotonPlayer> Players{ get; set; }              // 플레이어를 담는 리스트 , PhotonManager에서 Add시킴

  //  public List<PhotonPlayer> MousePlayerListOneSort { get; set; }         // 쥐를 담는 리스트, 1회 정렬 이외에 하지 않음.








    /************************************ UICanvas ***********************************************/





    /**** UICanvas ****/

    public GameObject UICanvas { set; get; }
    public void InitUICanvas() { UICanvas = GameObject.Find("UICanvas");
    }




    /**** AimPanel ****/

    public GameObject AimPanel { set; get; }
    public void InitAimPanel() { AimPanel = UICanvas.transform.Find("AimPanel").gameObject; }

    public GameObject AimImage { set; get; }
    public void InitAimImage() { AimImage = AimPanel.transform.Find("AimImage").gameObject; }    

    public void SetAim(bool isActive)
    {
        AimPanel.SetActive(isActive);
        AimImage.SetActive(isActive);
    }








    /************************************ ResultUI ***********************************************/


    /**** ResultUI ****/

    public GameObject ResultUI { set; get; }
    public void InitResultUI() { ResultUI = GameObject.Find("ResultUI"); }





    





    /********************** InGameCanvas ******************/
    public GameObject InGameCanvas { get; set; }
    public void InitInGameCanvas() { InGameCanvas = GameObject.Find("InGameCanvas"); }


    /********************** OverlayCanvas *******************/
    public GameObject OverlayCanvas { get; set; }
    public void InitOverlayCanvas() { OverlayCanvas = GameObject.Find("OverlayCanvas"); }

    IEnumerator CoroAwake;

    /*IEnumerator SetAwake()
    {

    }*/

    /**** 유니티 함수 ****/
    private void Awake()
    {
        
        uIManager = this;




        

        // UI 초기화들
        InitUICanvas();

        // UI 초기화들
        InitUICanvas();

        InitOverlayCanvas();


        InitInGameCanvas();

        InitResultUI();

        

        InitAimPanel();
        InitAimImage();



        limitTimePanelScript = new LimitTimePanelScript();
        limitTimePanelScript.InitData();


        endStatePanelScript = new EndStatePanelScript();
        endStatePanelScript.InitData();


        hpPanelScript = new HPPanelScript();
        hpPanelScript.InitData();

        timerBarPanelScript = new TimeBarPanelScript();
        timerBarPanelScript.InitData();


        mouseImagePanelScript = new MouseImagePanelScript();
        mouseImagePanelScript.InitData();

        turnOffPanelScript = new TurnOffPanelScript();
        turnOffPanelScript.InitData();

        saucePanelScript = new SaucePanelScript();
        saucePanelScript.InitData();

        gradePanelScript = new GradePanelScript();
        gradePanelScript.InitData();

        skillPanelScript = new SkillPanelScript();
        skillPanelScript.InitData();

        gameResultPanelScript = new GameResultPanelScript();
        gameResultPanelScript.InitData();

        pressImagePanelScript = new PressImagePanelScript();
        pressImagePanelScript.InitData();

        rescueBarPanelScript = new RescueBarPanelScript();
        rescueBarPanelScript.InitData();

        hidePanelScript = new HidePanelScript();
        hidePanelScript.InitData();
        
        gameStartCountPanelScript = new GameStartCountPanelScript();
        gameStartCountPanelScript.InitData();

        deadOutLinePanelScript = new DeadOutLinePanelScript();
        deadOutLinePanelScript.InitData();

        fadeImageScript = new FadeImageScript();
        fadeImageScript.InitData();

        rescueIconPanelScript = new RescueIconPanelScript();
        rescueIconPanelScript.InitData();


        interObjectGUIPanelScript = new InterObjectGUIPanelScript();
        interObjectGUIPanelScript.InitData();


    }

    private void Start()
    {
        gameResultPanelScript.InitEvent();
        gradePanelScript.InitEvent();
    }

    private void Update()
    {



        // 하위 스크립트의 이벤트들을 실행
        UpdateEvent();

        //UI 키입력 처리
        if (Input.GetKeyDown(KeyCode.Tab))
        {

            float guiHeightSize = interObjectGUIPanelScript.ObjGUIBgImageRect.sizeDelta.y;

            if (interObjectGUIPanelScript.GetObjectActive() == false)
            {

                interObjectGUIPanelScript.SetObjectActive(true);

                // 탭키, 시간 이미지 위치 변경.

                limitTimePanelScript.AddTimeYPosition(-guiHeightSize);
                interObjectGUIPanelScript.AddTapImageHeight(-guiHeightSize);

            }

            else
            {

                interObjectGUIPanelScript.SetObjectActive(false);

                limitTimePanelScript.AddTimeYPosition(+guiHeightSize);
                interObjectGUIPanelScript.AddTapImageHeight(+guiHeightSize);


            }
        }

    }



    public void SetDeadUI()
    {
        skillPanelScript.SkillPanel.SetActive(false);

        hpPanelScript.HPPanel.SetActive(false);

        timerBarPanelScript.TimeBarPanel.SetActive(false);

        pressImagePanelScript.PressImagePanel.SetActive(false);

        hidePanelScript.HidePanel.SetActive(false);

        turnOffPanelScript.TurnOffPanel.SetActive(false);

        saucePanelScript.SaucePanel.SetActive(false);

        deadOutLinePanelScript.DeadOutLinePanel.SetActive(true);
    }
}
