using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class NewLobbyRoomPhoton
{

    private int nowRoomPage;
    private int maxRoomPage;
    void FindRoomAwake()
    {
        nowRoomPage = 1;
    }
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


        // 2. 방의 총 크기에 따라 현재 방위치 수정
        int count = lobbyUIManager.waitingRoomPanelScript.MAX_ROOMLIST;
        int roomCount = fi.Length;


        // 안되는 것 : 6/6 , 12/6 (* 6/6 : 0  12/6 : 1 )     13/6 
        maxRoomPage = ((int)(roomCount / count)) + 1;       // 최대 방의 페이지
        if (nowRoomPage > maxRoomPage) nowRoomPage = maxRoomPage;


        // 최대 방 수  와   현재 방 수가  같을 경우,    위의 공식을 사용하면 에러가 나온다.       추가 처리
        if (roomCount % count == 0 && roomCount != 0) { maxRoomPage = roomCount / count; }


        // 현재 페이지 방 개수 설정
        int nowPageRoomAmount;

        // 현재 방의 개수와 최대 방의 개수가 다르면?
        if (maxRoomPage != nowRoomPage)
            nowPageRoomAmount = count;      //  현재 방 페이지의 방 개수를 최대로

        else
            nowPageRoomAmount = roomCount % count;

        // 한 페이지 내에 방이 꽉찬경우 설정
        if (roomCount != 0 && nowPageRoomAmount == 0) nowPageRoomAmount = count;

        int nowMinRoomCount = (nowRoomPage - 1) * count; // 이상
        int nowMaxRoomCount = nowMinRoomCount + nowPageRoomAmount; // 미만

        int tempMinRoomCount = nowMinRoomCount;
        
        for (int i = 0; i < count;  i++, tempMinRoomCount++)
        {
            if (nowMaxRoomCount > tempMinRoomCount)
            {
                lobbyUIManager.waitingRoomPanelScript.RoomList[i].SetActive(true);
                lobbyUIManager.waitingRoomPanelScript.RoomName[i].text = fi[tempMinRoomCount].Name;

                string playerAmount = fi[tempMinRoomCount].PlayerCount + " / " + fi[tempMinRoomCount].MaxPlayers;
                lobbyUIManager.waitingRoomPanelScript.RoomPlayerAmount[i].text = playerAmount;
            }
            else
            {
                lobbyUIManager.waitingRoomPanelScript.RoomList[i].SetActive(false);
            }
        }


        // 버튼 GUI 설정
        if (lobbyUIManager.waitingRoomPanelScript.isCanRoomChannelButton)
        {
            lobbyUIManager.waitingRoomPanelScript.SetInteractablePageButton(true, nowRoomPage, maxRoomPage);
        }
        
    }


    //Photon 함수에서
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

    private bool CheckEmptyName()
    {
        if (string.IsNullOrEmpty(lobbyUIManager.waitingRoomPanelScript.InputPlayerName.text))
        {
            return true;
        }

        else return false;
    }

    // FindRoom - 방만들기 클릭 시
    public void ClickCreateRoom()
    {

        if (CheckEmptyName())
        {
            lobbyUIManager.systemPanelScript.OutputRoomMessage(RoomSystemMessage.EnumSystemCondition.EMPTY_NAME);
            
            return;
        }


        // 1. 방 만드는 메뉴창 보여주기.
        lobbyUIManager.waitingRoomPanelScript.CreateRoomWindow.SetActive(true);

    }

    // FindRoom > CreateRoom - 방 생성 Order 클릭 시
    public void ClickCROrderButton()
    {

        //1 . 방이름 받아오기.
        string RoomName = lobbyUIManager.waitingRoomPanelScript.InputRoomName.text;

        // 같은 방 이름이 있는지 체크함.
        RoomInfo[] fi = PhotonNetwork.GetRoomList();


        int count = fi.Length;


        for (int i = 0; i < count; i++)
        {

            if (fi[i].Name == RoomName)
            {
                lobbyUIManager.systemPanelScript.OutputRoomMessage(RoomSystemMessage.EnumSystemCondition.SAME_ROOM);
                return;
            }
        }


        if (string.IsNullOrEmpty(RoomName))
        {
            lobbyUIManager.systemPanelScript.OutputRoomMessage(RoomSystemMessage.EnumSystemCondition.EMPTY_ROOM_NAME);
            return;
        }


        // 2. 방 fadeout시키기
        lobbyUIManager.waitingRoomPanelScript.CreateRoomWindow.SetActive(false);

        //3. 방 생성
        RoomOptions ro = new RoomOptions
        {
            MaxPlayers = 6
        };


        PhotonNetwork.playerName = lobbyUIManager.waitingRoomPanelScript.InputPlayerName.text;
        PhotonNetwork.CreateRoom(RoomName, ro, TypedLobby.Default);


    }
    // FindRoom > CreateRoom - 방만들기 퇴장 시
    public void ClickCRBackButton()
    {

        // 1. 방 만드는 메뉴창 보여주기.
        lobbyUIManager.waitingRoomPanelScript.CreateRoomWindow.SetActive(false);
    }






    // FindRoom - 빠른시작
    public void ClickQuickMatch()
    {
        if (CheckEmptyName())
        {
            lobbyUIManager.systemPanelScript.OutputRoomMessage(RoomSystemMessage.EnumSystemCondition.EMPTY_NAME);
            return;
        }

        PhotonNetwork.playerName = lobbyUIManager.waitingRoomPanelScript.InputPlayerName.text;
        Debug.Log("로비 이름 : " + PhotonNetwork.playerName);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        base.OnPhotonRandomJoinFailed(codeAndMsg);

        // 방찾기 실패
        lobbyUIManager.systemPanelScript.OutputRoomMessage(RoomSystemMessage.EnumSystemCondition.NOT_QUICK_MATCH);
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


    // FindRoom - 각 방 클릭 시
    public void ClickRoom1() { ClickJoinRoom(1); }
    public void ClickRoom2() { ClickJoinRoom(2); }
    public void ClickRoom3() { ClickJoinRoom(3); }
    public void ClickRoom4() { ClickJoinRoom(4); }
    public void ClickRoom5() { ClickJoinRoom(5); }
    public void ClickRoom6() { ClickJoinRoom(6); }

    public void ClickJoinRoom(int number)
    {
        if (CheckEmptyName())
        {
            lobbyUIManager.systemPanelScript.OutputRoomMessage(RoomSystemMessage.EnumSystemCondition.EMPTY_NAME);
            return;
        }

        RoomInfo[] fi = PhotonNetwork.GetRoomList();
        PhotonNetwork.playerName = lobbyUIManager.waitingRoomPanelScript.InputPlayerName.text;
        PhotonNetwork.JoinRoom(fi[number - 1].Name);
    }

    public void ClickRoomNextButton()
    {
        if(maxRoomPage > nowRoomPage) nowRoomPage++;

        lobbyUIManager.waitingRoomPanelScript.SetInteractablePageButton(true, nowRoomPage,maxRoomPage);
    }

    public void ClickRoomBeforeButton()
    {
        if(nowRoomPage>1) nowRoomPage--;

        lobbyUIManager.waitingRoomPanelScript.SetInteractablePageButton(true, nowRoomPage, maxRoomPage);
    }
    // FindRoom - 다음/이전 방으로 넘어가기



}