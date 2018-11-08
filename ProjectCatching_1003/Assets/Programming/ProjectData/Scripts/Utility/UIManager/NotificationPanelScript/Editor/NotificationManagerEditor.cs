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
        notificationManager.maxMessageTime = EditorGUILayout.FloatField("시스템 창 지속시간",
            notificationManager.maxMessageTime);

        EditorGUILayout.LabelField("쥐가 잡혔을 때");
        notificationManager.RopeNotification =
            EditorGUILayout.TextArea(notificationManager.RopeNotification);

        EditorGUILayout.LabelField("쥐가 살아났을 때");
        notificationManager.RescueNotification =
            EditorGUILayout.TextArea(notificationManager.RescueNotification);


        DynamicRestNotification();
    }

    

    private void DynamicRestNotification()
    {
        // 동적할당 조건 - 변수가 바뀌거나, 기존에 없는경우.
        int beforeMount = notificationManager.maxRestNotification;

        int nowMount =
            EditorGUILayout.IntField("최대 레스토랑 무너짐 메세지 개수", notificationManager.maxRestNotification);



        notificationManager.maxRestNotification = nowMount;

        string[] tempString = new string[nowMount];
        int[] tempAtLeastCount = new int[nowMount];
        bool[] tempBool = new bool[nowMount];
        if (nowMount >= 0)
        {

            for (int i = 0; i < nowMount; i++)
            {
                if (i < beforeMount) {
                    tempAtLeastCount[i] =
                        EditorGUILayout.IntField("특수 지점", notificationManager.AtLeastRestCount[i]);
                    tempString[i] = EditorGUILayout.TextArea(notificationManager.RestNotifications[i]);
                    tempBool[i] = notificationManager.isUseRestMessage[i];
                    
                }
                else {
                    tempAtLeastCount[i] =
                        EditorGUILayout.IntField("특수 지점", tempAtLeastCount[i]);
                    tempString[i] = EditorGUILayout.TextArea(tempString[i]);
                    tempBool[i] = false;
                }
                
            }
        }

        notificationManager.RestNotifications = tempString;
        notificationManager.AtLeastRestCount = tempAtLeastCount;
        notificationManager.isUseRestMessage = tempBool;
    }


   
}
