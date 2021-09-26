using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrimaryUI : MonoBehaviour
{
    public Text WaterYear;
    public Text PowerYear;

    public Transform alarm_container; 
    private RectTransform alarm_item_prefab;
    // Update is called once per frame
    void Start()
    {
        string waterYear = "", powerYear = "";
        PrimaryContorl.qryTotalEleWaterNum(ref powerYear, ref waterYear);
        WaterYear.text = waterYear;
        PowerYear.text = powerYear;
        alarm_item_prefab = Resources.Load<RectTransform>("UIPrefab/deviceAlarm");
        PrimaryContorl.qryAlarmList();
        for (int i = 0; i < PrimaryContorl.alarmList.Count; i++) {
            RectTransform item = GameObject.Instantiate<RectTransform>(alarm_item_prefab, alarm_container);
            AlarmItem control = item.GetComponentInChildren<AlarmItem>();
            control.setAlarmItem(PrimaryContorl.alarmList[i]);
            if (i % 2 == 1) {
                control.setColor();
            }
        }
    }

   

    
}
