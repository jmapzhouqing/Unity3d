using System;
using System.Collections;
using System.Collections.Generic;
using UIDataStruct;
using UnityEngine;
using UnityEngine.UI;

public class DeviceDetailControl : MonoBehaviour
{
    public Transform detail_container;
    private RectTransform detail_info_prefab;
    private RectTransform detail_info_item_prefab;
    private RectTransform detail_vedio_prefab;
    private RectTransform detail_grid_prefab;
    // Start is called before the first frame update
    void Start()
    {
        detail_info_prefab = Resources.Load<RectTransform>("UIPrefab/deviceInfoForm");
        detail_info_item_prefab = Resources.Load<RectTransform>("UIPrefab/deviceInfo");
        detail_vedio_prefab = Resources.Load<RectTransform>("UIPrefab/deviceVedioForm");
    }

    

    public void close()
    {
        //this.gameObject.SetActive(false);
        for (int i = 0, child_number = detail_container.childCount; i < child_number; i++)
        {
            GameObject.DestroyImmediate(detail_container.GetChild(0).gameObject);
        }
    }

    public void setContainer(DeviceEventType deviceEventType,DeviceInfo deviceInfo) 
    {
        for (int i = 0, child_number = detail_container.childCount; i < child_number; i++)
        {
            GameObject.DestroyImmediate(detail_container.GetChild(0).gameObject);
        }

        if (deviceEventType == DeviceEventType.DeTailInfo)
        {
            RectTransform item = GameObject.Instantiate<RectTransform>(detail_info_prefab, detail_container);
            Transform container = item.Find("container").transform;

            if (deviceInfo.monitorList == null)
            {
                RectTransform info_item = GameObject.Instantiate<RectTransform>(detail_info_item_prefab, container);
                DeviceItem deviceItem = info_item.GetComponentInChildren<DeviceItem>();
                deviceItem.SetKey("设备名称");
                deviceItem.SetValue(deviceInfo.deviceName);
            }
            else {
                    for (int i = 0; i < deviceInfo.monitorList.Count; i++) {
                        if (1==1) {
                            RectTransform info_item2 = GameObject.Instantiate<RectTransform>(detail_info_item_prefab, container);
                            DeviceItem deviceItem2 = info_item2.GetComponentInChildren<DeviceItem>();
                            deviceItem2.SetKey("MonitorName");
                            deviceItem2.SetValue(deviceInfo.monitorList[i].monitorName);

                        }
                    }
                }

            Button btn = item.GetComponentInChildren<Button>();
            btn.onClick.AddListener(delegate () {
                this.close();
            });
        }
        else if (deviceEventType == DeviceEventType.RecordGrid)
        {

        }
        else if (deviceEventType == DeviceEventType.Video)
        {
            RectTransform item = GameObject.Instantiate<RectTransform>(detail_vedio_prefab, detail_container);
            UniversalMediaPlayer control = item.GetComponentInChildren<UniversalMediaPlayer>();
            //control.Path = "http://clips.vorwaerts-gmbh.de/VfE_html5.mp4";
            control.Path = deviceInfo.rtsp;
            control.Play(); 
            Button btn = item.GetComponentInChildren<Button>();
            btn.onClick.AddListener(delegate () {
                this.close();
            });
        }
        

    }
}
