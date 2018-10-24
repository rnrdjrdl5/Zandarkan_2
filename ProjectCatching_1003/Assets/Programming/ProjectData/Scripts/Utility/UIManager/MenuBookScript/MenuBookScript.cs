using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBookScript : MonoBehaviour
{

    public SoundManager soundManager;

    public void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    public void CallPageSound()
    {

        soundManager.PlayEffectSound(SoundManager.EnumEffectSound.UI_TURNBOOK_1);
    }
}
