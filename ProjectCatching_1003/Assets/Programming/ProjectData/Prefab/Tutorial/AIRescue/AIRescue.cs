using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRescue : MonoBehaviour {

    Animator anim;
    SoundManager soundManager;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        soundManager = GetComponent<SoundManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SuccessRescue()
    {
        AIHealth aiHealth = GetComponent<AIHealth>();

        if (aiHealth != null)
        {
            aiHealth.isBind = false;
            aiHealth.ApplyDamage(-10);

            anim.SetBool("isRevive", true);
            anim.SetInteger("WeaponType", 0);
        }
    }

    public void OtherCancelEvent()
    {

    }

    public void OffRevive()
    {
        anim.SetBool("isRevive", false);
    }
    public void ReviveEffect()
    {
        GameObject go = PoolingManager.GetInstance().CreateEffect(PoolingManager.EffctType.REVIVE_EFFECT);
        soundManager.PlayEffectSound(SoundManager.EnumEffectSound.EFFECT_MOUSE_REVIVE);
        go.transform.position = transform.position;
    }

}
