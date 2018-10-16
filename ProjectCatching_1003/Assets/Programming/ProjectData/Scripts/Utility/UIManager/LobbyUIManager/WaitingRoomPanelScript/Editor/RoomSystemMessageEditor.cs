using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomSystemMessage))]
public class RoomSystemMessageEditor : Editor {

    RoomSystemMessage roomSystemMessage;

    private void OnEnable()
    {
        roomSystemMessage = (RoomSystemMessage)target;
    }

    public override void OnInspectorGUI()
    {
        // 데이터를 임시로 받음
        int tempSystemCount = EditorGUILayout.IntField("시스템 수",
            roomSystemMessage.maxSystem);

        if (tempSystemCount >= 0)
        {

            // 이전 데이터들
            int beforeSystemCount = roomSystemMessage.maxSystem;

            RoomSystemMessage.EnumSystemCondition[] tempSystemCondition 
                = roomSystemMessage.systemConditionType;

            string[] tempText = roomSystemMessage.SystemText;


            // 임시로 받은 데이터 지정
            roomSystemMessage.maxSystem = tempSystemCount;


            // 얻어온 값에 따라 생성
            if (tempSystemCount == 0) {
                roomSystemMessage.systemConditionType = null;
                roomSystemMessage.SystemText = null;
            }

            else {
                roomSystemMessage.systemConditionType
                    = new RoomSystemMessage.EnumSystemCondition[tempSystemCount];

                roomSystemMessage.SystemText 
                    = new string[tempSystemCount];
            }



            // 
            for (int i = 0; i < tempSystemCount; i++)
            {
                if (i < beforeSystemCount)
                {
                    roomSystemMessage.systemConditionType[i]
                        = tempSystemCondition[i];

                    roomSystemMessage.SystemText[i] =
                        tempText[i];
                }

                else
                {
                    roomSystemMessage.systemConditionType[i]
                        = new RoomSystemMessage.EnumSystemCondition();
                }
            }

            for (int i = 0; i < tempSystemCount; i++)
            {
                roomSystemMessage.systemConditionType[i] =
                    (RoomSystemMessage.EnumSystemCondition)EditorGUILayout.EnumPopup
                    ("조건",
                    roomSystemMessage.systemConditionType[i]);

                roomSystemMessage.SystemText[i] = 
                    EditorGUILayout.TextArea(roomSystemMessage.SystemText[i]);
            }
        }
    }


    public void SetSystem()
    {

    }
}
