using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UIDataStruct;

public class CategoryControl : MonoBehaviour
{
    private RectTransform result_prefab;

    public RectTransform container;

    public Text title;

    public Sprite expand_img;

    public Sprite unexpand_img;

    public Image expand_control;

    private Vector2 size;

    private bool is_expand = false;

    private Tween tween;

    private float duration = 0.2f;

    private LayoutElement element;

    private float viewer_height = 0;

    public Transform target;

    private Transform category_container;

    private string categoryName;

    // Start is called before the first frame update
    void Awake()
    {
        result_prefab = Resources.Load<RectTransform>("UIPrefab/result");

        viewer_height = container.parent.GetComponent<RectTransform>().sizeDelta.y;

        DOTween.Init(true, true, null);
        DOTween.defaultAutoPlay = AutoPlay.None;
        DOTween.defaultAutoKill = true;

        element = this.GetComponent<LayoutElement>();
        this.size = new Vector2(element.preferredWidth, element.preferredHeight);
        this.Expand(false);
    }

    public void SetCategoryName(string name) {
        this.title.text = name;
        this.categoryName = name;
        category_container = target?.Find(name);
       
    }

    public void CreateDeviceList(List<DeviceInfo> devices)
    {
        int i = 0;
        foreach (DeviceInfo device in devices) {
            RectTransform child = GameObject.Instantiate<RectTransform>(result_prefab, container);
            child.gameObject.GetComponentInChildren<Text>().text = device.deviceName == null?device.doorName:device.deviceName;
            ResultControl categoryControl = child.GetComponentInChildren<ResultControl>();
            ResultControl resultControl = child.GetComponentInChildren<ResultControl>();
            resultControl.setDeviceInfo(device);
            if (!string.IsNullOrEmpty(device.rtsp))
            {
                resultControl.DeviceEvent = DeviceEventType.Video;
            }
            else
            {
                resultControl.DeviceEvent = DeviceEventType.DeTailInfo;
            }
            if (i % 2 == 1)
            {
                Color temp = child.gameObject.GetComponent<Image>().color;
                child.gameObject.GetComponent<Image>().color = new Color(temp.r, temp.g, temp.b, 1);
            }
            i++;
        }

        StartCoroutine(UpdateElementHeight());
    }

    private IEnumerator UpdateElementHeight()
    {
        if (is_expand)
        {
            while (container.sizeDelta.y < Mathf.Pow(10, -2))
            {
                yield return new WaitForEndOfFrame();
            }

            float height = (title.rectTransform.sizeDelta.y + viewer_height);

            while (Mathf.Abs(element.preferredHeight - height) > Mathf.Pow(10, -2))
            {
                element.preferredHeight = height;
                yield return new WaitForEndOfFrame();
            }
        }
        //this.size = new Vector2(element.preferredWidth,element.preferredHeight);
    }

    public void Expand(bool is_expand)
    {
        if (tween != null && tween.IsPlaying())
        {
            return;
        }

        if (is_expand)
        {
            foreach (CategoryControl control in this.transform.parent.GetComponentsInChildren<CategoryControl>(true))
            {
                control.Expand(false);
            }
            size = new Vector2(this.size.x,title.rectTransform.sizeDelta.y + viewer_height);

            tween = element.DOPreferredSize(size, duration).Play();
            expand_control.sprite = expand_img;

            category_container?.gameObject.SetActive(true);
        }
        else
        {
            category_container?.gameObject.SetActive(false);

            tween = element.DOPreferredSize(new Vector2(this.size.x, title.rectTransform.sizeDelta.y), duration).Play();
            expand_control.sprite = unexpand_img;
        }

        this.is_expand = is_expand;
    }

    public void Expand()
    {
        this.Expand(!is_expand);
    }
}
