using System;
using System.Collections;
using System.Collections.Generic;
using UIDataStruct;
using UnityEngine;
using UnityEngine.UI;
using Vuplex.WebView;

public class DeviceDetailControl : MonoBehaviour
{
    public Transform detail_container;
    private RectTransform detail_info_prefab;
    private RectTransform detail_info_item_prefab;
    private RectTransform detail_vedio_prefab;
    private RectTransform detail_lift_prefab;
    private RectTransform detail_grid_prefab;
    private RectTransform detail_info_control_prefab;


    private RectTransform html_prefab;
    // Start is called before the first frame update
    void Start()
    {
        detail_info_prefab = Resources.Load<RectTransform>("UIPrefab/deviceInfoForm");
        detail_info_item_prefab = Resources.Load<RectTransform>("UIPrefab/deviceInfo");
        detail_vedio_prefab = Resources.Load<RectTransform>("UIPrefab/deviceVedioForm");
        detail_lift_prefab = Resources.Load<RectTransform>("UIPrefab/deviceLiftForm");
        detail_info_control_prefab = Resources.Load<RectTransform>("UIPrefab/deviceInfoControl");

        html_prefab = Resources.Load<RectTransform>("UIPrefab/HTML");
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
        if (deviceInfo.categoryId == 3|| (deviceInfo.categoryId == 5&& deviceInfo.monitorList == null)) {
            deviceInfo.rtsp = PrimaryContorl.qryDeviceRstp(deviceInfo.deviceId);
            if (!String.IsNullOrEmpty(deviceInfo.rtsp)) deviceEventType = DeviceEventType.Video;
        } else if (deviceInfo.categoryId == 17) {
            LiftInfo lift = PrimaryContorl.qryLiftRstp(deviceInfo.deviceEUI);
            deviceInfo.rtsp= lift.videoList[0].rtspUrl;
            deviceInfo.roomTemperature = lift.roomTemperature;
            deviceInfo.ytStatusName = lift.ytStatusName;
            deviceInfo.carTopTemperature = lift.carTopTemperature;
            if (!String.IsNullOrEmpty(deviceInfo.rtsp)) deviceEventType = DeviceEventType.Video;
        }
        /*if (deviceInfo.categoryId == 5 && deviceInfo.monitorList != null) {
            for (int i = 0; i < deviceInfo.monitorList.Count; i++)
            {
                if (deviceInfo.monitorList[i].monitorName == "温度") {
                    float temp = float.Parse(deviceInfo.monitorList[i].value);

                }
                else if(deviceInfo.monitorList[i].monitorName == "湿度") {
                    float temp = float.Parse(deviceInfo.monitorList[i].value);
                }
            }
        }*/

        for (int i = 0, child_number = detail_container.childCount; i < child_number; i++)
        {
            GameObject.DestroyImmediate(detail_container.GetChild(0).gameObject);
        }

        if (deviceEventType == DeviceEventType.DeTailInfo)
        {
            RectTransform item = GameObject.Instantiate<RectTransform>(detail_info_prefab, detail_container);
            Transform container = item.Find("view/container").transform;

            if (deviceInfo.monitorList == null)
            {

                if (deviceInfo.doorId == null)
                {
                    RectTransform info_item = GameObject.Instantiate<RectTransform>(detail_info_item_prefab, container);
                    DeviceItem deviceItem = info_item.GetComponentInChildren<DeviceItem>();
                    deviceItem.SetKey("设备名称");
                    deviceItem.SetValue(deviceInfo.deviceEUI);
                }
                else {
                    if (deviceInfo.dynamicEnvironmentDataList == null)
                    {
                        RectTransform info_item3 = GameObject.Instantiate<RectTransform>(detail_info_control_prefab, container);
                        DeviceItemControl deviceItem3 = info_item3.GetComponentInChildren<DeviceItemControl>();
                        deviceItem3.SetKey("操作");
                        deviceItem3.SetValue("打开");
                        deviceItem3.SetSourceCode(deviceInfo.doorId);
                        deviceItem3.setIsDoor();
                    }
                    else
                    {
                        for (int i = 0; i < deviceInfo.dynamicEnvironmentDataList.Count; i++)
                        {
                            RectTransform info_item = GameObject.Instantiate<RectTransform>(detail_info_item_prefab, container);
                            DeviceItem deviceItem = info_item.GetComponentInChildren<DeviceItem>();
                            deviceItem.SetKey(deviceInfo.dynamicEnvironmentDataList[i].serviceName);
                            deviceItem.SetValue(deviceInfo.dynamicEnvironmentDataList[i].statusInformation);
                            if (i % 2 == 1) deviceItem.setColor();
                        }
                    }
                }
            }
            else {
                for (int i = 0; i < deviceInfo.monitorList.Count; i++) {
                    if (deviceInfo.customType==2) {
                        RectTransform info_item3 = GameObject.Instantiate<RectTransform>(detail_info_control_prefab, container);
                        DeviceItemControl deviceItem3 = info_item3.GetComponentInChildren<DeviceItemControl>();
                        deviceItem3.SetKey(deviceInfo.monitorList[i].monitorName);
                        deviceItem3.SetValue("打开");
                        deviceItem3.SetSourceCode(deviceInfo.monitorList[i].historyTable);
                        if (i % 2 == 1) deviceItem3.setColor();
                    }
                    else if (String.IsNullOrEmpty(deviceInfo.monitorList[i].monitorPath)) {
                        RectTransform info_item2 = GameObject.Instantiate<RectTransform>(detail_info_item_prefab, container);
                        DeviceItem deviceItem2 = info_item2.GetComponentInChildren<DeviceItem>();
                        deviceItem2.SetKey(deviceInfo.monitorList[i].monitorName);

                        if (deviceInfo.categoryId == 6 && deviceInfo.monitorList[i].monitorName == "在离线")
                        {
                            string str = deviceInfo.monitorList[i].value == "true" ? "在线" : "离线";
                            deviceItem2.SetValue(str);
                        } else if (deviceInfo.categoryId == 9) {
                            deviceItem2.SetValue(float.Parse(deviceInfo.monitorList[i].value).ToString("F2"));
                        } else if (deviceInfo.categoryId == 16) {
                            if (!string.IsNullOrEmpty(deviceInfo.monitorList[i].value) && deviceInfo.monitorList[i].value.IndexOf(".") > -1) {
                                deviceItem2.SetValue(float.Parse(deviceInfo.monitorList[i].value).ToString("F2"));
                            }
                        }
                        else
                        {
                            deviceItem2.SetValue(deviceInfo.monitorList[i].value);
                        }
                        if (i % 2 == 1) deviceItem2.setColor();
                    }
                    else {
                        RectTransform info_item3 = GameObject.Instantiate<RectTransform>(detail_info_control_prefab, container);
                        DeviceItemControl deviceItem3 = info_item3.GetComponentInChildren<DeviceItemControl>();
                        deviceItem3.SetKey(deviceInfo.monitorList[i].monitorName);
                        deviceItem3.SetValue(deviceInfo.monitorList[i].value);
                        deviceItem3.SetSourceCode(deviceInfo.monitorList[i].historyTable);
                        if (i % 2 == 1) deviceItem3.setColor();
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
            if (deviceInfo.categoryId == 17)
            {

                RectTransform item = GameObject.Instantiate<RectTransform>(detail_lift_prefab, detail_container);

                Transform info = item.transform.Find("container/deviceInfo").transform;
                info.GetChild(1).GetComponent<Text>().text = deviceInfo.ytStatusName;
                info.GetChild(3).GetComponent<Text>().text = deviceInfo.carTopTemperature.ToString();
                info.GetChild(5).GetComponent<Text>().text = deviceInfo.roomTemperature.ToString();
                Button btn = item.GetComponentInChildren<Button>();
                btn.onClick.AddListener(delegate () {
                    this.close();
                });
                //UniversalMediaPlayer control = item.GetComponentInChildren<UniversalMediaPlayer>();
                //control.Path = "http://clips.vorwaerts-gmbh.de/VfE_html5.mp4";
                //control.Path = deviceInfo.rtsp;
                //control.Play();
                VideoControl control = item.GetComponentInChildren<VideoControl>();
                
                control.PlayVideo("ws://222.128.39.16:8866/live?url=" + deviceInfo.rtsp);

                
            }
            else {
                RectTransform item = GameObject.Instantiate<RectTransform>(detail_vedio_prefab, detail_container);
                Button btn = item.GetComponentInChildren<Button>();
                btn.onClick.AddListener(delegate () {
                    this.close();
                });
                //UniversalMediaPlayer control = item.GetComponentInChildren<UniversalMediaPlayer>();
                //control.Path = "http://clips.vorwaerts-gmbh.de/VfE_html5.mp4";
                //control.Path = deviceInfo.rtsp;
                //control.Play();
                VideoControl control = item.GetComponentInChildren<VideoControl>();
                if (deviceInfo.projectId == 3)
                {
                    control.PlayVideo("ws://222.128.39.16:8866/live?url=" + deviceInfo.rtsp);
                }
                else if (deviceInfo.projectId == 4) {
                    control.PlayVideo("ws://222.128.39.25:8866/live?url=" + deviceInfo.rtsp);
                }
                
                
            }
            
        }
        

    }

    public void setContainerHTML(string url) {
        RectTransform item = GameObject.Instantiate<RectTransform>(html_prefab, detail_container);
        Button btn = item.GetComponentInChildren<Button>();
        btn.onClick.AddListener(delegate () {
            this.close();
        });
        CanvasWebViewPrefab control = item.GetComponentInChildren<CanvasWebViewPrefab>();
        control.LoadHTML(url);
    }
}
