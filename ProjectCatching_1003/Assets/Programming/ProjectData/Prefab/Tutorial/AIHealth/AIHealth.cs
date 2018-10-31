using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour {

    public float health { get; set; }
    private Transform upHeadTransform;



    // Use this for initialization
    void Awake () {
        health = 100;

        upHeadTransform  = gameObject.transform.Find("UpHeadPosition");
    }
	
	// Update is called once per frame
	void Update () {

        if (isBind)
            HelpIcon();
        else
            DeleteHelpIcon();


    }


    public bool isBind = false;
    public void ApplyDamage(float _damage)
    {


        

        if (isBind) return;

        // 데미지 입음
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

    }

    void DeleteHelpIcon()
    {
        TutorialCanvasManager.GetInstance().RescueIconPanel.SetActive(false);

        TutorialCanvasManager.GetInstance().RescueSet1.SetActive(false);
    }

}
