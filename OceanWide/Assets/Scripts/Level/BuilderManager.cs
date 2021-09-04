using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderManager : MonoBehaviour
{
    private RectTransform builder_prefab;

    public RectTransform container;

    void Awake(){
        builder_prefab = Resources.Load<RectTransform>("UIPrefab/Builder");
        this.CreateBuilder();
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void CreateBuilder() {
        for (int i = 0; i < 2; i++) {
            RectTransform child = GameObject.Instantiate<RectTransform>(builder_prefab, container);
            BuilderControl builderControl = child.GetComponentInChildren<BuilderControl>();
            builderControl.SetBuilderName("香海园-"+i);
            builderControl.CreateLevelItem();
        }
    }

    public void Clear(){
        for (int i = 0, number = container.childCount; i < number; i++){
            GameObject.DestroyImmediate(container.GetChild(0).gameObject);
        }
    }
}
