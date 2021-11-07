using System.Collections;
using System.Collections.Generic;
using UIDataStruct;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TwinkleControl : MonoBehaviour
{
    HighlightableObject[] children;

    private DeviceEventType devideEvent;

    private Transform dynamic_container;

    private DeviceInfo deviceInfo;

    private float click_time;

    private Image image;

    private Tween tween;

    private float duration = 0.5f;

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
        DOTween.defaultAutoPlay = AutoPlay.None;
        DOTween.defaultAutoKill = true;

        image = this.GetComponent<Image>();
        children = this.GetComponentsInChildren<HighlightableObject>(true);
        dynamic_container = GameObject.Find("deviceContainer").transform;
        click_time = Time.realtimeSinceStartup;
    }

    public void OnMouseDown(){
        if (deviceInfo != null) {
            if (Time.realtimeSinceStartup - click_time < 0.5f)
            {
                DeviceDetailControl deviceDetailControl = dynamic_container.GetComponent<DeviceDetailControl>();
                deviceDetailControl.setContainer(this.devideEvent, this.deviceInfo);
            }
            click_time = Time.realtimeSinceStartup;
        }
    }

    public void OnMouseEnter()
    {
        image.color = new Color(1,1,0,1);
    }

    public void OnMouseExit()
    {
        image.color = Color.white;
    }


    public void setInfo(DeviceEventType type, DeviceInfo value) {
        this.devideEvent = type;
        this.deviceInfo = value;
    }

    public void Twinkle()
    {
        if (tween != null) {
            tween.Pause();
        }

        tween = image.DOColor(new Color(1,1,0,1), duration).SetLoops(10, LoopType.Yoyo).OnComplete(delegate(){
            image.color = Color.white;
        }).OnPause(delegate {
            image.color = Color.white;
        });
        tween.Play();
        /*
        foreach (HighlightableObject child in children) {
            child.ConstantOn();
        }
        StartCoroutine(Shut());*/

    }


    protected IEnumerator Shut()
    {
        yield return new WaitForSeconds(3.0f);
        foreach (HighlightableObject child in children){
            child.ConstantOff();
        }
    }
}
