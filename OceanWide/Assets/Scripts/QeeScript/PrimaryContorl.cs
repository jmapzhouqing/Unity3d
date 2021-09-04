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

    public static List<FloorInfo> XHY;
    public static List<FloorInfo> DF;

    private string categoryUrl = "http://" + urlPrefix + "/base/tenant/category/list";
    private static string deviceUrl = "http://" + urlPrefix + "/base/tenant/device/page?current=1&rowCount=100&projectId=";
    private static string deviceUrlSuffix ="&deviceName=3%E5%8F%B7%E6%A5%BC&positionId=";

    public static Dictionary<int, string> categoryDic = new Dictionary<int, string>();
    public static Dictionary<int, List<DeviceInfo>> deviceDic = new Dictionary<int, List<DeviceInfo>>();

    // Start is called before the first frame update
    void Awake()
    {
        tokenControl = new TokenControl();
        tokenControl.setUrl(this.url);
        tokenControl.SetUserName(userName);
        tokenControl.SetPassword(password);
        tokenControl.setTokenUrl1(tokenUrl1);
        tokenControl.setTokenUrl2(tokenUrl2);
        tokenControl.setTokenUrl3(tokenUrl3);
        token = tokenControl.getToken();

        string result_XHY = HTTPServiceControl.GetHttpResponse(floorUrlPrefix + "3", token);

        XHY = JsonMapper.ToObject<List<FloorInfo>>(result_XHY);

        for (int i = XHY.Count - 1; i >= 0; i--)
        {
            if (XHY[i].positionCode.IndexOf("XHY") < 0|| XHY[i].positionCode == "XHY" || XHY[i].positionCode == "XHYDXEC" || XHY[i].positionCode == "XHYSW")
                XHY.Remove(XHY[i]);
        }


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
        //qryDeviceByFloor(3, 26);
    }

    public static void qryDeviceByFloor(int projectId, int positionId) {
        string resultDevice = HTTPServiceControl.GetHttpResponse(deviceUrl + projectId.ToString() + deviceUrlSuffix + positionId.ToString(), token);
        DeviceRows devideRows = JsonMapper.ToObject<DeviceRows>(resultDevice);
        deviceDic.Clear();
        foreach (DeviceInfo item in devideRows.rows) {
            if (deviceDic.ContainsKey(item.categoryId))
            {
                deviceDic[item.categoryId].Add(item);
            }
            else {
                List<DeviceInfo> temp = new List<DeviceInfo>();
                temp.Add(item);
                deviceDic.Add(item.categoryId, temp);
            }
        }
    }



    void OnApplicationQuit()
    {
        websocket.Close();
    }
}


