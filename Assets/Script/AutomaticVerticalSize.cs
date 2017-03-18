using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticVerticalSize : MonoBehaviour {

    public float childHeight = 30f;

	// Use this for initialization
	void Start () {
        AdjustSize();
	}
	
    public void AdjustSize()
    {
        Vector2 size = this.GetComponent<RectTransform>().sizeDelta;
        Vector3 pos = this.GetComponent<RectTransform>().position;
        size.y = this.transform.childCount * childHeight;
        pos.y = size.y / 2f;
        this.GetComponent<RectTransform>().sizeDelta = size;
        this.GetComponent<RectTransform>().position = pos;
    }
}
