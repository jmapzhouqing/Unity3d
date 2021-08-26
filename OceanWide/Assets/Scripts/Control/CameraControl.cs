using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MouseControl {
    None,
    LeftClick,
    LeftDbclick,
    LeftDrag,
    RightClick,
    RightDbClick,
    RightDrag,
    WheelClick,
    WhellDbClick,
    WhellDrag
}
public class CameraControl : MonoBehaviour
{
    MouseControl control_state = MouseControl.None;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        Debug.Log(control_state);
    }

    void OnGUI(){
        Event mouse_event = Event.current;
        if (mouse_event.isMouse && mouse_event.button == 0 && mouse_event.type == EventType.MouseDown && mouse_event.clickCount == 1)
        {
            control_state = MouseControl.LeftClick;
        }
        else if (mouse_event.isMouse && mouse_event.button == 0 && mouse_event.type == EventType.MouseDown && mouse_event.clickCount == 2)
        {
            control_state = MouseControl.LeftDbclick;
        }
        else if (mouse_event.isMouse && mouse_event.button == 0 && mouse_event.type == EventType.MouseDrag)
        {
            control_state = MouseControl.LeftDrag;
        }
        else {
            control_state = MouseControl.None;
        }
    }
}
