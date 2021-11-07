using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using LitJson;

public class Util:EditorWindow
{
    [MenuItem("tools/prfab")]
    public static void ReplacePrefab() {
        GameObject[] selected = Selection.gameObjects;
        GameObject origin_q = AssetDatabase.LoadAssetAtPath("Assets/Resource/prefab/device/枪机.prefab", typeof(GameObject)) as GameObject;
        GameObject origin_Q = AssetDatabase.LoadAssetAtPath("Assets/Resource/prefab/device/球机.prefab", typeof(GameObject)) as GameObject;

        GameObject origin = AssetDatabase.LoadAssetAtPath("Assets/Resource/prefab/device/水表.prefab", typeof(GameObject)) as GameObject;
        foreach (GameObject item in selected) {
            GameObject child = null;
            
            
            if (item.transform.childCount == 2)
            {
                child = PrefabUtility.InstantiatePrefab(origin_q) as GameObject;
            }
            else {
                child = PrefabUtility.InstantiatePrefab(origin_Q) as GameObject;
            }

            //child = PrefabUtility.InstantiatePrefab(origin) as GameObject;

            child.transform.localScale = new Vector3(1, 1, 1);

            child.transform.SetParent(item.transform.parent);
            child.name = item.name;
            child.transform.position = item.transform.position;
            
            //child.transform.rotation = item.transform.rotation;

            /*Vector3 euler = item.transform.rotation.eulerAngles;

            child.transform.rotation = Quaternion.Euler(euler.x,euler.y,euler.z+90);*/

            /*
            Vector3 euler = item.transform.eulerAngles;
            child.transform.eulerAngles = new Vector3(euler.x, euler.y, euler.z);*/
            GameObject.DestroyImmediate(item);
        }
    }

    [MenuItem("tools/CreateInfo")]
    public static void CreateInfo()
    {
        GameObject[] selected = Selection.gameObjects;
        foreach (GameObject item in selected)
        {
            Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
            
            for (int i = 0, len = item.transform.childCount; i < len; i++) {
                Transform child = item.transform.GetChild(i);
                List<string> ids = new List<string>();

                foreach (TwinkleControl control in child.GetComponentsInChildren<TwinkleControl>(true)) {
                    ids.Add(control.name);
                }

                if (ids.Count != 0) {
                    dic.Add(child.name, ids);
                }
            }

            Debug.Log(JsonMapper.ToJson(dic));
        }
    }
}
