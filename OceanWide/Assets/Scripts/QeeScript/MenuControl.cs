using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UIDataStruct;

public class MenuControl : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public RectTransform childMenu;

    public GameObject prefab;


    private Text Name;

    private Image image;

    private Color Color;

    private RectTransform sub_menu_container_prefab;

    private RectTransform sub_menu_prefab;

    private Transform dynamic_container;
    // Start is called before the first frame update
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
        for (int i = 0, child_number = dynamic_container.childCount; i < child_number; i++){
            GameObject.DestroyImmediate(dynamic_container.GetChild(i).gameObject);
        }

        RectTransform container = GameObject.Instantiate<RectTransform>(sub_menu_container_prefab, dynamic_container);
        container.anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;

        for (int i = 0; i < 5; i++) {
            RectTransform menu = GameObject.Instantiate<RectTransform>(sub_menu_prefab, container);
        }

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

        this.SetSelectedColor();
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
