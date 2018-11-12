using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PhotonManager
{


    // UI 보여주고 게임 종료 조건 파악하는 트리거 사용
    [PunRPC]
    void RPCActionCheckCreatePlayer()
    {
        StopCoroutine(IEnumCoro);


        GameStartCountPanelScript countScript = UIManager.GetInstance().gameStartCountPanelScript;

        for (int i = 0; i < countScript.Count.Length; i++)
        {
            countScript.Count[i].SetActive(false);
        }

        //스타트 이미지.
        StartCoroutine(WaitStartImage());

        CurrentPlayer.GetComponent<PlayerState>().isCanActive = true;


        // 고양이 플레이어 변수 설정
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {

            string PlayerType = (string)PhotonNetwork.playerList[i].CustomProperties["PlayerType"];

            if (PlayerType == "Cat")
            {

                CatPhotonPlayer = PhotonNetwork.playerList[i];
                break;
            }

        }



            // 게임 종료 조건 시작
            condition = new Condition(CheckGameFinish);
            conditionLoop = new ConditionLoop(NoAction);
            rPCActionType = new RPCActionType(MasterResultCheck);
            IEnumCoro = CoroTrigger(condition, conditionLoop, rPCActionType, "RPCActionCheckGameFinish");
            StartCoroutine(IEnumCoro);


            StartCoroutine(Timer());


            

    }


    // 게임 끝났는지 파악,
    bool CheckGameFinish()
    {
        if (CheckMouseAllDead() || CheckEndTimer() || CheckAllBreak() || CheckCatDead())
            return true;

        else
            return false;
    }


   
    int MasterResultCheck()
    {
        int Type = -1;

        if (CheckAllBreak() || CheckCatDead())
            Type = 0;

        else if (CheckMouseAllDead())
            Type = 1;

        else if (CheckEndTimer())
            Type = 2;

        if (Type == -1)
        {
            Debug.LogWarning("에러발생");
            Type = 0;
        }

        return Type;
    }



    // 일정 게이지 이하일 때 
    bool CheckAllBreak()
    {
        if (CatPhotonPlayer == null)
        {
            Debug.LogWarning("에러");
            return false;
        }

        //float CatGradeScore = (float)CatPhotonPlayer.CustomProperties["CatScore"];
        float CatGradeScore = (float)PhotonNetwork.player.CustomProperties["StoreScore"];


        float CatGradePersent;

        if (CatGradeScore <= 0)
            CatGradePersent = 0;

        else
            CatGradePersent = CatGradeScore / PhotonManager.GetInstance().MaxCatScore * 100;


        if (CatGradePersent <= GameBreakCondition)
            return true;

        else
            return false;

    }

    // 모든 쥐가죽었는지 판단
    bool CheckMouseAllDead()
    {
        bool isFinish = true;
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {

            string PlayerType = (string)PhotonNetwork.playerList[i].CustomProperties["PlayerType"];

            if (PlayerType == "Mouse")
            {
                isFinish = false;
            }
        }

        return isFinish;
    }

    bool CheckEndTimer()
    {
        if (playTimerNumber <= 0)
            return true;
        else
            return false;
    }


    bool isLeftCatPlayer = false;

    bool CheckCatDead()
    {
        return isLeftCatPlayer;
    }



    IEnumerator WaitStartImage()
    {
        UIManager.GetInstance().gameStartCountPanelScript.Start.SetActive(true);

        string playerType = (string)PhotonNetwork.player.CustomProperties["PlayerType"];

        if (playerType == "Cat")
            SpringArmObject.GetInstance().GetSystemSoundManager().PlayEffectSound(SoundManager.EnumEffectSound.UI_STARTCOUNT_CAT);

        else if (playerType == "Mouse")
            SpringArmObject.GetInstance().GetSystemSoundManager().PlayEffectSound(SoundManager.EnumEffectSound.UI_STARTCOUNT_MOUSE);

        yield return new WaitForSeconds(StartImage_WaitTime);

        UIManager.GetInstance().gameStartCountPanelScript.Start.SetActive(false);
        UIManager.GetInstance().gameStartCountPanelScript.GameStartCountPanel.SetActive(false);

        yield break;
    }


    [PunRPC]
    void RPCTutoPlayingGame()
    {
        CurrentPlayer.GetComponent<PlayerState>().isCanActive = true;

        GameObject go = GameObject.Find("TutorialGuide");
        if (go == null) return;

        TutorialGuide tg = go.GetComponent<TutorialGuide>();
        if (tg == null) return;


        tg.playerObject = CurrentPlayer;
        SetTutorialGuide(tg);


        //FadeOutTutorialImage    
        StartCoroutine("FadeAndStart", tg);

    }



    IEnumerator FadeAndStart(TutorialGuide tg)
    {
        TutorialCanvasManager.GetInstance().FadeOutTutorialImage();

        yield return new WaitForSeconds(MenuUIFadeInFadeOut);

        tg.StartTutorial();

    }

    public void SetTutorialGuide(TutorialGuide tg)
    {
        // 플레이어를 정해준다.
        

        // 1. 스킬에 필요한 데이터와 플레이어의 데이터를 연결시킨다.

        string seletedChar = (string)PhotonNetwork.player.CustomProperties["PlayerType"];

        TutorialElement[] seletedCharTutoElem = null;
        int seletedCharCnt;

        if (seletedChar == "Mouse")
        {
            seletedCharTutoElem = tg.mouseTutorialElements;
            seletedCharCnt = tg.maxMouseTutorialCount;
            tg.SeletedCharType = TutorialGuide.EnumSeletedChar.MOUSE;
        }

        else if (seletedChar == "Cat")
        {
            seletedCharTutoElem = tg.catTutorialElements;
            seletedCharCnt = tg.maxCatTutorialCount;
            tg.SeletedCharType = TutorialGuide.EnumSeletedChar.CAT;
        }

        
        // 종류찾기.
        int tutorialElementCount = seletedCharTutoElem.Length;

        for (int i = 0; i < tutorialElementCount; i++)
        {
            TutorialElement tutorialElement = seletedCharTutoElem[i];

            int tutorialCdtCount = tutorialElement.tutorialConditions.Length;
            for (int tcc = 0; tcc < tutorialCdtCount; tcc++)
            {
                TutorialCondition tutorialCondition = tutorialElement.tutorialConditions[tcc];

                
                SetCondition(tutorialCondition, tcc);
                
            }

            int tutorialAcCondition = tutorialElement.tutorialActions.Length;
            for (int tac = 0; tac < tutorialAcCondition; tac++)
            {
                TutorialAction tutorialAction = tutorialElement.tutorialActions[tac];

                
                SetAction(tutorialAction);

            }
        }


    }

    public void SetCondition(TutorialCondition tutorialCondition, int tcc)
    {
        tutorialCondition.playerObject = CurrentPlayer;

        if (tutorialCondition.tutorialConditionType == TutorialCondition.EnumTutorialCondition.USEACTIVE)
        {
            if (tutorialCondition.activeType == TutorialCondition.EnumActive.SPEEDRUN)
            {
                NewSpeedRun newSpeedRun = CurrentPlayer.GetComponent<NewSpeedRun>();
                if (newSpeedRun != null)
                {
                    newSpeedRun.EventUseCtnSkill += tutorialCondition.IncreateTime;

                    newSpeedRun.EventExitCtnSkill += tutorialCondition.ResetMount;
                }
            }

            else if (tutorialCondition.activeType == TutorialCondition.EnumActive.RESCUE)
            {
                RescuePlayer rp = CurrentPlayer.GetComponent<RescuePlayer>();
                if (rp != null)
                    rp.SuccessRescueEvent += tutorialCondition.IncreateMount;

            }

            else if (tutorialCondition.activeType == TutorialCondition.EnumActive.TURN_OFF)
            {
                TurnOffLight tol = CurrentPlayer.GetComponent<TurnOffLight>();
                if (tol != null)
                    tol.UseTurnOffSkillEvent += tutorialCondition.IncreateMount;

            }

            else if (tutorialCondition.activeType == TutorialCondition.EnumActive.NINJA)
            {
                NinjaHide nh = CurrentPlayer.GetComponent<NinjaHide>();
                if (nh != null)
                    nh.NinjaHideEvent += tutorialCondition.IncreateMount;

            }

            else if (tutorialCondition.activeType == TutorialCondition.EnumActive.TRAP)
            {
                CatTrap ct = CurrentPlayer.GetComponent<CatTrap>();
                if (ct != null)
                {
                    ct.UseCatTrapEvent += tutorialCondition.IncreateMount;
                }
            }

        }

        if (tutorialCondition.tutorialConditionType == TutorialCondition.EnumTutorialCondition.USEINTERACTIVE)
        {
            NewInteractionSkill newInteractionSkill = CurrentPlayer.GetComponent<NewInteractionSkill>();
            tutorialCondition.intersMount = new int[tutorialCondition.MAX_INTERS];

            newInteractionSkill.EventInteractive += tutorialCondition.IncreaseInter;
            Debug.Log("사용횟수 체크");
        }

        if (tutorialCondition.tutorialConditionType == TutorialCondition.EnumTutorialCondition.HIT)
        {

            // 스킬 갯수만큼 초기화
            tutorialCondition.nowHitMount = new int[CollisionObject.OBJECT_MOUNT];

            // 히트판정에 있는 이벤트에 등록, 히트 물체 이름을 보낸다.
            tutorialCondition.aIObject.GetComponent<AIPlayerHit>().HitEvent +=
                tutorialCondition.IncreaseHitMount;
        }
        if (tutorialCondition.tutorialConditionType == TutorialCondition.EnumTutorialCondition.ROPEDEAD)
        {
            AIHealth ah = tutorialCondition.aIObject.GetComponent<AIHealth>();
            ah.AIRopeEvent += tutorialCondition.SetOnRopeDead;
        }

        if (tutorialCondition.tutorialConditionType == TutorialCondition.EnumTutorialCondition.PLAYERDEAD)
        {
            AIHealth ah = tutorialCondition.aIObject.GetComponent<AIHealth>();
            ah.PlayerDeadEvent += tutorialCondition.SetOnPlayerDead;
        }

    }

    public void SetAction(TutorialAction tutorialAction)
    {
        tutorialAction.playerObject = CurrentPlayer;
    }


}
