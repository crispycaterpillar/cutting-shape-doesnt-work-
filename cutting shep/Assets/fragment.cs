using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fragment : MonoBehaviour {
    public List<linedefinition> edges;
    Vector3 COM;

	// Use this for initialization
	void Start ()
    {
        edges = new List<linedefinition>();
	}
}
