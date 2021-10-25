using System.Collections;
using System.Collections.Generic;
using UIDataStruct;
using UnityEngine;

public class TwinkleControl : MonoBehaviour
{
    HighlightableObject[] children;

    private DeviceEventType devideEvent;

    private Transform dynamic_container;

    private DeviceInfo deviceInfo;

    public DeviceEventType DeviceEvent
    {
        get { return devideEvent; }
        set
        {
            this.devideEvent = value;
        }

    }

    // Start is called before the first frame update
    void Awake(){
        children = this.GetComponentsInChildren<HighlightableObject>(true);
        dynamic_container = GameObject.Find("deviceContainer").transform;
    }

    public void OnMouseDown()
    {
        DeviceDetailControl deviceDetailControl = dynamic_container.GetComponent<DeviceDetailControl>();
        deviceDetailControl.setContainer(this.devideEvent, this.deviceInfo);
    }


    public void setInfo(DeviceEventType type, DeviceInfo value) {
        this.devideEvent = type;
        this.deviceInfo = value;
    }

    public void Twinkle()
    {
        foreach (HighlightableObject child in children) {
            child.ConstantOn();
        }
        StartCoroutine(Shut());
    }


    protected IEnumerator Shut()
    {
        yield return new WaitForSeconds(3.0f);
        foreach (HighlightableObject child in children){
            child.ConstantOff();
        }
    }
}
