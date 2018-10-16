﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public partial class TutorialGuideEditor
{

    void SettingConditionInspector(TutorialCondition nowCondition)
    {
        switch (nowCondition.tutorialConditionType)
        {
            case TutorialCondition.EnumTutorialCondition.PLACE:
                PlaceInspector(nowCondition);
                break;
            case TutorialCondition.EnumTutorialCondition.ONMOUSE:
                OnMouseInspector(nowCondition);
                break;
            case TutorialCondition.EnumTutorialCondition.USEACTIVE:
                UseActionInspector(nowCondition);
                break;
            case TutorialCondition.EnumTutorialCondition.USEINTERACTIVE:
                UseInteractiveInspector(nowCondition);
                break;
        }
    }


    void PlaceInspector(TutorialCondition nowCondition)
    {

        // 1. 위치를 설정
        nowCondition.tutorialPlaceType =
             (TutorialCondition.EnumTutorialPlace)EditorGUILayout.EnumPopup
             ("원하는 위치 정의",
             nowCondition.tutorialPlaceType);

        // 2. enum을 토대로 설정
        int tutorialPlaceType = (int)nowCondition.tutorialPlaceType;

        // 3. enum은 Place가 있을때만 작동하도록, 위치 Object 설정
        if (tutorialPlaceType <= tutorialPlace.places.Length - 1)
        {

            nowCondition.tutorialPlace =
                tutorialPlace.places[tutorialPlaceType];

            nowCondition.checkTutorialPlace =
                nowCondition.tutorialPlace.GetComponent<CheckTutorialPlace>();
        }
    }

    void OnMouseInspector(TutorialCondition nowCondition)
    {
        // 1. 텍스트 타입 설정
        nowCondition.onMouseType = (TutorialCondition.EnumOnMouse)EditorGUILayout.EnumPopup
        ("마우스 대상",
        nowCondition.onMouseType);
    }

    void UseActionInspector(TutorialCondition nowCondition)
    {
        nowCondition.activeType = (TutorialCondition.EnumActive)EditorGUILayout.EnumPopup
        ("액티브 사용/횟수",
        nowCondition.activeType);

        nowCondition.activeMount = 
            EditorGUILayout.FloatField("사용 횟수", nowCondition.activeMount);
    }

    void UseInteractiveInspector(TutorialCondition nowCondition)
    {
        nowCondition.interactiveObjectType = (InteractiveState.EnumInteractiveObject)EditorGUILayout.EnumPopup
            ("상호작용 타입",
            nowCondition.interactiveObjectType);

        nowCondition.interactiveMount =
            EditorGUILayout.IntField("사용횟수", nowCondition.interactiveMount);
    }
}


