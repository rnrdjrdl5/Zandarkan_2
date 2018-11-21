using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class TurnOffLight : DefaultNewSkill
{
    // 불 끌때 각종 옵션입니다.
    public TurnOffLightState turnOffLightState;

    PostProcessingBehaviour nowPPB;

    PostProcessingProfile tempPPP;
    public PostProcessingProfile changePPP;

    private float nowTurnOffTime;

    

    protected override void Awake()
    {
        base.Awake();

        defaultCdtAct = new NormalCdtAct();
        defaultCdtAct.InitCondition(this);

        
    }

    private void Start()
    {
        PostProcessingBehaviour ppp = SpringArmObject.GetInstance().armCamera.GetComponent<PostProcessingBehaviour>();
        if (ppp != null)
        {
            nowPPB = ppp;
            tempPPP = ppp.profile;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (nowTurnOffTime >= 0)
        {
            nowTurnOffTime -= Time.deltaTime;

            if (nowTurnOffTime <= 0)
            {
                SetPostProcessing(false);
            }
        }
    }

    public override bool CheckState()
    {
        if (!isUseSkill) return false;

        //이동중이거나 가만히 있을 때 가능합니다.
        if ((
            playerState.EqualPlayerCondition(PlayerState.ConditionEnum.IDLE) ||
            playerState.EqualPlayerCondition(PlayerState.ConditionEnum.RUN)) &&
            playerState.isCanActive == true)

        {
            return true;
        }
        else
            return false;
    }

    public delegate void DeleUseTurnOffSkill();
    public event DeleUseTurnOffSkill UseTurnOffSkillEvent;
    public override void UseSkill()
    {
        if(UseTurnOffSkillEvent != null) UseTurnOffSkillEvent();

        // 쥐를 제외한 모든 플레이어에게 사용
        for (int i = 0; i < PhotonManager.GetInstance().AllPlayers.Count; i++)
        {
            if(!PhotonManager.GetInstance().AllPlayers[i].name.Contains("Cat"))
                PhotonManager.GetInstance().AllPlayers[i].GetComponent<PlayerState>().AddDebuffState(DefaultPlayerSkillDebuff.EnumSkillDebuff.GROGGY, 0.1f);
        }


         
        photonView.RPC("RPCTurnOffLight", PhotonTargets.All);
        Debug.Log("스킬시작");
    }

    // RPC입니다.
    [PunRPC]
    void RPCTurnOffLight()
    {
        CreateTurnOffPanel();
    }

    void SetPostProcessing(bool isChange)
    {
        nowTurnOffTime = turnOffLightState.TurnOffTime;
        PostProcessingBehaviour ppp = SpringArmObject.GetInstance().armCamera.GetComponent<PostProcessingBehaviour>();
        if (ppp != null)
        {
            if (isChange)
            {
                ppp.profile = changePPP;
            }
            else
            {
                ppp.profile = tempPPP;
            }
        }
    }

    public void CreateTurnOffPanel()
    {


        TurnOffPanelScript tops = UIManager.GetInstance().turnOffPanelScript;

        GameObject turnOffPanelObject = tops.TurnOffPanel;

        turnOffPanelObject.SetActive(true);


        if (photonView.isMine)
        {
            tops.BlackBackGround.SetActive(false);
            SetPostProcessing(true);
        }

        TurnOffPanel turnOffPanel =  turnOffPanelObject.GetComponent<TurnOffPanel>();
        if (turnOffPanel == null)
            return;


        turnOffPanel.SetTurnOffTime(turnOffLightState.TurnOffTime);
        



        turnOffPanel.SetisUseTurnOff(true);

        turnOffPanel.StartCutScene();


        SpringArmObject.GetInstance().GetSystemSoundManager().PlayEffectSound(SoundManager.EnumEffectSound.EFFECT_CAT_TURNOFF);


    }
}
