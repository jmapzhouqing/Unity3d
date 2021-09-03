using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LevelItemControl : MonoBehaviour,IPointerClickHandler
{
    public int level_number;

    private Image image;
    // Start is called before the first frame update
    void Awake(){
        image = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void Selected(){
        foreach (LevelItemControl control in this.transform.parent.GetComponentsInChildren<LevelItemControl>(true)) {
            control.UnSelected();
        }

        Color color = image.color;
        image.color = new Color(color.r,color.g,color.b,1);
    }

    public void UnSelected() {
        Color color = image.color;
        image.color = new Color(color.r, color.g, color.b, 0.5f);
    }

    public void OnPointerClick(PointerEventData eventData){
        this.Selected();
    }
}
