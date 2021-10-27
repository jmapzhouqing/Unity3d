using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceItemControl : MonoBehaviour
{
	public Text key;
	public Text value;

	private Color color;

	private bool controlState=false;
	private string sourceCode;

	// Start is called before the first frame update
	void Start()
	{
		this.color = this.transform.GetComponent<RawImage>().color;
	}


	public void SetKey(string value)
	{
		this.key.text = value;
	}

	public void SetSourceCode(string value)
	{
		this.sourceCode = value;
	}

	public void SetValue(string value)
	{
		if (string.IsNullOrEmpty(value) || value == "true" || value == "false")
		{
			this.value.text = value == "true" ? "关闭" : "开启";
			controlState= value == "true" ? true : false;
		}
		else
		{
			this.value.text = value;
		}
	}


	public void setColor()
	{
		this.transform.GetComponent<RawImage>().color = new Color(this.color.r, this.color.g, this.color.b, 193 / 255);
	}

	public void setControl() {
		PrimaryContorl.setDeviceControl(this.sourceCode, !this.controlState);
	}
}
