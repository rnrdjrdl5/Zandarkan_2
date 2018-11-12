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

    void RoomUpdate()
    {
        if (gameStateType == EnumGameState.ROOM)
        {
            roomPlayerDatas.Clear();
            InitPlayerList();

            DrawRoomState();

            // 키인식
            UseHelpWindow();
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

        DrawRoomName();

        CheckStartButton();

        // 패널  수 
        for (int i = 0; i < lobbyUIManager.roomPanelScript.playerPanelScripts.Length; i++)
        {

            // 유저 접속 패널
            if (i < PhotonNetwork.playerList.Length)
            {

                string selectPlayerType = (string)roomPlayerDatas[i].GetPhotonPlayer().CustomProperties["SelectPlayer"];

                // 1.플레이어 패널
                lobbyUIManager.roomPanelScript.playerPanelScripts[i].SetActive(true);
                lobbyUIManager.roomPanelScript.playerPanelScripts[i].SetSelectPlayer(selectPlayerType);
                lobbyUIManager.roomPanelScript.playerPanelScripts[i].NameText.text = roomPlayerDatas[i].GetPlayerName();

                if (roomPlayerDatas[i].GetIsMaster())
                    lobbyUIManager.roomPanelScript.playerPanelScripts[i].MasterImage.SetActive(true);
                else
                    lobbyUIManager.roomPanelScript.playerPanelScripts[i].MasterImage.SetActive(false);

                string readyPlayerType = (string)roomPlayerDatas[i].GetPhotonPlayer().CustomProperties["Ready"];


                // 방장이면 Ready 해제한다.
                if (roomPlayerDatas[i].GetPhotonPlayer().IsMasterClient)
                    roomPlayerDatas[i].GetPhotonPlayer().SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "Ready", "false" } });

                // true, false에 따른 레디 이미지 체크
                if (readyPlayerType == "true")
                {
                    lobbyUIManager.roomPanelScript.playerPanelScripts[i].ReadyImage.SetActive(true);
                }
                else if (readyPlayerType == "false")
                {
                    lobbyUIManager.roomPanelScript.playerPanelScripts[i].ReadyImage.SetActive(false);
                }
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

            lobbyUIManager.roomPanelScript.StartImage.SetActive(true);
            lobbyUIManager.roomPanelScript.ReadyImage.SetActive(false);
        }
        else
        {
            lobbyUIManager.roomPanelScript.StartImage.SetActive(false);
            lobbyUIManager.roomPanelScript.ReadyImage.SetActive(true);
        }
    }

    void DrawRoomName()
    {
        string roomName = PhotonNetwork.room.Name;

        lobbyUIManager.roomPanelScript.RoomNameText.text = roomName;

        lobbyUIManager.roomPanelScript.RoomName.SetActive(true);
    }

    // 방 들어갔을때, Photon Event임.
    void RoomEnter()
    {
        if (!isUseEvent) return;

        // 해쉬 데이터 설정
        InitHashData();

        gameStateType = EnumGameState.ROOM;

        DeleFadeOut = lobbyUIManager.waitingRoomPanelScript.FadeOutEffect;
        DeleFadeIn = lobbyUIManager.roomPanelScript.FadeInEffect;

        DeleSetOff = lobbyUIManager.waitingRoomPanelScript.SetActive;
        DeleSetOn = lobbyUIManager.roomPanelScript.SetActive;
        StartCoroutine("Finish_FadeOut_Start_Animation");


        // 플레이어 정보 생성
        InitPlayerList();

        // 그리기
        DrawRoomState();


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

        ExitGames.Client.Photon.Hashtable SelectPlayer = new ExitGames.Client.Photon.Hashtable
        {
            { "SelectPlayer","Random" }
        };

        ExitGames.Client.Photon.Hashtable ReadyType = new ExitGames.Client.Photon.Hashtable
        {
            { "Ready","false" }
        };

        PhotonNetwork.player.SetCustomProperties(PlayerSceneState);
        PhotonNetwork.player.SetCustomProperties(PlayerLoadingState);
        PhotonNetwork.player.SetCustomProperties(PlayerType);
        PhotonNetwork.player.SetCustomProperties(UseBoss);
        PhotonNetwork.player.SetCustomProperties(CatScore);
        PhotonNetwork.player.SetCustomProperties(SelectPlayer);
        PhotonNetwork.player.SetCustomProperties(ReadyType);

        PhotonNetwork.player.SetCustomProperties(Round);
    }


    // 키인식
    void UseHelpWindow()
    {
        if (lobbyUIManager.helpWindowScript.IsActiveHelpWindow())
        {
            if (Input.GetMouseButtonDown(0))
            {
                lobbyUIManager.helpWindowScript.NextPage();
            }

            if (Input.GetKeyDown(KeyCode.F1))
            {
                lobbyUIManager.helpWindowScript.SetActive(false);
            }
        }
    }

    /***** Click 이벤트 *****/

    // Room - 게임시작
    public void ClickGameStart()
    {

        if (!isUseEvent) return;

        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            if (!PhotonNetwork.playerList[i].IsMasterClient)
            {
                string data = (string)PhotonNetwork.playerList[i].CustomProperties["Ready"];

                if (data == "false") return;
            }
        }


        soundManager.PlayEffectSound(SoundManager.EnumEffectSound.UI_BUTTONCLICK_1);

        PhotonNetwork.room.IsOpen = false;
        PhotonNetwork.room.IsVisible = false;

        // 랜덤 결정 

        SetRandomPlayer();
        photonView.RPC("RPCLoadingScene", PhotonTargets.All);
    }

    public void SetRandomPlayer()
    {
        // 1. 고양이가 여러명 있으면 이중에 정한다.
        // 2, 고양이가 한명이면 이중에 정한다.
        // 3. 쥐밖에 없으면 그 중에 정한다.
        // 4. 쥐를 고르고 랜덤이 있으면 랜덤에서만 고양이를 선택한다.


        // 쥐 , 고양이, 랜덤  카운팅
        int catCount = 0;
        List<PhotonPlayer> catPlayers = new List<PhotonPlayer>();

        int mouseCount = 0;
        List<PhotonPlayer> mousePlayers = new List<PhotonPlayer>();

        int randomCount = 0;
        List<PhotonPlayer> randomPlayers = new List<PhotonPlayer>();


        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            if ((string)PhotonNetwork.playerList[i].CustomProperties["SelectPlayer"] ==
                "Cat")
            {
                catCount++;
                catPlayers.Add(PhotonNetwork.playerList[i]);
            }

            else if ((string)PhotonNetwork.playerList[i].CustomProperties["SelectPlayer"] ==
                "Mouse")
            {
                mouseCount++;
                mousePlayers.Add(PhotonNetwork.playerList[i]);
            }

            else
            {
                randomCount++;
                randomPlayers.Add(PhotonNetwork.playerList[i]);
            }

            
        }



        // 고양이 플레이어 정하기
        PhotonPlayer catPlayer = null;

        if (catCount >= 1)
        {
            int tempCount = Random.Range(0, catCount);

            catPlayer = catPlayers[tempCount];
        }

        else if (catCount == 0)
        {
            // 랜덤을 돌리는 대상은 랜덤 대상 위주로

            if (randomCount >= 1)
            {
                int tempCount = Random.Range(0, randomCount);
                catPlayer = randomPlayers[tempCount];
            }

            else
            {
                int tempCount = Random.Range(0, PhotonNetwork.playerList.Length);
                catPlayer = PhotonNetwork.playerList[tempCount];
                Debug.Log("모두쥐");
            }
        }


        // 실질적인 플레이어 정하기
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {

            if (catPlayer.ID == PhotonNetwork.playerList[i].ID)
            {
                PhotonNetwork.playerList[i].
                    SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "PlayerType", "Cat" } });
                Debug.Log("asf");
            }

            else
            {
                PhotonNetwork.playerList[i].
                SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "PlayerType", "Mouse" } });
            }
        }

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
    public void ClickExplaneImage()
    {
        if (!isUseEvent) return;
        lobbyUIManager.helpWindowScript.ShowHelp();

        soundManager.PlayEffectSound(SoundManager.EnumEffectSound.UI_BUTTONCLICK_1);
    }

    // Room - 쥐 선택 시
    public void ClickSelectMouse()
    {
        if (!isUseEvent) return;

        soundManager.PlayEffectSound(SoundManager.EnumEffectSound.UI_BUTTONCLICK_1);
        PhotonNetwork.player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "SelectPlayer", "Mouse" } });
    }

    // Room - 고양이 선택 시
    public void ClickSelectCat()
    {
        if (!isUseEvent) return;

        soundManager.PlayEffectSound(SoundManager.EnumEffectSound.UI_BUTTONCLICK_1);
        PhotonNetwork.player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "SelectPlayer", "Cat" } });
    }

    // Room - 랜덤 선택 시 
    public void ClickSelectRandom()
    {
        if (!isUseEvent) return;

        soundManager.PlayEffectSound(SoundManager.EnumEffectSound.UI_BUTTONCLICK_1);
        PhotonNetwork.player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "SelectPlayer", "Random" } });
    }

    // Ready 누를 시 , 방장은 당연히 못누른다.
    public void ClickReadyImage()
    {
        if (!isUseEvent) return;

        if (lobbyUIManager.roomPanelScript.ReadyImage.activeInHierarchy == true)
        {
            soundManager.PlayEffectSound(SoundManager.EnumEffectSound.UI_BUTTONCLICK_1);

            string readyData = (string)PhotonNetwork.player.CustomProperties["Ready"];
            if (readyData == "true")
            {

                PhotonNetwork.player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "Ready", "false" } });
                Debug.Log("1");
            }

            else if (readyData == "false")
            {

                PhotonNetwork.player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "Ready", "true" } });
                Debug.Log("2");
            }
        }

    }


    /**** RPC ****/
    [PunRPC]
    public void RPCLoadingScene()
    {
        lobbyUIManager.helpWindowScript.SetActive(false);

        DeleFadeOut = lobbyUIManager.roomPanelScript.FadeOutEffect;
        DeleFadeIn = null;
        DeleSetOff = lobbyUIManager.roomPanelScript.SetActive;
        DeleSetOn = null;
        FinishFadeEvent = ChangeScene;
        StartCoroutine("Finish_FadeOut_Start_Animation");
        soundManager.FadeOutSound();

    }
}
