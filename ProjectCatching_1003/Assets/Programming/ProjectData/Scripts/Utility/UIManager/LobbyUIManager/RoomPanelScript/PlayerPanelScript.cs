using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerPanelScript {

    private LobbyUIManager lobbyUIManager;

    public GameObject PlayerPanel { get; set; }

  /*  public GameObject MeImage { get; set; }
    public GameObject ChairImage { get; set; }
    public GameObject CatImage { get; set; }
    public GameObject MasterImage { get; set; }*/

    /*private UIEffect MeImageEffect;
    private UIEffect ChairImageEffect;
    private UIEffect CatImageEffect;
    private UIEffect MasterImageEffect;*/


    public GameObject ChaserImage;
    public GameObject RunnerImage;
    public GameObject RandomImage;
    public GameObject MasterImage;
    public GameObject PlayerSpace;
    public Text NameText;

    public GameObject CatMainImage;
    public GameObject MouseMainImage;
    public GameObject RandomMainImage;
    public GameObject ReadyImage;


    private UIEffect ChaserImageEffect;
    private UIEffect RunnerImageEffect;
    private UIEffect RandomImageEffect;
    private UIEffect MasterImageEffect;
    private UIEffect PlayerSpaceEffect;

    private UIEffect CatMainImageEffect;
    private UIEffect MouseMainImageEffect;
    private UIEffect RandomMainImageEffect;
    private UIEffect ReadyImageEffect;

    public void InitData(int PlayerPanelNumber)
    {
        
        lobbyUIManager = LobbyUIManager.GetInstance();

        PlayerPanel = lobbyUIManager.roomPanelScript.RoomPanel.transform.Find("PlayerPanel" + (PlayerPanelNumber + 1)).gameObject;

        ChaserImage = PlayerPanel.transform.Find("ChaserImage").gameObject;
        RunnerImage = PlayerPanel.transform.Find("RunnerImage").gameObject;
        RandomImage = PlayerPanel.transform.Find("RandomImage").gameObject;
        MasterImage = PlayerPanel.transform.Find("MasterImage").gameObject;
        PlayerSpace = PlayerPanel.transform.Find("PlayerSpace").gameObject;
        NameText = PlayerPanel.transform.Find("NameText").GetComponent<Text>();

        CatMainImage = PlayerPanel.transform.Find("CatMainImage").gameObject;
        MouseMainImage = PlayerPanel.transform.Find("MouseMainImage").gameObject;
        RandomMainImage = PlayerPanel.transform.Find("RandomMainImage").gameObject;
        ReadyImage = PlayerPanel.transform.Find("ReadyImage").gameObject;

        ChaserImageEffect = new UIEffect();
        RunnerImageEffect = new UIEffect();
        RandomImageEffect = new UIEffect();
        MasterImageEffect = new UIEffect();
        PlayerSpaceEffect = new UIEffect();

        CatMainImageEffect = new UIEffect();
        MouseMainImageEffect = new UIEffect();
        RandomMainImageEffect = new UIEffect();
        ReadyImageEffect = new UIEffect();

    }

    public void SetActive(bool isActive)
    {

        PlayerPanel.SetActive(isActive);

        ChaserImage.SetActive(isActive);
        RunnerImage.SetActive(isActive);
        RandomImage.SetActive(isActive);
        MasterImage.SetActive(isActive);
        PlayerSpace.SetActive(isActive);
        NameText.gameObject.SetActive(isActive);

        CatMainImage.SetActive(isActive);
        MouseMainImage.SetActive(isActive);
        RandomMainImage.SetActive(isActive);
        ReadyImage.SetActive(isActive);
    }

    public void SetSelectPlayer(string Player)
    {
        ChaserImage.SetActive(false);
        RunnerImage.SetActive(false);
        RandomImage.SetActive(false);

        MouseMainImage.SetActive(false);
        CatMainImage.SetActive(false);
        RandomMainImage.SetActive(false);

        if (Player == "Random")
        {
            RandomImage.SetActive(true);
            RandomMainImage.SetActive(true);
        }

        else if (Player == "Cat")
        {
            ChaserImage.SetActive(true);
            CatMainImage.SetActive(true);
        }

        else if (Player == "Mouse")
        {
            RunnerImage.SetActive(true);
            MouseMainImage.SetActive(true);
        }
    }

    private void FadeInEffect()
    {

        ChaserImageEffect.AddFadeEffectNode(ChaserImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);
        RunnerImageEffect.AddFadeEffectNode(RunnerImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);
        RandomImageEffect.AddFadeEffectNode(RandomImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);
        MasterImageEffect.AddFadeEffectNode(MasterImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);
        PlayerSpaceEffect.AddFadeEffectNode(PlayerSpace, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);

        CatMainImageEffect.AddFadeEffectNode(CatMainImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);
        MouseMainImageEffect.AddFadeEffectNode(MouseMainImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);
        RandomMainImageEffect.AddFadeEffectNode(RandomMainImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);
        ReadyImageEffect.AddFadeEffectNode(ReadyImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.IN);

        lobbyUIManager.UpdateEvent += ChaserImageEffect.EffectEventLobby;
        lobbyUIManager.UpdateEvent += RunnerImageEffect.EffectEventLobby;
        lobbyUIManager.UpdateEvent += RandomImageEffect.EffectEventLobby;
        lobbyUIManager.UpdateEvent += MasterImageEffect.EffectEventLobby;
        lobbyUIManager.UpdateEvent += PlayerSpaceEffect.EffectEventLobby;

        lobbyUIManager.UpdateEvent += CatMainImageEffect.EffectEventLobby;
        lobbyUIManager.UpdateEvent += MouseMainImageEffect.EffectEventLobby;
        lobbyUIManager.UpdateEvent += RandomMainImageEffect.EffectEventLobby;
        lobbyUIManager.UpdateEvent += ReadyImageEffect.EffectEventLobby;

    }


    private void FadeOutEffect()
    {

        ChaserImageEffect.AddFadeEffectNode(ChaserImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);
        RunnerImageEffect.AddFadeEffectNode(RunnerImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);
        RandomImageEffect.AddFadeEffectNode(RandomImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);
        MasterImageEffect.AddFadeEffectNode(MasterImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);
        PlayerSpaceEffect.AddFadeEffectNode(PlayerSpace, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);

        CatMainImageEffect.AddFadeEffectNode(CatMainImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);
        MouseMainImageEffect.AddFadeEffectNode(MouseMainImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);
        RandomMainImageEffect.AddFadeEffectNode(RandomMainImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);
        ReadyImageEffect.AddFadeEffectNode(ReadyImage, lobbyUIManager.UIFadeTime, UIEffectNode.EnumFade.OUT);

        lobbyUIManager.UpdateEvent += ChaserImageEffect.EffectEventLobby;
        lobbyUIManager.UpdateEvent += RunnerImageEffect.EffectEventLobby;
        lobbyUIManager.UpdateEvent += RandomImageEffect.EffectEventLobby;
        lobbyUIManager.UpdateEvent += MasterImageEffect.EffectEventLobby;
        lobbyUIManager.UpdateEvent += PlayerSpaceEffect.EffectEventLobby;

        lobbyUIManager.UpdateEvent += CatMainImageEffect.EffectEventLobby;
        lobbyUIManager.UpdateEvent += MouseMainImageEffect.EffectEventLobby;
        lobbyUIManager.UpdateEvent += RandomMainImageEffect.EffectEventLobby;
        lobbyUIManager.UpdateEvent += ReadyImageEffect.EffectEventLobby;

    }

    public void CallFadeInEffect()
    {
        FadeInEffect();
    }

    public void CallFadeOutEffect()
    {
        FadeOutEffect();
    }

}
