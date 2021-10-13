using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

<<<<<<< HEAD
public class LevelItemControl : MonoBehaviour, IPointerClickHandler
=======
public class LevelItemControl : MonoBehaviour,IPointerClickHandler
>>>>>>> dab206c7fe45844fb501de68831e78a7a77090d3
{
    public int level_number;

    private Image image;

    private int categoryId;
    private int positionId;
    private string floorName;
<<<<<<< HEAD

    private string levelExhibitionName;

    private LevelExhibitionControl level_exhibition_control;

    // Start is called before the first frame update
    void Awake()
    {
=======
    
    // Start is called before the first frame update
    void Awake(){
>>>>>>> dab206c7fe45844fb501de68831e78a7a77090d3
        image = this.GetComponent<Image>();
    }

    // Update is called once per frame
<<<<<<< HEAD
    void Update()
    {

    }

    public void Selected()
    {
        foreach (LevelItemControl control in this.transform.parent.GetComponentsInChildren<LevelItemControl>(true))
        {
            control.UnSelected();
        }

        Color color = image.color;
        //image.color = new Color(color.r,color.g,color.b,1);

        if (this.level_exhibition_control != null)
        {
            this.level_exhibition_control.SelectLevel(this.levelExhibitionName);
        }
=======
    void Update(){
        
    }

    public void Selected(){
        foreach (LevelItemControl control in this.transform.parent.GetComponentsInChildren<LevelItemControl>(true)) {
            control.UnSelected();
        }

>>>>>>> dab206c7fe45844fb501de68831e78a7a77090d3

        image.color = new Color(10 / 255f, 91 / 255f, 167 / 255f, 1f);
    }

<<<<<<< HEAD
    public void UnSelected()
    {
=======
    public void UnSelected() {
>>>>>>> dab206c7fe45844fb501de68831e78a7a77090d3
        Color color = image.color;
        image.color = new Color(60 / 255f, 87 / 255f, 122 / 255f, 0.3f);
    }

<<<<<<< HEAD
    public void OnPointerClick(PointerEventData eventData)
    {
=======
    public void OnPointerClick(PointerEventData eventData) {
>>>>>>> dab206c7fe45844fb501de68831e78a7a77090d3
        this.Selected();
        PrimaryContorl.qryDeviceByFloor(this.categoryId, this.positionId);
        GameObject.FindObjectOfType<ResultManager>().SetLevelName(this.floorName);
        if (PrimaryContorl.isDevice)
        {
            GameObject.FindObjectOfType<ResultManager>().CreateCategory(PrimaryContorl.categoryDic, PrimaryContorl.deviceDic);
        }
<<<<<<< HEAD
        else
        {
=======
        else {
>>>>>>> dab206c7fe45844fb501de68831e78a7a77090d3
            GameObject.FindObjectOfType<ResultManager>().Clear();
        }
    }

<<<<<<< HEAD
    public void setCategoryId(int value)
    {
=======
    public void setCategoryId(int value) {
>>>>>>> dab206c7fe45844fb501de68831e78a7a77090d3
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
<<<<<<< HEAD

    public void SetLevelControl(LevelExhibitionControl control)
    {
        this.level_exhibition_control = control;
    }

    public void SetLevelName(string name)
    {
        this.levelExhibitionName = name;
        this.GetComponentInChildren<Text>().text = name;
    }
=======
>>>>>>> dab206c7fe45844fb501de68831e78a7a77090d3
}
