﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCanvasManager : MonoBehaviour {


    private static  TutorialCanvasManager tutorialCanvasManager;
    public static TutorialCanvasManager GetInstance() { return tutorialCanvasManager; }

    public GameObject OverlayCanvas;
    public GameObject RescueIconPanel;
    public GameObject RescueSet1;
    public Image RescueIcon;


    public GameObject TutorialBigUI;
    public GameObject GradeUI;
    public GameObject SpeedUI;
    public GameObject MarbleUI;
    public GameObject NinjaUI;

    // Use this for initialization
    private void Awake()
    {
        tutorialCanvasManager = this;

        OverlayCanvas = transform.Find("TutoOverlayCanvas").gameObject;
        RescueIconPanel = OverlayCanvas.transform.Find("RescueIconPanel").gameObject;

        RescueSet1 = RescueIconPanel.transform.Find("RescueSet1").gameObject;
        RescueIcon = RescueSet1.transform.Find("RescueIcon").GetComponent<Image>();

        TutorialBigUI = transform.Find("TutorialBigUI").gameObject;
        GradeUI = TutorialBigUI.transform.Find("GradeUI").gameObject;

        SpeedUI = TutorialBigUI.transform.Find("SpeedUI").gameObject;
        MarbleUI = TutorialBigUI.transform.Find("MarbleUI").gameObject;
        NinjaUI = TutorialBigUI.transform.Find("NinjaUI").gameObject;


    }

}
