using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TutorialElement{

    // 조건
    public TutorialCondition[] tutorialConditions;
    public int maxTutorialCondition;

    // 액션
    public TutorialAction[] tutorialActions;
    public int maxTutorialAction;

 

    // 튜토리얼 시작
    public bool CheckCondition()
    {
        int nowCount = tutorialConditions.Length;


        for (int i = 0; i < nowCount; i++)
        {
            tutorialConditions[i].isUsedCondition = true;
        }

        for (int i = 0; i < nowCount; i++)
        {
            if (tutorialConditions[i].CheckCondition() == false) return false;
        }


        // 조건 만족 시 현재 사용중인 Conditions들 모두 사용금지 설정
        for (int i = 0; i < nowCount; i++)
        {
            Debug.Log("Asdf");
            tutorialConditions[i].ResetCondition();

            tutorialConditions[i].isUsedCondition = false;
        }

        return true;
    }



    
}
