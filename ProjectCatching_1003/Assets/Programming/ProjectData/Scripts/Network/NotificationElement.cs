using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationElement{

    public enum EnumNotification { ROPE, RESCUE , DESTROY};
    public EnumNotification NotificationType;

    public string ropeNotification { get; set; }
    public string rescueNotification { get; set; }
}
