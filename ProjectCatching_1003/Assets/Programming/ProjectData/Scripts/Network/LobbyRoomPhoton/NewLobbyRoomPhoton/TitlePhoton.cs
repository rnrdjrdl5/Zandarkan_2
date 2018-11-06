using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class NewLobbyRoomPhoton
{

    private GameObject readyTimeLine;
    private GameObject playTimeLine;

    void TitleAwake()
    {
        GameObject OpeningObject = GameObject.Find("OpeningTimeLine").gameObject;

        readyTimeLine = OpeningObject.transform.Find("ReadyTimeLine").gameObject;
        playTimeLine = OpeningObject.transform.Find("PlayTimeLine").gameObject;
    }

    void TitleStart()
    {
        StartCoroutine("FadeInGame");
    }
    void TitleUpdate()
    {
        if (gameStateType == EnumGameState.TITLE)
        {
            if (!Input.anyKeyDown) return;
            if (!isUseEvent) return;

            StartCoroutine("ChangeTitleLobby");

        }
    }

    IEnumerator ChangeTitleLobby()
    {

        readyTimeLine.SetActive(false);
        playTimeLine.SetActive(true);

        isUseEvent = false;

        DeleFadeOut = lobbyUIManager.titlePanelScript.FadeOutEffect;
        DeleFadeOut();

        yield return new WaitForSeconds(3.0f);


        gameStateType = EnumGameState.LOBBY;

        lobbyUIManager.titlePanelScript.SetActive(false);

        DeleSetOn = lobbyUIManager.lobbyPanelScript.SetActive;
        DeleFadeIn = lobbyUIManager.lobbyPanelScript.FadeInEffect;

        DeleSetOn(true);
        DeleFadeIn();


        yield return new WaitForSeconds(lobbyUIManager.UIFadeTime);

        isUseEvent = true;
    }

    IEnumerator FadeInGame()
    {
        // 새운드 재생과 함께 숨겨진 로고이미지 삭제
        //soundManager.PlayBGSound(SoundManager.EnumBGSound.BG_LOBBY_SOUND);


        lobbyUIManager.fadePanelScript.FadeOutEffect();
        yield return new WaitForSeconds(lobbyUIManager.UIFadeTime);

        lobbyUIManager.fadePanelScript.SetActive(false);

        isUseEvent = true;
        gameStateType = EnumGameState.TITLE;
    }


    /**** Click 이벤트 ****/


    // Title - Lobby 입장 클릭 시
    public void ClickEnterLobby()
    {
        if (!isUseEvent) return;

        soundManager.PlayEffectSound(SoundManager.EnumEffectSound.UI_BUTTONCLICK_1);
        // 로비 이동 클릭 시 먼저 입장함 이후 UI 변경
        PhotonNetwork.JoinLobby();
    }

    // Title - Lobby 퇴장 클릭 시
    public void ClickExitClient()
    {
        if (!isUseEvent) return;

        soundManager.PlayEffectSound(SoundManager.EnumEffectSound.UI_BUTTONCLICK_1);
        Application.Quit();
    }


    public void ClickGameTutorial()
    {
        if (!isUseEvent) return;

        soundManager.PlayEffectSound(SoundManager.EnumEffectSound.UI_BUTTONCLICK_1);

        //FindRoomGUI();
        gameStateType = EnumGameState.TUTORIAL;

        DeleFadeOut = lobbyUIManager.lobbyPanelScript.FadeOutEffect;
        DeleFadeIn = lobbyUIManager.tutorialPanelScript.FadeInEffect;

        DeleSetOff = lobbyUIManager.lobbyPanelScript.SetActive;
        DeleSetOn = lobbyUIManager.tutorialPanelScript.SetActive;
        StartCoroutine("Finish_FadeOut_Start_Animation");
        FinishFadeEvent = WaitingRoomEvent;
    }



}
