using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialLoadingPanelScript{

    public GameObject TutorialLoadingPanel;
    
    public GameObject LoadingObject;
    public Image LoadingImage;

    public UIEffect TutorialLoadingPanelEffect;
    public UIEffect LoadingObjectEffect;

    LobbyUIManager lobbyUIManager;

    public void InitData()
    {
        lobbyUIManager = LobbyUIManager.GetInstance();

        TutorialLoadingPanel = lobbyUIManager.UICanvas.transform.Find("TutorialLoadingPanel").gameObject;

        LoadingObject = TutorialLoadingPanel.transform.Find("LoadingObject").gameObject;
        LoadingImage = LoadingObject.GetComponent<Image>();

        TutorialLoadingPanelEffect = new UIEffect();
        LoadingObjectEffect = new UIEffect();
    }

    public void SetActive(bool isActive)
    {
        TutorialLoadingPanel.SetActive(isActive);

        LoadingObject.SetActive(isActive);
    }

    public void FadeInEffect()
    {
        TutorialLoadingPanelEffect.AddFadeEffectNode(TutorialLoadingPanel, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);
        lobbyUIManager.UpdateEvent += TutorialLoadingPanelEffect.EffectEventLobby;

        LoadingObjectEffect.AddFadeEffectNode(LoadingObject, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);
        lobbyUIManager.UpdateEvent += LoadingObjectEffect.EffectEventLobby;
    }

    public void FadeOutEsffect()
    {
        TutorialLoadingPanelEffect.AddFadeEffectNode(TutorialLoadingPanel, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);
        lobbyUIManager.UpdateEvent += TutorialLoadingPanelEffect.EffectEventLobby;

        LoadingObjectEffect.AddFadeEffectNode(LoadingObject, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);
        lobbyUIManager.UpdateEvent += LoadingObjectEffect.EffectEventLobby;
    }
}
