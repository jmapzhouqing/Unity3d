using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinkleControl : MonoBehaviour
{
    HighlightableObject[] children;
    // Start is called before the first frame update
    void Awake(){
        children = this.GetComponentsInChildren<HighlightableObject>(true);
    }

    // Update is called once per fram

    public void Twinkle()
    {
        foreach (HighlightableObject child in children) {
            child.ConstantOn();
        }
        StartCoroutine(Shut());
    }


    protected IEnumerator Shut()
    {
        yield return new WaitForSeconds(3.0f);
        foreach (HighlightableObject child in children){
            child.ConstantOff();
        }
    }
}
