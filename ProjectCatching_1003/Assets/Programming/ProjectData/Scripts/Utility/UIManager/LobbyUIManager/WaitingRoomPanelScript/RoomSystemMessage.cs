using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class RoomSystemMessage : MonoBehaviour {

    public int maxSystem;

    public enum EnumSystemCondition {NOT_QUICK_MATCH, SAME_ROOM , EMPTY_NAME , EMPTY_ROOM_NAME , NOT_DEVELOP};
    public EnumSystemCondition[] systemConditionType; 

    public string[] SystemText;
}
