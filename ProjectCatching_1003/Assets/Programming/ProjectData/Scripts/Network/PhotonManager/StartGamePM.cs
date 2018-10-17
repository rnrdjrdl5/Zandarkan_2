using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PhotonManager
{
    // 게임 시작 후 플레이어가 생성될 때 까지 기다립니다.
    [PunRPC]
    void RPCActionCheckGameStart()
    {

        // 서버 ++ ) 고양이 쥐 지정, 생성
        if (PhotonNetwork.isMasterClient)
        {
            // 플레이어 생성
            photonView.RPC("RPCCreatePlayer", PhotonTargets.All);
        }


        // 플레이어 생성 완료
        condition = new Condition(CheckLoading);
        conditionLoop = new ConditionLoop(NoAction);
        rPCActionType = new RPCActionType(NoRPCActonCondition);

        IEnumCoro = CoroTrigger(condition, conditionLoop, rPCActionType, "RPCPreStartCount");
        StartCoroutine(IEnumCoro);
    }


    // 생성완료 
    bool CheckLoading()
    {
        bool isLoading = true;

        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            string CreatePlayerState = (string)PhotonNetwork.playerList[i].CustomProperties["Offset"];

            if (CreatePlayerState != "LoadingComplete")
            {
                isLoading = false;
            }
        }

        return isLoading;

    }

    [PunRPC]
    void RPCTutoActionCheckGameStart()
    {

        // 1. 선택한 플레이어로 생성한다. 임시로 쥐
        ExitGames.Client.Photon.Hashtable MouseHash = new ExitGames.Client.Photon.Hashtable { { "PlayerType", "Mouse" } };
        PhotonNetwork.player.SetCustomProperties(MouseHash);
        photonView.RPC("RPCTutoCreatePlayer", PhotonTargets.All);

        // 플레이어 생성 완료 체크
        condition = new Condition(CheckLoading);
        conditionLoop = new ConditionLoop(NoAction);
        rPCActionType = new RPCActionType(NoRPCActonCondition);


        // 인원수에 따른 물체 삭제
        if (objectManager != null)
        {
            objectManager.DeleteObjPropPlayer();

            objectManager.RegisterObjectMount();

            objectManager.CalcObjectMag();
        }

        // 최대 게이지 표현
        GameTimeOutCondition = mouseWinScoreCondition[0];

        IEnumCoro = CoroTrigger(condition, conditionLoop, rPCActionType, "RPCTutoPlayingGame");
        StartCoroutine(IEnumCoro);
    }
}
