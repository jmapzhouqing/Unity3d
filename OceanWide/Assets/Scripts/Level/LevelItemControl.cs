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

    private int categoryId;
    private int positionId;
    private string floorName;
    
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


        image.color = new Color(10 / 255f, 91 / 255f, 167 / 255f, 1f);
    }

    public void UnSelected() {
        Color color = image.color;
        image.color = new Color(60 / 255f, 87 / 255f, 122 / 255f, 0.3f);
    }

    public void OnPointerClick(PointerEventData eventData) {
        this.Selected();
        PrimaryContorl.qryDeviceByFloor(this.categoryId, this.positionId);
        GameObject.FindObjectOfType<ResultManager>().SetLevelName(this.floorName);
        if (PrimaryContorl.isDevice)
        {
            GameObject.FindObjectOfType<ResultManager>().CreateCategory(PrimaryContorl.categoryDic, PrimaryContorl.deviceDic);
        }
        else {
            GameObject.FindObjectOfType<ResultManager>().Clear();
        }
    }

    public void setCategoryId(int value) {
        this.categoryId = value;
    }

    public void setPositionId(int value)
    {
        this.positionId = value;
    }
    public void setFloorName(string value)
    {
        this.floorName = value;
    }
}
