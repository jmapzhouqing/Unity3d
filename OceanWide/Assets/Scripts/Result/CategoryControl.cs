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
    // Start is called before the first frame update
    void Awake()
    {
        result_prefab = Resources.Load<RectTransform>("UIPrefab/result");

        DOTween.Init(true, true, null);
        DOTween.defaultAutoPlay = AutoPlay.None;
        DOTween.defaultAutoKill = true;

        element = this.GetComponent<LayoutElement>();
        this.size = new Vector2(element.preferredWidth, element.preferredHeight);
        this.Expand(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCategoryName(string name) {
        this.title.text = name;
    }

    public void CreateDeviceList(List<DeviceInfo> devices)
    {
        int i = 0;
        foreach (DeviceInfo device in devices) {
            RectTransform child = GameObject.Instantiate<RectTransform>(result_prefab, container);
            child.gameObject.GetComponentInChildren<Text>().text = device.deviceName;
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
            float height = 400; //(title.rectTransform.sizeDelta.y + container.sizeDelta.y);

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
            foreach (BuilderControl control in this.transform.parent.GetComponentsInChildren<BuilderControl>(true))
            {
                control.Expand(false);
            }
            size = new Vector2(this.size.x, title.rectTransform.sizeDelta.y + container.sizeDelta.y);

            tween = element.DOPreferredSize(size, duration).Play();
            expand_control.sprite = expand_img;
        }
        else
        {
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
