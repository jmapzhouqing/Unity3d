using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UIDataStruct;
using System;

public class BuilderControl : MonoBehaviour
{
    public RectTransform container;

    public Text title;

<<<<<<< HEAD
    public Sprite expand_img;

    public Sprite unexpand_img;

    public Image expand;
=======
    public Texture expand_img;

    public Texture unExpand_img;

    public RawImage background;
>>>>>>> dab206c7fe45844fb501de68831e78a7a77090d3

    private Vector2 size;

    private bool is_expand = false;

    private Tween tween;

    private float duration = 0.2f;

    private LayoutElement element;

    private RectTransform level_prefab;

    private Dictionary<int, LevelItemControl> levels;

<<<<<<< HEAD
    public LevelExhibitionControl level_exhibition_control;

    private ResultManager result_manager;

    // Start is called before the first frame update
    void Awake()
    {
=======
    // Start is called before the first frame update
    void Awake() {
>>>>>>> dab206c7fe45844fb501de68831e78a7a77090d3
        DOTween.Init(true, true, null);
        DOTween.defaultAutoPlay = AutoPlay.None;
        DOTween.defaultAutoKill = true;

<<<<<<< HEAD
        result_manager = FindObjectOfType<ResultManager>();

=======
>>>>>>> dab206c7fe45844fb501de68831e78a7a77090d3
        levels = new Dictionary<int, LevelItemControl>();

        level_prefab = Resources.Load<RectTransform>("UIPrefab/LevelItem");

        element = this.GetComponent<LayoutElement>();
        this.size = new Vector2(element.preferredWidth, element.preferredHeight);
        this.Expand(false);
    }

    // Update is called once per frame
<<<<<<< HEAD
    void Update()
    {

    }

    public void SetBuilderName(string name)
    {
        title.text = name;
    }

    public void CreateLevelItem(List<FloorInfo> floorList, string name)
    {
        level_exhibition_control = GameObject.Find(name)?.GetComponent<LevelExhibitionControl>();

        for (int i = 0; i < floorList.Count; i++)
        {
            RectTransform child = GameObject.Instantiate<RectTransform>(level_prefab, container);
            LevelItemControl control = child.GetComponentInChildren<LevelItemControl>();
            switch (name)
            {
                case "LHY":
                    child.gameObject.GetComponentInChildren<Text>().text = Enum.Format(typeof(LHYfloor), floorList[i].positionId, "g");
                    control.setCategoryId(3);
                    control.setFloorName("兰海园3号楼" + floorList[i].positionName);
                    control.SetLevelName(Enum.Format(typeof(LHYfloor), floorList[i].positionId, "g"));
                    break;
                case "DF":
                    control.setCategoryId(3);
                    control.setFloorName("东府5号楼" + floorList[i].positionName);
                    control.SetLevelName(Enum.Format(typeof(DFfloor), floorList[i].positionId, "g"));
                    break;
            }


            control.setPositionId(floorList[i].positionId);
            control.SetLevelControl(this.level_exhibition_control);
            levels.Add(i, control);
        }

        StartCoroutine(UpdateElementHeight());
    }

    public void SetLevel(int index)
    {
        LevelItemControl control;
        if (levels.TryGetValue(index, out control))
        {
=======
    void Update() {

    }

    public void SetBuilderName(string name) {
        title.text = name;
    }

    public void CreateLevelItem(List<FloorInfo> floorList,string name) {
        for (int i = 0; i < floorList.Count; i++) {
            RectTransform child = GameObject.Instantiate<RectTransform>(level_prefab, container);
            LevelItemControl control = child.GetComponentInChildren<LevelItemControl>();
            switch (name) {
                case "LHY":
                    child.gameObject.GetComponentInChildren<Text>().text = Enum.Format(typeof(LHYfloor), floorList[i].positionId, "g");
                    control.setCategoryId(3);
                    control.setFloorName("兰海园3号楼"+floorList[i].positionName);
                    break;
                case "DF":
                    child.gameObject.GetComponentInChildren<Text>().text = Enum.Format(typeof(DFfloor), floorList[i].positionId, "g");
                    control.setCategoryId(3);
                    control.setFloorName("东府5号楼" + floorList[i].positionName);
                    break;
            }
            control.setPositionId(floorList[i].positionId);
            levels.Add(i, control);
        }
        StartCoroutine(UpdateElementHeight());
    }

    public void SetLevel(int index) {
        LevelItemControl control;
        if (levels.TryGetValue(index, out control)) {
>>>>>>> dab206c7fe45844fb501de68831e78a7a77090d3
            control.Selected();
        }
    }

<<<<<<< HEAD
    private IEnumerator UpdateElementHeight()
    {
        if (is_expand)
        {
            while (container.sizeDelta.y < Mathf.Pow(10, -2))
            {
=======
    private IEnumerator UpdateElementHeight() {
        if (is_expand) {
            while (container.sizeDelta.y < Mathf.Pow(10, -2)) {
>>>>>>> dab206c7fe45844fb501de68831e78a7a77090d3
                yield return new WaitForEndOfFrame();
            }
            float height = (title.rectTransform.sizeDelta.y + container.sizeDelta.y);

<<<<<<< HEAD
            while (Mathf.Abs(element.preferredHeight - height) > Mathf.Pow(10, -2))
            {
=======
            while (Mathf.Abs(element.preferredHeight - height) > Mathf.Pow(10, -2)) {
>>>>>>> dab206c7fe45844fb501de68831e78a7a77090d3
                element.preferredHeight = height;
                yield return new WaitForEndOfFrame();
            }
        }
        //this.size = new Vector2(element.preferredWidth,element.preferredHeight);
    }

<<<<<<< HEAD
    public void Expand(bool is_expand)
    {
=======
    public void Expand(bool is_expand) {
>>>>>>> dab206c7fe45844fb501de68831e78a7a77090d3
        if (tween != null && tween.IsPlaying())
        {
            return;
        }

<<<<<<< HEAD
        if (is_expand)
        {
            foreach (BuilderControl control in this.transform.parent.GetComponentsInChildren<BuilderControl>(true))
            {
=======
        if (is_expand) {
            foreach (BuilderControl control in this.transform.parent.GetComponentsInChildren<BuilderControl>(true)) {
>>>>>>> dab206c7fe45844fb501de68831e78a7a77090d3
                control.Expand(false);
            }
            size = new Vector2(this.size.x, title.rectTransform.sizeDelta.y + container.sizeDelta.y);

            tween = element.DOPreferredSize(size, duration).Play();
<<<<<<< HEAD

            if (this.level_exhibition_control)
            {
                this.level_exhibition_control.CameraLocation();
            }

            expand.sprite = expand_img;
        }
        else
        {
            tween = element.DOPreferredSize(new Vector2(this.size.x, title.rectTransform.sizeDelta.y), duration).Play();
            if (this.level_exhibition_control)
            {
                this.level_exhibition_control.Recover();
            }
            result_manager.Clear();
            expand.sprite = unexpand_img;
=======
            background.texture = expand_img;
        } else {
            tween = element.DOPreferredSize(new Vector2(this.size.x, title.rectTransform.sizeDelta.y), duration).Play();
            background.texture = unExpand_img;
>>>>>>> dab206c7fe45844fb501de68831e78a7a77090d3
        }

        this.is_expand = is_expand;
    }

<<<<<<< HEAD
    public void Expand()
    {
        this.Expand(!is_expand);
    }

    public void Clear()
    {
        for (int i = 0, number = container.childCount; i < number; i++)
        {
=======
    public void Expand() {
        this.Expand(!is_expand);
    }

    public void Clear(){
        for (int i = 0, number = container.childCount; i < number; i++) {
>>>>>>> dab206c7fe45844fb501de68831e78a7a77090d3
            GameObject.DestroyImmediate(container.GetChild(0).gameObject);
        }
    }
}
