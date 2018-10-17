using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSystemMessage : MonoBehaviour {

    public int maxSystem;

    public enum EnumSystemCondition {NOT_QUICK_MATCH, SAME_ROOM , EMPTY_NAME};
    public EnumSystemCondition[] systemConditionType;

    public string[] SystemText;
}
