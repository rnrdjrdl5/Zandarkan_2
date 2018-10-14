﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaitingRoomPanelScript{
    private LobbyUIManager lobbyUIManager;

    public GameObject WaitingRoomPanel { get; set; }

    public void InitWaitingRoomPanel() {
        WaitingRoomPanel = lobbyUIManager.UICanvas.transform.Find("WaitingRoomPanel").gameObject;
    }


    public int MAX_ROOMLIST = 6;
    public GameObject[] RoomList;
    public GameObject[] BackGround;
    public Text[] RoomName;
    public Text[] RoomPlayerAmount;

    public Text PlayerName;

    public void InitRoomList()
    {
        RoomList = new GameObject[MAX_ROOMLIST];
        BackGround = new GameObject[MAX_ROOMLIST];
        RoomName = new Text[MAX_ROOMLIST];
        RoomPlayerAmount = new Text[MAX_ROOMLIST];

        for (int i = 0; i < MAX_ROOMLIST; i++)
        {
            RoomList[i] = WaitingRoomPanel.transform.
                Find("RoomList" + (i + 1).ToString()).gameObject;

            BackGround[i] = RoomList[i].transform.
                Find("BackGround").gameObject;

            RoomName[i] =
                BackGround[i].transform.Find("RoomName").GetComponent<Text>();

            RoomPlayerAmount[i] =
                BackGround[i].transform.Find("RoomPlayerAmount").GetComponent<Text>();
        }

        PlayerName = WaitingRoomPanel.transform.Find("PlayerName").GetComponent<Text>();
    }


    public GameObject CreateRoomWindow;
    public InputField InputRoomName;
    public InputField InputRoomPW;

    public void InitCreateRoomWindow()
    {
        CreateRoomWindow = WaitingRoomPanel.transform.
            Find("CreateRoomWindow").gameObject;

        InputRoomName =
            CreateRoomWindow.transform.Find("InputRoomName").GetComponent<InputField>();

        InputRoomPW =
            CreateRoomWindow.transform.Find("InputRoomPW").GetComponent<InputField>();

    }


    public GameObject CreatePlayerName;
    public InputField InputPlayerName;
    public void InitCreatePlayerName()
    {
        CreatePlayerName = WaitingRoomPanel.transform.Find("CreatePlayerName").gameObject;

        InputPlayerName = CreatePlayerName.transform.Find("InputPlayerName").GetComponent<InputField>();
    }


    public void InitData()
    {

        lobbyUIManager = LobbyUIManager.GetInstance();
        InitWaitingRoomPanel();

        InitRoomList();
        InitCreateRoomWindow();
        InitCreatePlayerName();
    }

    public void SetActive(bool isActive)
    {

        WaitingRoomPanel.SetActive(isActive);
    }

    public void FadeOutEffect()
    {

    }

    public void FadeInEffect()
    {

    }



}
