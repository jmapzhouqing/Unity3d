using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using httpTool;
using LitJson;
using UIDataStruct;

public class BuilderManager : MonoBehaviour
{
    private RectTransform builder_prefab;

    public RectTransform container;

    private Dictionary<string, List<FloorInfo>> all_building;

    void Awake(){
        builder_prefab = Resources.Load<RectTransform>("UIPrefab/Builder");
        
    }
    private void Start()
    {
        all_building = new Dictionary<string, List<FloorInfo>> {
            { "XHY",PrimaryContorl.XHY},
            { "DF",PrimaryContorl.DF}
        };
        this.CreateBuilder();
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void CreateBuilder() {
        foreach (string key in all_building.Keys) {
            RectTransform child = GameObject.Instantiate<RectTransform>(builder_prefab, container);
            BuilderControl builderControl = child.GetComponentInChildren<BuilderControl>();
            switch (key) {
                case "XHY":
                    builderControl.SetBuilderName("香海园");
                    break;
                case "DF":
                    builderControl.SetBuilderName("东府");
                    break;
            }
            
            builderControl.CreateLevelItem(all_building[key], key);
        }
    }
}
