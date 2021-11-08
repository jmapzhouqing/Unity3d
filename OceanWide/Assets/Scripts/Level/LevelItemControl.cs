using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LevelItemControl : MonoBehaviour, IPointerClickHandler
{
    public int level_number;

    private Image image;

    private int projectId;
    private int positionId;
    private string floorName;

    private string levelExhibitionName;

    private LevelExhibitionControl level_exhibition_control;

    // Start is called before the first frame update
    void Awake()
    {
        image = this.GetComponent<Image>();
    }

    // Update is called once per frame
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

        image.color = new Color(10 / 255f, 91 / 255f, 167 / 255f, 1f);
    }

    public void UnSelected()
    {
        Color color = image.color;
        image.color = new Color(60 / 255f, 87 / 255f, 122 / 255f, 0.3f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        this.Selected();

        ResultManager resultManager = GameObject.FindObjectOfType<ResultManager>();

        Transform floor = level_exhibition_control.transform.Find(this.levelExhibitionName);

        PrimaryContorl.qryDeviceByFloor(this.projectId, this.positionId);
        resultManager.SetLevelName(this.floorName);
        if (PrimaryContorl.isDevice)
        {
            resultManager.CreateCategory(floor,PrimaryContorl.categoryDic, PrimaryContorl.deviceDic);
        }
        else
        {
            resultManager.Clear();
        }
    }

    public void setProjectId(int value)
    {
        this.projectId = value;
    }

    public void setPositionId(int value)
    {
        this.positionId = value;
    }
    public void setFloorName(string value)
    {
        this.floorName = value;
    }

    public void SetLevelControl(LevelExhibitionControl control)
    {
        this.level_exhibition_control = control;
    }

    public void SetLevelName(string name)
    {
        this.levelExhibitionName = name;
        this.GetComponentInChildren<Text>().text = name;
    }
}
