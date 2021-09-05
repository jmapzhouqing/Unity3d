using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DeviceItem : MonoBehaviour, IPointerClickHandler
{
    private Text Name;

    private Text Info;

	private Color color = new Color(80 / 255, 80 / 255, 80 / 255);


    // Start is called before the first frame update
    void Start()
    {
		this.Name = this.transform.GetChild(0).GetComponent<Text>();
		this.Info = this.transform.GetChild(1).GetComponent<Text>();
	}


	public void SetName(string value)
	{
		this.Name.text = value;
	}

	public void SetId(string value)
	{
		this.Info.text = value;
	}


	public void OnPointerClick(PointerEventData eventData)
	{
		
	}
}
