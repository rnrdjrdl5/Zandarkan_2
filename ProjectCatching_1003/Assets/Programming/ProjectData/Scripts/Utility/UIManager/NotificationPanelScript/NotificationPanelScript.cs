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

    GameObject NotificationBackGround;
    RectTransform NotificationBackGroundRectTransfrom;

    UIEffect NotifiUIEffect;

    public void InitData()
    {
        uIManager = UIManager.GetInstance();
        uICanvas = uIManager.UICanvas;

        NotificationPanel = uICanvas.transform.Find("NotificationPanel").gameObject;
        NotificationText = NotificationPanel.transform.Find("NotificationText").gameObject;
        NotificationTextText = NotificationText.GetComponent<Text>();

        NotificationBackGround = NotificationPanel.transform.Find("NotificationBackGround").gameObject;
        NotificationBackGroundRectTransfrom = NotificationBackGround.GetComponent<RectTransform>();

        NotifiUIEffect = new UIEffect();


    }

    public void SetActive(bool isActive)
    {
        NotificationText.SetActive(isActive);
        NotificationPanel.SetActive(isActive);
        NotificationBackGround.SetActive(isActive);
    }

    public void SetBackGroundY(int y)
    {

        NotificationBackGroundRectTransfrom.sizeDelta = new Vector2(
            NotificationBackGroundRectTransfrom.sizeDelta.x,
            y);
    }

    public void MoveAction()
    {
        NotifiUIEffect.AddMoveEffectNode(NotificationPanel,
           Vector2.right * -NotificationPanel.GetComponent<RectTransform>().rect.width,
           Vector2.zero, 0.5f);

        NotifiUIEffect.AddWaitEffectNode(2.0f);

        NotifiUIEffect.AddMoveEffectNode(NotificationPanel,
            Vector2.zero,
            Vector2.right * -NotificationPanel.GetComponent<RectTransform>().rect.width,
            0.5f);

        UIManager.GetInstance().UpdateEvent += NotifiUIEffect.EffectEvent;
    }
}
