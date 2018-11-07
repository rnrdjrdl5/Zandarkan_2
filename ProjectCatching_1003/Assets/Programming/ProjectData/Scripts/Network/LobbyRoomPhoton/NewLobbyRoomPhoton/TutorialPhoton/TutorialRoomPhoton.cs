using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class NewLobbyRoomPhoton
{

    void TutorialUpdate()
    {
        if (gameStateType == EnumGameState.TUTORIAL)
        {
            if (tutorialSelectedType == "Mouse")
            {
                lobbyUIManager.tutorialPanelScript.MouseImage.SetActive(true);
                lobbyUIManager.tutorialPanelScript.CatImage.SetActive(false);

                lobbyUIManager.tutorialPanelScript.playerText.text =
                lobbyUIManager.tutorialPanelScript.tutorialPlayerText.MouseText;
            }

            else if (tutorialSelectedType == "Cat")
            {
                lobbyUIManager.tutorialPanelScript.MouseImage.SetActive(false);
                lobbyUIManager.tutorialPanelScript.CatImage.SetActive(true);

                lobbyUIManager.tutorialPanelScript.playerText.text =
                lobbyUIManager.tutorialPanelScript.tutorialPlayerText.CatText;
            }
        }

    }
    public string tutorialSelectedType = "Mouse";

    public void ClickTutorialSelectMouse()
    {
        if (!isUseEvent) return;

        tutorialSelectedType = "Mouse";

        lobbyUIManager.tutorialPanelScript.MouseImage.SetActive(true);
        lobbyUIManager.tutorialPanelScript.CatImage.SetActive(false);

    }

    public void ClickTutorialSelectCat()
    {
        if (!isUseEvent) return;

        tutorialSelectedType = "Cat";

        lobbyUIManager.tutorialPanelScript.MouseImage.SetActive(false);
        lobbyUIManager.tutorialPanelScript.CatImage.SetActive(true);
    }

    public void ClickTutorialStartButton()
    {

        DeleFadeOut = lobbyUIManager.tutorialPanelScript.FadeOutEffect;
        DeleFadeIn = lobbyUIManager.fadePanelScript.FadeInEffect;
        DeleSetOff = lobbyUIManager.tutorialPanelScript.SetActive;
        DeleSetOn = lobbyUIManager.fadePanelScript.SetActive;
        FinishFadeEvent = EnterLobbyTutorial;
        StartCoroutine("Finish_FadeOut_Start_Animation");
        soundManager.FadeOutSound();
    }

    public void ClickTutorialReturnButton()
    {


        gameStateType = EnumGameState.LOBBY;

        DeleFadeOut = lobbyUIManager.tutorialPanelScript.FadeOutEffect;
        DeleFadeIn = lobbyUIManager.lobbyPanelScript.FadeInEffect;
        DeleSetOff = lobbyUIManager.tutorialPanelScript.SetActive;
        DeleSetOn = lobbyUIManager.lobbyPanelScript.SetActive;
        FinishFadeEvent = WaitingRoomEvent;
        StartCoroutine("Finish_FadeOut_Start_Animation");
    }




    public void EnterLobbyTutorial()
    {
        StartCoroutine("TutorialLoading");
    }

    IEnumerator TutorialLoading()
    {

        lobbyUIManager.tutorialLoadingPanelScript.SetActive(true);
        lobbyUIManager.tutorialLoadingPanelScript.FadeInEffect();

        yield return new WaitForSeconds(1.5f);

        PhotonNetwork.JoinLobby();
    }

    public void EnterRoomTutorial()
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
            { "PlayerType",tutorialSelectedType }
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

        PhotonNetwork.player.SetCustomProperties(PlayerSceneState);
        PhotonNetwork.player.SetCustomProperties(PlayerLoadingState);
        PhotonNetwork.player.SetCustomProperties(PlayerType);
        PhotonNetwork.player.SetCustomProperties(UseBoss);
        PhotonNetwork.player.SetCustomProperties(CatScore);
        PhotonNetwork.player.SetCustomProperties(SelectPlayer);

        PhotonNetwork.player.SetCustomProperties(Round);


        SceneManager.LoadScene("TutorialScene");

        
    }



    public void TutorialEnter()
    {
        RoomOptions ro = new RoomOptions
        {
            MaxPlayers = 2,
            IsVisible = false,
            IsOpen = false
        };


        PhotonNetwork.playerName = lobbyUIManager.waitingRoomPanelScript.InputPlayerName.text;
        PhotonNetwork.CreateRoom("tutorial" + Random.Range(0, 100000), ro, TypedLobby.Default);
    }
}

