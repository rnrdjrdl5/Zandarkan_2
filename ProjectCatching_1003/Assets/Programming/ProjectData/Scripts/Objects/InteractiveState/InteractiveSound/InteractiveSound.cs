using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveSound : MonoBehaviour {

    //  상호작용마다 사운드 출력하기 위해 사용

    SoundManager soundManager;
    AudioSource audioSource;
    private void Start()
    {
        soundManager = SpringArmObject.GetInstance().GetSystemSoundManager();
        audioSource = GetComponent<AudioSource>();
    }

    private void BringPosDrawerSound()
    {

        audioSource.PlayOneShot(
        soundManager.FindRandomSoundAtDic(
            SoundManager.EnumEffectSound.EFFECT_POSDRAWER_BRING1,
            SoundManager.EnumEffectSound.EFFECT_POSDRAWER_BRING3));
    }

    private void BoomPosDrawerSound()
    {

        audioSource.PlayOneShot(
        soundManager.FindRandomSoundAtDic(
            SoundManager.EnumEffectSound.EFFECT_DRAWERPIANO_BOOM1,
            SoundManager.EnumEffectSound.EFFECT_DRAWERPIANO_BOOM3));
    }

    private void BreakCandleSound()
    {
        audioSource.PlayOneShot(
        soundManager.FindSoundAtDic(SoundManager.EnumEffectSound.EFFECT_CANDLE_BREAK1));
    }

    public void OpenPosMeka()
    {
        audioSource.PlayOneShot(
        soundManager.FindSoundAtDic(SoundManager.EnumEffectSound.EFFECT_POSMEKA_OPEN1));
    }
}
