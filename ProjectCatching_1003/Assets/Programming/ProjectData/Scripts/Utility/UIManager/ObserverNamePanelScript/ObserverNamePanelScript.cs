using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObserverNamePanelScript {

    UIManager uIManager;
    GameObject uICanvas;

    GameObject ObserverNamePanel;

    GameObject ObserverNameText;
    public Text ObserverNameTextText;
    public void InitData()
    {
        uIManager = UIManager.GetInstance();
        uICanvas = uIManager.UICanvas;

        ObserverNamePanel = uICanvas.transform.Find("ObserverNamePanel").gameObject;
        ObserverNameText = ObserverNamePanel.transform.Find("ObserverNameText").gameObject;
        ObserverNameTextText = ObserverNameText.GetComponent<Text>();

        isUseShow = false;
    }


    public bool isUseShow { get; set; }
    public void SetActive(bool isActive)
    {
        if (!isUseShow)
        {
            ObserverNameText.SetActive(false);
            ObserverNamePanel.SetActive(false);
        }
        else
        {
            ObserverNameText.SetActive(isActive);
            ObserverNamePanel.SetActive(isActive);
        }

        
    }
}
