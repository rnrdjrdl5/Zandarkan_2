using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlayerData{

    private string playerName;
    private int playerID;
    private bool isMaster;
    private PhotonPlayer photonPlayer;
    public void InitPlayerData(PhotonPlayer pp)
    {

        playerName = pp.NickName;
        playerID = pp.ID;
        isMaster = pp.IsMasterClient;
        photonPlayer = pp;
    }

    public void SortPlayerData()
    {

    }

    public string GetPlayerName() { return playerName; }
    public int GetPlayerID() { return playerID; }
    public bool GetIsMaster() { return isMaster; }
    public PhotonPlayer GetPhotonPlayer() { return photonPlayer; }
    
}
