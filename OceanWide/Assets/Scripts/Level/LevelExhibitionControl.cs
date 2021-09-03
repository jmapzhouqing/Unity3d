using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelExhibitionControl : MonoBehaviour
{
    public Vector3 increment = new Vector3(0,5,0);

    private int index = 0;

    private float duration = 0.2f;

    private List<Vector3> origin_position;
    private List<Transform> children;
    private int child_number = 0;

    private Sequence sequence;

    private int pre_index = -1;
    // Start is called before the first frame update
    void Awake()
    {
        origin_position = new List<Vector3>();
        children = new List<Transform>();

        child_number = this.transform.childCount;
        for (int i = 0; i < child_number; i++) {
            Transform trans = this.transform.GetChild(i);
            children.Add(trans);
            origin_position.Add(trans.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectLevel(int index) {
        if (sequence != null && sequence.IsPlaying()) {
            return;
        }

        //index = Mathf.FloorToInt(Random.Range(0,child_number))+1;

        if (pre_index != -1)
        {
            if (index < pre_index)
            {
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
}
