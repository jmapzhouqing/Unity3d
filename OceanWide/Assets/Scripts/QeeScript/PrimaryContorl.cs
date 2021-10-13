using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using httpTool;
using System;
using UIDataStruct;
using LitJson;
using System.Linq;

public class PrimaryContorl : MonoBehaviour
{
    public static WebSocketControl websocket = new WebSocketControl();
    private TokenControl tokenControl;
    public static string token;
    private static string urlPrefix = "uat.qywlsp.com.cn/saascloud/saas";
    private string url = "http://" + urlPrefix + "/sso/servlet/login?wk=1";
    private string tokenUrl1 = "http://" + urlPrefix + "/sso/servlet/cookie?t=";
    private string tokenUrl2 = "http://" + urlPrefix + "/tenant/manager/findCurrentTenantList";
    private string tokenUrl3 = "http://" + urlPrefix + "/sso/param/decided/tenant";
    private string userName = "qy";
    private string password = "000000";

    private string floorUrlPrefix = "http://" + urlPrefix + "/base/tenant/position/findPositionTree?projectId=";

    public static List<FloorInfo> LHY;
    public static List<FloorInfo> DF;

    private string categoryUrl = "http://" + urlPrefix + "/base/tenant/category/list";
    private static string deviceUrl = "http://" + urlPrefix + "/base/tenant/device/page?current=1&rowCount=100&projectId=";
    private static string deviceUrlSuffix = "&deviceName=3%E5%8F%B7%E6%A5%BC&positionId=";

    public static Dictionary<int, string> categoryDic = new Dictionary<int, string>();
    public static Dictionary<int, List<DeviceInfo>> deviceDic = new Dictionary<int, List<DeviceInfo>>();

    public static bool isDevice = false;

    public static GameObject dialog;

    public static string waterEleUrl = "http://" + urlPrefix + "/camera/camera/consumption/level1?pageNum=1&pageSize=10&energytype=";
    public static string alarmUrl = "http://" + urlPrefix + "/nanhai/tenant/realtime/alarm?level=";
    public static string positionIdStr = "000,78,77,22,23,63,62,000";
    public static List<AlarmGrid> alarmList=new List<AlarmGrid>();
    // Start is called before the first frame update
    void Awake()
    {
        dialog = this.transform.Find("messageBox").gameObject;

        tokenControl = new TokenControl();
        tokenControl.setUrl(this.url);
        tokenControl.SetUserName(userName);
        tokenControl.SetPassword(password);
        tokenControl.setTokenUrl1(tokenUrl1);
        tokenControl.setTokenUrl2(tokenUrl2);
        tokenControl.setTokenUrl3(tokenUrl3);
        token = tokenControl.getToken();

        string result_LHY = HTTPServiceControl.GetHttpResponse(floorUrlPrefix + "3", token);

        LHY = JsonMapper.ToObject<List<FloorInfo>>(result_LHY);

        for (int i = LHY.Count - 1; i >= 0; i--)
        {
            if (LHY[i].positionCode.IndexOf("LHY") < 0|| LHY[i].positionCode == "LHY" || LHY[i].positionCode == "LHYDXEC" || LHY[i].positionCode == "LHYSW"||LHY[i].positionCode== "LHYDXYC")
                LHY.Remove(LHY[i]);
        }

        LHY.Reverse();
        string result_DF= HTTPServiceControl.GetHttpResponse(floorUrlPrefix + "4", token);

        DF = JsonMapper.ToObject<List<FloorInfo>>(result_DF);

        for (int i = DF.Count - 1; i >= 0; i--)
        {
            if (DF[i].positionCode.IndexOf("DFHS") <0|| DF[i].positionCode == "DFHS")
                DF.Remove(DF[i]);
        }

        DF.Reverse();

        string result_category= HTTPServiceControl.GetHttpResponse(this.categoryUrl, token);

        List<CategoryInfo> dic_category = JsonMapper.ToObject<List<CategoryInfo>>(result_category);

        foreach (CategoryInfo item in dic_category) {

            categoryDic.Add(item.categoryId, item.categoryName);
        }
       

    }

    public static void qryDeviceByFloor(int projectId, int positionId) {
        isDevice = false;
        string resultDevice = HTTPServiceControl.GetHttpResponse(deviceUrl + projectId.ToString() + deviceUrlSuffix + positionId.ToString(), token);
        DeviceRows devideRows = JsonMapper.ToObject<DeviceRows>(resultDevice);
        deviceDic.Clear();
        foreach (DeviceInfo item in devideRows.rows) {
            if (deviceDic.ContainsKey(item.categoryId))
            {
                deviceDic[item.categoryId].Add(item);
            }
            else {
                isDevice = true;
                List<DeviceInfo> temp = new List<DeviceInfo>();
                temp.Add(item);
                deviceDic.Add(item.categoryId, temp);
            }
        }
        if (positionId == 77 || positionId == 78) {
            string resultDeviceEC;
            if (positionId == 77)
            {
                resultDeviceEC = HTTPServiceControl.GetHttpResponse(deviceUrl + projectId.ToString() + deviceUrlSuffix + "30", token);
            }
            else {
                resultDeviceEC = HTTPServiceControl.GetHttpResponse(deviceUrl + projectId.ToString() + deviceUrlSuffix + "25", token);
            }
            DeviceRows devideRowsEC = JsonMapper.ToObject<DeviceRows>(resultDeviceEC);
            foreach (DeviceInfo item in devideRowsEC.rows)
            {
                if (deviceDic.ContainsKey(item.categoryId))
                {
                    deviceDic[item.categoryId].Add(item);
                }
                else
                {
                    isDevice = true;
                    List<DeviceInfo> temp = new List<DeviceInfo>();
                    temp.Add(item);
                    deviceDic.Add(item.categoryId, temp);
                }
            }
        }
    }


    public static void qryTotalEleWaterNum(ref string powerYear,ref string waterYear) {
        string result = HTTPServiceControl.GetHttpResponse(waterEleUrl, token);
        WaterPower waterPower = JsonMapper.ToObject<WaterPower>(result);
        waterYear = waterPower.data.waterYear;
        powerYear = waterPower.data.powerYear;
    }

    public static void qryAlarmList()
    {
        alarmList.Clear();
        string result = HTTPServiceControl.GetHttpResponse(alarmUrl + "3", token);
        List<AlarmInfo> alarmInfoList = JsonMapper.ToObject<List<AlarmInfo>>(result);
        foreach (AlarmInfo alarmInfo in alarmInfoList) {
            if (positionIdStr.IndexOf(","+alarmInfo.positionId.ToString()+",") > -1) {
                int idx = alarmInfo.deviceName.IndexOf("_");
                AlarmGrid alarmGrid = new AlarmGrid();
                alarmGrid.deviceName = alarmInfo.deviceName.Substring(idx + 1);
                alarmGrid.monitorName = alarmInfo.monitorName;
                alarmGrid.categoryName = alarmInfo.categoryName;
                alarmGrid.descName = alarmInfo.descName + "_" + alarmInfo.deviceName.Substring(0, idx);
                alarmList.Add(alarmGrid);
            }
        }
    }



    void OnApplicationQuit()
    {
        websocket.Close();
    }
}


