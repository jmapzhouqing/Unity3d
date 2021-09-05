using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIDataStruct;

public class PrimaryUI : MonoBehaviour
{
    public static Dictionary<string, List<SubMenu>> menus;


    // Start is called before the first frame update
    void Awake()
    {
        menus = new Dictionary<string, List<SubMenu>>();
        SubMenu item = new SubMenu();
        item.name = "空调监测系统";
        item.id = "1";
        List<SubMenu> submenus = new List<SubMenu>();
        submenus.Add(item);
        menus.Add("楼控系统", submenus);
        menus.Add("安防系统", submenus);
        menus.Add("信息管理", submenus);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    
}
