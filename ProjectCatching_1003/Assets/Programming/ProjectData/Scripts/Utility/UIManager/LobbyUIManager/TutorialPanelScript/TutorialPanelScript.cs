using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanelScript : MonoBehaviour {

    LobbyUIManager lobbyUIManager;

    GameObject TutorialPanel;

    public GameObject MouseSelect;
    public GameObject CatSelect;

    public GameObject MouseImage;
    public GameObject CatImage;

    public GameObject TutorialStartButton;
    public GameObject ReturnButton;

    public Text playerText;
    public TutorialPlayerText tutorialPlayerText;



    private UIEffect MouseSelectEffect;
    private UIEffect CatSelectEffect;

    private UIEffect MouseImageEffect;
    private UIEffect CatImageEffect;

    private UIEffect TutorialStartButtonEffect;
    private UIEffect ReturnButtonEffect;


    public void InitData()
    {

        lobbyUIManager = LobbyUIManager.GetInstance();
        TutorialPanel = lobbyUIManager.UICanvas.transform.Find("TutorialPanel").gameObject;

        MouseSelect = TutorialPanel.transform.Find("MouseSelect").gameObject;
        CatSelect = TutorialPanel.transform.Find("CatSelect").gameObject;

        MouseImage = TutorialPanel.transform.Find("MouseImage").gameObject;
        CatImage = TutorialPanel.transform.Find("CatImage").gameObject;

        TutorialStartButton = TutorialPanel.transform.Find("TutorialStartButton").gameObject;
        ReturnButton = TutorialPanel.transform.Find("ReturnButton").gameObject;

        playerText = TutorialPanel.transform.Find("PlayerText").GetComponent<Text>();
        tutorialPlayerText = TutorialPanel.transform.Find("PlayerText").gameObject.GetComponent<TutorialPlayerText>();

        MouseSelectEffect = new UIEffect();
        CatSelectEffect = new UIEffect();
        MouseImageEffect = new UIEffect();
        CatImageEffect = new UIEffect();
        TutorialStartButtonEffect = new UIEffect();
        ReturnButtonEffect = new UIEffect();


    }

    public void SetActive(bool isActive)
    {

        MouseSelect.SetActive(isActive);
        CatSelect.SetActive(isActive);

        TutorialPanel.SetActive(isActive);

        lobbyUIManager.LineSetActive(isActive);
    }

    public void FadeOutEffect()
    {

        MouseSelectEffect.AddFadeEffectNode(MouseSelect, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);
        lobbyUIManager.UpdateEvent += MouseSelectEffect.EffectEventLobby;

        CatSelectEffect.AddFadeEffectNode(CatSelect, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);
        lobbyUIManager.UpdateEvent += CatSelectEffect.EffectEventLobby;


        MouseImageEffect.AddFadeEffectNode(MouseImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);
        lobbyUIManager.UpdateEvent += MouseImageEffect.EffectEventLobby;

        CatImageEffect.AddFadeEffectNode(CatImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);
        lobbyUIManager.UpdateEvent += CatImageEffect.EffectEventLobby;



        TutorialStartButtonEffect.AddFadeEffectNode(TutorialStartButton, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);
        lobbyUIManager.UpdateEvent += TutorialStartButtonEffect.EffectEventLobby;

        ReturnButtonEffect.AddFadeEffectNode(ReturnButton, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);
        lobbyUIManager.UpdateEvent += ReturnButtonEffect.EffectEventLobby;




        lobbyUIManager.LineFadeOutEffect();
    }

    public void FadeInEffect()
    {

        MouseSelectEffect.AddFadeEffectNode(MouseSelect, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);
        lobbyUIManager.UpdateEvent += MouseSelectEffect.EffectEventLobby;

        CatSelectEffect.AddFadeEffectNode(CatSelect, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);
        lobbyUIManager.UpdateEvent += CatSelectEffect.EffectEventLobby;



        MouseImageEffect.AddFadeEffectNode(MouseImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);
        lobbyUIManager.UpdateEvent += MouseImageEffect.EffectEventLobby;

        CatImageEffect.AddFadeEffectNode(CatImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);
        lobbyUIManager.UpdateEvent += CatImageEffect.EffectEventLobby;



        TutorialStartButtonEffect.AddFadeEffectNode(TutorialStartButton, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);
        lobbyUIManager.UpdateEvent += TutorialStartButtonEffect.EffectEventLobby;

        ReturnButtonEffect.AddFadeEffectNode(ReturnButton, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);
        lobbyUIManager.UpdateEvent += ReturnButtonEffect.EffectEventLobby;


        lobbyUIManager.LineFadeInEffect();
    }
}
