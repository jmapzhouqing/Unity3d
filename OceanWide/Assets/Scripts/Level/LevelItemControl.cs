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

    private string levelExhibitionName;

    private LevelExhibitionControl level_exhibition_control;
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

        if (this.level_exhibition_control != null) {
            this.level_exhibition_control.SelectLevel(this.levelExhibitionName);
        }
    }

    public void UnSelected() {
        Color color = image.color;
        image.color = new Color(color.r, color.g, color.b, 0.5f);
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

    public void SetLevelControl(LevelExhibitionControl control) {
        this.level_exhibition_control = control;
    }

    public void SetLevelName(string name) {
        this.levelExhibitionName = name;
        this.GetComponentInChildren<Text>().text = name;
    }
}
