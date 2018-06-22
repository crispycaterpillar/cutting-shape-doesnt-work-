using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startingsquarecreator : MonoBehaviour {
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

	// Use this for initialization
	void Start ()
    {
        MakeMeshData();
        CreateMesh();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void MakeMeshData()
    {
        vertices = new Vector3[] { new Vector3(-2, 2, 0), new Vector3(2, 2, 0), new Vector3(2, -2, 0), new Vector3(-2, -2, 0) };
        triangles = new int[] { 0, 1, 3, 1, 2, 3 };
    }

    void CreateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}
