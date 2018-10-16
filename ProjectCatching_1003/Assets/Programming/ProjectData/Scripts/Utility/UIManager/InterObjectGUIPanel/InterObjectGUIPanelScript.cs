using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterObjectGUIPanelScript
{
    
    private GameObject InterObjectGUIPanel;
    private void InitInterObjectGUIPanel()
    {
        InterObjectGUIPanel = 
            UIManager.GetInstance().UICanvas.transform.Find("InterObjectGUIPanel").gameObject;
    }



    const int MAX_GUI_MOUNT = 6;

    public GameObject[] GUIObjects;
    public InterGUIType[] interGUITypes;        // 타입을 받아오기 위해 사용
    private Text[] GUINowTexts;
    private Text[] GUIMaxTexts;


    public void InitGUIs()
    {
        GUIObjects = new GameObject[MAX_GUI_MOUNT];
        interGUITypes = new InterGUIType[MAX_GUI_MOUNT];
        GUINowTexts = new Text[MAX_GUI_MOUNT];
        GUIMaxTexts = new Text[MAX_GUI_MOUNT];


        for (int i = 0; i < MAX_GUI_MOUNT; i++)
        {
            string Text = "GUI" + (i + 1).ToString();

            GUIObjects[i] = InterObjectGUIPanel.transform.Find(Text).gameObject;
            interGUITypes[i] = GUIObjects[i].GetComponent<InterGUIType>();

            GUINowTexts[i] = GUIObjects[i].transform.Find("NowText").GetComponent<Text>();
            GUIMaxTexts[i] = GUIObjects[i].transform.Find("MaxText").GetComponent<Text>();
        }
        
    }

    public void InitData()
    {

        InitInterObjectGUIPanel();

        InitGUIs();
    }

    public void SetActive(bool isActive)
    {
        InterObjectGUIPanel.SetActive(isActive);
    }
    public bool GetActive()
    {
        return InterObjectGUIPanel.activeInHierarchy;
    }


    public void SetText(int[] maxData, int[] nowData)
    {
        // 1. GUI에게서 타입을 얻어온다.
        // 2. 해당 타입을 이용.

        for (int i = 0; i < MAX_GUI_MOUNT; i++)
        {
            // 1. GUI 별로 타입을 받아오기 위해서 사용한다.

            // 2. 데이터 설정, GUI의 설정한 타입의 개수를 가져온다.
            GUINowTexts[i].text =
                nowData[(int)interGUITypes[i].interactiveObjectType].ToString();

            GUIMaxTexts[i].text =
                maxData[(int)interGUITypes[i].interactiveObjectType].ToString();
        }


    }
}
