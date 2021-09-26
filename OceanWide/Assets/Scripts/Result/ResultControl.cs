using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum DeviceEventType { 
    Video=0,
    RecordGrid=1,
    DeTailInfo=2
}
public class ResultControl : MonoBehaviour,IPointerClickHandler
{
    // Start is called before the first frame update

    private DeviceEventType devideEvent;

    public DeviceEventType DeviceEvent
    {
        get { return devideEvent; }
        set {
            this.devideEvent = value;
        }
    
    }
    private double t1, t2;
    public void OnPointerClick(PointerEventData eventData) {
        t2 = Time.realtimeSinceStartup;
        if (t2 - t1 < 0.2)
        {
            if (devideEvent == DeviceEventType.DeTailInfo) {

            } else if (devideEvent == DeviceEventType.RecordGrid) {

            } else if (devideEvent == DeviceEventType.Video) { 
            
            }
        }
        t1 = t2;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
