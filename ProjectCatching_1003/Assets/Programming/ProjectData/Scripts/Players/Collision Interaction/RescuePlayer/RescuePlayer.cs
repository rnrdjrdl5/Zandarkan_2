using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescuePlayer : Photon.MonoBehaviour, IPunObservable
{
    public float RescueDistance = 10.0f;        // 레이 거리
    public float MaxRescueTime = 3.0f;
    public float RescueHP = 10.0f;

    public float MinPlayerDis = 1.5f;         //플레이어 사이 거리

    public DefaultInput defaultInput;

    public PointToLocation pointToLocation;



    private PhotonView photonView;
    private GameObject tempTargetObject;
    private GameObject targetObject;
    private PlayerState playerState;
    private Animator animator;
    private PlayerMove playerMove;
    private PlayerHealth playerHealth;
    private UIManager uIManager;
    private PlayerBodyPart playerBodyPart;
    private SoundManager soundManager;

    private bool isUsedRescue = false;      // 동기화, 살려지는지 판단


    private float NowRescueTime = 0.0f;

    public delegate void deleUpdateRescue(float now, float max);

    public deleUpdateRescue UpdateRescueEvent;

    IEnumerator CoroRescueTime;


    GameObject helpEffect;

    public delegate void deleSuccessRescue();
    public event deleSuccessRescue SuccessRescueEvent;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();
        playerState = GetComponent<PlayerState>();
        playerMove = GetComponent<PlayerMove>();
        playerHealth = GetComponent<PlayerHealth>();
        playerBodyPart = GetComponent<PlayerBodyPart>();
        soundManager = GetComponent<SoundManager>();

        uIManager = UIManager.GetInstance();

        pointToLocation = new PointToLocation();

        CoroRescueTime = UpdateResqueTime();


        gameObject.GetComponent<PhotonAnimatorView>().SetParameterSynchronized("RescueType", PhotonAnimatorView.ParameterType.Int, PhotonAnimatorView.SynchronizeType.Discrete);
        gameObject.GetComponent<PhotonAnimatorView>().SetParameterSynchronized("isRevive", PhotonAnimatorView.ParameterType.Bool, PhotonAnimatorView.SynchronizeType.Discrete);

    }

    private void Start()
    {
        RegisterEvent();
    }

    private bool CheckMinDistance(float playerDis)
    {

        if (MinPlayerDis >= playerDis) return true;

        else return false;
    }


    // Update => 대상의 CehckUseRescue로 이동
    public void Update()
    {
        // Tutorial 용 체크 제작
        if (PhotonManager.GetInstance().isTutorial)
        {
            if (!UseTutorialRescue())
                UIManager.GetInstance().pressImagePanelScript.RescueImage.SetActive(false);

            if (!defaultInput.IsUseKey()) return;

            targetObject = tempTargetObject;

            UseRescue();
        }


        // 일반 플레이 시 사용
        else
        {
            if (!CheckPhotonMine())
                return;

            // 실행 프로세스 체크
            if (!ExecuteRescueProcess())
                UIManager.GetInstance().pressImagePanelScript.RescueImage.SetActive(false);

            if (!defaultInput.IsUseKey())
                return;


            // Ray로 찾지 못하면 한 대상을 계속 가리키게 된다.
            targetObject = tempTargetObject;

            targetObject.GetComponent<RescuePlayer>().CallCheckUseRescue(PhotonNetwork.player.ID);
        }
    }



    bool ExecuteRescueProcess()
    {

        if (!CheckState()) return false;


        tempTargetObject = pointToLocation.FindObject
            (RescueDistance, "RescueLayer", SpringArmObject.GetInstance().armCamera);

        if (tempTargetObject != null) tempTargetObject = tempTargetObject.transform.root.gameObject;
        else return false;


        float PlayerDistance = (tempTargetObject.transform.position - gameObject.transform.position).magnitude;

        //temp를 찾은상태인지, 거리는 충분한지
        if (!CheckCanUseResque(PlayerDistance)) return false;

        // 상대방의 거리 차이는 얼마나되는지
        if (!CheckOtherPlayerState()) return false;


        UIManager.GetInstance().pressImagePanelScript.RescueImage.SetActive(true);


        return true;
    }

    private bool CheckPhotonMine()
    {
        if (photonView.isMine)
            return true;
        else
            return false;
    }


    private bool CheckCanUseResque(float playerDis)
    {


        if (tempTargetObject == null)
            return false;

        if (CheckMinDistance(playerDis))
            return true;

        else
            return false;




    }

    private bool CheckState()
    {
        if (
             (playerState.EqualPlayerCondition(PlayerState.ConditionEnum.IDLE) ||
                playerState.EqualPlayerCondition(PlayerState.ConditionEnum.RUN)) &&
             playerState.GetWeaponType() == PlayerState.WeaponEnum.NONE)
        {
            return true;
        }

        else
            return false;

    }


    // 대상과 본인의 거리를 판단해서 살릴 수 있는지 판단한다.
    private void CheckMinRescue()
    {

        if (playerState.GetPlayerCondition() != PlayerState.ConditionEnum.RESCUE)
        {
            CancelEvent();
            return;
        }


        if (tempTargetObject == null)
        {
            CancelEvent();
            return;
        }

        float PlayerDistance = (tempTargetObject.transform.position - gameObject.transform.position).magnitude;

        if (!CheckMinDistance(PlayerDistance))
            CancelEvent();
    }


    private bool CheckOtherPlayerState()
    {
        Animator targetAnimator = tempTargetObject.GetComponent<Animator>();

        if (targetAnimator == null)
            return false;


        if (
            (tempTargetObject.GetComponent<PlayerHealth>().GetNowRopeDeadTime() > 0.0f) &&
            (targetAnimator.GetInteger("WeaponType") == 2))
            return true;

        else
            return false;

    }

    private GameObject FindPlayerObject(int vID)
    {
        for (int i = 0; i < PhotonManager.GetInstance().AllPlayers.Count; i++)
        {

            if (PhotonManager.GetInstance().AllPlayers[i].GetPhotonView().ownerId == vID)
                return PhotonManager.GetInstance().AllPlayers[i];
        }

        Debug.LogWarning("에러");
        return null;
    }

    private void UseRescue()
    {

        animator.SetInteger("RescueType", 1);
        playerState.SetPlayerCondition(PlayerState.ConditionEnum.RESCUE);

        playerMove.ResetMoveSpeed();


        CoroRescueTime = UpdateResqueTime();
        StartCoroutine(CoroRescueTime);

    }


    // 살리기 시작한 뒤 코루틴
    private IEnumerator UpdateResqueTime()
    {
        uIManager.rescueBarPanelScript.SetActive(true);


        while (true)
        {
            Debug.Log("살리는 코루틴 동작 시행중");

            CheckMinRescue();

            NowRescueTime += Time.deltaTime;

            UpdateRescueEvent(NowRescueTime, MaxRescueTime);
            if (playerHealth.GetNowHealth() <= 0) StopCoroutine(CoroRescueTime);

            if (NowRescueTime >= MaxRescueTime)// 살려짐
            {
                if(SuccessRescueEvent != null) SuccessRescueEvent();

                NowRescueTime = 0.0f;

                uIManager.rescueBarPanelScript.SetActive(false);

                animator.SetInteger("RescueType", 2);


                // TUtorial인지 아닌지 구분해서 사용
                RescuePlayer rp = targetObject.GetComponent<RescuePlayer>();

                if (rp != null)
                    rp.CallSuccessRescue();
                else
                    targetObject.GetComponent<AIRescue>().SuccessRescue();

                StopCoroutine(CoroRescueTime);
                // 코루틴 종료

                // 상태변경하고

                // 체력체우고

                yield break;

            }




            yield return null;
        }
    }

    private void RegisterEvent()
    {

        // UI 이벤트 등록
        UIManager.GetInstance().rescueBarPanelScript.SetEvent(gameObject);
        playerMove.RescueCancelEvent = CancelEvent;

        // 플레이어 사망 이벤트 등록
        playerHealth.FinishHealthEvent += CancelEvent;
    }

    public void CancelEvent()
    {
        // 이미 살리는게 완료되었으면. finish 상태때.
        if (animator.GetInteger("RescueType") == 0)
            return;

        animator.SetInteger("RescueType", 0);
        UIManager.GetInstance().rescueBarPanelScript.SetActive(false);
        NowRescueTime = 0.0f;
        StopCoroutine(CoroRescueTime);

        if (targetObject == null)
            return;


        // TUtorial인지 아닌지 구분해서 사용
        RescuePlayer rp = targetObject.GetComponent<RescuePlayer>();
        if (rp != null)
            rp.CallOtherCancelEvent();
        else
            targetObject.GetComponent<AIRescue>().OtherCancelEvent();



    }

    

    public void CreateHelpEffect()
    {

        helpEffect = PoolingManager.GetInstance().CreateEffect(PoolingManager.EffctType.HELP_BUBBLE);
        helpEffect.transform.position = transform.position;
    }

    public void DeleteHelpEffect()
    {

        if (helpEffect == null) return;

        PoolingManager.GetInstance().PushObject(helpEffect);

    }


    // 애니메이션 이벤트

    public void OffRevive()
    {
        animator.SetBool("isRevive", false);
    }
    public void ReviveEffect()
    {
        GameObject go = PoolingManager.GetInstance().CreateEffect(PoolingManager.EffctType.REVIVE_EFFECT);
        soundManager.PlayEffectSound(SoundManager.EnumEffectSound.EFFECT_MOUSE_REVIVE);
        go.transform.position = transform.position;
    }
    private void RescuingSound()
    {
        soundManager.PlayEffectSound(SoundManager.EnumEffectSound.EFFECT_RESCUING_1);
    }



    private bool UseTutorialRescue()
    {

        if (!CheckState()) return false;


        tempTargetObject = pointToLocation.FindObject
            (RescueDistance, "RescueLayer", SpringArmObject.GetInstance().armCamera);

        if (tempTargetObject != null) tempTargetObject = tempTargetObject.transform.parent.gameObject;
        else return false;


        float PlayerDistance = (tempTargetObject.transform.position - gameObject.transform.position).magnitude;

        //temp를 찾은상태인지, 거리는 충분한지
        if (!CheckCanUseResque(PlayerDistance)) return false;

        // 상대방의 거리 차이는 얼마나되는지
        if (!CheckTutorialTeamState()) return false;

        UIManager.GetInstance().pressImagePanelScript.RescueImage.SetActive(true);

        return true;
    }

    public bool CheckTutorialTeamState()
    {
        Animator targetAnimator = tempTargetObject.GetComponent<Animator>();

        if (targetAnimator == null)
            return false;


        if ((targetAnimator.GetInteger("WeaponType") == 2))
            return true;

        else
            return false;
    }





    /***** Call RPC 함수 *****/

    public void CallCheckUseRescue(int vID)
    {
        photonView.RPC("RPCCheckUseRescue", PhotonTargets.All, vID);
    }

    public void CallOKSign()
    {
        photonView.RPC("RPCOKSign", PhotonTargets.All);
    }

    public void CallNoSign()
    {
        photonView.RPC("RPCNoSign", PhotonTargets.All);
    }

    public void CallOtherCancelEvent()
    {
        photonView.RPC("RPCOtherCancelEvent", PhotonTargets.All);
    }

    public void CallSuccessRescue()
    {
        photonView.RPC("RPCSuccessRescue", PhotonTargets.All);
    }
    /***** RPC *****/


    
    // 타겟 대상의 RPC사용한다.
    [PunRPC]
    public void RPCCheckUseRescue(int vID)
    {

        // 살릴 대상을 체크한다.
        if (!CheckPhotonMine())
            return;

        // 살려주는 대상 찾아내기
        GameObject go = FindPlayerObject(vID);
        RescuePlayer rescuePlayer = go.GetComponent<RescuePlayer>();


        // 누군가가 살리지 않고 있으면 시도
        if (!isUsedRescue && animator.GetInteger("WeaponType") == 2)
        {

            rescuePlayer.CallOKSign();
            isUsedRescue = true;

            playerHealth.SetisUseRopeDead(false);
        }

        else
            rescuePlayer.CallNoSign();
    }

    [PunRPC]
    public void RPCOKSign()
    {
        if (photonView.isMine)
        {

            UseRescue();
        }


        
        
    }

    [PunRPC]
    public void RPCNoSign()
    {
        if (photonView.isMine)
        {
            Debug.Log("실패함.");
        }


    }

    [PunRPC]
    public void RPCOtherCancelEvent()
    {
        if (!CheckPhotonMine())
            return;


        // 혹시라도 Rope 상태가 아닐 때 해제 시도할 경우
        string playerType = (string)PhotonNetwork.player.CustomProperties["PlayerType"];

        if (playerType != "Rope") return;
        


        

        isUsedRescue = false;

        // 죄없는 플레이어를 use 시키면 안된다.
        playerHealth.SetisUseRopeDead(true);
    }

    [PunRPC]
    public void RPCSuccessRescue()
    {
        playerBodyPart.LegRope.SetActive(false);
        playerBodyPart.HandRope.SetActive(false);


        UIManager.GetInstance().rescueIconPanelScript.RescueIconsImage[
            playerHealth.GetPlayerNumber()].fillAmount = 1.0f;
        UIManager.GetInstance().rescueIconPanelScript.RescueSet[
            playerHealth.GetPlayerNumber()].SetActive(false);
        
        playerHealth.SetPlayerNumber(-1);




        NotificationManager.GetInstance().NotificationMessage(NotificationManager.EnumNotification.RESCUE);
        if (!CheckPhotonMine())
            return;




        PhotonNetwork.player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "PlayerType", "Mouse" } });
        


        playerHealth.ResetNowRopeDeadTime();
        playerHealth.CallDecreaseDeadEvent();

        UIManager.GetInstance().hpPanelScript.SetAliveActive(true);
        isUsedRescue = false;

        playerState.SetWeaponType(PlayerState.WeaponEnum.NONE);
        animator.SetBool("isRevive", true);
        animator.SetInteger("WeaponType", 0);


        playerHealth.CallApplyDamage(-RescueHP);



    }






    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
        
}
