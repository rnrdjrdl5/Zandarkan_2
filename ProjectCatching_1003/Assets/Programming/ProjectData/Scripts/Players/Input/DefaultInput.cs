﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DefaultInput{

    // 키의 상태를 파악하기 위한 변수입니다.
    public enum EnumSkillKey
    {
        LEFTMOUSE, RIGHTMOUSE, LEFTSHIFT, RIGHTSHIFT, SPACE, Q, E, F ,
         PUSINGLEFTSHIFT , NOTPUSHINGLEFTSHIFT
            , F1,F2,F3,F4,F5,F6 , TWO, THREE , FOUR , FIVE, SIX
    }

    public EnumSkillKey SkillKeyType;


    // 키 코드가 현재 변수값이랑 같은지 판단.
    protected bool EqualSkillKeyType()
    {
        bool ReturnType = false;
        switch (SkillKeyType)
        {

            case EnumSkillKey.LEFTMOUSE:
                ReturnType = Input.GetMouseButtonDown(0);
                break;

            case EnumSkillKey.RIGHTMOUSE:
                ReturnType = Input.GetMouseButtonDown(1);
                break;

            case EnumSkillKey.LEFTSHIFT:
                ReturnType = Input.GetKeyDown(KeyCode.LeftShift);
                break;

            case EnumSkillKey.SPACE:
                ReturnType = Input.GetKeyDown(KeyCode.Space);
                break;
            case EnumSkillKey.Q:
                ReturnType = Input.GetKeyDown(KeyCode.Q);
                break;
            case EnumSkillKey.E:
                ReturnType = Input.GetKeyDown(KeyCode.E);
                break;
            case EnumSkillKey.PUSINGLEFTSHIFT:
                ReturnType = Input.GetKey(KeyCode.LeftShift);
                break;
            case EnumSkillKey.NOTPUSHINGLEFTSHIFT:
                ReturnType = !Input.GetKey(KeyCode.LeftShift);
                break;
            case EnumSkillKey.F1:
                ReturnType = Input.GetKey(KeyCode.F1);
                break;
            case EnumSkillKey.TWO:
                ReturnType = Input.GetKey(KeyCode.Alpha2);
                break;
            case EnumSkillKey.THREE:
                ReturnType = Input.GetKey(KeyCode.Alpha3);
                break;
            case EnumSkillKey.FOUR:
                ReturnType = Input.GetKey(KeyCode.Alpha4);
                break;
            case EnumSkillKey.FIVE:
                ReturnType = Input.GetKey(KeyCode.Alpha5);
                break;
            case EnumSkillKey.SIX:
                ReturnType = Input.GetKey(KeyCode.Alpha6);
                break;

            case EnumSkillKey.F:
                ReturnType = Input.GetKeyDown(KeyCode.F);
                break;
        }
        return ReturnType;
    }

    //키를 눌렀는지 판단합니다.
    virtual public bool IsUseKey()
    {
        return EqualSkillKeyType();
    }
}
