using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using System.IO;

public class CreateMaterial : EditorWindow
{
    [MenuItem("Assets/CreateMaterial")]
    public static void create_material() {
        GameObject[] selected = Selection.gameObjects;

        foreach (GameObject item in selected) {
            string path = AssetDatabase.GetAssetPath(item);
            path = Path.GetDirectoryName(path);

            string material_path = Path.Combine(path, "Materials");
            string texture_path = Path.Combine(path, "texture");

            List<Material> materials = new List<Material>();

            foreach (Material mat in item.GetComponent<MeshRenderer>().sharedMaterials) {
                string map_path = Path.Combine(texture_path, item.name + ".jpg");
                string mat_name = item.name + "-" + mat.name+".mat";
                string mat_path = Path.Combine(material_path, mat_name);

                Material clone_mat = new Material(mat);
                AssetDatabase.CreateAsset(clone_mat, mat_path);

                Texture texture = AssetDatabase.LoadAssetAtPath<Texture>(map_path);

                clone_mat.SetTexture("_DetailAlbedoMap", texture);

                materials.Add(clone_mat);    
                
                //clone_mat.se
            }

            item.GetComponent<MeshRenderer>().sharedMaterials = materials.ToArray();


            Debug.Log(path);
        }
    }
}
