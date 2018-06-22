using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linedefinition {
    public float m, c;


	// Use this for initialization
	void Start () {
        m = 0;
        c = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static Vector3 SolveSimEquations(linedefinition edge, linedefinition cut)
    {
        Vector3 answer = new Vector3(0, 0, 0);
        answer.x = (edge.c - cut.c) / (cut.m - edge.m);
        answer.y = answer.x * cut.m + cut.c;
        return answer;
    }

    
}
