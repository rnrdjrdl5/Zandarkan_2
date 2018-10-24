using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ****************주의점*********************

// 인스펙터의 사운드 순서와
// 열거형 순서와 일치해야합니다.

/*****************************************/
public class SoundManager : MonoBehaviour {

    /**** Public ****/

    public AudioClip[] EffectAudios;

    public AudioClip[] BGAudios;

    public enum EnumEffectSound
    {
        UI_BUTTONCLICK_1, UI_BUTTONCLICK_EXIT_1, UI_LOBBYENTER_1, UI_LOBBYENTER_2, UI_LOBBYLEFT_1, UI_TURNBOOK_1,

        UI_ROULETTE_1, UI_ROULETTE_2, UI_ROULETTE_3,

        EFFECT_CHAR_JUMP_1, EFFECT_CHAR_JUMP_2, EFFECT_CHAR_JUMP_3,

        EFFECT_CAT_ATTACK_1, EFFECT_CAT_ATTACK_2,
        EFFECT_CAT_ATTACK_HIT_1, EFFECT_CAT_ATTACK_HIT_2,

        EFFECT_CAT_THROWPAN_1, EFFECT_CAT_THROWPAN_2,

        EFFECT_CAT_TRAP_1,

        EFFECT_CAT_HITTRAP_1, EFFECT_CAT_HITTRAP_2, EFFECT_CAT_HITTRAP_3,

        EFFECT_CAT_BUTTONCLICK1, EFFECT_CAT_BUTTONBEEP, EFFECT_CAT_TURNOFF,

        UI_LAST_30_SECOND,
        
        UI_TIMEOVER_1,

        UI_MOUSE_WIN , UI_CAT_WIN,

        EFFECT_CAT_SPREADMARBLE, 

        EFFECT_MOUSE_REVIVE,

        EFFECT_MOUSE_DEAD,

        EFFECT_MOUSE_HIDE,

        EFFECT_MOUSE_CHEESE,

        EFFECT_CHAR_MOVE_1,
        EFFECT_CHAR_MOVE_2,
        EFFECT_CHAR_MOVE_3,

        UI_MOUSE_OVER,

        UI_KEYBOARD_HIT,

        UI_PRECOUNTDOWN_1, UI_PRECOUNTDOWN_2, UI_PRECOUNTDOWN_3,

        UI_STARTCOUNT_CAT , UI_STARTCOUNT_MOUSE,

        EFFECT_MOUSE_PRECHAIR , EFFECT_MOUSE_PRETABLE,

        EFFECT_VOICEMOUSE_TABLECHAIR_HIT_1, EFFECT_VOICEMOUSE_TABLECHAIR_HIT_2,

        EFFECT_FALLDOWN,

        EFFECT_MOUSE_TABLECHAIR_ACTION1, EFFECT_MOUSE_TABLECHAIR_ACTION2, EFFECT_MOUSE_TABLECHAIR_ACTION3,

        EFFECT_DRAWERPIANO_BOOM1, EFFECT_DRAWERPIANO_BOOM2, EFFECT_DRAWERPIANO_BOOM3,

        EFFECT_POSDRAWER_BRING1, EFFECT_POSDRAWER_BRING2, EFFECT_POSDRAWER_BRING3,

        EFFECT_CANDLE_BREAK1 , 
        EFFECT_POSMEKA_OPEN1



    }

    // 이 열거형은 PoolingManager의 EffectType과 동일하게 해야합니다.
    public enum EnumRandomEffectSound
    {
        EFFECT_ATTACK_1 = 0, EFFECT_CAT_HITTRAP = 21, CHEESE_HEALING_EFFECT = 22, EFFECT_THROWPAN_1 = 26, NONE = 29
    }

    public enum EnumBGSound
    {
        BG_LOBBY_SOUND, BG_INGAME_SOUND, BG_FAST_INGAME_SOUND
    }


    /**** Private ****/

    private AudioSource sourceComponent;

    private Dictionary<string, AudioClip> effectDic;
    private Dictionary<string, AudioClip> bgDic;

    private void Awake()
    {

            sourceComponent = GetComponent<AudioSource>();

            sourceComponent.loop = true;

        effectDic = new Dictionary<string, AudioClip>();
        bgDic = new Dictionary<string, AudioClip>();

        for (int i = 0; i < EffectAudios.Length; i++)
        {
            effectDic.Add(EffectAudios[i].name, EffectAudios[i]);
        }

        for (int i = 0; i < BGAudios.Length; i++)
        {
            bgDic.Add(BGAudios[i].name, BGAudios[i]);
        }
    }



    /**** Effect Sound ****/

    public void PlayEffectSound(EnumEffectSound enumEffectSound)
    {
        AudioClip audioClip = FindSoundAtDic(enumEffectSound);
        

        sourceComponent.PlayOneShot(audioClip);

    }

    public AudioClip FindSoundAtDic(EnumEffectSound enumEffectSound)
    {
        AudioClip audioClip = null;

        switch (enumEffectSound)
        {

            case EnumEffectSound.UI_BUTTONCLICK_1:
                audioClip = effectDic["Sound_UI_ButtonClick1"];
                break;
            case EnumEffectSound.UI_BUTTONCLICK_EXIT_1:
                audioClip = effectDic["Sound_UI_ButtonClick2"];
                break;


            case EnumEffectSound.UI_LOBBYENTER_1:
                audioClip = effectDic["Sound_Cat_Meow_1"];
                break;
            case EnumEffectSound.UI_LOBBYENTER_2:
                audioClip = effectDic["Sound_Cat_Meow_2"];
                break;
            case EnumEffectSound.UI_LOBBYLEFT_1:
                audioClip = effectDic["Sound_Cat_Meow_3"] ;
                break;


            case EnumEffectSound.UI_TURNBOOK_1:
                audioClip = effectDic["Effect_Book_Page_1"] ;
                break;


            case EnumEffectSound.UI_ROULETTE_1:
                audioClip = effectDic["Sound_Roulette_01"] ;
                break;
            case EnumEffectSound.UI_ROULETTE_2:
                audioClip = effectDic["Sound_Roulette_02"] ;
                break;
            case EnumEffectSound.UI_ROULETTE_3:
                audioClip = effectDic["Sound_Roulette_03"] ;
                break;

            case EnumEffectSound.EFFECT_CHAR_JUMP_1:
                audioClip = effectDic["Effect_Char_Jump_1"] ;
                break;
            case EnumEffectSound.EFFECT_CHAR_JUMP_2:
                audioClip = effectDic["Effect_Char_Jump_2"] ;
                break;
            case EnumEffectSound.EFFECT_CHAR_JUMP_3:
                audioClip = effectDic["Effect_Char_Jump_3"] ;
                break;

            case EnumEffectSound.EFFECT_CAT_ATTACK_1:
                audioClip = effectDic["Sound_Cat_Attack_1"] ;
                break;
            case EnumEffectSound.EFFECT_CAT_ATTACK_2:
                audioClip = effectDic["Sound_Cat_Attack_2"] ;
                break;
            case EnumEffectSound.EFFECT_CAT_ATTACK_HIT_1:
                audioClip = effectDic["Sound_Cat_Attack_Hit_1"] ;
                break;
            case EnumEffectSound.EFFECT_CAT_ATTACK_HIT_2:
                audioClip = effectDic["Sound_Cat_Attack_Hit_2"] ;
                break;

            case EnumEffectSound.EFFECT_CAT_THROWPAN_1:
                audioClip = effectDic["Effect_Cat_ThrowPan_1"] ;
                break;
            case EnumEffectSound.EFFECT_CAT_THROWPAN_2:
                audioClip = effectDic["Effect_Cat_ThrowPan_2"] ;
                break;

            case EnumEffectSound.EFFECT_CAT_TRAP_1:
                audioClip = effectDic["Effect_Cat_Trap_1"] ;
                break;

            case EnumEffectSound.EFFECT_CAT_HITTRAP_1:
                audioClip = effectDic["Effect_Hit_Trap_1"] ;
                break;
            case EnumEffectSound.EFFECT_CAT_HITTRAP_2:
                audioClip = effectDic["Effect_Hit_Trap_2"] ;
                break;
            case EnumEffectSound.EFFECT_CAT_HITTRAP_3:
                audioClip = effectDic["Effect_Hit_Trap_3"] ;
                break;

            case EnumEffectSound.EFFECT_CAT_BUTTONCLICK1:
                audioClip = effectDic["Effect_Cat_ButtonClick_1"] ;
                break;
            case EnumEffectSound.EFFECT_CAT_BUTTONBEEP:
                audioClip = effectDic["Effect_Cat_ButtonBeep"] ;
                break;
            case EnumEffectSound.EFFECT_CAT_TURNOFF:
                audioClip = effectDic["Effect_Cat_TurnOff"] ;
                break;

            case EnumEffectSound.UI_LAST_30_SECOND:
                audioClip = effectDic["UI_Last_30Second"] ;
                break;

            case EnumEffectSound.UI_TIMEOVER_1:
                audioClip = effectDic["UI_TimeOver_1"] ;
                break;

            case EnumEffectSound.UI_MOUSE_WIN:
                audioClip = effectDic["UI_Mouse_Win"] ;
                break;
            case EnumEffectSound.UI_CAT_WIN:
                audioClip = effectDic["UI_Cat_Win"] ;
                break;

            case EnumEffectSound.EFFECT_CAT_SPREADMARBLE:
                audioClip = effectDic["Sound_Mouse_SpreadMarble"] ;
                break;

            case EnumEffectSound.EFFECT_MOUSE_REVIVE:
                audioClip = effectDic["Effect_Mouse_Revive"] ;
                break;

            case EnumEffectSound.EFFECT_MOUSE_DEAD:
                audioClip = effectDic["Effect_Mouse_Dead"] ;
                break;

            case EnumEffectSound.EFFECT_MOUSE_HIDE:
                audioClip = effectDic["Effect_Mouse_Hide"] ;
                break;

            case EnumEffectSound.EFFECT_MOUSE_CHEESE:
                audioClip = effectDic["Effect_Mouse_Cheese"] ;
                break;

            case EnumEffectSound.EFFECT_CHAR_MOVE_1:
                audioClip = effectDic["Effect_Char_Move_1"] ;
                break;
            case EnumEffectSound.EFFECT_CHAR_MOVE_2:
                audioClip = effectDic["Effect_Char_Move_2"] ;
                break;
            case EnumEffectSound.EFFECT_CHAR_MOVE_3:
                audioClip = effectDic["Effect_Char_Move_3"] ;
                break;

            case EnumEffectSound.UI_MOUSE_OVER:
                audioClip = effectDic["UI_Mouse_Over"] ;
                break;

            case EnumEffectSound.UI_KEYBOARD_HIT:
                audioClip = effectDic["UI_Keyboard_Hit"] ;
                break;

            case EnumEffectSound.UI_PRECOUNTDOWN_1:
                audioClip = effectDic["UI_PreCountDown_1"] ;
                break;

            case EnumEffectSound.UI_PRECOUNTDOWN_2:
                audioClip = effectDic["UI_PreCountDown_2"] ;
                break;

            case EnumEffectSound.UI_PRECOUNTDOWN_3:
                audioClip = effectDic["UI_PreCountDown_3"] ;
                break;

            case EnumEffectSound.UI_STARTCOUNT_MOUSE:
                audioClip = effectDic["UI_StartCount_Mouse"] ;
                break;

            case EnumEffectSound.UI_STARTCOUNT_CAT:
                audioClip = effectDic["UI_StartCount_Cat"] ;
                break;

            case EnumEffectSound.EFFECT_MOUSE_PRECHAIR:
                audioClip = effectDic["Effect_Mouse_PreChair"] ;
                break;

            case EnumEffectSound.EFFECT_MOUSE_PRETABLE:
                audioClip = effectDic["Effect_Mouse_PreTable"] ;
                break;

            case EnumEffectSound.EFFECT_VOICEMOUSE_TABLECHAIR_HIT_1:
                audioClip = effectDic["Effect_VoiceMouse_TableChair_Hit_1"] ;
                break;

            case EnumEffectSound.EFFECT_VOICEMOUSE_TABLECHAIR_HIT_2:
                audioClip = effectDic["Effect_VoiceMouse_TableChair_Hit_2"] ;
                break;

            case EnumEffectSound.EFFECT_FALLDOWN:
                audioClip = effectDic["Effect_FallDown"] ;
                break;

            case EnumEffectSound.EFFECT_MOUSE_TABLECHAIR_ACTION1:
                audioClip = effectDic["Effect_Mouse_TableChair_Action1"] ;
                break;
            case EnumEffectSound.EFFECT_MOUSE_TABLECHAIR_ACTION2:
                audioClip = effectDic["Effect_Mouse_TableChair_Action2"] ;
                break;
            case EnumEffectSound.EFFECT_MOUSE_TABLECHAIR_ACTION3:
                audioClip = effectDic["Effect_Mouse_TableChair_Action3"] ;
                break;

            case EnumEffectSound.EFFECT_POSDRAWER_BRING1:
                audioClip = effectDic["Effect_PosDrawer_Bring1"] ;
                break;
            case EnumEffectSound.EFFECT_POSDRAWER_BRING2:
                audioClip = effectDic["Effect_PosDrawer_Bring2"] ;
                break;
            case EnumEffectSound.EFFECT_POSDRAWER_BRING3:
                audioClip = effectDic["Effect_PosDrawer_Bring3"] ;
                break;

            case EnumEffectSound.EFFECT_DRAWERPIANO_BOOM1:
                audioClip = effectDic["Effect_DrawerPiano_Boom1"] ;
                break;
            case EnumEffectSound.EFFECT_DRAWERPIANO_BOOM2:
                audioClip = effectDic["Effect_DrawerPiano_Boom2"] ;
                break;
            case EnumEffectSound.EFFECT_DRAWERPIANO_BOOM3:
                audioClip = effectDic["Effect_DrawerPiano_Boom3"] ;
                break;
            case EnumEffectSound.EFFECT_POSMEKA_OPEN1:
                audioClip = effectDic["Effect_PosMeka_Open1"];
                break;
            case EnumEffectSound.EFFECT_CANDLE_BREAK1:
                audioClip = effectDic["Effect_Candle_Break1"];
                break;
        }
        return audioClip;

    }

    public AudioClip FindRandomSoundAtDic(EnumEffectSound startEffect, EnumEffectSound endEffect)
    {

        int random = Random.Range((int)startEffect, (int)endEffect + 1);

        return FindSoundAtDic((EnumEffectSound)random);
    }



    public void PlayRandomEffectSound(EnumEffectSound startEffect, EnumEffectSound endEffect)
    {

        int random = Random.Range((int)startEffect, (int)endEffect + 1);

        PlayEffectSound((EnumEffectSound)random);
    }

    // 이 열거형은 PoolingManager의 EffectType과 동일하게 해야합니다.
    public void PlayRandomEffectSound(EnumRandomEffectSound enumRandomEffectSound)
    {
        switch (enumRandomEffectSound)
        {
            case EnumRandomEffectSound.EFFECT_ATTACK_1:
                PlayRandomEffectSound(EnumEffectSound.EFFECT_CAT_ATTACK_HIT_1, EnumEffectSound.EFFECT_CAT_ATTACK_HIT_2);
                break;

            case EnumRandomEffectSound.EFFECT_THROWPAN_1:
                PlayRandomEffectSound(EnumEffectSound.EFFECT_CAT_ATTACK_HIT_1, EnumEffectSound.EFFECT_CAT_ATTACK_HIT_2);
                break;

            case EnumRandomEffectSound.EFFECT_CAT_HITTRAP:
                PlayRandomEffectSound(EnumEffectSound.EFFECT_CAT_HITTRAP_1, EnumEffectSound.EFFECT_CAT_HITTRAP_3);
                break;

            case EnumRandomEffectSound.CHEESE_HEALING_EFFECT:
                PlayRandomEffectSound(EnumEffectSound.EFFECT_MOUSE_CHEESE, EnumEffectSound.EFFECT_MOUSE_CHEESE);
                break;

            case EnumRandomEffectSound.NONE:
                break;

        }

    }




    /**** BackGround Sound ****/

    public void PlayBGSound(EnumBGSound enumBGSound)
    {
        switch (enumBGSound)
        {

            case EnumBGSound.BG_LOBBY_SOUND:
                sourceComponent.clip = bgDic["Sound_Lobby_BackGround_1"];
                break;

            case EnumBGSound.BG_INGAME_SOUND:
                sourceComponent.clip = bgDic["BG_InGame_Normal_1"];
                break;
            case EnumBGSound.BG_FAST_INGAME_SOUND:
                sourceComponent.clip = bgDic["BG_InGame_Fast_1"];
                break;

        }
        sourceComponent.Play();

    }

    public void StopBGSound()
    {

        //sourceComponent.Stop();
        sourceComponent.clip = null;
    }

    public void FadeOutSound()
    {

        StartCoroutine("IEnumFadeOutSound");
    }

    IEnumerator IEnumFadeOutSound()
    {
        while (true)
        {
            sourceComponent.volume -= 0.05f;
            if (sourceComponent.volume <= 0)
            {
                sourceComponent.volume = 1.0f;
                sourceComponent.Stop();
                StopCoroutine("IEnumFadeOutSound");
                yield break;
            }

            else
            {
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
