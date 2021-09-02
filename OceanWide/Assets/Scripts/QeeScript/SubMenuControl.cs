using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SubMenuControl : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
	private Text Name;

	private string id;

	private Color Color;

	private bool isPointerInside = false;

	// Start is called before the first frame update
	void Awake()
	{
		this.Name = this.transform.GetComponentInChildren<Text>();
		this.Color = this.GetComponent<RawImage>().color;
	}

	// Update is called once per frame
	void Update()
	{
		/*if (Input.GetMouseButtonDown(0))
		{
			if (isPointerInside)
			{
				Debug.Log("click");
				this.transform.parent.gameObject.SetActive(false);
			}
			else
			{
				this.transform.parent.gameObject.SetActive(false); 
			}
		}*/

	}

	public void SetName(string value) {
		this.Name.text = value;
	}

	public void SetId(string value) {
		this.id = value;
	}


	public void OnPointerEnter(PointerEventData eventData)
	{
		this.GetComponent<RawImage>().color = Color.gray;
		isPointerInside = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		this.GetComponent<RawImage>().color = this.Color;
		isPointerInside = false;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		//PrimaryContorl.websocket.SendMsg("");
		this.transform.parent.gameObject.SetActive(false);
	}
}
