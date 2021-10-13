using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Util:EditorWindow
{
    [MenuItem("tools/prfab")]
    public static void ReplacePrefab() {
        GameObject[] selected = Selection.gameObjects;
        GameObject origin = AssetDatabase.LoadAssetAtPath("Assets/Resource/prefab/球机.prefab", typeof(GameObject)) as GameObject;
        foreach (GameObject item in selected) {
            
            GameObject child = PrefabUtility.InstantiatePrefab(origin) as GameObject;

            child.transform.SetParent(item.transform.parent);
            child.name = item.name;
            child.transform.position = item.transform.position;
            child.transform.rotation = item.transform.rotation;

            /*Vector3 euler = item.transform.rotation.eulerAngles;

            child.transform.rotation = Quaternion.Euler(euler.x,euler.y,euler.z+90);*/

            /*
            Vector3 euler = item.transform.eulerAngles;
            child.transform.eulerAngles = new Vector3(euler.x + 90, euler.y, euler.z);*/
            GameObject.DestroyImmediate(item);
        }
    }
}
