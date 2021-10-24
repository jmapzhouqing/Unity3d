using UnityEngine;
using System.Collections;

public class HighlightingController : MonoBehaviour
{
	protected HighlightableObject ho;
	
	void Awake(){
        ho = gameObject.GetComponent<HighlightableObject>() ?? gameObject.AddComponent<HighlightableObject>();
	}

    private void Start(){
        
    }

    public void Twinkle() {
        ho.ConstantOn();
        StartCoroutine(Shut());
    }

    protected virtual void AfterUpdate() { }


    protected IEnumerator Shut() {
        yield return new WaitForSeconds(3.0f);
        ho.ConstantOff();
    }
}