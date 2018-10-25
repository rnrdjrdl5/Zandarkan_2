using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplainGamePanelScript
{

    public GameObject ExplainGamePanel;

    public GameObject MouseExplain;
    public GameObject CatExplain;

    

    public void InitData()
    {

        isShowing = false;
        isCanUseExplain = true;

        ExplainGamePanel = UIManager.GetInstance().UICanvas.transform.Find("ExplainGamePanel").gameObject;

        MouseExplain = ExplainGamePanel.transform.Find("MouseExplain").gameObject;
        CatExplain = ExplainGamePanel.transform.Find("CatExplain").gameObject;
    }

    public void SetActive(bool isActive)
    {
        MouseExplain.SetActive(isActive);
        CatExplain.SetActive(isActive);

        ExplainGamePanel.SetActive(isActive);
    }



    private bool isCanUseExplain;
    public void OffisCanUseExplain(){ isCanUseExplain = false; }
    private bool isShowing;

    public void ShowExplain(string playerType)
    {

        Debug.Log(playerType);
        if (!isCanUseExplain) return;
        Debug.Log("2");


        isShowing = !isShowing;

        ExplainGamePanel.SetActive(isShowing);
        MouseExplain.SetActive(false);
        CatExplain.SetActive(false);

        if (isShowing)
        {
            if (playerType == "Cat")
            {
                CatExplain.SetActive(true);
            }

            else
            {
                MouseExplain.SetActive(true);
            }
        }

    }


}
