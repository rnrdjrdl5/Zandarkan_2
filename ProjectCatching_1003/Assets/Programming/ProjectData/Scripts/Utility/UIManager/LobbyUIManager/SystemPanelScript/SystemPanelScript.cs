using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemPanelScript{

    // 시스템 UI
    private LobbyUIManager lobbyUIManager;
    

    public GameObject CreateSystemWindow;

    public Text RoomSystemText;
    public RoomSystemMessage roomSystemMessage;

    public void InitData()
    {
        lobbyUIManager = LobbyUIManager.GetInstance();

        CreateSystemWindow =
            lobbyUIManager.UICanvas.transform.Find("CreateSystemWindow").gameObject;

        RoomSystemText =
            CreateSystemWindow.transform.Find("RoomSystemText").GetComponent<Text>();

        roomSystemMessage = RoomSystemText.GetComponent<RoomSystemMessage>();
    }

    public void SetActive(bool isActive)
    {
        CreateSystemWindow.SetActive(isActive);
    }

    public void OutputRoomMessage(RoomSystemMessage.EnumSystemCondition systemConditionType)
    {
        CreateSystemWindow.SetActive(true);

        int count = roomSystemMessage.systemConditionType.Length;

        for (int i = 0; i < count; i++)
        {
            if (roomSystemMessage.systemConditionType[i] == systemConditionType)
            {
                RoomSystemText.text = roomSystemMessage.SystemText[i];
            }
        }
    }


}
