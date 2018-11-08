using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPanelScript {

    UIManager uIManager;
    GameObject uICanvas;

    GameObject NotificationPanel;
    GameObject NotificationText;
    public Text NotificationTextText { get; set; }
    public void InitData()
    {
        uIManager = UIManager.GetInstance();
        uICanvas = uIManager.UICanvas;

        NotificationPanel = uICanvas.transform.Find("NotificationPanel").gameObject;
        NotificationText = NotificationPanel.transform.Find("NotificationText").gameObject;
        NotificationTextText = NotificationText.GetComponent<Text>();


    }

    public void SetActive(bool isActive)
    {
        NotificationText.SetActive(isActive);
        NotificationPanel.SetActive(isActive);
    }
}
