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

	public enum XHYfloor { 
		B2=27,
		B1=26,
		F1=33,
		F2=70
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
	}

	public class DeviceRows
	{
		public List<DeviceInfo> rows;
	}
}
