using System.Collections;
using System.Collections.Generic;
using UIDataStruct;
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
    
    private Transform dynamic_container;

    private DeviceInfo deviceInfo;

    public DeviceEventType DeviceEvent
    {
        get { return devideEvent; }
        set {
            this.devideEvent = value;
        }
    
    }

    public void setDeviceInfo(DeviceInfo value) {
        this.deviceInfo = value;
    }
    private double t1, t2;
    public void OnPointerClick(PointerEventData eventData) {
        t2 = Time.realtimeSinceStartup;
        if (t2 - t1 < 0.5)
        {
            DeviceDetailControl deviceDetailControl = dynamic_container.GetComponent<DeviceDetailControl>();
            deviceDetailControl.setContainer(this.devideEvent,this.deviceInfo);
        }
        t1 = t2;
    }
    void Start()
    {
        dynamic_container = this.transform.root.Find("deviceContainer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
