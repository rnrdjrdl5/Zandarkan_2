using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour {

    static private NotificationManager notificationManager;
    static public NotificationManager GetInstance() { return notificationManager; }

    public string RopeNotification;
    public string RescueNotification;
    public string FinishDestroyMessage;
    public string FastSecondMouseMessage;
    public string FastSecondCatMessage;

    public int maxRestNotification;
    public string[] RestNotifications;
    public int[] AtLeastRestCount;
    public bool[] isUseRestMessage;


    public enum EnumNotification { RESCUE, ROPE, REST , FINISH_DESTROY , FAST_MOUSE_SECOND, FAST_CAT_SECOND };


    private float nowMessageTime;
    private bool isUseMessage;
    public float maxMessageTime;
    // Use this for initialization
    private void Awake()
    {
        notificationManager = this;
        isUseMessage = false;
    }


    
    
    public void Update()
    {
        if (isUseMessage)
        {
            nowMessageTime += Time.deltaTime;

            if (nowMessageTime >= maxMessageTime)
            {
                isUseMessage = false;

                nowMessageTime = 0.0f;
                UIManager.GetInstance().notificationPanelScript.SetActive(false);
                UIManager.GetInstance().notificationPanelScript.NotificationTextText.text = "";
            }
        }
    }


    public void NotificationMessage(EnumNotification enumNotification)
    {

        UIManager.GetInstance().notificationPanelScript.SetActive(true);

        if (isUseMessage)
        {
            nowMessageTime = 0.0f;
        }


        string tempMessage = null;
        int playerCount = 0;


        bool isUseData = false;
        switch (enumNotification)
        {
            // 메세지 결정.
            case EnumNotification.ROPE:
                tempMessage = RopeNotification;
                playerCount = CountMousePlayer();
                isUseData = true;
                break;

            case EnumNotification.RESCUE:
                tempMessage = RescueNotification;
                playerCount = CountMousePlayer();
                isUseData = true;
                break;

            case EnumNotification.FINISH_DESTROY:
                tempMessage = FinishDestroyMessage;
                isUseData = false;
                break;

            case EnumNotification.FAST_CAT_SECOND:
                tempMessage = FastSecondCatMessage;
                isUseData = false;
                break;

            case EnumNotification.FAST_MOUSE_SECOND:
                tempMessage = FastSecondMouseMessage;
                isUseData = false;
                break;

        }
        if (isUseData) { SetNotifiText(tempMessage, playerCount); }
        else { SetNotifiText(tempMessage); }
    }


    public void NotificationMessage(EnumNotification enumNotification, int Count)
    {

        UIManager.GetInstance().notificationPanelScript.SetActive(true);
        if (isUseMessage)
        {
            nowMessageTime = 0.0f;
        }

        string tempMessage = null;
        int playerCount = 0;

        switch (enumNotification)
        {
            // 메세지 결정.
            case EnumNotification.REST:
                tempMessage = RestNotifications[Count];
                playerCount = AtLeastRestCount[Count];
                break;
        }

        SetNotifiText(tempMessage, playerCount);
    }


    public void SetNotifiText(string tempMessage, int playerCount)
    {
        string[] messageSplit = tempMessage.Split('@');

        Debug.Log(tempMessage);
        Debug.Log(messageSplit);
        Debug.Log(tempMessage[0]);
        Debug.Log(tempMessage[1]);


        string connectString = messageSplit[0] + playerCount + messageSplit[1];
        UIManager.GetInstance().notificationPanelScript.NotificationTextText.text = connectString;

        isUseMessage = true;
    }

    public void SetNotifiText(string tempMessage)
    {
        UIManager.GetInstance().notificationPanelScript.NotificationTextText.text = tempMessage;
        isUseMessage = true;
    }

    public int CountMousePlayer()
    {
        int playerCount= 0;
        int tempCount = PhotonNetwork.playerList.Length;
        for (int i = 0; i < tempCount; i++)
        {
            if ((string)PhotonNetwork.playerList[i].CustomProperties["PlayerType"] == "Mouse")
            {
                playerCount++;
            }
        }

        return playerCount;
    }
}
