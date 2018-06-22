using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fragmentmanager : MonoBehaviour {
    public static List<fragment> shapes;
    Mesh tempMesh;
    fragment tempFrag;
    Vector3 shapepos;

	// Use this for initialization
	void Start ()
    {
        shapes = new List<fragment>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RecalculateBoundaries();
        }
	}

    void RecalculateBoundaries()
    {
        for (int i = 0; i < shapes.Count; i++)
        {
            tempFrag = shapes[i].gameObject.GetComponent<fragment>();
            shapepos = tempFrag.gameObject.transform.position;
            tempMesh = shapes[i].gameObject.GetComponent<MeshFilter>().mesh;
            CSEdges();
        }
    }

    void CSEdges()
    {
        for (int j = 0; j < tempMesh.vertices.Length; j++)
        {
            CSGradient(j);
            CSConstant(j);
        }
    }

    void CSGradient(int j)
    {
        if (j != tempMesh.vertices.Length - 1)
        {
            tempFrag.edges[j].m = (((shapepos.y + tempMesh.vertices[j + 1].y) - (shapepos.y + tempMesh.vertices[j].y)) / ((shapepos.x + tempMesh.vertices[j + 1].x) - (shapepos.x + tempMesh.vertices[j].x)));
        }
        else tempFrag.edges[j].m = ((shapepos.y + tempMesh.vertices[0].y) - (shapepos.y + tempMesh.vertices[j].y)) / ((shapepos.x + tempMesh.vertices[0].x) - (shapepos.x + tempMesh.vertices[j].x));
    }

    void CSConstant(int j)
    {
        tempFrag.edges[j].c = (shapepos.y + tempMesh.vertices[j].y) / (tempFrag.edges[j].m * (shapepos.x + tempMesh.vertices[j].x));
    }

}
