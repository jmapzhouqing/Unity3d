using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIDataStruct;

public class AlarmItem : MonoBehaviour
{
    public Text deviceName;
    public Text monitorName;
    public Text categoryName;
    public Text descName;

    private Color color;

    void Start()
    {
        this.color = this.transform.GetComponent<RawImage>().color;
    }

    public void setAlarmItem(AlarmGrid item) {
        deviceName.text = item.deviceName;
        monitorName.text = item.monitorName;
        categoryName.text = item.categoryName;
        descName.text = item.descName;
    }

    public void setColor() {
        this.transform.GetComponent<RawImage>().color = new Color(this.color.r, this.color.g, this.color.b, 0);
    }



}
