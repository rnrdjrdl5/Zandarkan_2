using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallDownDebuff : PlayerDefaultDebuff
{
    protected override void Start()
    {
        base.Start();
        CreateDebuffEffect(PoolingManager.EffctType.STUN_EFFECT, playerBodyPart.UpHeadPosition.transform);

    }

    protected override void ExitDebuff()
    {
        gameObject.GetComponent<Animator>().SetBool("isFallDown", false);

        
        if (DebuffEffect != null)
        {
            PoolingManager.GetInstance().PushObject(DebuffEffect);
        }
    }
}
