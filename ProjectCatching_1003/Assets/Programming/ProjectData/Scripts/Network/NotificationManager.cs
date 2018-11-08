using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour {

    static private NotificationManager notificationManager;
    static public NotificationManager GetInstance() { return notificationManager; }

    public int maxNotification;
    public NotificationElement[] notificationElements;


    // Use this for initialization
    private void Awake()
    {
        notificationManager = this;
    }


}
