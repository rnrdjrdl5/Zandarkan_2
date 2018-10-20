using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDefaultDebuff : MonoBehaviour {



    protected float MaxDebuffTime;



    public void SetMaxDebuffTime(float MDT) { MaxDebuffTime = MDT; }
    public float GetMaxDebuffTime() { return MaxDebuffTime; }


    protected float NowDebuffTime;

    public void SetNowDebuffTime(float NDT) { NowDebuffTime = NDT; }
    public float GetNowDebuffTime() { return NowDebuffTime; }

    protected GameObject DebuffEffect;


    protected Animator animator;

    // Use this for initialization

    protected PlayerBodyPart playerBodyPart;

    virtual protected void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.Log(" 애니메이터가 없습니다1");

        playerBodyPart = GetComponent<PlayerBodyPart>();
        if (playerBodyPart == null)
            Debug.LogWarning("Player Body 스크립트가 없음.");
    }
    virtual protected void Start () {
        NowDebuffTime = 0.0f;
        
    }
	
	// Update is called once per frame
	virtual protected void Update () {
        if (MaxDebuffTime <= NowDebuffTime)
        {
            ExitDebuff();
            Destroy(this);
        }
        else
            NowDebuffTime += Time.deltaTime;
	}

    protected virtual void ExitDebuff()
    {

    }

    // 이펙트 1개만 대응함.
    protected virtual void CreateDebuffEffect(PoolingManager.EffctType effctType , Transform debuffTransform)
    {
        DebuffEffect = PoolingManager.GetInstance().CreateEffect(effctType);

        DebuffEffect.transform.position =
           debuffTransform.position - transform.up * 0.5f;

        DebuffEffect.transform.SetParent(debuffTransform);
    }

    

    protected virtual void PopDebuffEffect()
    {
        if (DebuffEffect != null)
        {
            PoolingManager.GetInstance().PushObject(DebuffEffect);
        }
    }
}
