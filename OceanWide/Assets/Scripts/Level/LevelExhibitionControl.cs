using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelExhibitionControl : MonoBehaviour
{
<<<<<<< HEAD
    public Vector3 increment = new Vector3(0, 5, 0);

    public Vector3 target_position;
    public Vector2 rotation;
    public float distance;
=======
    public Vector3 increment = new Vector3(0,5,0);
>>>>>>> dab206c7fe45844fb501de68831e78a7a77090d3

    private int index = 0;

    private float duration = 0.2f;

    private List<Vector3> origin_position;
    private List<Transform> children;
    private int child_number = 0;

    private Sequence sequence;

    private int pre_index = -1;
<<<<<<< HEAD

    private CameraControl camera_control;

    private GameObject terrain;
    // Start is called before the first frame update
    void Awake()
    {
        terrain = GameObject.Find("terrain_" + this.name);

        camera_control = FindObjectOfType<CameraControl>();

=======
    // Start is called before the first frame update
    void Awake()
    {
>>>>>>> dab206c7fe45844fb501de68831e78a7a77090d3
        origin_position = new List<Vector3>();
        children = new List<Transform>();

        child_number = this.transform.childCount;
<<<<<<< HEAD
        for (int i = 0; i < child_number; i++)
        {
=======
        for (int i = 0; i < child_number; i++) {
>>>>>>> dab206c7fe45844fb501de68831e78a7a77090d3
            Transform trans = this.transform.GetChild(i);
            children.Add(trans);
            origin_position.Add(trans.position);
        }
    }

<<<<<<< HEAD
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
        int index = this.transform.Find(name).GetSiblingIndex();
        this.SelectLevel(index);
    }

    public void SelectLevel(int index)
    {
        if (sequence != null && sequence.IsPlaying())
        {
            return;
        }

=======
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectLevel(int index) {
        if (sequence != null && sequence.IsPlaying()) {
            return;
        }

        //index = Mathf.FloorToInt(Random.Range(0,child_number))+1;

>>>>>>> dab206c7fe45844fb501de68831e78a7a77090d3
        if (pre_index != -1)
        {
            if (index < pre_index)
            {
<<<<<<< HEAD
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
=======
                for (int i = index; i < pre_index; i++){
                    sequence.Append(children[i].DOMove(origin_position[i] + increment, duration)).SetAutoKill(true);
                }
            }else if (index > pre_index) {
                for (int i = pre_index; i < index; i++){
                    sequence.Append(children[i].DOMove(origin_position[i], duration)).SetAutoKill(true);
                }
            }
        }else {
            for (int i = index; i < child_number; i++){
                sequence.Append(children[i].DOMove(origin_position[i] + increment, duration)).SetAutoKill(true);
            }
        }
        pre_index = index;
    }
>>>>>>> dab206c7fe45844fb501de68831e78a7a77090d3
}
