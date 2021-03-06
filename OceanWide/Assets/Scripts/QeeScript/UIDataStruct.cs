using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIDataStruct {
	public class SubMenu
	{
		public string name;
		public string id; 
	}

	public class FloorInfo {
		public int positionId;
		public string positionName;
		public int sort;
		public string positionCode;
	}

	public enum LHYfloor { 
		B2=78,
		B1=77,
		F1=22,
		F2=23
	}

	public enum DFfloor
	{
		B1 = 63,
		F1 = 62
	}

	public class CategoryInfo
	{
		public int categoryId;
		public string categoryName;
	}

	public class DeviceInfo
	{
		public int categoryId;
		public int deviceId;
		public string deviceName;
		public string rtsp;
		public string deviceEUI;
		public List<monitorItem> monitorList;
		public int digitalMapId;
		public string doorId;
		public string doorName;
		public List<DynamicEnvironmentDataItem> dynamicEnvironmentDataList;
		public int customType;

		public double carTopTemperature;
		public string ytStatusName;
		public double roomTemperature;
		public int projectId;
	}

	public class DynamicEnvironmentDataItem {
		public string serviceName;
		public string statusInformation;
	}

	public class DoorInfo {
		public int code;
		public List<DeviceInfo> data;
		public string msg;
	}

	public class monitorItem {
		public string monitorName;
		public string historyTable;
		public string value;
		public string deviceEUI;
		public string monitorPath;
	}

	public class DeviceControl {
		public string sourceCode;
		public bool value;
	}

	public class DeviceShowInfo
	{
		public string alarmFlagName;
		public string monitorName;
	}

	public class DeviceRows
	{
		public List<DeviceInfo> rows;
	}

	public class WaterPower {
		public string msg;
		public int code;
		public WaterPowerData data;
	}
	public class WaterPowerData {
		public string waterYear;
		public string powerYear;
	}

	public class AlarmInfo {
		public string deviceName;
		public string monitorName;
		public string categoryName;
		public string descName;
		public int positionId;
		public int projectId;
	}

	public class AlarmGrid
	{
		public string deviceName;
		public string monitorName;
		public string categoryName;
		public string descName;
	}

	public class DeviceDetail {
		public int rowCount;
		public List<DeviceDetailData> rows;
	}

	public class DeviceDetailData
	{
		public int deviceId;
		public string deviceEui;
	}

	public class TabData{
		public string name;
		public string value;
		public string type;
		public int deviceId;
	}

	public class FireProtect {
		public string floor;
		public Dictionary<string, string> deviceData;
	}

	public class LiftInfo {
		public double carTopTemperature;
		public double roomTemperature;
		public string ytStatusName;
		public List<LiftVideo> videoList;
	}

	public class LiftVideo {
		public string rtspUrl;
	}

	public class LiftInfoAll
	{
		public string msg;
		public int code;
		public LiftInfo data;
	}

	public class CallBackResult {
		public bool isSucc;
		public int id;
		public string dataMsg;
		public int positionId;
	}
}
