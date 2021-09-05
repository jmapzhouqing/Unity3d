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
    public void CreateCategory(Dictionary<int,string> categoryName,Dictionary<int,List<DeviceInfo>> devices)
    {
        for (int i = 0, number = container.childCount; i < number; i++)
        {
            GameObject.DestroyImmediate(container.GetChild(0).gameObject);
        }
        foreach (KeyValuePair<int,List<DeviceInfo>> item in devices)
        {
            RectTransform child = GameObject.Instantiate<RectTransform>(category_prefab, container);
            CategoryControl categoryControl = child.GetComponentInChildren<CategoryControl>();
            string category_name;
            if (categoryName.TryGetValue(item.Key, out category_name)) {
                categoryControl.SetCategoryName(category_name);
            }
            categoryControl.CreateDeviceList(item.Value);
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
        for (int i = 0, number = container.childCount; i < number; i++){
            GameObject.DestroyImmediate(container.GetChild(0).gameObject);
        }
        this.Exhition(false);
    }
}
