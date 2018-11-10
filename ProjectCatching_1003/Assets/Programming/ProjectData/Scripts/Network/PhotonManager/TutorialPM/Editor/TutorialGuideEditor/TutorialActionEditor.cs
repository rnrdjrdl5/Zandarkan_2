 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;




public partial class TutorialGuideEditor
{
    void SettingActionInspector(TutorialAction nowAction)
    {
        switch (nowAction.tutorialActionType)
        {
            case TutorialAction.EnumTutorialAction.MESSAGE:
                MessageInspector(nowAction);
                break;
            case TutorialAction.EnumTutorialAction.WAIT:
                WaitInspector(nowAction);
                break;
            case TutorialAction.EnumTutorialAction.EMOTION:
                EmotionInspector(nowAction);
                break;
            case TutorialAction.EnumTutorialAction.DRAW_IMAGE:
                DrawImage(nowAction);
                break;
            case TutorialAction.EnumTutorialAction.PRACTICE_SKILL:
                SetPracticeSkill(nowAction);
                break;
            case TutorialAction.EnumTutorialAction.RESET_PRACTICE:
                SetResetSkill(nowAction);
                break;
            case TutorialAction.EnumTutorialAction.SET_AI_STATE:
                SetAIState(nowAction);
                break;
            case TutorialAction.EnumTutorialAction.SHOW_IMAGE:
                ShowImage(nowAction);
                break;
            case TutorialAction.EnumTutorialAction.RELEASE_IMAGE:
                ReleaseImage(nowAction);
                break;
            case TutorialAction.EnumTutorialAction.SET_ACTIVE_AI:
                SetActiveAI(nowAction);
                break;
            case TutorialAction.EnumTutorialAction.SET_USE_DEAD_COUNT:
                UseDeadCount(nowAction);
                break;
            case TutorialAction.EnumTutorialAction.SET_AI_USE_HEALTH_DOWN:
                UseHealthDown(nowAction);
                break;
            case TutorialAction.EnumTutorialAction.BLOCK_OTHER_SKILL:
                UseBlockOtherSkill(nowAction);
                break;
            case TutorialAction.EnumTutorialAction.ACTIVE_OTHER_SKILL:
                UseActiveOtherSkill(nowAction);
                break;
            case TutorialAction.EnumTutorialAction.FADE_OUT:
                UseFadeOut(nowAction);
                break;
            case TutorialAction.EnumTutorialAction.GAME_END:
                UseGameEnd(nowAction);
                break;
        }
    }

    void MessageInspector(TutorialAction nowAction)
    {
        // 1. 텍스트 타입 설정
        nowAction.messageSizeType = (TutorialAction.EnumMessageSize)EditorGUILayout.EnumPopup
        ("텍스트 창 타입",
        nowAction.messageSizeType);

        // 2. 텍스트 설정
        EditorGUILayout.LabelField("텍스트 내용");
        nowAction.messageText = EditorGUILayout.TextArea(nowAction.messageText);
    }

    void WaitInspector(TutorialAction nowAction)
    {
        nowAction.waitTime = EditorGUILayout.FloatField("대기시간", nowAction.waitTime);
    }


    // AI오브젝트를 정한다.
    void SetAITarget(string enumText , TutorialAction nowAction)
    {
        //1. 대상
        nowAction.tutorialAIType = (TutorialAction.EnumTutorialAI)EditorGUILayout.EnumPopup
        (enumText,
        nowAction.tutorialAIType);

        // 1-1 대상을 토대로 설정
        int tutorialAIType = (int)nowAction.tutorialAIType;

        // 1-2 대상의 오브젝트 선정
        if (tutorialAIType <= tutorialAI.AI.Length - 1)
        {
            nowAction.aIObject = tutorialAI.AI[tutorialAIType];
        }
    }


    void EmotionInspector(TutorialAction nowAction)
    {
        SetAITarget("이모티콘 AI", nowAction);

        //2. 어떤이모티콘
            nowAction.emoticonType = (TutorialAction.EnumEmoticon)EditorGUILayout.EnumPopup
        ("이모티콘 종류",
        nowAction.emoticonType);
    }

    void DrawImage(TutorialAction nowAction)
    {
        // 1. 어떤 이미지를 보여줄건지 선택하고.
        nowAction.imageObject = EditorGUILayout.ObjectField("이미지 선택",
            nowAction.imageObject, typeof(GameObject), false) as GameObject;


        // 2. 어디 좌표에 이미지를 보여줄건지 선택한다.
        nowAction.imageXPosition = EditorGUILayout.FloatField("X축 선택",
            nowAction.imageXPosition);
        nowAction.imageYPosition = EditorGUILayout.FloatField("Y축 선택",
            nowAction.imageYPosition);

        



    }

    void SetPracticeSkill(TutorialAction nowAction)
    {
        nowAction.PracticeSkillType = (TutorialAction.EnumPracticeSkill)EditorGUILayout.EnumPopup
            ("연습스킬 선택",
            nowAction.PracticeSkillType);
    }

    void SetResetSkill(TutorialAction nowAction)
    {
        nowAction.PracticeSkillType = (TutorialAction.EnumPracticeSkill)EditorGUILayout.EnumPopup
            ("리셋스킬 선택",
            nowAction.PracticeSkillType);
    }

    void SetAIState(TutorialAction nowAction)
    {
        SetAITarget("설정 대상", nowAction);

        nowAction.AnimationStateType = (TutorialAction.EnumAnimationState)EditorGUILayout.EnumPopup
            ("애니메이션 설정대상",
            nowAction.AnimationStateType);
    }

    void ShowImage(TutorialAction nowAction)
    {
        nowAction.ShowImageType = (TutorialAction.EnumShowImage)EditorGUILayout.EnumPopup
            ("UI이미지 대상",
            nowAction.ShowImageType);
    }

    void ReleaseImage(TutorialAction nowAction)
    {
        nowAction.ReleaseImageType = (TutorialAction.EnumShowImage)EditorGUILayout.EnumPopup
            ("UI이미지 대상",
            nowAction.ShowImageType);
    }

    void SetActiveAI(TutorialAction nowAction)
    {
        SetAITarget("활성화 변경 대상", nowAction);

        nowAction.SetActiveType = (TutorialAction.EnumSetActiveAI)EditorGUILayout.EnumPopup
            ("활성화 여부 설정",
            nowAction.SetActiveType);
    }

    void UseDeadCount(TutorialAction nowAction)
    {
        SetAITarget("대상, 쥐만 결정해주세요.", nowAction);
        nowAction.SetUseDeadCountType = (TutorialAction.EnumSetUseDeadCount)EditorGUILayout.EnumPopup
            ("활성화 여부 결정",
            nowAction.SetUseDeadCountType);
        
    }

    void UseHealthDown(TutorialAction nowAction)
    {
        SetAITarget("대상, 쥐만 결정해주세요.", nowAction);
        nowAction.AIUseHealthDownType = (TutorialAction.EnumAIUseHealthDown)EditorGUILayout.EnumPopup
            ("활성화 여부 결정",
            nowAction.AIUseHealthDownType);
    }

    void UseBlockOtherSkill(TutorialAction nowAction)
    {
        nowAction.SkillType = (TutorialAction.EnumSkill)EditorGUILayout.EnumPopup
            ("무슨스킬인지 선택",
            nowAction.SkillType);
    }

    void UseActiveOtherSkill(TutorialAction nowAction)
    {
        EditorGUILayout.LabelField("모든스킬 재활성화");
    }

    void UseFadeOut(TutorialAction nowAction)
    {
        nowAction.FadeOutTime = EditorGUILayout.FloatField("페이드아웃 시간", nowAction.FadeOutTime);
    }

    void UseGameEnd(TutorialAction nowAction)
    {
        EditorGUILayout.LabelField("게임 종료");
    }
}
