using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterObjectGUIPanelScript
{

    public    bool isOpen;

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

    public GameObject Tabs;
    public void InitTabs() { Tabs = InterObjectGUIPanel.transform.Find("Tabs").gameObject; }

    public GameObject TabClose;
    public void InitTabClose() { TabClose = Tabs.transform.Find("TabClose").gameObject; }
    public GameObject TabOpen;
    public void InitTabOpen() { TabOpen = Tabs.transform.Find("TabOpen").gameObject; }

    public GameObject MaxTabText;
    public Text MaxTabTextText;
    public void InitMaxTabText() {
        MaxTabText = Tabs.transform.Find("MaxTabText").gameObject;
        MaxTabTextText = MaxTabText.GetComponent<Text>();
    }
    public GameObject NowTabText;
    public Text NowTabTextText;
    public void InitNowTabText() {
        NowTabText = Tabs.transform.Find("NowTabText").gameObject;
        NowTabTextText = NowTabText.GetComponent<Text>();
    }

    public void InitData()
    {
        isOpen = false;

        InitInterObjectGUIPanel();

        InitObjects();

        InitObjGUIBgImage();
        InitObjGUIBgImageRect();

        InitGUIs();


        InitTabs();
        InitTabClose();
        InitTabOpen();
        InitMaxTabText();
        InitNowTabText();

    }

    public void SetActive(bool isActive)
    {
        InterObjectGUIPanel.SetActive(isActive);

        ObjGUIBgImage.SetActive(isActive);

        Tabs.SetActive(isActive);
        TabOpen.SetActive(isActive);
        TabClose.SetActive(isActive);

        NowTabText.SetActive(isActive);
        MaxTabText.SetActive(isActive);

        if (isActive == true)
        {
            PushTab();
        }
    }

    public void PushTab()
    {
        if (isOpen)
        {
            TabOpen.SetActive(true);
            TabClose.SetActive(false);
        }

        else if (!isOpen)
        {
            TabOpen.SetActive(false);
            TabClose.SetActive(true);
        }

        SetTextColor(isOpen);
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
    public void SetNowMaxText(int nowData, int maxData)
    {
        Debug.Log("nowData : " + nowData);
        NowTabTextText.text = nowData.ToString();
        MaxTabTextText.text = maxData.ToString();
    }

    public void SetTextColor(bool io)
    {
        if (io)
        {
            NowTabTextText.color = Color.black;
            MaxTabTextText.color = Color.black;
        }

        else if (!io)
        {
            NowTabTextText.color = Color.white;
            MaxTabTextText.color = Color.white;
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
