using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceDetailControl : MonoBehaviour
{
    public Transform detail_container;
    private RectTransform detail_prefab;
    // Start is called before the first frame update
    void Start()
    {
        detail_prefab = Resources.Load<RectTransform>("UIPrefab/deviceVedio");
    }

    

    public void close()
    {
        this.gameObject.SetActive(false);
    }

    public void setContainer(DeviceEventType deviceEventType) 
    {
        for (int i = 0, child_number = detail_container.childCount; i < child_number; i++)
        {
            GameObject.DestroyImmediate(detail_container.GetChild(0).gameObject);
        }

        if (deviceEventType == DeviceEventType.DeTailInfo)
        {

        }
        else if (deviceEventType == DeviceEventType.RecordGrid)
        {

        }
        else if (deviceEventType == DeviceEventType.Video)
        {
            RectTransform item = GameObject.Instantiate<RectTransform>(detail_prefab, detail_container);
            UniversalMediaPlayer control = item.GetComponentInChildren<UniversalMediaPlayer>();
            //control.Path = "";
            control.Play();
        }
    }
}
