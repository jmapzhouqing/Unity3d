using System.Collections;
using System.Collections.Generic;
using UIDataStruct;
using UnityEngine;
using UnityEngine.EventSystems;


public class HtmlResultControl : MonoBehaviour,IPointerClickHandler
{
    // Start is called before the first frame update
    private string urlParam;
    private Transform dynamic_container;

    private string urlPrefix = "http://uat.qywlsp.com.cn/html-new/index.html?data=";

    public void SetUrlParam(string value) {
        this.urlParam = value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            DeviceDetailControl deviceDetailControl = dynamic_container.GetComponent<DeviceDetailControl>();
            deviceDetailControl.setContainerHTML(urlPrefix + this.urlParam);

            /*switch (this.urlParam) {
                case "er_bpdjcxt":
                    deviceDetailControl.setContainerHTML(urlPrefix+ "distributionElectricity/dongfu-high");
                    break;
                case "er_bpdzt":
                    deviceDetailControl.setContainerHTML(urlPrefix + "distributionElectricity/dongfu-high-list");
                    break;
                case "er_db":
                    deviceDetailControl.setContainerHTML(urlPrefix + "distributionElectricity/dongfu-low");
                    break;
                case "er_xfjcxt":
                    deviceDetailControl.setContainerHTML(urlPrefix + "fireFightingSystem/floor?type=1");
                    break;
                case "er_rlsbjl":
                    deviceDetailControl.setContainerHTML(urlPrefix + "accessControl/access?local=0");
                    break;
                case "er_tcccx":
                    deviceDetailControl.setContainerHTML(urlPrefix + "fireFightingSystem/floor");
                    break;
                case "er_znzmglxt":
                    deviceDetailControl.setContainerHTML(urlPrefix + "illuminationSystem/index");
                    break;
                case "yi_nhglxt":
                    deviceDetailControl.setContainerHTML(urlPrefix + "distributionElectricity/index");
                    break;
                case "yi_mjskjl":
                    deviceDetailControl.setContainerHTML(urlPrefix + "accessControl/door?positionCode=LHY");
                    break;
                case "yi_rlsbjl":
                    deviceDetailControl.setContainerHTML(urlPrefix + "accessControl/index?local=1");
                    break;
            }*/
        }
    }
    void Start(){
        dynamic_container = this.transform.root.Find("deviceContainer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
