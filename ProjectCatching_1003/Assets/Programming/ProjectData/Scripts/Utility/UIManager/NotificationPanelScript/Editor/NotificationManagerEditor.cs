using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NotificationManager))]
public class NotificationManagerEditor : Editor {

    NotificationManager notificationManager;
    private void OnEnable()
    {
        notificationManager = (NotificationManager)target;
    }

    public override void OnInspectorGUI()
    {

        // Notification 동적할당
        DynamicNotifications();


        int count = notificationManager.notificationElements.Length;

        for (int i = 0; i < count; i++)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("---  알림 선택 ---");

            NotificationElement notificationElement =
                notificationManager.notificationElements[i];

            notificationElement.NotificationType =
                (NotificationElement.EnumNotification)EditorGUILayout.EnumPopup
                    ("타입 선정",
                    notificationElement.NotificationType);

            SettingNotification(notificationElement);

        }

        
    }

    private void DynamicNotifications()
    {
        // 동적할당 조건 - 변수가 바뀌거나, 기존에 없는경우.
        int beforeMount = notificationManager.maxNotification;

        int nowMount =
            EditorGUILayout.IntField("알림 타입 갯수 설정", notificationManager.maxNotification);



        notificationManager.maxNotification = nowMount;

        // 값이 커지면 동적할당.
        // 1 이상인경우 동적할당
        // 동적할당 시 기존 값은 전달해야한다.
        if (nowMount >= 0)
        {

            // 1. 기존 값 백업
            NotificationElement[] tempNotificationElement = notificationManager.notificationElements;

            // 2. 새 값 생성
            notificationManager.notificationElements = new NotificationElement[nowMount];

            // 3. 0이면 null처리
            if (nowMount == 0) notificationManager.notificationElements = null;

            for (int i = 0; i < nowMount; i++)
            {

                // 기존 값이 있으면 기존 값 사용
                if (i < beforeMount)
                    notificationManager.notificationElements[i] = tempNotificationElement[i];

                // 새 값이면 할당
                else
                    notificationManager.notificationElements[i] = new NotificationElement();
            }
        }
    }

    private void SettingNotification(NotificationElement notificationElement)
    {
        switch (notificationElement.NotificationType)
        {
            case NotificationElement.EnumNotification.ROPE:
                UseRopeInspector(notificationElement);
                break;

            case NotificationElement.EnumNotification.RESCUE:
                break;

            case NotificationElement.EnumNotification.DESTROY:
                break;
        }

    }

    void UseRopeInspector(NotificationElement notificationElement)
    {
        EditorGUILayout.LabelField("Rope 구출 내용");
        notificationElement.ropeNotification = 
            EditorGUILayout.TextArea(notificationElement.ropeNotification);
    }

    void UseRescueInspector(NotificationElement notificationElement)
    {
        EditorGUILayout.LabelField("Rescue 구출 내용");
        notificationElement.rescueNotification =
            EditorGUILayout.TextArea(notificationElement.rescueNotification);
    }

    void UseDestroyInspector(NotificationElement notificationElement)
    {
        EditorGUILayout.LabelField("Destroy 내용");
    }
}
