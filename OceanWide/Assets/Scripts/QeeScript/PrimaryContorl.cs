using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using httpTool;
using System;
using UIDataStruct;
using LitJson;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading.Tasks;

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
    private static string deviceUrl = "http://" + urlPrefix + "/base/tenant/device/page?current=1&rowCount=10000&projectId=";
    private static string deviceUrlSuffix = "&deviceName=3%E5%8F%B7%E6%A5%BC&positionId=";

    public static Dictionary<int, string> categoryDic = new Dictionary<int, string>();
    public static Dictionary<int, List<DeviceInfo>> deviceDic = new Dictionary<int, List<DeviceInfo>>();

    public static bool isDevice = false;

    public static GameObject dialog;

    public static string waterEleUrl = "http://" + urlPrefix + "/camera/camera/consumption/level1?pageNum=1&pageSize=10&energytype=";
    public static string alarmUrl = "http://" + urlPrefix + "/nanhai/tenant/realtime/alarm?level=";
    public static string positionIdStrLHY = "000,78,77,22,23,24,000";
    public static string positionIdStrDF = "000,47,48,59,49,87,88,89,90,91,92,93,94,95,50,000";
    public static List<AlarmGrid> alarmList = new List<AlarmGrid>();

    public static string deviceDetailUrl = "http://" + urlPrefix + "/base/tenant/device/page?current=1&rowCount=40000";
    public static Dictionary<int, DeviceDetailData> deviceInfo = new Dictionary<int, DeviceDetailData>();
    //public static string LHYDevice = "{\"22\":[\"SPEGCU_0038\",\"SPEGCU_0039\"],\"78\":[\"SPVSCMSP_0559\",\"SPVSCMSP_0497\",\"SPVSCMSP_0490\",\"SPVSCMSP_0571\",\"SPVSCMSP_0561\",\"SPVSCMSP_0568\",\"SPVSCMSP_0498\",\"SPVSCMSP_0567\",\"SPVSCMSP_0569\",\"SPVSCMSP_0560\",\"SPVSCMSP_0563\",\"SPVSCMSP_0557\",\"SPVSCMSP_0562\",\"SPVSCMSP_0564\",\"SPVSCMSP_0555\",\"SPVSCMSP_0491\",\"SPVSCMSP_0556\",\"SPVSCMSP_0572\",\"SPVSCMSP_0558\",\"SPVSCMSP_0570\",\"SPVSCMSP_0566\",\"WEECEE_0112\",\"WEECEE_0113\",\"WEECEE_0114\",\"WEECEE_0115\",\"WEECEE_0116\"],\"77\":[\"SPVSCMSP_0535\",\"SPVSCMSP_0495\",\"SPVSCMSP_0540\",\"SPVSCMSP_0488\",\"SPVSCMSP_0553\",\"SPVSCMSP_0565\",\"SPVSCMSP_0541\",\"SPVSCMSP_0552\",\"SPVSCMSP_0489\",\"SPVSCMSP_0543\",\"SPVSCMSP_0545\",\"SPVSCMSP_0542\",\"SPVSCMSP_0549\",\"SPVSCMSP_0546\",\"SPVSCMSP_0551\",\"SPVSCMSP_0537\",\"SPVSCMSP_0554\",\"SPVSCMSP_0548\",\"SPVSCMSP_0544\",\"SPVSCMSP_0538\",\"SPVSCMSP_0539\",\"SPVSCMSP_0496\",\"SPVSCMSP_0536\",\"WEECWM_0007\",\"WEECWM_0008\"]}";
    public static string LHYDevice = "{\"78\":[\"SPVSCMSP_0491\",\"SPVSCMSP_0555\",\"SPVSCMSP_0556\",\"SPVSCMSP_0557\",\"SPVSCMSP_0558\",\"SPVSCMSP_0560\",\"SPVSCMSP_0562\",\"SPVSCMSP_0563\",\"SPVSCMSP_0566\",\"SPVSCMSP_0567\",\"SPVSCMSP_0564\",\"SPVSCMSP_0568\",\"SPVSCMSP_0569\",\"SPVSCMSP_0570\",\"SPVSCMSP_0572\",\"SPVSCMSP_0580\",\"SPVSCMSP_0498\",\"SPVSCMSP_0559\",\"SPVSCMSP_0561\",\"SPVSCMSP_0565\",\"SPVSCMSP_0571\",\"SPVSCMSP_0497\",\"SPVSCMSP_0490\",\"WEECEE_0112\",\"WEECEE_0115\",\"WEECEE_0114\",\"WEECEE_0113\",\"WEECEE_0116\"],\"77\":[\"SPVSCMSP_0541\",\"SPVSCMSP_0548\",\"SPVSCMSP_0549\",\"SPVSCMSP_0550\",\"SPVSCMSP_0551\",\"SPVSCMSP_0552\",\"SPVSCMSP_0554\",\"SPVSCMSP_0496\",\"SPVSCMSP_0538\",\"SPVSCMSP_0539\",\"SPVSCMSP_0579\",\"SPVSCMSP_0489\",\"SPVSCMSP_0542\",\"SPVSCMSP_0543\",\"SPVSCMSP_0544\",\"SPVSCMSP_0545\",\"SPVSCMSP_0546\",\"SPVSCMSP_0536\",\"SPVSCMSP_0537\",\"SPVSCMSP_0488\",\"SPVSCMSP_0547\",\"SPVSCMSP_0553\",\"SPVSCMSP_0495\",\"SPVSCMSP_0540\",\"SPVSCMSP_0535\",\"WEECWM_0008\",\"WEECWM_0007\"],\"22\":[\"SPEGCU_0039\",\"SPEGCU_0038\"],\"23\":[\"SPVSCMSP_0494\",\"SPVSCMSP_0487\",\"SPVSCMSP_0493\",\"SPVSCMSP_0492\",\"SPVSCMSP_0500\",\"SPVSCMSP_0499\"]}";
    public static string DFDevice = "{\"49\":[\"SPVSCMSP_0998\",\"SPVSCMSP_0999\",\"SPVSCMSP_1000\",\"SPVSCMSP_1001\",\"SPVSCMSP_1002\",\"SPVSCMSP_1003\",\"SPVSCMSP_1004\",\"SPVSCMSP_1005\",\"SPVSCMSP_1006\",\"SPVSCMSP_1007\",\"SPVSCMSP_1008\"],\"B2\":[\"SPVSCMSP_0834\",\"SPVSCMSP_1191\",\"SPVSCMSP_1192\",\"SPVSCMSP_1196\",\"SPVSCMSP_1193\",\"SPVSCMSP_0875\",\"SPVSCMSP_0877\",\"SPVSCMSP_1198\",\"SPVSCMSP_0835\",\"SPVSCMSP_1195\",\"SPVSCMSP_1194\",\"SPVSCMSP_0983\",\"SPVSCMSP_0984\",\"SPVSCMSP_0985\",\"SPVSCMSP_0986\",\"SPVSCMSP_0987\",\"SPVSCMSP_0988\",\"SPVSCMSP_0989\",\"SPVSCMSP_0990\",\"SPVSCMSP_0991\",\"SPVSCMSP_0992\",\"SPVSCMSP_0993\",\"ACATFU_0018\",\"ACATFU_0020\",\"ACATFU_0022\",\"ACATFU_0024\",\"ACVTFN_0001\",\"ACVTFN_0002\",\"ACVTFN_0003\",\"ACVTFN_0004\",\"ACVTFN_005\",\"ACVTFN_006\",\"ACVTFN_007\",\"ACVTFN_008\",\"WEECWM_0017\",\"WEECWM_0018\"],\"59\":[\"SPVSCMSP_0978\",\"SPVSCMSP_0976\",\"SPVSCMSP_0971\",\"SPVSCMSP_0973\",\"SPVSCMSP_0980\",\"SPVSCMSP_0970\",\"SPVSCMSP_0974\",\"SPVSCMSP_0982\",\"SPVSCMSP_0975\",\"SPVSCMSP_0979\",\"SPVSCMSP_0972\",\"SPVSCMSP_0981\",\"SPVSCMSP_0977\"],\"11F\":[\"SPVSCMSP_0924\",\"SPVSCMSP_0925\",\"SPVSCMSP_0926\",\"SPVSCMSP_0927\",\"SPVSCMSP_0928\",\"SPVSCMSP_0929\",\"SPVSCMSP_0930\",\"SPVSCMSP_0994\",\"SPVSCMSP_0995\",\"SPVSCMSP_0996\",\"SPVSCMSP_0997\"]}";
    public static Dictionary<string, List<string>> LHYDeviceDic;
    public static Dictionary<string, List<string>> DFDeviceDic;

    public static Dictionary<int, string> LHYFloorDic = new Dictionary<int, string> { { 78, "B2" }, { 77, "B1" }, { 24, "室外" }, { 22, "1F" }, { 23, "19F" } };//{ 24, "室外" },
    //public static Dictionary<int, string> DFFloorDic = new Dictionary<int, string> { { 63, "B1" }, { 62, "1F" } };
    public static Dictionary<int, string> DFFloorDic = new Dictionary<int, string> { { 48, "B2" }, { 59, "B1" }, { 47, "室外" }, { 49, "1F" }, { 87, "2F" }, { 88, "3F" }, { 89, "4F" }, { 90, "5F" }, { 91, "6F" }, { 92, "7F" }, { 93, "8F" }, { 94, "9F" }, { 95, "10F" }, { 50, "11F" } };//{ 47, "室外" }, 
    int[] DFFloorSort = new int[] { 47, 48, 59, 49, 87, 88, 89, 90, 91, 92, 93, 94, 95, 50 };
    int[] LHYFloorSort = new int[] { 24, 78, 77, 22, 23 };

    public static string deviceDialogUrl = "http://" + urlPrefix + "/base/tenant/devicemap/selectDeviceList";
    public static Dictionary<int, int[]> LHYfloor2MapDic = new Dictionary<int, int[]>() { { 23, new int[] { 7 } }, { 22, new int[] { 6 } }, { 24, new int[] { 12 } }, { 77, new int[] { 18, 43 } }, { 78, new int[] { 17, 44 } } };
    public static Dictionary<int, int[]> DFfloor2MapDic = new Dictionary<int, int[]>() { { 50, new int[] { 46 } }, { 48, new int[] { 49 } }, { 59, new int[] { 47 } }, { 47, new int[] { 51 } }, { 49, new int[] { 45 } } };

    public static Dictionary<string, string> deviceDetailShowDic = new Dictionary<string, string>() { { "运行", "monitorName" }, { "故障", "alarmFlagName" }, { "远程/就地", "method" }, { "启动", "value" } };

    public static string deviceControlUrl = "http://" + urlPrefix + "/base/tenant/controlsource/setPoint";

    public static string doorInfoUrl = "http://" + urlPrefix + "/attendance/door/list";
    public static string doorControlUrl = "http://" + urlPrefix + "/attendance/door/remoteOpenDoor?doorId=";

    public static string fireProtectTypeUrl = "http://" + urlPrefix + "/base/tenant/devicemap/fireProtectionSystemSummaryOfType?categoryId=11";
    public static string fireProtectFloorUrl = "http://" + urlPrefix + "/base/tenant/devicemap/fireProtectionSystemSummaryOfFloor?categoryId=11";
    public static Dictionary<string, string> DFTypes = new Dictionary<string, string>();
    public static string DFValue;
    public static Dictionary<int, string> DFFireProtectDic = new Dictionary<int, string>() { { 48, "-2F" }, { 59, "-1F" }, { 49, "F01" }, { 87, "F02" }, { 88, "F03" }, { 89, "F04" }, { 90, "F05" }, { 91, "F06" }, { 92, "F07" }, { 93, "F08" }, { 94, "F09" }, { 95, "F10" }, { 50, "RF" } };

    public static string parkingUrl = "http://" + urlPrefix + "/base/tenant/devicemap/selectDeviceListByCategoryId?categoryId=8";
    public static string lightUrl = "http://" + urlPrefix + "/base/tenant/devicemap/selectDeviceListByCategoryId?categoryId=7";

    public static string rstpUrl = "http://" + urlPrefix + "/base/tenant/device/get/";

    public static string liftUrl = "http://" + urlPrefix + "/attendance/elevator/queryLiftDetail?registerCode=";

    public static int currentPositionId = -1;

    public static string elecUrl = "http://" + urlPrefix + "/base/tenant/devicemap/selectElectricity?categoryId=16&positionParentId=34";

    public delegate void displayUI();

    private void Awake()
    {
        dialog = this.transform.Find("messageBox").gameObject;
        LHYDeviceDic = JsonMapper.ToObject<Dictionary<string, List<string>>>(LHYDevice);
        DFDeviceDic = JsonMapper.ToObject<Dictionary<string, List<string>>>(DFDevice);
        tokenControl = new TokenControl();
        tokenControl.setUrl(this.url);
        tokenControl.SetUserName(userName);
        tokenControl.SetPassword(password);
        tokenControl.setTokenUrl1(tokenUrl1);
        tokenControl.setTokenUrl2(tokenUrl2);
        tokenControl.setTokenUrl3(tokenUrl3);
        token = tokenControl.getToken();
        Debug.Log(token);

        string result_LHY = HTTPServiceControl.GetHttpResponse(floorUrlPrefix + "3", token);

        if (!string.IsNullOrEmpty(result_LHY))
        {
            LHY = JsonMapper.ToObject<List<FloorInfo>>(result_LHY);

            for (int i = LHY.Count - 1; i >= 0; i--)
            {
                if (LHY[i].positionCode.IndexOf("LHY") < 0 || LHY[i].positionCode == "LHY" || LHY[i].positionCode == "LHYDXEC" || LHY[i].positionCode == "LHYDXYC")//|| LHY[i].positionCode == "LHYSW"
                    LHY.Remove(LHY[i]);
            }

            LHY = LHY.OrderBy(e =>
            {
                int index = 0;
                index = Array.IndexOf(LHYFloorSort, e.positionId);
                if (index != -1) { return index; }
                else
                {
                    return int.MaxValue;
                }
            }).ToList();
        }

        //LHY.Reverse();

        string result_DF = HTTPServiceControl.GetHttpResponse(floorUrlPrefix + "4", token);

        if (!string.IsNullOrEmpty(result_DF))
        {
            DF = JsonMapper.ToObject<List<FloorInfo>>(result_DF);

            for (int i = DF.Count - 1; i >= 0; i--)
            {
                //if (DF[i].positionCode.IndexOf("DFHS") <0|| DF[i].positionCode == "DFHS")
                if (DF[i].positionCode.IndexOf("DF") < 0 || DF[i].positionCode.IndexOf("DFHS") > -1 || DF[i].positionCode == "DF")//|| DF[i].positionCode == "DFSW"
                    DF.Remove(DF[i]);
            }

            DF = DF.OrderBy(e =>
            {
                int index = 0;
                index = Array.IndexOf(DFFloorSort, e.positionId);
                if (index != -1) { return index; }
                else
                {
                    return int.MaxValue;
                }
            }).ToList();
        }

        string result_category = HTTPServiceControl.GetHttpResponse(this.categoryUrl, token);

        if (!string.IsNullOrEmpty(result_category))
        {
            List<CategoryInfo> dic_category = JsonMapper.ToObject<List<CategoryInfo>>(result_category);

            foreach (CategoryInfo item in dic_category)
            {
                categoryDic.Add(item.categoryId, item.categoryName);
            }

            categoryDic[15] = "给排水监测系统";
            categoryDic[12] = "送排风监测系统";
            categoryDic[3] = "视频监控系统";
            categoryDic[6] = "能耗管理系统";
            categoryDic[9] = "UPS监测系统";
            categoryDic[5] = "机房环境监测系统";
        }

        string result = HTTPServiceControl.GetHttpResponse(fireProtectTypeUrl, token);

        if (!string.IsNullOrEmpty(result))
        {
            Dictionary<string, List<TabData>> typeInfos = JsonMapper.ToObject<Dictionary<string, List<TabData>>>(result);
            List<TabData> types = typeInfos["tabData"];
            DFValue = "";
            foreach (TabData tabData in types)
            {
                if (tabData.name == "东府")
                {
                    DFValue = tabData.value;
                    break;
                }
            }
            foreach (TabData item in typeInfos[DFValue])
            {
                DFTypes.Add(item.deviceId.ToString(), item.type);
            }
        }
    }

    public static void qryDeviceByFloor(int projectId, int positionId, displayUI show)
    {
        isDevice = false;
        deviceDic.Clear();
        if (projectId == 3)
        {
            if (!LHYfloor2MapDic.ContainsKey(positionId)) return;
            int[] mapArr = LHYfloor2MapDic[positionId];
            for (int i = 0; i < mapArr.Length; i++)
            {
                Task<string> query_data = HTTPServiceControl.GetDataAsync(deviceDialogUrl + "?digitalMapId=" + mapArr[i] + "&categoryId=0", token);

                query_data.GetAwaiter().OnCompleted(() =>
                {
                    if (!string.IsNullOrEmpty(query_data.Result))
                    {
                        try
                        {
                            List<DeviceInfo> deviceInfos = JsonMapper.ToObject<List<DeviceInfo>>(query_data.Result);
                            foreach (DeviceInfo item in deviceInfos)
                            {
                                item.projectId = 3;
                                //门禁
                                if (item.categoryId == 4)
                                {
                                    item.doorId = item.deviceEUI.Substring(item.deviceEUI.IndexOf('-') + 1);
                                    item.customType = 3;
                                }
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
                            if (deviceDic.ContainsKey(8)) deviceDic.Remove(8);
                            if (deviceDic.ContainsKey(16)) deviceDic.Remove(16);
                            if (deviceDic.ContainsKey(18)) deviceDic.Remove(18);
                            foreach (int key in deviceDic.Keys)
                            {
                                deviceDic[key].Sort(new NameCompare());
                            }
                            show();
                        }
                        catch (Exception e)
                        {
                            dialog.SetActive(true);
                            dialog.GetComponent<DialogControl>().setContent(query_data.Result);
                        }
                    }
                });

                //门禁动环 int[] typeArr = new int[] { 0, 1 }; 

                /*int[] typeArr = new int[] { 0 };
                for (int j = 0; j < typeArr.Length; j++)
                {
                    string doorParam = JsonMapper.ToJson(new Dictionary<string, int> {
                                                            {"digitalMapId",mapArr[i]},
                                                            {"type",typeArr[j]},
                                                            {"ifBind",1},
                                                            {"projectId",3} });
                    Task<CallBackResult> resultDoor = HTTPServiceControl.PostDataAsyncNew(doorInfoUrl, doorParam, token, mapArr[i], positionId);
                    resultDoor.GetAwaiter().OnCompleted(() => doorQryCallback(resultDoor.Result, show));

                }*/
                #region 停车
                if (positionId == 24)
                {
                    Task<CallBackResult> resultPark = HTTPServiceControl.GetDataAsyncNew(parkingUrl, token, mapArr[i], positionId);
                    resultPark.GetAwaiter().OnCompleted(() => parkQryCallback(resultPark.Result, show));
                }
                #endregion
            }


            #region 电表
            /*if (positionId == 77)
            {
                Task<CallBackResult> parkResult = HTTPServiceControl.GetDataAsyncNew(elecUrl, token,positionId,positionId);

                parkResult.GetAwaiter().OnCompleted(() =>
                {
                    if (parkResult.Result.positionId != currentPositionId) return;
                    if (parkResult.Result.isSucc)
                    {
                        try
                        {
                            List<DeviceInfo> parkInfos = JsonMapper.ToObject<List<DeviceInfo>>(parkResult.Result.dataMsg);
                            foreach (DeviceInfo item in parkInfos)
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
                            deviceDic[16].Sort(new NameCompare());
                            show();
                        }
                        catch (Exception e)
                        {
                            dialog.SetActive(true);
                            dialog.GetComponent<DialogControl>().setContent(parkResult.Result.dataMsg);
                        }
                    }
                });
            }*/
            #endregion

        }
        else if (projectId == 4)
        {
            if (!DFfloor2MapDic.ContainsKey(positionId)) return;
            int[] mapArr = DFfloor2MapDic[positionId];
            for (int i = 0; i < mapArr.Length; i++)
            {
                Task<string> resultMap = HTTPServiceControl.GetDataAsync(deviceDialogUrl + "?digitalMapId=" + mapArr[i] + "&categoryId=0", token);

                resultMap.GetAwaiter().OnCompleted(() =>
                {
                    if (!string.IsNullOrEmpty(resultMap.Result))
                    {
                        try
                        {
                            List<DeviceInfo> deviceInfos = JsonMapper.ToObject<List<DeviceInfo>>(resultMap.Result);
                            foreach (DeviceInfo item in deviceInfos)
                            {
                                item.projectId = 4;
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
                            if (deviceDic.ContainsKey(16)) deviceDic.Remove(16);
                            if (deviceDic.ContainsKey(8)) deviceDic.Remove(8);
                            if (deviceDic.ContainsKey(18)) deviceDic.Remove(18);//人脸识别
                            if (deviceDic.ContainsKey(19)) deviceDic.Remove(19);//变配电
                            //照明重组
                            if (deviceDic.ContainsKey(7))
                            {
                                deviceDic.Remove(7);
                                /*Task<string> resultLight = HTTPServiceControl.GetDataAsync(lightUrl, token);

                                resultLight.GetAwaiter().OnCompleted(() =>
                                {
                                    if (!string.IsNullOrEmpty(resultLight.Result))
                                    {
                                        try
                                        {
                                            List<DeviceInfo> lights = JsonMapper.ToObject<List<DeviceInfo>>(resultLight.Result);
                                            foreach (DeviceInfo item in lights)
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
                                            Dictionary<string, List<monitorItem>> lightItems = new Dictionary<string, List<monitorItem>>();
                                            foreach (DeviceInfo item in deviceDic[7])
                                            {
                                                if (item.deviceName.IndexOf("#") > -1) item.deviceName = item.deviceName.Split('#')[1];
                                                monitorItem monitor = new monitorItem();
                                                monitor.monitorName = item.deviceName;
                                                string prefix = item.deviceName.Substring(0, item.deviceName.IndexOf("-") + 2).ToUpper();
                                                monitor.value = item.monitorList[0].value;
                                                monitor.historyTable = item.monitorList[0].historyTable;
                                                if (lightItems.ContainsKey(prefix))
                                                {
                                                    lightItems[prefix].Add(monitor);
                                                }
                                                else
                                                {
                                                    List<monitorItem> temp = new List<monitorItem>();
                                                    temp.Add(monitor);
                                                    lightItems.Add(prefix, temp);
                                                }
                                            }
                                            deviceDic.Remove(7);
                                            List<DeviceInfo> info = new List<DeviceInfo>();
                                            deviceDic.Add(7, info);
                                            foreach (string key in lightItems.Keys)
                                            {
                                                lightItems[key].OrderBy(m => m.monitorName).ToList();
                                                DeviceInfo device = new DeviceInfo();
                                                device.deviceName = key;
                                                device.monitorList = lightItems[key];
                                                device.customType = 2;
                                                deviceDic[7].Add(device);
                                            }
                                            deviceDic[7].Sort(new NameCompare());
                                            show();
                                        }
                                        catch (Exception e)
                                        {
                                            dialog.SetActive(true);
                                            dialog.GetComponent<DialogControl>().setContent(resultLight.Result);
                                        }
                                    }
                                });*/
                            }
                            

                            foreach (int key in deviceDic.Keys)
                            {
                                deviceDic[key].Sort(new NameCompare());
                            }
                            show();
                        }
                        catch (Exception e)
                        {
                            dialog.SetActive(true);
                            dialog.GetComponent<DialogControl>().setContent(resultMap.Result);
                        }
                    }
                });

                //消防
                //Task<CallBackResult> resultFloor = HTTPServiceControl.GetDataAsyncNew(fireProtectFloorUrl, token, positionId, positionId);
                //resultFloor.GetAwaiter().OnCompleted(() => fireProtectCallBack(resultFloor.Result, show));


                //动环
                /*string doorParam = JsonMapper.ToJson(new Dictionary<string, int> {
                                                            {"digitalMapId",mapArr[i]},
                                                            {"type",1},
                                                            {"ifBind",1},
                                                            {"projectId",4} });
                Task<CallBackResult> resultDoor = HTTPServiceControl.PostDataAsyncNew(doorInfoUrl, doorParam, token, mapArr[i], positionId);
                resultDoor.GetAwaiter().OnCompleted(() => doorQryCallback(resultDoor.Result, show));
                */
                #region 停车
                if (positionId == 47)
                {
                    Task<CallBackResult> resultPark = HTTPServiceControl.GetDataAsyncNew(parkingUrl, token, mapArr[i], positionId);
                    resultPark.GetAwaiter().OnCompleted(() => parkQryCallback(resultPark.Result, show));

                    /*Task<string> parkResult = HTTPServiceControl.GetDataAsync(parkingUrl, token);

                    parkResult.GetAwaiter().OnCompleted(() =>
                    {
                        if (!string.IsNullOrEmpty(parkResult.Result))
                        {
                            try
                            {
                                List<DeviceInfo> parkInfos = JsonMapper.ToObject<List<DeviceInfo>>(parkResult.Result);
                                foreach (DeviceInfo item in parkInfos)
                                {
                                    if (deviceDic.ContainsKey(item.categoryId))
                                    {
                                        //bool isAct = false;
                                        //foreach (DeviceInfo deviceInfo in deviceDic[item.categoryId])
                                        //{
                                        //    if (deviceInfo.deviceId == item.deviceId)
                                        //    {
                                        //        isAct = true;
                                        //        break;
                                        //    }
                                        //}
                                        //if (!isAct) deviceDic[item.categoryId].Add(item);
                                        deviceDic[item.categoryId].Add(item);
                                    }
                                    else
                                    {
                                        isDevice = true;
                                        List<DeviceInfo> temp = new List<DeviceInfo>();
                                        temp.Add(item);
                                        deviceDic.Add(item.categoryId, temp);
                                    }
                                    //}
                                }
                                show();
                            }
                            catch (Exception e)
                            {
                                dialog.SetActive(true);
                                dialog.GetComponent<DialogControl>().setContent(parkResult.Result);
                            }
                        }
                    });*/
                }
                #endregion
                #region 电表
                /*if (positionId == 59)
                {
                    Task<CallBackResult> parkResult = HTTPServiceControl.GetDataAsyncNew(elecUrl, token,positionId,positionId);

                    parkResult.GetAwaiter().OnCompleted(() =>
                    {
                        if (parkResult.Result.positionId != currentPositionId) return;
                        if (parkResult.Result.isSucc)
                        {
                            try
                            {
                                List<DeviceInfo> parkInfos = JsonMapper.ToObject<List<DeviceInfo>>(parkResult.Result.dataMsg);
                                foreach (DeviceInfo item in parkInfos)
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
                                deviceDic[16].Sort(new NameCompare());
                                show();
                            }
                            catch (Exception e)
                            {
                                dialog.SetActive(true);
                                dialog.GetComponent<DialogControl>().setContent(parkResult.Result.dataMsg);
                            }
                        }
                    });
                }*/
                #endregion
            }
        }

        //安防系统rstp查询
        /*if (deviceDic.ContainsKey(3)) {
            foreach (DeviceInfo item in deviceDic[3]) {
                string resultLight = HTTPServiceControl.GetHttpResponse(rstpUrl+ item.deviceId.ToString(), token);
                DeviceInfo info = JsonMapper.ToObject<DeviceInfo>(resultLight);
                item.rtsp = info.rtsp;
            }
        }*/
    }

    public static void parkQryCallback(CallBackResult result, displayUI show)
    {
        if (result.positionId != currentPositionId) return;
        if (result.isSucc)
        {
            try
            {
                List<DeviceInfo> doorInfos = JsonMapper.ToObject<List<DeviceInfo>>(result.dataMsg);
                int categoryId = -1;
                foreach (DeviceInfo item in doorInfos)
                {
                    if (item.digitalMapId == result.id)
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
                            categoryId = item.categoryId;
                        }
                    }
                }
                if (categoryId > -1) deviceDic[categoryId].Sort(new NameCompare());
                show();
            }
            catch (Exception e)
            {
                dialog.SetActive(true);
                dialog.GetComponent<DialogControl>().setContent(result.dataMsg);
            }
        }
        else
        {
            dialog.SetActive(true);
            dialog.GetComponent<DialogControl>().setContent(result.dataMsg);
        }
    }
    public static void doorQryCallback(CallBackResult result, displayUI show)
    {
        if (result.positionId != currentPositionId) return;
        if (result.isSucc)
        {
            try
            {
                DoorInfo doorInfos = JsonMapper.ToObject<DoorInfo>(result.dataMsg);
                int categoryId = -1;
                foreach (DeviceInfo door in doorInfos.data)
                {
                    if (door.digitalMapId == result.id)
                    {
                        door.deviceEUI = door.doorId;
                        door.deviceName = door.doorName;
                        door.customType = 3;
                        if (deviceDic.ContainsKey(door.categoryId))
                        {
                            categoryId = door.categoryId;
                            deviceDic[door.categoryId].Add(door);
                        }
                        else
                        {
                            isDevice = true;
                            List<DeviceInfo> temp = new List<DeviceInfo>();
                            temp.Add(door);
                            deviceDic.Add(door.categoryId, temp);
                        }
                    }
                }
                if (categoryId > -1) deviceDic[categoryId].Sort(new NameCompare());
                show();
            }
            catch (Exception e)
            {
                dialog.SetActive(true);
                dialog.GetComponent<DialogControl>().setContent(result.dataMsg);
            }
        }
        else
        {
            dialog.SetActive(true);
            dialog.GetComponent<DialogControl>().setContent(result.dataMsg);
        }
    }
    public static void fireProtectCallBack(CallBackResult result, displayUI show)
    {
        if (result.positionId != currentPositionId) return;
        if (result.isSucc)
        {
            try
            {
                Dictionary<string, List<FireProtect>> fireProtects = JsonMapper.ToObject<Dictionary<string, List<FireProtect>>>(result.dataMsg);
                List<FireProtect> fireProtect = fireProtects[DFValue];
                if (DFFireProtectDic.ContainsKey(result.id))
                {
                    foreach (FireProtect protect in fireProtect)
                    {
                        if (protect.floor == DFFireProtectDic[result.id])
                        {
                            isDevice = true;
                            List<DeviceInfo> temp = new List<DeviceInfo>();
                            foreach (KeyValuePair<string, string> data in protect.deviceData)
                            {
                                DeviceInfo item = new DeviceInfo();
                                item.deviceName = DFTypes[data.Key] + ":" + data.Value;
                                item.customType = 1;
                                temp.Add(item);
                            }
                            if (deviceDic.ContainsKey(11)) deviceDic.Remove(11);
                            deviceDic.Add(11, temp);
                            deviceDic[11].Sort(new NameCompare());
                            show();
                            break;
                        }
                    }
                }

            }
            catch (Exception e)
            {
                dialog.SetActive(true);
                dialog.GetComponent<DialogControl>().setContent(result.dataMsg);
            }
        }
        else
        {
            dialog.SetActive(true);
            dialog.GetComponent<DialogControl>().setContent(result.dataMsg);
        }
    }

    public static string qryDeviceRstp(int deviceId)
    {
        string resultLight = HTTPServiceControl.GetHttpResponse(rstpUrl + deviceId.ToString(), token);
        DeviceInfo info = JsonMapper.ToObject<DeviceInfo>(resultLight);
        return info.rtsp;
    }

    public static LiftInfo qryLiftRstp(string deviceEUI)
    {
        string resultLight = HTTPServiceControl.GetHttpResponse(liftUrl + deviceEUI, token);
        LiftInfoAll info = JsonMapper.ToObject<LiftInfoAll>(resultLight);
        return info.data;
    }


    public static void qryDeviceByFloorBak(int projectId, int positionId)
    {
        isDevice = false;
        //if ((projectId == 3 && !LHYDeviceDic.ContainsKey(positionId.ToString())) || (projectId == 4 && !DFDeviceDic.ContainsKey(positionId.ToString())))
        //return;
        //string resultDevice = HTTPServiceControl.GetHttpResponse(deviceUrl + projectId.ToString() + deviceUrlSuffix + positionId.ToString(), token);
        string resultDevice = HTTPServiceControl.GetHttpResponse(deviceUrl + projectId.ToString() + "&positionId=" + positionId.ToString(), token);
        DeviceRows devideRows = JsonMapper.ToObject<DeviceRows>(resultDevice);
        deviceDic.Clear();
        foreach (DeviceInfo item in devideRows.rows)
        {
            //if ((projectId==3&&LHYDeviceDic[positionId.ToString()].Contains(item.deviceEui))|| (projectId == 4 && DFDeviceDic[positionId.ToString()].Contains(item.deviceEui))) {
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
            //}
        }
        if (projectId == 3 && (positionId == 77 || positionId == 78))
        {
            string resultDeviceEC;
            if (positionId == 77)
            {
                resultDeviceEC = HTTPServiceControl.GetHttpResponse(deviceUrl + projectId.ToString() + "&positionId=" + "30", token);
            }
            else
            {
                resultDeviceEC = HTTPServiceControl.GetHttpResponse(deviceUrl + projectId.ToString() + "&positionId=" + "25", token);
            }
            DeviceRows devideRowsEC = JsonMapper.ToObject<DeviceRows>(resultDeviceEC);
            foreach (DeviceInfo item in devideRowsEC.rows)
            {
                //if (LHYDeviceDic[positionId.ToString()].Contains(item.deviceEui))
                //{
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
                //}
            }
        }

        if (projectId == 3)
        {
            if (!LHYfloor2MapDic.ContainsKey(positionId)) return;
            int[] mapArr = LHYfloor2MapDic[positionId];
            for (int i = 0; i < mapArr.Length; i++)
            {
                string resultMap = HTTPServiceControl.GetHttpResponse(deviceDialogUrl + "?digitalMapId=" + mapArr[i] + "&categoryId=0", token);
                List<DeviceInfo> deviceInfos = JsonMapper.ToObject<List<DeviceInfo>>(resultMap);
                foreach (DeviceInfo item in deviceInfos)
                {
                    if (item.monitorList != null)
                    {
                        if (deviceDic.ContainsKey(item.categoryId))
                        {
                            foreach (DeviceInfo origin in deviceDic[item.categoryId])
                            {
                                if (origin.deviceEUI == item.monitorList[0].deviceEUI)
                                    origin.monitorList = item.monitorList;
                            }
                            //deviceDic[item.categoryId].Add(item);
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
                //门禁动环
                int[] typeArr = new int[] { 0, 1 };
                for (int j = 0; j < typeArr.Length; j++)
                {
                    string doorParam = JsonMapper.ToJson(new Dictionary<string, int> {
                                                            {"digitalMapId",mapArr[i]},
                                                            {"type",typeArr[j]},
                                                            {"ifBind",1},
                                                            {"projectId",3} });
                    string resultDoor = HTTPServiceControl.GetPostHttpResponse(doorInfoUrl, doorParam, token);
                    DoorInfo doorInfos = JsonMapper.ToObject<DoorInfo>(resultDoor);
                    foreach (DeviceInfo door in doorInfos.data)
                    {
                        if (door.digitalMapId == mapArr[i])
                        {
                            if (deviceDic.ContainsKey(door.categoryId))
                            {
                                deviceDic[door.categoryId].Add(door);
                            }
                            else
                            {
                                isDevice = true;
                                List<DeviceInfo> temp = new List<DeviceInfo>();
                                temp.Add(door);
                                deviceDic.Add(door.categoryId, temp);
                            }
                        }
                    }
                }

            }
        }
        else if (projectId == 4)
        {
            if (!DFfloor2MapDic.ContainsKey(positionId)) return;
            int[] mapArr = DFfloor2MapDic[positionId];
            for (int i = 0; i < mapArr.Length; i++)
            {
                string resultMap = HTTPServiceControl.GetHttpResponse(deviceDialogUrl + "?digitalMapId=" + mapArr[i] + "&categoryId=0", token);
                List<DeviceInfo> deviceInfos = JsonMapper.ToObject<List<DeviceInfo>>(resultMap);
                foreach (DeviceInfo item in deviceInfos)
                {
                    if (item.monitorList != null)
                    {
                        if (deviceDic.ContainsKey(item.categoryId))
                        {
                            foreach (DeviceInfo origin in deviceDic[item.categoryId])
                            {
                                if (origin.deviceEUI == item.monitorList[0].deviceEUI)
                                    origin.monitorList = item.monitorList;
                            }
                            //deviceDic[item.categoryId].Add(item);
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

                //门禁动环
                int[] typeArr = new int[] { 0, 1 };
                for (int j = 0; j < typeArr.Length; j++)
                {
                    string doorParam = JsonMapper.ToJson(new Dictionary<string, int> {
                                                            {"digitalMapId",mapArr[i]},
                                                            {"type",typeArr[j]},
                                                            {"ifBind",1},
                                                            {"projectId",4} });
                    string resultDoor = HTTPServiceControl.GetPostHttpResponse(doorInfoUrl, doorParam, token);
                    DoorInfo doorInfos = JsonMapper.ToObject<DoorInfo>(resultDoor);
                    foreach (DeviceInfo door in doorInfos.data)
                    {
                        if (door.digitalMapId == mapArr[i])
                        {
                            if (deviceDic.ContainsKey(door.categoryId))
                            {
                                deviceDic[door.categoryId].Add(door);
                            }
                            else
                            {
                                isDevice = true;
                                List<DeviceInfo> temp = new List<DeviceInfo>();
                                temp.Add(door);
                                deviceDic.Add(door.categoryId, temp);
                            }
                        }
                    }
                }
            }
        }


    }


    public static void qryTotalEleWaterNum(ref string powerYear, ref string waterYear)
    {
        string result = HTTPServiceControl.GetHttpResponse(waterEleUrl, token);
        if (!string.IsNullOrEmpty(result)) {
            WaterPower waterPower = JsonMapper.ToObject<WaterPower>(result);
            waterYear = waterPower.data.waterYear;
            powerYear = waterPower.data.powerYear;
        }
    }

    public static void qryAlarmList()
    {
        try
        {
            alarmList.Clear();
            qryAlarmByType("3");
            qryAlarmByType("2");
            qryAlarmByType("1");

        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public static void qryAlarmByType(string type)
    {
        string result = HTTPServiceControl.GetHttpResponse(alarmUrl + type, token);

        if (!string.IsNullOrEmpty(result)) {
            List<AlarmInfo> alarmInfoList = JsonMapper.ToObject<List<AlarmInfo>>(result);
            foreach (AlarmInfo alarmInfo in alarmInfoList)
            {
                if ((positionIdStrLHY.IndexOf("," + alarmInfo.positionId.ToString() + ",") > -1 && alarmInfo.projectId == 3)
                    || (positionIdStrDF.IndexOf("," + alarmInfo.positionId.ToString() + ",") > -1 && alarmInfo.projectId == 4))
                {
                    int idx = alarmInfo.deviceName.IndexOf("_");
                    AlarmGrid alarmGrid = new AlarmGrid();
                    alarmGrid.deviceName = alarmInfo.deviceName.Substring(idx + 1);
                    alarmGrid.monitorName = alarmInfo.monitorName;
                    alarmGrid.categoryName = alarmInfo.categoryName;
                    if (idx > -1)
                    {
                        alarmGrid.descName = alarmInfo.descName + "_" + alarmInfo.deviceName.Substring(0, idx);
                    }
                    else
                    {
                        alarmGrid.descName = alarmInfo.descName + "_" + alarmInfo.deviceName;
                    }
                    alarmList.Add(alarmGrid);
                }
            }
        }
    }

    public static void qryDeviceDetail(int projectId, int positionId)
    {
        string result = HTTPServiceControl.GetHttpResponse(deviceDetailUrl + "&projectId=" + projectId + "&positionId=" + positionId, token);
        DeviceDetail alarmInfoList = JsonMapper.ToObject<DeviceDetail>(result);
    }

    public static void setDeviceControl(string sourceCode, bool value)
    {
        DeviceControl deviceControl = new DeviceControl();
        deviceControl.sourceCode = sourceCode;
        deviceControl.value = value;
        string content = JsonMapper.ToJson(deviceControl);
        //string result = HTTPServiceControl.GetPostHttpResponse(deviceControlUrl, content, token);
        Task<string> query_data = HTTPServiceControl.PostDataAsync(deviceControlUrl, content, token);

        query_data.GetAwaiter().OnCompleted(() =>
        {
            if (!string.IsNullOrEmpty(query_data.Result))
            {
                try
                {

                }
                catch (Exception e)
                {
                    dialog.SetActive(true);
                    dialog.GetComponent<DialogControl>().setContent(query_data.Result);
                }
            }
        });

    }
    public static void setDoorControl(string sourceCode)
    {
        /*string result = HTTPServiceControl.GetHttpResponse(doorControlUrl+ sourceCode, token);
        DoorInfo doorInfos = JsonMapper.ToObject<DoorInfo>(result);
        if (doorInfos.code==0) {
            dialog.SetActive(true);
            dialog.GetComponent<DialogControl>().setContent(doorInfos.msg);
        }*/
        Task<string> query_data = HTTPServiceControl.GetDataAsync(doorControlUrl + sourceCode, token);

        query_data.GetAwaiter().OnCompleted(() =>
        {
            if (!string.IsNullOrEmpty(query_data.Result))
            {
                try
                {
                    DoorInfo doorInfos = JsonMapper.ToObject<DoorInfo>(query_data.Result);
                    if (doorInfos.code == 0)
                    {
                        dialog.SetActive(true);
                        dialog.GetComponent<DialogControl>().setContent(doorInfos.msg);
                    }
                }
                catch (Exception e)
                {
                    dialog.SetActive(true);
                    dialog.GetComponent<DialogControl>().setContent(query_data.Result);
                }
            }
        });
    }

    void OnApplicationQuit()
    {
        websocket.Close();
    }

    public static void qryDeviceByFloorAnycBak(int projectId, int positionId, displayUI show)
    {
        isDevice = false;
        deviceDic.Clear();
        if (projectId == 3)
        {
            if (!LHYfloor2MapDic.ContainsKey(positionId)) return;
            int[] mapArr = LHYfloor2MapDic[positionId];
            for (int i = 0; i < mapArr.Length; i++)
            {

                string resultMap = HTTPServiceControl.GetHttpResponse(deviceDialogUrl + "?digitalMapId=" + mapArr[i] + "&categoryId=0", token);
                List<DeviceInfo> deviceInfos = JsonMapper.ToObject<List<DeviceInfo>>(resultMap);
                foreach (DeviceInfo item in deviceInfos)
                {

                    //if (item.monitorList != null)
                    //{
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
                    //}

                }
                //门禁动环
                int[] typeArr = new int[] { 0, 1 };
                for (int j = 0; j < typeArr.Length; j++)
                {
                    string doorParam = JsonMapper.ToJson(new Dictionary<string, int> {
                                                            {"digitalMapId",mapArr[i]},
                                                            {"type",typeArr[j]},
                                                            {"ifBind",1},
                                                            {"projectId",3} });
                    string resultDoor = HTTPServiceControl.GetPostHttpResponse(doorInfoUrl, doorParam, token);
                    DoorInfo doorInfos = JsonMapper.ToObject<DoorInfo>(resultDoor);
                    foreach (DeviceInfo door in doorInfos.data)
                    {
                        if (door.digitalMapId == mapArr[i])
                        {
                            door.deviceEUI = door.doorId;
                            door.deviceName = door.doorName;
                            door.customType = 3;
                            if (deviceDic.ContainsKey(door.categoryId))
                            {
                                deviceDic[door.categoryId].Add(door);
                            }
                            else
                            {
                                isDevice = true;
                                List<DeviceInfo> temp = new List<DeviceInfo>();
                                temp.Add(door);
                                deviceDic.Add(door.categoryId, temp);
                            }
                        }
                    }
                }


            }
        }
        else if (projectId == 4)
        {
            if (!DFfloor2MapDic.ContainsKey(positionId)) return;
            int[] mapArr = DFfloor2MapDic[positionId];
            for (int i = 0; i < mapArr.Length; i++)
            {
                string resultMap = HTTPServiceControl.GetHttpResponse(deviceDialogUrl + "?digitalMapId=" + mapArr[i] + "&categoryId=0", token);
                List<DeviceInfo> deviceInfos = JsonMapper.ToObject<List<DeviceInfo>>(resultMap);
                foreach (DeviceInfo item in deviceInfos)
                {
                    //if (item.monitorList != null)
                    //{
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
                    //}
                }

                //消防
                string resultFloor = HTTPServiceControl.GetHttpResponse(fireProtectFloorUrl, token);
                Dictionary<string, List<FireProtect>> fireProtects = JsonMapper.ToObject<Dictionary<string, List<FireProtect>>>(resultFloor);
                List<FireProtect> fireProtect = fireProtects[DFValue];
                if (DFFireProtectDic.ContainsKey(positionId))
                {
                    foreach (FireProtect protect in fireProtect)
                    {
                        if (protect.floor == DFFireProtectDic[positionId])
                        {
                            isDevice = true;
                            List<DeviceInfo> temp = new List<DeviceInfo>();
                            foreach (KeyValuePair<string, string> data in protect.deviceData)
                            {
                                DeviceInfo item = new DeviceInfo();
                                item.deviceName = DFTypes[data.Key] + ":" + data.Value;
                                item.customType = 1;
                                temp.Add(item);
                            }
                            if (deviceDic.ContainsKey(11)) deviceDic.Remove(11);
                            deviceDic.Add(11, temp);
                            break;
                        }
                    }
                }

                //停车
                if (positionId == 47)
                {
                    string parkResult = HTTPServiceControl.GetHttpResponse(parkingUrl, token);
                    List<DeviceInfo> parkInfos = JsonMapper.ToObject<List<DeviceInfo>>(parkResult);
                    foreach (DeviceInfo item in parkInfos)
                    {
                        //if (item.monitorList != null)
                        //{
                        if (deviceDic.ContainsKey(item.categoryId))
                        {
                            bool isAct = false;
                            foreach (DeviceInfo deviceInfo in deviceDic[item.categoryId])
                            {
                                if (deviceInfo.deviceId == item.deviceId)
                                {
                                    isAct = true;
                                    break;
                                }
                            }
                            if (!isAct) deviceDic[item.categoryId].Add(item);
                        }
                        else
                        {
                            isDevice = true;
                            List<DeviceInfo> temp = new List<DeviceInfo>();
                            temp.Add(item);
                            deviceDic.Add(item.categoryId, temp);
                        }
                        //}
                    }
                }

                //动环
                string doorParam = JsonMapper.ToJson(new Dictionary<string, int> {
                                                            {"digitalMapId",mapArr[i]},
                                                            {"type",1},
                                                            {"ifBind",1},
                                                            {"projectId",3} });
                string resultDoor = HTTPServiceControl.GetPostHttpResponse(doorInfoUrl, doorParam, token);
                DoorInfo doorInfos = JsonMapper.ToObject<DoorInfo>(resultDoor);
                foreach (DeviceInfo door in doorInfos.data)
                {
                    if (door.digitalMapId == mapArr[i])
                    {
                        door.deviceEUI = door.doorId;
                        door.deviceName = door.doorName;
                        door.customType = 3;
                        if (deviceDic.ContainsKey(door.categoryId))
                        {
                            deviceDic[door.categoryId].Add(door);
                        }
                        else
                        {
                            isDevice = true;
                            List<DeviceInfo> temp = new List<DeviceInfo>();
                            temp.Add(door);
                            deviceDic.Add(door.categoryId, temp);
                        }
                    }
                }
            }
        }

        //照明重组
        if (deviceDic.ContainsKey(7))
        {
            deviceDic.Remove(7);
            string resultLight = HTTPServiceControl.GetHttpResponse(lightUrl, token);
            List<DeviceInfo> lights = JsonMapper.ToObject<List<DeviceInfo>>(resultLight);
            foreach (DeviceInfo item in lights)
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
            Dictionary<string, List<monitorItem>> lightItems = new Dictionary<string, List<monitorItem>>();
            foreach (DeviceInfo item in deviceDic[7])
            {
                if (item.deviceName.IndexOf("#") > -1) item.deviceName = item.deviceName.Split('#')[1];
                monitorItem monitor = new monitorItem();
                monitor.monitorName = item.deviceName;
                string prefix = item.deviceName.Substring(0, item.deviceName.IndexOf("-") + 2).ToUpper();
                monitor.value = item.monitorList[0].value;
                monitor.historyTable = item.monitorList[0].historyTable;
                if (lightItems.ContainsKey(prefix))
                {
                    lightItems[prefix].Add(monitor);
                }
                else
                {
                    List<monitorItem> temp = new List<monitorItem>();
                    temp.Add(monitor);
                    lightItems.Add(prefix, temp);
                }
            }
            deviceDic.Remove(7);
            List<DeviceInfo> info = new List<DeviceInfo>();
            deviceDic.Add(7, info);
            foreach (string key in lightItems.Keys)
            {
                lightItems[key].OrderBy(m => m.monitorName).ToList();
                DeviceInfo device = new DeviceInfo();
                device.deviceName = key;
                device.monitorList = lightItems[key];
                device.customType = 2;
                deviceDic[7].Add(device);
            }
        }

        //安防系统rstp查询
        /*if (deviceDic.ContainsKey(3)) {
            foreach (DeviceInfo item in deviceDic[3]) {
                string resultLight = HTTPServiceControl.GetHttpResponse(rstpUrl+ item.deviceId.ToString(), token);
                DeviceInfo info = JsonMapper.ToObject<DeviceInfo>(resultLight);
                item.rtsp = info.rtsp;
            }
        }*/
        foreach (int key in deviceDic.Keys)
        {
            //deviceDic[key].OrderBy(d => int.Parse(Regex.Match(d.deviceName, @"\d+").Value)).ToList();
            deviceDic[key].Sort(new NameCompare());
        }
    }

}

public class NameCompare : IComparer<DeviceInfo>
{
    public int Compare(DeviceInfo x, DeviceInfo y)
    {

        int result = 0;
        string xNum = Regex.Replace(x.deviceName, @"[^0-9]+", "");
        string yNum = Regex.Replace(y.deviceName, @"[^0-9]+", "");
        if (!String.IsNullOrEmpty(xNum) && !String.IsNullOrEmpty(yNum))
        {
            if (xNum.Length == yNum.Length)
            {
                try
                {
                    result = int.Parse(xNum).CompareTo(int.Parse(yNum));
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            }
            else
            {
                if (int.Parse(xNum) < int.Parse(yNum))
                {
                    int len = yNum.Length - xNum.Length;
                    result = (int.Parse(xNum) * len * 10).CompareTo(int.Parse(yNum));
                }
                else
                {
                    int len = xNum.Length - yNum.Length;
                    result = int.Parse(xNum).CompareTo((int.Parse(yNum) * len * 10));
                }
            }
        }
        else
        {
            result = CompareInfo.GetCompareInfo("zh-cn").Compare(x.deviceName, y.deviceName, CompareOptions.IgnoreCase);
        }
        return result;
    }
}


