using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimitTimePanelScript
{
    /**** LimitTime ****/

    public GameObject LimitTimePanel { set; get; }
    public void InitLimitTimePanel() { LimitTimePanel = UIManager.GetInstance().UICanvas.transform.Find("LimitTimePanel").gameObject; }


    public GameObject LimitTimeText { get; set; }
    public void InitLimitTimeText() { LimitTimeText = LimitTimePanel.transform.Find("LimitTimeText").gameObject; }

    public Text LimitTimeTextText { get; set; }
    public void InitLimitTimeTextText() { LimitTimeTextText = LimitTimeText.GetComponent<Text>(); }

    public GameObject LimitTimeLineImage { get; set; }
    public void InitLimitTimeLineImage() { LimitTimeLineImage = LimitTimePanel.transform.Find("LimitTimeLineImage").gameObject; }






    public void SetLimitTime(bool isActive)
    {
        LimitTimePanel.SetActive(isActive);

        LimitTimeText.SetActive(isActive);

        LimitTimeLineImage.SetActive(isActive);

    }

    public void InitData()
    {
        InitLimitTimePanel();

        InitLimitTimeText();
        InitLimitTimeTextText();

        InitLimitTimeLineImage();
    }

    public void TimeTickUpdateEvent(int timeData)
    {
        if (LimitTimePanel.activeInHierarchy == false)
            return;

        // 타임 이벤트 에 따른 이미지 로 전환해주기.,

        int MinuteTime = 0;

        while (true){

            if (timeData < 60)
                break;

            timeData -= 60;
            MinuteTime += 1;               
        }

        string minuteString = null;
        minuteString += MinuteTime.ToString();

        string secondString = null;
        if (timeData < 10) secondString = "0";
        secondString += timeData.ToString();



        LimitTimeTextText.text = minuteString + " : " + secondString;

    }

    public void AddTimeYPosition(float yPosition)
    {
        LimitTimeText.transform.localPosition = new Vector3(
        LimitTimeText.transform.localPosition.x,
        LimitTimeText.transform.localPosition.y + yPosition,
        LimitTimeText.transform.localPosition.z);

        LimitTimeLineImage.transform.localPosition = new Vector3(
        LimitTimeLineImage.transform.localPosition.x,
        LimitTimeLineImage.transform.localPosition.y + yPosition,
        LimitTimeLineImage.transform.localPosition.z);
    }
}
