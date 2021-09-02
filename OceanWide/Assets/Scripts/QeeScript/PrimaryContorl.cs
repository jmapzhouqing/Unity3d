using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using httpTool;
using System;

public class PrimaryContorl : MonoBehaviour
{
    public RectTransform devive_container;
    private RectTransform deviveItem_prefab;
    public static WebSocketControl websocket = new WebSocketControl();
    private TokenControl tokenControl;
    public string token;
    public static string urlPrefix = "qy_uat.civicsquare.com.cn/saascloud/saas";
    private string url = "http://" + urlPrefix + "/sso/servlet/login?wk=1";
    private string tokenUrl1 = "http://" + urlPrefix + "/sso/servlet/cookie?t=";
    private string tokenUrl2 = "http://" + urlPrefix + "/tenant/manager/findCurrentTenantList";
    private string tokenUrl3 = "http://" + urlPrefix + "/sso/param/decided/tenant";
    public static string userName = "zg";
    public static string password = "000000";
    private string subscribeMsg = "Subscribe:FL_SHS_leftrun,FL_SHS_rightrun,FL_SHS_highlevel,FL_SHS_lowlevel,WSSTSP_FL_SHS_leftrun_ResetAcc,WSSTSP_FL_SHS_leftrun_AccTime,WSSTSP_FL_SHS_leftrun_VFDSwitchSetCount,WSSTSP_FL_SHS_leftrun_VFDSwitchSetCountReset,WSSTSP_FL_SHS_rightrun_ResetAcc,WSSTSP_FL_SHS_rightrun_AccTime,WSSTSP_FL_SHS_rightrun_VFDSwitchSetCount,WSSTSP_FL_SHS_rightrun_VFDSwitchSetCountReset,FL_SHS_leftfault,FL_SHS_rightfault,FL_A43_leftrun,FL_A43_leftfault,FL_A43_rightrun,FL_A43_highlevel,FL_A43_lowlevel,WSSTSP_FL_A43_leftrun_ResetAcc,WSSTSP_FL_A43_leftrun_AccTime,WSSTSP_FL_A43_leftrun_VFDSwitchSetCount,WSSTSP_FL_A43_leftrun_VFDSwitchSetCountReset,WSSTSP_FL_A43_rightrun_ResetAcc,WSSTSP_FL_A43_rightrun_AccTime,WSSTSP_FL_A43_rightrun_VFDSwitchSetCount,WSSTSP_FL_A43_rightrun_VFDSwitchSetCountReset,FL_A43_rightfault,FL_A53_leftrun,FL_A53_leftfault,FL_A53_rightrun,FL_A53_rightfault,FL_A53_highlevel,FL_A53_lowlevel,WSSTSP_FL_A53_leftrun_ResetAcc,WSSTSP_FL_A53_leftrun_AccTime,WSSTSP_FL_A53_leftrun_VFDSwitchSetCount,WSSTSP_FL_A53_leftrun_VFDSwitchSetCountReset,WSSTSP_FL_A53_rightrun_ResetAcc,WSSTSP_FL_A53_rightrun_AccTime,WSSTSP_FL_A53_rightrun_VFDSwitchSetCount,WSSTSP_FL_A53_rightrun_VFDSwitchSetCountReset,sensor_0005_RH1,sensor_0005_temp,sensor_0005_wateralarm,sensor_0006_RH,sensor_0006_temp,sensor_0006_wateralarm,sensor_0007_RH,sensor_0007_temp,sensor_0007_wateralarm,sensor_0008_RH,sensor_0008_temp,sensor_0008_wateralarm1,sensor_0009_RH,sensor_0009_temp,sensor_0009_wateralarm,sensor_0010_RH,sensor_0010_temp,sensor_0010_wateralarm,sensor_0011_RH,sensor_0011_temp,sensor_0011_wateralarm,sensor_0012_RH,sensor_0012_temp,sensor_0012_wateralarm,RF_0001_onoffstatus1,RF_0001_workingconditions1,RF_0001_alarmstatus1,RF_0001_timelimitstate1,RF_0001_preheatcondition1,RF_0001_digitaloutput1,RF_0001_digitalinput1,RF_0001_coolingbackwater1,RF_0001_frozenoutletwater1,RF_0001_refrigeratedbackwater1,RF_0001_exhausttemperature1,RF_0001_evaporatingpressure1,RF_0001_exhaustpressure1,RF_0001_theoilpressure1,RF_0001_compressorcurrent1,RF_0001_outlettemperature1,RF_";
    // Start is called before the first frame update
    void Start()
    {
        /*tokenControl = new TokenControl();
        tokenControl.setUrl(this.url);
        tokenControl.SetUserName(userName);
        tokenControl.SetPassword(password);
        tokenControl.setTokenUrl1(tokenUrl1);
        tokenControl.setTokenUrl2(tokenUrl2);
        tokenControl.setTokenUrl3(tokenUrl3);
        token = tokenControl.getToken();*/
        websocket.SetUrl("");
        websocket.SetSubscribe(this.subscribeMsg);
        websocket.receivePross += pointDataReceive;
        websocket.Connect();
        //websocket.SendMsg("");
        devive_container = Resources.Load<RectTransform>("UIPrefab/deviceListContainer");
        deviveItem_prefab = Resources.Load<RectTransform>("UIPrefab/deviceItem");
    }

   

    private void pointDataReceive(object sender, string msg)
    {
        
    }


    void OnApplicationQuit()
    {
        websocket.Close();
    }
}
