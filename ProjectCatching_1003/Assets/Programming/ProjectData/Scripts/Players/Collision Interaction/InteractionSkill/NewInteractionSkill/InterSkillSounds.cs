using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class NewInteractionSkill {


    // CallAction 사용 시 
    private void ActionSound()
    {
        switch (interactiveState.interactiveObjectType)
        {
            case InteractiveState.EnumInteractiveObject.TABLE:
                soundManager.PlayRandomEffectSound(
                    SoundManager.EnumEffectSound.EFFECT_VOICEMOUSE_TABLECHAIR_HIT_1,
                    SoundManager.EnumEffectSound.EFFECT_VOICEMOUSE_TABLECHAIR_HIT_2);

                soundManager.PlayRandomEffectSound(
                    SoundManager.EnumEffectSound.EFFECT_MOUSE_TABLECHAIR_ACTION1,
                    SoundManager.EnumEffectSound.EFFECT_MOUSE_TABLECHAIR_ACTION3);
                break;

            case InteractiveState.EnumInteractiveObject.CHAIR:
                soundManager.PlayRandomEffectSound(
                    SoundManager.EnumEffectSound.EFFECT_VOICEMOUSE_TABLECHAIR_HIT_1,
                    SoundManager.EnumEffectSound.EFFECT_VOICEMOUSE_TABLECHAIR_HIT_2);

                soundManager.PlayRandomEffectSound(
                    SoundManager.EnumEffectSound.EFFECT_MOUSE_TABLECHAIR_ACTION1,
                    SoundManager.EnumEffectSound.EFFECT_MOUSE_TABLECHAIR_ACTION3);
                break;

        }
    }

}
