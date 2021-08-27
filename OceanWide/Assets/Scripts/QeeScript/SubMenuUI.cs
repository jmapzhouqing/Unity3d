using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SubMenuUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isPointerInside = false;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (!isPointerInside)
			{
				this.gameObject.SetActive(false);
			}
		}

	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		isPointerInside = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isPointerInside = false;
	}

	
}
