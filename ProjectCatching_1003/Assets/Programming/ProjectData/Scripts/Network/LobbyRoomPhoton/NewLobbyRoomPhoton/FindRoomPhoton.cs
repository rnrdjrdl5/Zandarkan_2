using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class NewLobbyRoomPhoton
{
    private string playerName;


    void FindRoomUpdate()
    {
        if (gameStateType == EnumGameState.FINDROOM)
        {

            FindRoomGUI();
        }
    }

    private void FindRoomGUI()
    {

        // 1. 방 받아오기
        RoomInfo[] fi = PhotonNetwork.GetRoomList();

        // 2. 받아온 방의 정보로 각 방 매칭시키기

        int count = lobbyUIManager.waitingRoomPanelScript.MAX_ROOMLIST;
        int roomCount = fi.Length;
        Debug.Log(roomCount);
        for (int i = 0; i < count; i++)
        {
            if (roomCount > i)
            {
                lobbyUIManager.waitingRoomPanelScript.RoomList[i].SetActive(true);
                lobbyUIManager.waitingRoomPanelScript.RoomName[i].text = fi[i].Name;

                string playerAmount = fi[i].PlayerCount + " / " + fi[i].MaxPlayers;
                lobbyUIManager.waitingRoomPanelScript.RoomPlayerAmount[i].text = playerAmount;
            }
            else
            {
                lobbyUIManager.waitingRoomPanelScript.RoomList[i].SetActive(false);
            }
        }
    }

    private void FindRoomEnter()
    {
        // 방 탐지?
        FindRoomGUI();
        gameStateType = EnumGameState.FINDROOM;

        soundManager.PlayEffectSound(SoundManager.EnumEffectSound.UI_BUTTONCLICK_1);

        DeleFadeOut = lobbyUIManager.lobbyPanelScript.FadeOutEffect;
        DeleFadeIn = lobbyUIManager.waitingRoomPanelScript.FadeInEffect;

        DeleSetOff = lobbyUIManager.lobbyPanelScript.SetActive;
        DeleSetOn = lobbyUIManager.waitingRoomPanelScript.SetActive;
        StartCoroutine("Finish_FadeOut_Start_Animation");
        FinishFadeEvent = WaitingRoomEvent;
    }


    /*****  Click 이벤트들  *****/

    // FindRoom - 방만들기 클릭 시
    public void ClickCreateRoom()
    {

        // 이외에 정보들 클릭 금지상태로 변경
        lobbyUIManager.waitingRoomPanelScript.SetInteractable(false);

        // 1. 방 만드는 메뉴창 보여주기.
        lobbyUIManager.waitingRoomPanelScript.CreateRoomWindow.SetActive(true);

    }

    // FindRoom > CreateRoom - 방 생성 Order 클릭 시
    public void ClickCROrderButton()
    {
        // 이외에 정보들 클릭 가능
        lobbyUIManager.waitingRoomPanelScript.SetInteractable(true);

        //1 . 방이름 받아오기.
        string RoomName = lobbyUIManager.waitingRoomPanelScript.InputRoomName.text;
        string RoomPassword = lobbyUIManager.waitingRoomPanelScript.InputRoomPW.text;

        // 같은 방 이름이 있는지 체크함.
        RoomInfo[] fi = PhotonNetwork.GetRoomList();


        int count = fi.Length;
        for (int i = 0; i < count; i++)
        {
            if (fi[i].Name == RoomName)
            {
                return;
            }
        }


        // 2. 방 fadeout시키기
        lobbyUIManager.waitingRoomPanelScript.CreateRoomWindow.SetActive(false);

        //3. 방 생성
        RoomOptions ro = new RoomOptions
        {
            MaxPlayers = 6
        };

        ExitGames.Client.Photon.Hashtable PlayerSceneState = new ExitGames.Client.Photon.Hashtable
        {

            { "Password", RoomPassword}
        };

        ro.CustomRoomPropertiesForLobby = new string[] { RoomPassword };// = PlayerSceneState;

        PhotonNetwork.CreateRoom(RoomName, ro, TypedLobby.Default);


    }
    // FindRoom > CreateRoom - 방만들기 퇴장 시
    public void ClickCRBackButton()
    {
        // 이외에 정보들 클릭 가능
        lobbyUIManager.waitingRoomPanelScript.SetInteractable(true);

        // 1. 방 만드는 메뉴창 보여주기.
        lobbyUIManager.waitingRoomPanelScript.CreateRoomWindow.SetActive(false);
    }



    // FindRoom - 빠른시작
    public void ClickQuickMatch()
    {
        PhotonNetwork.JoinRandomRoom();
    }



    // FindRoom - 타이틀로 돌아가기
    public void ClickReturnTitleButton()
    {
        PhotonNetwork.LeaveLobby();

        soundManager.PlayEffectSound(SoundManager.EnumEffectSound.UI_BUTTONCLICK_1);

        DeleFadeOut = lobbyUIManager.waitingRoomPanelScript.FadeOutEffect;
        DeleFadeIn = lobbyUIManager.lobbyPanelScript.FadeInEffect;

        DeleSetOff = lobbyUIManager.waitingRoomPanelScript.SetActive;
        DeleSetOn = lobbyUIManager.lobbyPanelScript.SetActive;
        StartCoroutine("Finish_FadeOut_Start_Animation");
        FinishFadeEvent = LobbyRoomEvent;
    }



    // FindRoom - 닉네임 설정 버튼 클릭 시 
    public void ClickCreateNameButton()
    {
        // 이외에 정보들 클릭 가능
        lobbyUIManager.waitingRoomPanelScript.SetInteractable(false);

        lobbyUIManager.waitingRoomPanelScript.CreatePlayerName.SetActive(true);
    }

    // FindRoom > CreateName - 닉네임 결정 시
    public void ClickCPOrderButton()
    {
        // 이외에 정보들 클릭 가능
        lobbyUIManager.waitingRoomPanelScript.SetInteractable(true);

        playerName = lobbyUIManager.waitingRoomPanelScript.InputPlayerName.text;
        lobbyUIManager.waitingRoomPanelScript.PlayerName.text = playerName;
        lobbyUIManager.waitingRoomPanelScript.CreatePlayerName.SetActive(false);
    }
    // FindRoom > CreateName - 닉네임 설정 퇴장 시
    public void ClickCPBackButtonButton()
    {
        // 이외에 정보들 클릭 가능
        lobbyUIManager.waitingRoomPanelScript.SetInteractable(true);
        lobbyUIManager.waitingRoomPanelScript.CreatePlayerName.SetActive(false);
    }


    // FindRoom - 각 방 클릭 시
    public void ClickRoom1() { ClickJoinRoom(1); }
    public void ClickRoom2() { ClickJoinRoom(2); }
    public void ClickRoom3() { ClickJoinRoom(3); }
    public void ClickRoom4() { ClickJoinRoom(4); }
    public void ClickRoom5() { ClickJoinRoom(5); }
    public void ClickRoom6() { ClickJoinRoom(6); }

    public void ClickJoinRoom(int number)
    {

        RoomInfo[] fi = PhotonNetwork.GetRoomList();
        PhotonNetwork.JoinRoom(fi[number - 1].Name);
    }



}