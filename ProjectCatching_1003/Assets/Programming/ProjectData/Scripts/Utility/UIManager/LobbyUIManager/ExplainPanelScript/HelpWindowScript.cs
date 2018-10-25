using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpWindowScript
{

    LobbyUIManager lobbyUIManager;

    GameObject HelpWindow;
    public bool IsActiveHelpWindow() { return HelpWindow.activeInHierarchy; }


    GameObject MouseHelp;
    GameObject CatHelp;

    public void InitData()
    {
        lobbyUIManager = LobbyUIManager.GetInstance();

        HelpWindow = lobbyUIManager.UICanvas.transform.Find("HelpWindow").gameObject;

        MouseHelp = HelpWindow.transform.Find("MouseHelp").gameObject;
        CatHelp = HelpWindow.transform.Find("CatHelp").gameObject;
    }

    public void SetActive(bool isActive)
    {
        HelpWindow.SetActive(isActive);

        MouseHelp.SetActive(isActive);
        CatHelp.SetActive(isActive);
    }

    public void ShowHelp()
    {
        HelpWindow.SetActive(true);

        string seletedPlayerType
            = (string)PhotonNetwork.player.CustomProperties["SelectPlayer"];

        Debug.Log(seletedPlayerType);

        MouseHelp.SetActive(false);
        CatHelp.SetActive(false);

        if (seletedPlayerType == "Random" ||
            seletedPlayerType == "Mouse")
        {
            MouseHelp.SetActive(true);
        }

        else if (seletedPlayerType == "Cat")
        {
            CatHelp.SetActive(true);
        }

    }

    public void NextPage()
    {
        if (MouseHelp.activeInHierarchy)
        {
            MouseHelp.SetActive(false);
            CatHelp.SetActive(true);
        }

        else if (CatHelp.activeInHierarchy)
        {
            MouseHelp.SetActive(true);
            CatHelp.SetActive(false);
        }
    }
}
