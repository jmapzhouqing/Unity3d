using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogControl : MonoBehaviour
{

    public Text content;
    

    public void setContent(string value) {
        this.content.text = value;
    }

    public void close() {
        this.gameObject.SetActive(false);
    }
}
