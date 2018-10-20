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

    private GameObject Objects;
    private void InitObjects()
    {
        Objects = InterObjectGUIPanel.transform.Find("Objects").gameObject;
    }

    private GameObject ObjGUIBgImage;
    private void InitObjGUIBgImage()
    {
        ObjGUIBgImage = Objects.transform.Find("ObjGUIBgImage").gameObject;
    }
    public RectTransform ObjGUIBgImageRect;
    private void InitObjGUIBgImageRect()
    {
        ObjGUIBgImageRect = ObjGUIBgImage.GetComponent<RectTransform>();
    }

    const int MAX_GUI_MOUNT = 10;

    public GameObject[] GUIObjects;
    public InterGUIType[] interGUITypes;        // 타입을 받아오기 위해 사용
    private Text[] GUINowTexts;
    private Text[] GUIMaxTexts;
    private GameObject[] GUICheck;


    public void InitGUIs()
    {
        GUIObjects = new GameObject[MAX_GUI_MOUNT];
        interGUITypes = new InterGUIType[MAX_GUI_MOUNT];
        GUINowTexts = new Text[MAX_GUI_MOUNT];
        GUIMaxTexts = new Text[MAX_GUI_MOUNT];
        GUICheck = new GameObject[MAX_GUI_MOUNT];


        for (int i = 0; i < MAX_GUI_MOUNT; i++)
        {
            string Text = "GUI" + (i + 1).ToString();

            GUIObjects[i] = Objects.transform.Find(Text).gameObject;
            interGUITypes[i] = GUIObjects[i].GetComponent<InterGUIType>();

            GUINowTexts[i] = GUIObjects[i].transform.Find("NowText").GetComponent<Text>();
            GUIMaxTexts[i] = GUIObjects[i].transform.Find("MaxText").GetComponent<Text>();

            GUICheck[i] = GUIObjects[i].transform.Find("UICheck").gameObject;
        }



    }

    public GameObject TapImage;
    public void InitTapImage() { TapImage = InterObjectGUIPanel.transform.Find("TapImage").gameObject; }
    public void AddTapImageHeight(float yPosition)
    {
        TapImage.transform.localPosition = new Vector3(
            TapImage.transform.localPosition.x,
            TapImage.transform.localPosition.y + yPosition,
            TapImage.transform.localPosition.z);
        
    }

    public void InitData()
    {

        InitInterObjectGUIPanel();

        InitObjects();

        InitObjGUIBgImage();
        InitObjGUIBgImageRect();

        InitGUIs();

        InitTapImage();
    }

    public void SetActive(bool isActive)
    {
        Debug.Log("dsafsadsf");
        InterObjectGUIPanel.SetActive(isActive);

        ObjGUIBgImage.SetActive(isActive);
        TapImage.SetActive(isActive);
    }
    public bool GetObjectActive()
    {
        return Objects.activeInHierarchy;
    }

    public void SetObjectActive(bool isActive)
    {
        Objects.SetActive(isActive);
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

    public void SetGUICheck(int[] maxData, int[] nowData)
    {

        for (int i = 0; i < MAX_GUI_MOUNT; i++)
        {

            if (nowData[(int)interGUITypes[i].interactiveObjectType] <= 0)
                GUICheck[i].SetActive(true);

            else
                GUICheck[i].SetActive(false);
        }
    }
}
