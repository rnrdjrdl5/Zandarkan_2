using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaitingRoomPanelScript{
    private LobbyUIManager lobbyUIManager;

    public GameObject WaitingRoomPanel { get; set; }

    public void InitWaitingRoomPanel() {
        WaitingRoomPanel = lobbyUIManager.UICanvas.transform.Find("WaitingRoomPanel").gameObject;
    }


    // 방들
    public int MAX_ROOMLIST = 6;
    public GameObject[] RoomList;
    public GameObject[] BackGround;
    public Button[] RoomListButton;
    public Text[] RoomName;
    public Text[] RoomPlayerAmount;

    
    // 방 목록 변경하기
    public bool isCanRoomChannelButton { get; set; }
    public Button RoomBeforeButtonButton;
    public Button RoomNextButtonButton;

    public void InitRoomList()
    {
        RoomList = new GameObject[MAX_ROOMLIST];
        RoomListButton = new Button[MAX_ROOMLIST];
        BackGround = new GameObject[MAX_ROOMLIST];
        RoomName = new Text[MAX_ROOMLIST];
        RoomPlayerAmount = new Text[MAX_ROOMLIST];

        for (int i = 0; i < MAX_ROOMLIST; i++)
        {
            RoomList[i] = WaitingRoomPanel.transform.
                Find("RoomList" + (i + 1).ToString()).gameObject;

            BackGround[i] = RoomList[i].transform.
                Find("BackGround").gameObject;

            RoomListButton[i] = BackGround[i].GetComponent<Button>();

            RoomName[i] =
                BackGround[i].transform.Find("RoomName").GetComponent<Text>();

            RoomPlayerAmount[i] =
                BackGround[i].transform.Find("RoomPlayerAmount").GetComponent<Text>();
        }

        isCanRoomChannelButton = true;
        RoomBeforeButtonButton = WaitingRoomPanel.transform.Find("RoomBeforeButton").GetComponent<Button>();
        RoomNextButtonButton = WaitingRoomPanel.transform.Find("RoomNextButton").GetComponent<Button>();
    }
    

    // 이름
    public InputField InputPlayerName;
    public void InitInputPlayerName()
    {
        InputPlayerName = WaitingRoomPanel.transform.Find("InputPlayerName").GetComponent<InputField>();
    }

    // 자식 방만들기 객체
    public GameObject CreateRoomWindow;
    public InputField InputRoomName;

    public void InitCreateRoomWindow()
    {
        CreateRoomWindow = WaitingRoomPanel.transform.
            Find("CreateRoomWindow").gameObject;

        InputRoomName =
            CreateRoomWindow.transform.Find("InputRoomName").GetComponent<InputField>();

    }





    // 비활성화 용
    public GameObject QuickMatchButton;
    public Button QuickMatchButtonButton;
    public void InitQuickMatchButton()
    {
        QuickMatchButton = WaitingRoomPanel.transform.Find("QuickMatchButton").gameObject;
        QuickMatchButtonButton = QuickMatchButton.GetComponent<Button>();
    }

    public GameObject ReturnTitleButton;
    public Button ReturnTitleButtonButton;
    public void InitReturnTitleButton()
    {
        ReturnTitleButton = WaitingRoomPanel.transform.Find("ReturnTitleButton").gameObject;
        ReturnTitleButtonButton = ReturnTitleButton.GetComponent<Button>();
    }

    public GameObject CreateRoomButton;
    public Button CreateRoomButtonButton;
    public void InitCreateRoomButton()
    {
        CreateRoomButton = WaitingRoomPanel.transform.Find("CreateRoomButton").gameObject;
        CreateRoomButtonButton = CreateRoomButton.GetComponent<Button>();
    }


    public void InitData()
    {

        lobbyUIManager = LobbyUIManager.GetInstance();
        InitWaitingRoomPanel();

        InitRoomList();
        InitCreateRoomWindow();

        InitQuickMatchButton();
        InitReturnTitleButton();
        InitCreateRoomButton();
        InitInputPlayerName();
    }



    public void SetInteractablePageButton(bool isInteractable, int nowRoomPage, int maxRoomPage)
    {

        if (!isInteractable)
        {
            RoomNextButtonButton.interactable = isInteractable;
            RoomBeforeButtonButton.interactable = isInteractable;
        }

        else
        {
            if (nowRoomPage >= maxRoomPage)
                RoomNextButtonButton.interactable = false;
            else
                RoomNextButtonButton.interactable = true; 

            if(nowRoomPage <= 1)
                RoomBeforeButtonButton.interactable = false;
            else
                RoomBeforeButtonButton.interactable = true;
        }
    }



    public void SetActive(bool isActive)
    {

        WaitingRoomPanel.SetActive(isActive);

        lobbyUIManager.LineSetActive(isActive);
    }

    public void FadeOutEffect()
    {
        lobbyUIManager.LineFadeOutEffect();
    }

    public void FadeInEffect()
    {
        lobbyUIManager.LineFadeInEffect();
    }



}
