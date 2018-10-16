using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class NewLobbyRoomPhoton
{
    void RoomAwake()
    {
        roomPlayerDatas = new List<RoomPlayerData>();
    }


    List<RoomPlayerData> roomPlayerDatas;
    public XMLManager xmlManager;

    void RoomUpdate()
    {
        if (gameStateType == EnumGameState.ROOM)
        {

            roomPlayerDatas.Clear();
            InitPlayerList();
            DrawRoomState();
            Debug.Log(PhotonNetwork.playerName);
        }
    }

    // 플레이어 리스트 초기화 후 정렬
    void InitPlayerList()
    {

        // 플레이어 정보 생성
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {

            // 플레이어 데이터 생성 후 삽입
            CreateAddPlayerData(PhotonNetwork.playerList[i]);
        }

        //플레이어 데이터 정렬
        SortPlayerData();
    }

    // 플레이어 데이터 생성 후 삽입
    void CreateAddPlayerData(PhotonPlayer pp)
    {
        // 정보 생성
        RoomPlayerData rpd = new RoomPlayerData();

        rpd.InitPlayerData(pp);

        // 리스트에 삽입
        roomPlayerDatas.Add(rpd);
    }

    // 플레이어 리스트 Sort
    void SortPlayerData()
    {

        roomPlayerDatas.Sort(
            (RoomPlayerData rp1, RoomPlayerData rp2) =>
            {
                if (rp1.GetPlayerID() > rp2.GetPlayerID())
                    return 1;

                else if (rp1.GetPlayerID() < rp2.GetPlayerID())
                    return -1;

                return 0;
            });

    }

    // 플레이어 그리기
    void DrawRoomState()
    {

        CheckStartButton();

        // 패널  수 
        for (int i = 0; i < lobbyUIManager.roomPanelScript.playerPanelScripts.Length; i++)
        {

            // 유저 접속 패널
            if (i < PhotonNetwork.playerList.Length)
            {

                lobbyUIManager.roomPanelScript.playerPanelScripts[i].SetActive(true);

                if (roomPlayerDatas[i].GetPlayerID() == PhotonNetwork.player.ID)
                    lobbyUIManager.roomPanelScript.playerPanelScripts[i].MeImage.SetActive(true);

                else
                    lobbyUIManager.roomPanelScript.playerPanelScripts[i].MeImage.SetActive(false);


                if (roomPlayerDatas[i].GetIsMaster())
                    lobbyUIManager.roomPanelScript.playerPanelScripts[i].MasterImage.SetActive(true);
                else
                    lobbyUIManager.roomPanelScript.playerPanelScripts[i].MasterImage.SetActive(false);

            }


            // 유저가 접속 안한 패널
            else
            {

                lobbyUIManager.roomPanelScript.playerPanelScripts[i].SetActive(false);
            }

        }
    }

    // 방장인지 파악하고 Start버튼 활성화
    void CheckStartButton()
    {

        if (PhotonNetwork.isMasterClient)
        {

            //StartPanel 허용한다.
            lobbyUIManager.roomPanelScript.StartImage.GetComponent<Button>().interactable = true;
        }
        else
        {

            lobbyUIManager.roomPanelScript.StartImage.GetComponent<Button>().interactable = false;
        }
    }

    // 방 들어갔을때, Photon Event임.
    void RoomEnter()
    {
        if (!isUseEvent) return;

        gameStateType = EnumGameState.ROOM;

        DeleFadeOut = lobbyUIManager.waitingRoomPanelScript.FadeOutEffect;
        DeleFadeIn = lobbyUIManager.roomPanelScript.FadeInEffect;

        DeleSetOff = lobbyUIManager.waitingRoomPanelScript.SetActive;
        DeleSetOn = lobbyUIManager.roomPanelScript.SetActive;
        StartCoroutine("Finish_FadeOut_Start_Animation");

        // 이름 랜덤 생성
      //  CreateRandomID();

        // 플레이어 정보 생성
        InitPlayerList();

        // 그리기
        DrawRoomState();

        // 해쉬 데이터 설정
        InitHashData();

    }

    // 해쉬 데이터를 설정합니다.
    void InitHashData()
    {
        // 씬을 위해서 해쉬 생성
        ExitGames.Client.Photon.Hashtable PlayerSceneState = new ExitGames.Client.Photon.Hashtable
        {
            { "Scene", "Room" }
        };

        ExitGames.Client.Photon.Hashtable PlayerLoadingState = new ExitGames.Client.Photon.Hashtable
        {
            { "Offset","NULL" }
        };

        ExitGames.Client.Photon.Hashtable PlayerType = new ExitGames.Client.Photon.Hashtable
        {
            { "PlayerType","NULL" }
        };

        ExitGames.Client.Photon.Hashtable UseBoss = new ExitGames.Client.Photon.Hashtable
        {
            { "UseBoss",false }
        };

        ExitGames.Client.Photon.Hashtable CatScore = new ExitGames.Client.Photon.Hashtable
        {
            { "StoreScore",0f }
        };

        ExitGames.Client.Photon.Hashtable Round = new ExitGames.Client.Photon.Hashtable
        {
            { "Round",1 }
        };

        PhotonNetwork.player.SetCustomProperties(PlayerSceneState);
        PhotonNetwork.player.SetCustomProperties(PlayerLoadingState);
        PhotonNetwork.player.SetCustomProperties(PlayerType);
        PhotonNetwork.player.SetCustomProperties(UseBoss);
        PhotonNetwork.player.SetCustomProperties(CatScore);

        PhotonNetwork.player.SetCustomProperties(Round);
    }

    // 이름 랜덤 생성
    void CreateRandomID()
    {

        xmlManager = new XMLManager();
        List<string> Names = xmlManager.XmlRead();

        while (true)
        {
            string RandomMyName = Names[Random.Range(0, Names.Count)];

            bool isFind = false;


            string SelectMyName = null;
            for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
            {

                // 없으면 자기 닉네임으로 선정
                if (PhotonNetwork.playerList[i].NickName != RandomMyName)
                {

                    if (!isFind)
                        SelectMyName = RandomMyName;
                    isFind = true;
                }

                // 하나라도 겹치면 제외
                else
                {
                    Names.Remove(RandomMyName);
                    isFind = false;
                    break;
                }
            }

            // 찾으면 나감.
            if (isFind)
            {
                PhotonNetwork.playerName = SelectMyName;
                break;
            }

        }
    }

    /***** Click 이벤트 *****/

    // Room - 게임시작
    public void ClickGameStart()
    {

        if (!isUseEvent) return;

        soundManager.PlayEffectSound(SoundManager.EnumEffectSound.UI_BUTTONCLICK_1);

        PhotonNetwork.room.IsOpen = false;
        PhotonNetwork.room.IsVisible = false;

        photonView.RPC("RPCLoadingScene", PhotonTargets.All);
    }

    // Room - 게임 퇴장
    public void ClickGameExit()
    {


        if (!isUseEvent) return;

        soundManager.PlayEffectSound(SoundManager.EnumEffectSound.UI_BUTTONCLICK_EXIT_1);


        PhotonNetwork.LeaveRoom();
        gameStateType = EnumGameState.LOBBY;

        DeleFadeOut = lobbyUIManager.roomPanelScript.FadeOutEffect;
        DeleFadeIn = lobbyUIManager.lobbyPanelScript.FadeInEffect;

        DeleSetOff = lobbyUIManager.roomPanelScript.SetActive;
        DeleSetOn = lobbyUIManager.lobbyPanelScript.SetActive;

        StartCoroutine("Finish_FadeOut_Start_Animation");
    }

    // Room - Help 클릭 시
    public void ClickGameHelp()
    {
        if (!isUseEvent) return;

        soundManager.PlayEffectSound(SoundManager.EnumEffectSound.UI_BUTTONCLICK_1);
    }


    /**** RPC ****/
    [PunRPC]
    public void RPCLoadingScene()
    {
        DeleFadeOut = lobbyUIManager.roomPanelScript.FadeOutEffect;
        DeleFadeIn = null;
        DeleSetOff = lobbyUIManager.roomPanelScript.SetActive;
        DeleSetOn = null;
        FinishFadeEvent = ChangeScene;
        StartCoroutine("Finish_FadeOut_Start_Animation");
        soundManager.FadeOutSound();

    }
}
