using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerNamePanelScript{

    UIManager uIManager;
    GameObject uICanvas;

    GameObject PlayerNamePanel;
    GameObject PlayerNameText;
    public Text PlayerNameTextText;


    public bool isCanShow { get; set; }

    public void InitData()
    {
        isCanShow = true;
        uIManager = UIManager.GetInstance();
        uICanvas = uIManager.UICanvas;

        PlayerNamePanel = uICanvas.transform.Find("PlayerNamePanel").gameObject;
        PlayerNameText = PlayerNamePanel.transform.Find("PlayerNameText").gameObject;
        PlayerNameTextText = PlayerNameText.GetComponent<Text>();

        

    }

    public void SetActive(bool isActive)
    {
        if (!isCanShow)
        {
            PlayerNameText.SetActive(false);
            PlayerNamePanel.SetActive(false);
        }

        else
        {
            PlayerNameText.SetActive(isActive);
            PlayerNamePanel.SetActive(isActive);
        }            
    }
}
