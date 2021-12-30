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

    private CameraControl camera_control;


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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            if (GameObject.Find(this.deviceInfo.deviceEUI)==null) {
                if(deviceInfo.customType != 1) { 
                    DeviceDetailControl deviceDetailControl = dynamic_container.GetComponent<DeviceDetailControl>();
                    deviceDetailControl.setContainer(this.devideEvent, this.deviceInfo);
                }
            }
            else {
                this.Location(this.deviceInfo.deviceEUI);
            }
            //DeviceDetailControl deviceDetailControl = dynamic_container.GetComponent<DeviceDetailControl>();
            //deviceDetailControl.setContainer(this.devideEvent, this.deviceInfo);
        }
    }
    void Start(){
        dynamic_container = this.transform.root.Find("deviceContainer");

        camera_control = FindObjectOfType<CameraControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Location(string id) {
        GameObject target = GameObject.Find(id);
        target.GetComponent<TwinkleControl>()?.Twinkle();
        target.GetComponent<TwinkleControl>()?.setInfo(this.devideEvent,this.deviceInfo);
        camera_control.Location(target.transform);
    }
}
