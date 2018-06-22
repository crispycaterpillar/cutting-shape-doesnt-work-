using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linepositioning : MonoBehaviour {
    LineRenderer cuttingline;
    Vector3 temp;
    float a;

	// Use this for initialization
	void Start () {
        cuttingline = this.gameObject.GetComponent<LineRenderer>();
        cuttingline.enabled = false;
        temp.z = 0;
        a = -0.5f;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetMouseButtonDown(0))
        {
            temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            temp.z = a;
            cuttingline.SetPosition(0, temp);
            cuttingline.SetPosition(1, temp);
            cuttingline.enabled = true;
        }

        else if (Input.GetMouseButton(0))
        {
            temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            temp.z = a;
            cuttingline.SetPosition(1, temp);
        }

        if (Input.GetMouseButtonUp(0))
        {
            cuttingline.enabled = false;
        }
	}
}
