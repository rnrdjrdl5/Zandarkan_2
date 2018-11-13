using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradePanelScript{

    public int ShakeFrequency;
    public int ShakePower;

    public GameObject GradePanel { set; get; }
    public void InitGradePanel() { GradePanel = UIManager.GetInstance().UICanvas.transform.Find("GradePanel").gameObject; }


    public GameObject GradeBarImage { get; set; }
    public void InitGradeBarImage() { GradeBarImage = GradePanel.transform.Find("GradeBarImage").gameObject; }

    public Image GradeBarImageImage { get; set; }
    public void InitGradeBarImageImage(){GradeBarImageImage = GradeBarImage.GetComponent<Image>();}

    
    public RectTransform GradeBarImageRect { get; set; }
    public void InitGradeBarImageRect() { GradeBarImageRect = GradeBarImage.GetComponent<RectTransform>(); }


    public GameObject GradeBarBackImage { get; set; }
    public void InitGradeBarBackImage() { GradeBarBackImage = GradePanel.transform.Find("GradeBarBackImage").gameObject; }


    public GameObject GradeHouseImage { get; set; }
    public void InitGradeHouseImage() { GradeHouseImage = GradePanel.transform.Find("GradeHouseImage").gameObject; }


    public GameObject GradeArrow { get; set; }
    public void InitGradeArrow() { GradeArrow = GradePanel.transform.Find("GradeArrow").gameObject; }

    public GameObject GradeRedArrow { get; set; }
    public void InitGradeRedArrow() { GradeRedArrow = GradePanel.transform.Find("GradeRedArrow").gameObject; }

    public GameObject GradeYellowArrow { get; set; }
    public void InitGradeYellowArrow() { GradeYellowArrow = GradePanel.transform.Find("GradeYellowArrow").gameObject; }

    private UIEffect uIEffect;

    private UIEffect ArrowEffect;
    private UIEffect RedArrowEffect;
    private UIEffect YellowArrowEffect;

    public void InitData()
    {
        InitGradePanel();

        InitGradeBarImage();
        InitGradeBarImageImage();
        InitGradeBarImageRect();

        InitGradeBarBackImage();

        InitGradeHouseImage();

        InitGradeArrow();
        InitGradeRedArrow();
        InitGradeYellowArrow();

        UIManager uIManager = UIManager.GetInstance();

        uIEffect = new UIEffect();
        ShakeFrequency = uIManager.HPPanel_ShakeFrequency;
        ShakePower = uIManager.HPPanel_ShakePower;

        uIManager.UpdateEvent += UpdateGrade;


        ArrowEffect = new UIEffect();
        RedArrowEffect = new UIEffect();
        YellowArrowEffect = new UIEffect();


}

    public void InitEvent()
    {
        ObjectManager.GetInstance().RemoveEvent += ShakeRestaurantEvent;
    }

    public void SetActiveObjects(bool isActive)
    {
        GradeBarImage.SetActive(isActive);

        GradeBarBackImage.SetActive(isActive);

        GradeHouseImage.SetActive(isActive);

        GradeArrow.SetActive(isActive);

        GradeRedArrow.SetActive(isActive);

        GradeYellowArrow.SetActive(isActive);
    }


    void UpdateGrade()
    {
        float CatGradeScore = 0;
      /*  for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
          {
              if ((string)PhotonNetwork.playerList[i].CustomProperties["PlayerType"] == "Cat")
              {
                  CatGradeScore = (float)PhotonNetwork.playerList[i].CustomProperties["CatScore"];
                  break;
              }
         }*/
        
        CatGradeScore = (float)PhotonNetwork.player.CustomProperties["StoreScore"];

        PhotonManager photonManager = PhotonManager.GetInstance();

        float GradePersent = (float)CatGradeScore / (float)photonManager.MaxCatScore;

        GradeBarImageImage.fillAmount = GradePersent;


        float GradeRootYPos = GradeBarImage.transform.localPosition.y -
            GradeBarImageRect.rect.height / 2;


        float GradeArrowYPos = GradeRootYPos + GradePersent * GradeBarImageRect.rect.height;

        float GradeRedArrowYPos = GradeRootYPos + photonManager.GameBreakCondition / 100 * GradeBarImageRect.rect.height;

        float GradeYellowArrowYPos = GradeRootYPos + photonManager.GameTimeOutCondition / 100 * GradeBarImageRect.rect.height;

        
        GradeArrow.transform.localPosition = new Vector3(
            GradeArrow.transform.localPosition.x,
            GradeArrowYPos,
            GradeArrow.transform.localPosition.z);

        GradeRedArrow.transform.localPosition = new Vector3(
            GradeArrow.transform.localPosition.x,
            GradeRedArrowYPos,
            GradeArrow.transform.localPosition.z
            );

        GradeYellowArrow.transform.localPosition = new Vector3(
            GradeArrow.transform.localPosition.x,
            GradeYellowArrowYPos,
            GradeArrow.transform.localPosition.z
            );





    }

    private void ShakeRestaurantEvent()
    {
        uIEffect.CheckResetUIEffect(GradePanel);
        uIEffect.originalLocalPosition = GradePanel.transform.localPosition;
        uIEffect.AddShakeEffectNode(GradePanel, ShakeFrequency, ShakePower, UIEffectNode.ShakeType.RIGHTLEFT);

        UIManager.GetInstance().UpdateEvent += uIEffect.EffectEvent;
    }


    public void AlmostTimeOut()
    {
        ArrowScale(ArrowEffect, GradeArrow, 100, 200, 0.15f);
        ArrowEffect.AddWaitEffectNode(0.35f);

        ArrowScale(ArrowEffect, GradeArrow, 100, 200, 0.15f);
        ArrowEffect.AddWaitEffectNode(0.35f);

        ArrowScale(ArrowEffect, GradeArrow, 100, 200, 0.15f);
        ArrowEffect.AddWaitEffectNode(0.35f);

        UIManager.GetInstance().UpdateEvent += ArrowEffect.EffectEvent;



        ArrowScale(RedArrowEffect, GradeRedArrow, 100, 200, 0.15f);
        RedArrowEffect.AddWaitEffectNode(0.35f);

        ArrowScale(RedArrowEffect, GradeRedArrow, 100, 200, 0.15f);
        RedArrowEffect.AddWaitEffectNode(0.35f);

        ArrowScale(RedArrowEffect, GradeRedArrow, 100, 200, 0.15f);
        RedArrowEffect.AddWaitEffectNode(0.35f);

        UIManager.GetInstance().UpdateEvent += RedArrowEffect.EffectEvent;



        ArrowScale(YellowArrowEffect, GradeYellowArrow, 100, 200, 0.15f);
        YellowArrowEffect.AddWaitEffectNode(0.35f);

        ArrowScale(YellowArrowEffect, GradeYellowArrow, 100, 200, 0.15f);
        YellowArrowEffect.AddWaitEffectNode(0.35f);

        ArrowScale(YellowArrowEffect, GradeYellowArrow, 100, 200, 0.15f);
        YellowArrowEffect.AddWaitEffectNode(0.35f);

        UIManager.GetInstance().UpdateEvent += YellowArrowEffect.EffectEvent;
    }

    private void ArrowScale(UIEffect uIEffect, GameObject arrow, float min, float max, float time)
    {
        float currentTime = time / 3;

        uIEffect.AddScaleEffectNode(arrow, 100.0f, max, currentTime);
        uIEffect.AddScaleEffectNode(arrow, max, min, currentTime);
        uIEffect.AddScaleEffectNode(arrow, min, 100.0f, currentTime);

    }
}
