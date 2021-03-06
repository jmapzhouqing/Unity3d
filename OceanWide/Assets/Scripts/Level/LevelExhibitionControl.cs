using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelExhibitionControl : MonoBehaviour
{
    public Vector3 increment = new Vector3(0, 5, 0);

    public Vector3 target_position;
    public Vector2 rotation;
    public float distance;

    private int index = 0;

    private float duration = 0.2f;

    private List<Vector3> origin_position;
    private List<Transform> children;
    private int child_number = 0;

    private Sequence sequence;

    private int pre_index = -1;

    private CameraControl camera_control;

    private GameObject terrain;
    // Start is called before the first frame update
    void Awake()
    {
        terrain = GameObject.Find("terrain_" + this.name);

        camera_control = FindObjectOfType<CameraControl>();

        origin_position = new List<Vector3>();
        children = new List<Transform>();

        child_number = this.transform.childCount;
        for (int i = 0; i < child_number; i++)
        {
            Transform trans = this.transform.GetChild(i);
            children.Add(trans);
            origin_position.Add(trans.position);
        }
    }

    public void SelectLevel(string name){
        if (name.Contains("B"))
        {
            if (terrain.activeSelf)
            {
                terrain.SetActive(false);
            }
        }else {
            if (!terrain.activeSelf) {
                terrain.SetActive(true);
            }
        }

        int? index = this.transform.Find(name)?.GetSiblingIndex();
        if (index.HasValue) {
            this.SelectLevel(index.Value);
        }
        
    }

    public void SelectLevel(int index)
    {
        if (sequence != null && sequence.IsPlaying())
        {
            return;
        }

        if (pre_index != -1)
        {
            if (index < pre_index)
            {
                for (int i = index + 1; i <= pre_index; i++)
                {
                    sequence.Append(children[i].DOMove(origin_position[i] + increment, duration).Play()).SetAutoKill(true);
                }
            }
            else if (index > pre_index)
            {
                for (int i = pre_index + 1; i <= index; i++)
                {
                    sequence.Append(children[i].DOMove(origin_position[i], duration).Play()).SetAutoKill(true);
                }
            }
        }
        else
        {
            for (int i = index + 1; i < child_number; i++)
            {
                sequence.Append(children[i].DOMove(origin_position[i] + increment, duration).Play()).SetAutoKill(true);
            }
        }

        pre_index = index;
    }

    public void CameraLocation()
    {
        camera_control.target_position = this.target_position;
        camera_control.distance = this.distance;
        camera_control.rotation = this.rotation;
    }

    public void Recover()
    {
        pre_index = -1;
        if (!terrain.activeSelf) {
            terrain.SetActive(true);
        }

        for (int i = 0, number = this.transform.childCount; i < number; i++){
            Transform child = this.transform.GetChild(i);
            child.transform.position = origin_position[i];
        }
    }
}
