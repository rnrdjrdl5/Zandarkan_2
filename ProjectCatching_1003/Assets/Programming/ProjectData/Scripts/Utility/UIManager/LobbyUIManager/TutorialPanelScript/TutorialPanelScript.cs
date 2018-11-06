using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanelScript : MonoBehaviour {

    LobbyUIManager lobbyUIManager;

    GameObject TutorialPanel;

    GameObject MouseSelect;
    GameObject CatSelect;

    private UIEffect MouseSelectEffect;
    private UIEffect CatSelectEffect;

    public void InitData()
    {

        lobbyUIManager = LobbyUIManager.GetInstance();
        TutorialPanel = lobbyUIManager.UICanvas.transform.Find("TutorialPanel").gameObject;

        MouseSelect = TutorialPanel.transform.Find("MouseSelect").gameObject;
        CatSelect = TutorialPanel.transform.Find("CatSelect").gameObject;

        MouseSelectEffect = new UIEffect();
        CatSelectEffect = new UIEffect();
    }

    public void SetActive(bool isActive)
    {

        MouseSelect.SetActive(isActive);
        CatSelect.SetActive(isActive);

        TutorialPanel.SetActive(isActive);
    }

    public void FadeOutEffect()
    {

        MouseSelectEffect.AddFadeEffectNode(MouseSelect, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);
        lobbyUIManager.UpdateEvent += MouseSelectEffect.EffectEventLobby;

        CatSelectEffect.AddFadeEffectNode(CatSelect, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);
        lobbyUIManager.UpdateEvent += CatSelectEffect.EffectEventLobby;

        lobbyUIManager.LineFadeOutEffect();
    }

    public void FadeInEffect()
    {

        MouseSelectEffect.AddFadeEffectNode(MouseSelect, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);
        lobbyUIManager.UpdateEvent += MouseSelectEffect.EffectEventLobby;

        CatSelectEffect.AddFadeEffectNode(CatSelect, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);
        lobbyUIManager.UpdateEvent += CatSelectEffect.EffectEventLobby;

        lobbyUIManager.LineFadeOutEffect();
    }
}
