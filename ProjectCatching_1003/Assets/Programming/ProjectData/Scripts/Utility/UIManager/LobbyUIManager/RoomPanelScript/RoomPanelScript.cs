using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanelScript{

    

    private LobbyUIManager lobbyUIManager;

    
    public GameObject RoomPanel { get; set; }


    // 게임 시작, 설명, 나가기
    public GameObject StartImage { get;set;}
    public GameObject ReadyImage { get; set; }
    public GameObject ExplaneImage { get; set; }
    public GameObject ExitImage { get; set; }


    // 쥐 고양이 선택하기
    public GameObject ChoosePlayer { get; set; }
    public GameObject ChooseEdge { get; set; }
    public GameObject RunnerButton { get; set; }
    public GameObject ChaserButton { get; set; }


    // 플레이어 정보
    public PlayerPanelScript[] playerPanelScripts;

    // 플레이어 이름 백그라운드
    public GameObject[] PlayerSpace;
    private UIEffect[] PlayerSpaceEffect;

    private UIEffect StartImageEffect;
    private UIEffect ReadyImageEffect;
    private UIEffect HelpImageEffect;
    private UIEffect ExitImageEffect;


    private UIEffect ChooseEdgeEffect;
    private UIEffect RunnerButtonEffect;
    private UIEffect ChaserButtonEffect;


    public void SetActive(bool isActive)
    {
        RoomPanel.SetActive(isActive);

        StartImage.SetActive(isActive);
        ReadyImage.SetActive(isActive);
        ExplaneImage.SetActive(isActive);
        ExitImage.SetActive(isActive);

        ChoosePlayer.SetActive(isActive);
        ChooseEdge.SetActive(isActive);
        RunnerButton.SetActive(isActive);
        ChaserButton.SetActive(isActive);

        for (int i = 0; i < LobbyUIManager.MAXPLAYER; i++)
        {
            if(i < PhotonNetwork.playerList.Length) playerPanelScripts[i].SetActive(isActive);
            else playerPanelScripts[i].SetActive(false);
        }

        for (int i = 0; i < LobbyUIManager.MAXPLAYER; i++)
        {
            PlayerSpace[i].SetActive(isActive);
        }


        lobbyUIManager.LineSetActive(isActive);

    }

    public void InitData()
    {

        lobbyUIManager = LobbyUIManager.GetInstance();

        RoomPanel = lobbyUIManager.UICanvas.transform.Find("RoomPanel").gameObject;

        StartImage = RoomPanel.transform.Find("StartImage").gameObject;
        ExplaneImage = RoomPanel.transform.Find("ExplaneImage").gameObject;
        ExitImage = RoomPanel.transform.Find("ExitImage").gameObject;
        ReadyImage = RoomPanel.transform.Find("ReadyImage").gameObject;

        ChoosePlayer = RoomPanel.transform.Find("ChoosePlayer").gameObject;
        ChooseEdge = ChoosePlayer.transform.Find("ChooseEdge").gameObject;
        RunnerButton = ChoosePlayer.transform.Find("RunnerButton").gameObject;
        ChaserButton = ChoosePlayer.transform.Find("ChaserButton").gameObject;


        PlayerSpace = new GameObject[LobbyUIManager.MAXPLAYER];
        for (int i = 0; i < LobbyUIManager.MAXPLAYER; i++)
        {
            PlayerSpace[i] = RoomPanel.transform.Find("PlayerSpace" + (i + 1).ToString()).gameObject;
        }

        InitPlayerPanelScript();

        StartImageEffect = new UIEffect();
        ExitImageEffect = new UIEffect();
        HelpImageEffect = new UIEffect();
        ReadyImageEffect = new UIEffect();

        ChooseEdgeEffect = new UIEffect();
        RunnerButtonEffect = new UIEffect();
        ChaserButtonEffect = new UIEffect();

        PlayerSpaceEffect = new UIEffect[LobbyUIManager.MAXPLAYER];
        for (int i = 0; i < LobbyUIManager.MAXPLAYER; i++)
        {
            PlayerSpaceEffect[i] = new UIEffect();
        }

}

    public void FadeInEffect()
    {

        ReadyImageEffect.AddFadeEffectNode(StartImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);
        StartImageEffect.AddFadeEffectNode(StartImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);
        ExitImageEffect.AddFadeEffectNode(ExitImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);
        HelpImageEffect.AddFadeEffectNode(ExplaneImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);


        ChooseEdgeEffect.AddFadeEffectNode(ChooseEdge, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);
        RunnerButtonEffect.AddFadeEffectNode(RunnerButton, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);
        ChaserButtonEffect.AddFadeEffectNode(ChaserButton, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);

        for (int i = 0; i < LobbyUIManager.MAXPLAYER; i++)
        {
            PlayerSpaceEffect[i].AddFadeEffectNode(ChaserButton, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);
            lobbyUIManager.UpdateEvent += PlayerSpaceEffect[i].EffectEventLobby;
        }



        PlayerPanelUIEffect(UIEffectNode.EnumFade.IN);

        lobbyUIManager.UpdateEvent += StartImageEffect.EffectEventLobby;
        lobbyUIManager.UpdateEvent += ReadyImageEffect.EffectEventLobby;
        lobbyUIManager.UpdateEvent += ExitImageEffect.EffectEventLobby;
        lobbyUIManager.UpdateEvent += HelpImageEffect.EffectEventLobby;
        

        lobbyUIManager.UpdateEvent += ChooseEdgeEffect.EffectEventLobby;
        lobbyUIManager.UpdateEvent += RunnerButtonEffect.EffectEventLobby;
        lobbyUIManager.UpdateEvent += ChaserButtonEffect.EffectEventLobby;


        lobbyUIManager.LineFadeInEffect();


    }

    public void FadeOutEffect()
    {
        ReadyImageEffect.AddFadeEffectNode(StartImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);
        StartImageEffect.AddFadeEffectNode(StartImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);
        ExitImageEffect.AddFadeEffectNode(ExitImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);
        HelpImageEffect.AddFadeEffectNode(ExplaneImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);

        ChooseEdgeEffect.AddFadeEffectNode(ChooseEdge, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);
        RunnerButtonEffect.AddFadeEffectNode(RunnerButton, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);
        ChaserButtonEffect.AddFadeEffectNode(ChaserButton, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);


        for (int i = 0; i < LobbyUIManager.MAXPLAYER; i++)
        {
            PlayerSpaceEffect[i].AddFadeEffectNode(ChaserButton, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);
            lobbyUIManager.UpdateEvent += PlayerSpaceEffect[i].EffectEventLobby;
        }


        PlayerPanelUIEffect(UIEffectNode.EnumFade.OUT);

        lobbyUIManager.UpdateEvent += StartImageEffect.EffectEventLobby;
        lobbyUIManager.UpdateEvent += ReadyImageEffect.EffectEventLobby;
        lobbyUIManager.UpdateEvent += ExitImageEffect.EffectEventLobby;
        lobbyUIManager.UpdateEvent += HelpImageEffect.EffectEventLobby;

        lobbyUIManager.UpdateEvent += ChooseEdgeEffect.EffectEventLobby;
        lobbyUIManager.UpdateEvent += RunnerButtonEffect.EffectEventLobby;
        lobbyUIManager.UpdateEvent += ChaserButtonEffect.EffectEventLobby;


        lobbyUIManager.LineFadeOutEffect();
    }

    private void InitPlayerPanelScript()
    {
        playerPanelScripts = new PlayerPanelScript[LobbyUIManager.MAXPLAYER];

        for (int i = 0; i < LobbyUIManager.MAXPLAYER; i++)
        {
            playerPanelScripts[i] = new PlayerPanelScript();
            playerPanelScripts[i].InitData(i);

        }
    }

    private void PlayerPanelUIEffect(UIEffectNode.EnumFade enumFade)
    {


        for (int i = 0; i < LobbyUIManager.MAXPLAYER; i++)
        {


            if (enumFade == UIEffectNode.EnumFade.IN)  playerPanelScripts[i].CallFadeInEffect();

            else if (enumFade == UIEffectNode.EnumFade.OUT) playerPanelScripts[i].CallFadeOutEffect();
        }
    }




}
