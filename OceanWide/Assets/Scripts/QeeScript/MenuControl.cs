using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UIDataStruct;

public class MenuControl : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public string key = "management";

    public RectTransform childMenu;

    public GameObject prefab;


    private Text Name;

    private Image image;

    private Color Color;

    private RectTransform sub_menu_container_prefab;

    private RectTransform sub_menu_prefab;

    private Transform dynamic_container;
    // Start is called before the first frame update

    public Management management;
    public Management control;

    private Dictionary<string, Dictionary<string,string>> all_menu;

    private ResultManager result_manager;

    private CameraControl camera_control;

    public void Awake()
    {
        camera_control = FindObjectOfType<CameraControl>();
        result_manager = FindObjectOfType<ResultManager>();
        all_menu = new Dictionary<string, Dictionary<string, string>> {
            { "management",new Dictionary<string, string>() },
            { "buildercontrol",new Dictionary<string, string>{ { "8", "BA系统" }, { "7", "人脸识别系统" }, { "6", "能耗系统" }, { "5", "停车场系统" }, { "4","变配电系统" },{"3", "智能照明系统" },{"2","机房环境监测"},{"1", "电梯运行监测" } } },
            { "security",new Dictionary<string, string>{ {"1","视频监控"},{"2","消防系统"}, { "3", "电梯运行监测" } } },
            { "information",new Dictionary<string, string>{ {"1","动态"}} },
        };
    }

    void Start()
    {

        this.Name = this.transform.GetComponentInChildren<Text>();
        this.image = this.transform.GetComponentInChildren<Image>();
        this.Color = this.Name.color;

        sub_menu_container_prefab = Resources.Load<RectTransform>("UIPrefab/SubMenuContainer");

        sub_menu_prefab = Resources.Load<RectTransform>("UIPrefab/SubMenu");

        dynamic_container = this.transform.root.Find("container");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData){
        if (key.Equals("management"))
        {
            management.enabled = true;
            control.enabled = false;
            //关闭详细信息
            Transform detail_container=this.transform.parent.parent.Find("deviceContainer").transform;
            for (int i = 0, child_number = detail_container.childCount; i < child_number; i++)
            {
                GameObject.DestroyImmediate(detail_container.GetChild(0).gameObject);
            }
        }
        else {
            management.enabled = false;
            control.enabled = true;
        }

        for (int i = 0, child_number = dynamic_container.childCount; i < child_number; i++){
            GameObject.DestroyImmediate(dynamic_container.GetChild(0).gameObject);
        }

        RectTransform container = GameObject.Instantiate<RectTransform>(sub_menu_container_prefab, dynamic_container);
        container.anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;


        /*Dictionary<string, string> submenus;

        if (all_menu.TryGetValue(key, out submenus)) {
            foreach (KeyValuePair<string, string> item in submenus) {
                RectTransform menu = GameObject.Instantiate<RectTransform>(sub_menu_prefab, container);
                SubMenuControl control = menu.GetComponentInChildren<SubMenuControl>();
                control.SetName(item.Value);
                control.SetId(item.Key);
            }
        }*/

        //container.sizeDelta

        /*
        int subMenuCnt = childMenu.transform.childCount;
        for (int i = subMenuCnt - 1; i > -1; i--){
            DestroyImmediate(childMenu.transform.GetChild(i).gameObject);
        }
        List<subMenu> subMenuList;
        if (PrimaryUI.menus.TryGetValue(this.Name.text, out subMenuList))
        {
            foreach (subMenu item in subMenuList)
            {
                RectTransform rect = GameObject.Instantiate<GameObject>(prefab).GetComponent<RectTransform>();
                SubMenuControl subMenuControl = rect.GetComponent<SubMenuControl>();
                subMenuControl.SetName(item.name);
                subMenuControl.SetId(item.id);
                subMenuControl.transform.SetParent(this.childMenu.transform);
                rect.anchoredPosition3D = Vector3.zero;
                rect.localScale = new Vector3(1, 1, 1);
            }
            childMenu.anchoredPosition = new Vector2(this.transform.GetComponent<RectTransform>().anchoredPosition.x + 100f, 80f);
            childMenu.gameObject.SetActive(true);
        }*/

        foreach (MenuControl control in this.transform.parent.GetComponentsInChildren<MenuControl>(true)) {
            control.SetUnSelectColor();
        }


        camera_control.ReWind();

        this.SetSelectedColor();
        result_manager.Clear();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       
    }

    public void SetSelectedColor() {
        this.Name.color = Color.cyan;
        this.image.color = Color.cyan;
    }

    public void SetUnSelectColor() {
        this.Name.color = this.Color;
        this.image.color = this.Color;
    }

     
}
