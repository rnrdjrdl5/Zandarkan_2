﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Tutorial > TutorialElement > TutorialAction

[System.Serializable]
public class TutorialAction {

    // 기본 정보
    public GameObject tutorialCanvas;
    public GameObject playerObject;

    // 액션타입.
    public enum EnumTutorialAction { MESSAGE, WAIT, DEBUG, EMOTION, DRAW_IMAGE, PRACTICE_SKILL, RESET_PRACTICE 
            , SET_AI_STATE, SHOW_IMAGE , RELEASE_IMAGE , SET_ACTIVE_AI , SET_USE_DEAD_COUNT ,SET_AI_USE_HEALTH_DOWN ,
    BLOCK_OTHER_SKILL ,ACTIVE_OTHER_SKILL , FADE_OUT , GAME_END, SET_INTER_TYPE , RESET_INTER_TYPE}
    public EnumTutorialAction tutorialActionType;

    // 텍스트용
    public string messageText;
    public enum EnumMessageSize { SMALL, NORMAL};     // 주의점 , MessageImageScript와 다른 열거형  사용, 순서를 맞추자.
    public EnumMessageSize messageSizeType;

    // 메세지 박스
    public GameObject messageObject;
    public MessageImageScript messageImageScript;

    // 대기시간
    public float waitTime;


    // 타겟
    public enum EnumTutorialAI { TOMATO , CAT, TOMATO2};
    public EnumTutorialAI tutorialAIType;
    public GameObject aIObject;



    // 이모티콘 
    public enum EnumEmoticon { ANGRY, HAPPY, HI, MERONG, SAD };
    public EnumEmoticon emoticonType;


    // 이미지 생성
    public GameObject imageObject;
    public float imageXPosition;
    public float imageYPosition;

    // 스킬 연습
    public enum EnumPracticeSkill { MOUSE_SPREAD, NINJA_HIDE, FLYINGPAN, TRAP, TURNOFF }
    public EnumPracticeSkill PracticeSkillType;

    // 애니메이션 상태 설정 종류
    public enum EnumAnimationState { RESCUE }
    public EnumAnimationState AnimationStateType;


    public enum EnumShowImage { GRADEUI, MOUSE_QSKILL, MOUSE_SHIFTSKILL , MOUSE_ESKILL , CAT_RSKILL, CAT_QSKILL , CAT_ESKILL }
    public EnumShowImage ShowImageType;
    public EnumShowImage ReleaseImageType;


    public enum EnumNoHit { YES, NO };
    public EnumNoHit NoHitType;

    public enum EnumSetActiveAI { YES, NO };
    public EnumSetActiveAI SetActiveType;

    public enum EnumSetUseDeadCount { YES, NO };
    public EnumSetUseDeadCount SetUseDeadCountType;

    public enum EnumAIUseHealthDown { YES, NO };
    public EnumAIUseHealthDown AIUseHealthDownType;

    public enum EnumSkill { SPEEDRUN , MARBLE , HIDE, FRYING_PAN, TURNOFF, TRAP , ATTACK};
    public EnumSkill SkillType;

    public float FadeOutTime;


    public enum EnumInterPosition {TABLE_CHAIR , DRAWER };
    public EnumInterPosition InterPositionType;    

    public InteractiveState.EnumInteractiveObject[] tutorInterObj;
    public int maxTutoInterObj;

    public float UseAction()
    {
        float returnTime = 0f;

        switch (tutorialActionType)
        {
            case EnumTutorialAction.DEBUG:
                Debug.Log("DebugLog");
                break;

            case EnumTutorialAction.WAIT:
                returnTime = waitTime;
                break;

            case EnumTutorialAction.MESSAGE:
                messageObject.SetActive(true);
                messageImageScript.PrintMessage(messageText, (MessageImageScript.EnumMessageSize)messageSizeType);
                break;

            case EnumTutorialAction.EMOTION:
                UseEmotion();
                break;

            case EnumTutorialAction.PRACTICE_SKILL:
                PracticeSkill();
                break;

            case EnumTutorialAction.RESET_PRACTICE:
                ResetSkill();
                break;

            case EnumTutorialAction.SET_AI_STATE:
                SetAIState();
                break;

            case EnumTutorialAction.SHOW_IMAGE:
                ShowImage();
                break;

            case EnumTutorialAction.RELEASE_IMAGE:
                ReleaseImage();
                break;

            case EnumTutorialAction.SET_ACTIVE_AI:
                SetActiveAI();
                break;

            case EnumTutorialAction.SET_USE_DEAD_COUNT:
                SetUseDeadCount();
                break;
            case EnumTutorialAction.SET_AI_USE_HEALTH_DOWN:
                SetUseHealthDown();
                break;
            case EnumTutorialAction.BLOCK_OTHER_SKILL:
                SetBlockSkill();
                break;
            case EnumTutorialAction.ACTIVE_OTHER_SKILL:
                SetActiveOtherSkill();
                break;
            case EnumTutorialAction.FADE_OUT:
                UseFadeOut();
                break;
            case EnumTutorialAction.GAME_END:
                UseGameEnd();
                break;

            case EnumTutorialAction.SET_INTER_TYPE:
                UseSetInterType();
                break;

            case EnumTutorialAction.RESET_INTER_TYPE:
                UseResetInterType();
                break;


        }

        return returnTime;
    }



    void UseEmotion()
    {
        aIObject.GetComponent<AIEmotions>().UseEmotion((int)emoticonType);
    }
    void PracticeSkill()
    {
        switch (PracticeSkillType)
        {
            case EnumPracticeSkill.MOUSE_SPREAD:
                playerObject.GetComponent<MouseSpread>().coolDown.PracticeCoolDown();
                break;
            case EnumPracticeSkill.NINJA_HIDE:
                playerObject.GetComponent<NinjaHide>().coolDown.PracticeCoolDown();
                break;
            case EnumPracticeSkill.FLYINGPAN:
                playerObject.GetComponent<NewThrowFryingPan>().coolDown.PracticeCoolDown();
                break;
            case EnumPracticeSkill.TRAP:
                playerObject.GetComponent<CatTrap>().coolDown.PracticeCoolDown();
                break;
            case EnumPracticeSkill.TURNOFF:
                playerObject.GetComponent<TurnOffLight>().coolDown.PracticeCoolDown();
                break;


        }
    }

    void ResetSkill()
    {
        switch (PracticeSkillType)
        {
            case EnumPracticeSkill.MOUSE_SPREAD:
                playerObject.GetComponent<MouseSpread>().coolDown.ResetCoolDown();
                break;
            case EnumPracticeSkill.NINJA_HIDE:
                playerObject.GetComponent<NinjaHide>().coolDown.ResetCoolDown();
                break;
            case EnumPracticeSkill.FLYINGPAN:
                playerObject.GetComponent<NewThrowFryingPan>().coolDown.ResetCoolDown();
                break;
            case EnumPracticeSkill.TRAP:
                playerObject.GetComponent<CatTrap>().coolDown.ResetCoolDown();
                break;
            case EnumPracticeSkill.TURNOFF:
                playerObject.GetComponent<TurnOffLight>().coolDown.ResetCoolDown();
                break;
        }
    }

    void SetAIState()
    {

        Animator anim = aIObject.GetComponent<Animator>();
        switch (AnimationStateType)
        {
            case EnumAnimationState.RESCUE:
                aIObject.GetComponent<AIHealth>().ApplyDamage(100);
                break;
        }

        
    }

    void ShowImage()
    {

        SettingImage(true, ShowImageType);
    }

    void ReleaseImage()
    {

        SettingImage(false, ReleaseImageType);
    }

    void SettingImage(bool isActive , EnumShowImage imageType)
    {
        if (imageType == EnumShowImage.MOUSE_QSKILL)
        {
            TutorialCanvasManager.GetInstance().MarbleUI.SetActive(isActive);
        }

        if (imageType == EnumShowImage.MOUSE_SHIFTSKILL)
        {
            TutorialCanvasManager.GetInstance().SpeedUI.SetActive(isActive);
        }

        if (imageType == EnumShowImage.GRADEUI)
        {
            TutorialCanvasManager.GetInstance().GradeUI.SetActive(isActive);
        }

        if (imageType == EnumShowImage.MOUSE_ESKILL)
        {
            TutorialCanvasManager.GetInstance().NinjaUI.SetActive(isActive);
        }

        if (imageType == EnumShowImage.CAT_QSKILL)
        {
            TutorialCanvasManager.GetInstance().TrapUI.SetActive(isActive);
        }

        if (imageType == EnumShowImage.CAT_RSKILL)
        {
            TutorialCanvasManager.GetInstance().FrypanUI.SetActive(isActive);
        }

        if (imageType == EnumShowImage.CAT_ESKILL)
        {
            TutorialCanvasManager.GetInstance().TurnOffUI.SetActive(isActive);
        }
    }

    void SetActiveAI()
    {
        if (SetActiveType == EnumSetActiveAI.YES)
        {
            aIObject.SetActive(true);
        }

        else if (SetActiveType == EnumSetActiveAI.NO)
        {
            aIObject.SetActive(false);
        }
    }

    void SetUseDeadCount()
    {
        AIHealth ah = aIObject.GetComponent<AIHealth>();
        if (ah == null)
        {
            Debug.Log("에러, 확인바람");
        }

        if (SetUseDeadCountType == EnumSetUseDeadCount.YES)
            ah.isUseDownBindTime = true;

        else if (SetUseDeadCountType == EnumSetUseDeadCount.NO)
            ah.isUseDownBindTime = false;
    }

    void SetUseHealthDown()
    {
        AIHealth ah = aIObject.GetComponent<AIHealth>();
        if (ah == null)
        {
            Debug.Log("에러, 확인바람");
        }

        if (SetUseDeadCountType == EnumSetUseDeadCount.YES)
        {
            ah.isUseDownHealth = true;
        }

        else if (SetUseDeadCountType == EnumSetUseDeadCount.NO)
        {
            ah.isUseDownHealth = false;
        }
    }

    void SetBlockSkill()
    {
        

        NewSpeedRun ns = playerObject.GetComponent<NewSpeedRun>();
        if (ns != null) ns.isUseSkill = false;

        CatAttack ca = playerObject.GetComponent<CatAttack>();
        if (ca != null) ca.isUseSkill = false;

        CatTrap ct = playerObject.GetComponent<CatTrap>();
        if (ct != null) ct.isUseSkill = false;

        NewThrowFryingPan tfp = playerObject.GetComponent<NewThrowFryingPan>();
        if (tfp != null) tfp.isUseSkill = false;

        TurnOffLight tol = playerObject.GetComponent<TurnOffLight>();
        if (tol != null) tol.isUseSkill = false;


        MouseSpread ms = playerObject.GetComponent<MouseSpread>();
        if (ms != null) ms.isUseSkill = false;

        NinjaHide nh = playerObject.GetComponent<NinjaHide>();
        if (nh != null) nh.isUseSkill = false;




        switch (SkillType)
        {
            case EnumSkill.FRYING_PAN:
                if (tfp != null) tfp.isUseSkill = true;
                break;

            case EnumSkill.TRAP:
                if (ct != null) ct.isUseSkill = true;
                break;

            case EnumSkill.TURNOFF:
                if (tol != null) tol.isUseSkill = true;
                break;

            case EnumSkill.SPEEDRUN:
                if (ns != null) ns.isUseSkill = true;
                break;
            case EnumSkill.ATTACK:
                if (ca != null) ca.isUseSkill = true;
                break;

            case EnumSkill.MARBLE:
                if (ms != null) ms.isUseSkill = true;
                break;

            case EnumSkill.HIDE:
                if (nh != null) nh.isUseSkill = true;
                break;


        }
    }

    void SetActiveOtherSkill()
    {
        NewSpeedRun ns = playerObject.GetComponent<NewSpeedRun>();
        if (ns != null) ns.isUseSkill = true;

        CatAttack ca = playerObject.GetComponent<CatAttack>();
        if (ca != null) ca.isUseSkill = true;

        CatTrap ct = playerObject.GetComponent<CatTrap>();
        if (ct != null) ct.isUseSkill = true;

        NewThrowFryingPan tfp = playerObject.GetComponent<NewThrowFryingPan>();
        if (tfp != null) tfp.isUseSkill = true;

        TurnOffLight tol = playerObject.GetComponent<TurnOffLight>();
        if (tol != null) tol.isUseSkill = true;

        MouseSpread ms = playerObject.GetComponent<MouseSpread>();
        if (ms != null) ms.isUseSkill = true;

        NinjaHide nh = playerObject.GetComponent<NinjaHide>();
        if (nh != null) nh.isUseSkill = true;
    }

    void UseFadeOut()
    {
        UIManager.GetInstance().fadeImageScript.FadeEffect(FadeOutTime);
    }

    void UseGameEnd()
    {
        PhotonNetwork.LeaveRoom();
    }

    void UseSetInterType()
    {
        NewInteractionSkill newInterSkill = playerObject.GetComponent<NewInteractionSkill>();

        if (newInterSkill != null)
        {

            newInterSkill.tutorialInterObj = tutorInterObj;
            newInterSkill.isUseChoiseInter = true;
        }
    }

    void UseResetInterType()
    {
        NewInteractionSkill newInterSkill = playerObject.GetComponent<NewInteractionSkill>();

        if (newInterSkill != null)
        {
            newInterSkill.tutorialInterObj = null;
            newInterSkill.isUseChoiseInter = false;
        }
    }

}
