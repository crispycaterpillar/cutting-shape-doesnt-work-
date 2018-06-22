using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setfloorcolour : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.red);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
