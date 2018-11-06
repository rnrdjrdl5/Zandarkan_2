using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIHealth : MonoBehaviour {

    public float health { get; set; }

    public float maxBindTime { get; set; }
    public float nowBindTime { get; set; }
    public bool isUseDownBindTime = false;

    public bool isUseDownHealth = false;
    private Transform upHeadTransform;



    // Use this for initialization
    void Awake () {
        health = 100;

        nowBindTime = 10;
        maxBindTime = 10;


        upHeadTransform  = gameObject.transform.Find("UpHeadPosition");
    }
	
	// Update is called once per frame
	void Update () {

        if (isBind)
        {
            HelpIcon();

            if(isUseDownBindTime)
            nowBindTime -= Time.deltaTime;

            CheckDead();
        }

        else
            DeleteHelpIcon();


    }


    public bool isBind = false;
    public void ApplyDamage(float _damage)
    {


        

        if (isBind) return;

        // 데미지 입음
        if (_damage > 0)
        {
            if (isUseDownHealth)
                health -= _damage;
        }
        
        else
            health -= _damage;






        // 체력 0이하면 밧줄로 변경
        if (health <= 0)
        {

            isBind = true;
            health = 0;
            BindRope();
        }

        else if (health >= 100)
            health = 100.0f;
    }

    public void BindRope()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetInteger("WeaponType", 2);

        StartCoroutine("CoroRopeDeadAnimation");

    }

    IEnumerator CoroRopeDeadAnimation()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetBool("isRopeDead", true);

        


        yield return new WaitForSeconds(2.0f);
        anim.SetBool("isRopeDead", false);

        yield return null;
    }

    void HelpIcon()
    {
        Vector3 RescueIconPosition =
           SpringArmObject.GetInstance().armCamera.GetComponent<Camera>().
           WorldToScreenPoint(upHeadTransform.position);

        GameObject RescueIconObject =
            TutorialCanvasManager.GetInstance().RescueSet1;

        TutorialCanvasManager.GetInstance().RescueIconPanel.SetActive(true);


        if (RescueIconPosition.z < 0)
            RescueIconObject.SetActive(false);
        else
            RescueIconObject.SetActive(true);

        RescueIconObject.transform.position = RescueIconPosition;



        if (nowBindTime > 0)
        {
            TutorialCanvasManager.GetInstance().RescueIcon.fillAmount =
                nowBindTime / maxBindTime;
        }
        else
        {
            TutorialCanvasManager.GetInstance().RescueIcon.fillAmount = 0.0f;
        }

    }

    void DeleteHelpIcon()
    {
        TutorialCanvasManager.GetInstance().RescueIconPanel.SetActive(false);

        TutorialCanvasManager.GetInstance().RescueSet1.SetActive(false);
    }


    void CheckDead()
    {
        if (nowBindTime <= 0)
        {
            StartCoroutine("PlayerDead");
        }
    }

    IEnumerator PlayerDead()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetBool("isRopeDead", true);


        yield return new WaitForSeconds(1.5f);

        DeadEffect();


        yield return new WaitForSeconds(1.5f);

        DeadAction();
        yield break;
    }


    public void DeadEffect()
    {

        GameObject go = PoolingManager.GetInstance().CreateEffect(PoolingManager.EffctType.DIE_EFFECT);
        GetComponent<SoundManager>().PlayEffectSound(SoundManager.EnumEffectSound.EFFECT_MOUSE_DEAD);

        go.transform.SetParent(transform);
        go.transform.localPosition = new Vector3(0.0f, 0.3f, 1.0f);
    }

    public void DeadAction()
    {
        TutorialCanvasManager.GetInstance().RescueIconPanel.SetActive(false);
        gameObject.SetActive(false);
    }

}
