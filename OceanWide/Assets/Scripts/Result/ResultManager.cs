using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UIDataStruct;


public class ResultManager : MonoBehaviour
{
    // Start is called before the first frame update
    private RectTransform category_prefab;

    public RectTransform container;

    public Text title;

    public RectTransform result_exhibition;
    private Tween result_tween;

    private float duration = 0.2f;

    private Dictionary<int, CategoryControl> categoryControlList;
    private RectTransform result_prefab;
    private int positionId;
    private bool isLoad = false;
    private Dictionary<string, string> DFHtml = new Dictionary<string, string>() {
        { "变配电监测系统","er_bpdjcxt"},{ "变配电状态","er_bpdzt"},{ "电表系统","er_db"}
    ,{ "消防监测系统","er_xfjcxt"} ,{ "人脸识别记录","er_rlsbjl"} ,{ "停车场查询","er_tcccx"}
    ,{ "智能照明管理系统","er_znzmglxt"} };
    private Dictionary<string, string> LHYHtml = new Dictionary<string, string>() {
        { "电表系统","yi_nhglxt"},{ "门禁刷卡记录","yi_mjskjl"},{ "人脸识别记录","yi_rlsbjl"}
    ,{ "停车场查询","yi_tcccx"}};
    void Awake()
    {
        DOTween.Init(true, true, null);
        DOTween.defaultAutoPlay = AutoPlay.None;
        DOTween.defaultAutoKill = false;


        if (result_exhibition != null)
        {
            result_tween = result_exhibition.DOAnchorPos3DX(0, duration);
        }

        category_prefab = Resources.Load<RectTransform>("UIPrefab/Category");
        
        result_prefab = Resources.Load<RectTransform>("UIPrefab/htmlResult");

        categoryControlList = new Dictionary<int, CategoryControl>();
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetLevelName(string name) {
        this.title.text = name;
    }

    public void SetPositionId(int value) {
        this.positionId = value;
    }

    public void CreateCategory(Transform floor,Dictionary<int,string> categoryName,Dictionary<int,List<DeviceInfo>> devices)
    {
        /*for (int i = 0, number = container.childCount; i < number; i++)
        {
            GameObject.DestroyImmediate(container.GetChild(0).gameObject);
        }*/
        //东府
        if (this.positionId == 47 && !isLoad)
        {
            isLoad = true;
            foreach (KeyValuePair<string, string> item in DFHtml) {
                RectTransform child = GameObject.Instantiate<RectTransform>(result_prefab, container);
                HtmlResultControl resultControl = child.GetComponentInChildren<HtmlResultControl>();
                child.gameObject.GetComponentInChildren<Text>().text = item.Key;
                resultControl.SetUrlParam(item.Value);
            }
        } else if (this.positionId == 24 && !isLoad) {
            isLoad = true;
            foreach (KeyValuePair<string, string> item in LHYHtml)
            {
                RectTransform child = GameObject.Instantiate<RectTransform>(result_prefab, container);
                HtmlResultControl resultControl = child.GetComponentInChildren<HtmlResultControl>();
                child.gameObject.GetComponentInChildren<Text>().text = item.Key;
                resultControl.SetUrlParam(item.Value);
            }
        }
        else {
            isLoad = false;
        }

        foreach (KeyValuePair<int,List<DeviceInfo>> item in devices)
        {
            if (!categoryControlList.ContainsKey(item.Key))
            {
                RectTransform child = GameObject.Instantiate<RectTransform>(category_prefab, container);
                CategoryControl categoryControl = child.GetComponentInChildren<CategoryControl>();
                categoryControlList.Add(item.Key, categoryControl);
                categoryControl.target = floor;
                string category_name;
                if (categoryName.TryGetValue(item.Key, out category_name))
                {
                    categoryControl.SetCategoryName(category_name);
                }
                categoryControl.CreateDeviceList(item.Value);
            }
            else {
                categoryControlList[item.Key].CreateDeviceList(item.Value);
            }
        }

        if (devices.Count != 0) {
            this.Exhition(true);
        }
    }

    private void Exhition(bool station){
        if (result_tween != null){
            if (result_tween.IsPlaying())
            {
                result_tween.Pause();
            }

            if (station)
            {
                result_tween.PlayForward();
            }
            else
            {
                result_tween.PlayBackwards();
            }
        }
    }

    public void Clear() {
        this.title.name = "";
        this.categoryControlList.Clear();
        for (int i = 0, number = container.childCount; i < number; i++){
            GameObject.DestroyImmediate(container.GetChild(0).gameObject);
        }
        this.Exhition(false);
    }
}
