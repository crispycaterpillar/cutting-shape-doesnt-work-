using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutting : MonoBehaviour {
    Vector3 start;
    Vector3 end;
    Vector3 inter1, inter2, relinter1, relinter2;
    int edge1, edge2;
    public linedefinition cut;
    public GameObject copiable;


	// Use this for initialization
	void Start () {
        cut = new linedefinition();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            start = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("start x is " + start.x);
        }

        if (Input.GetMouseButtonUp(0))
        {
            end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("end x is " + end.x);
            CSCuttingLine();
            CSIntersections();
        }
	}

    void CSCuttingLine()
    {
        CSGradient();
        CSConstant();
    }

    void CSGradient()
    {
        cut.m = (end.y - start.y) / (end.x - start.x);
        Debug.Log("cut gradient = " + cut.m);
    }

    void CSConstant()
    {
        cut.c = start.y / (cut.m * start.x);
        Debug.Log("cut constant = " + cut.c);
    }

    void CSIntersections()
    {
        int counter;
        for (int i = 0; i < fragmentmanager.shapes.Count; i++)
        {
            counter = 0;
            for (int j= 0; j < fragmentmanager.shapes[i].edges.Count; j++)
            {
                if (IsIntersection(i, j, counter)) counter++;
            }
            if (counter == 2)
            {
                MakeShapes(i);
            }
        }
    }

    bool IsIntersection(int shapenum, int edgenum, int counter)
    {
        Vector3 tempintersection;
        linedefinition edgeline = fragmentmanager.shapes[shapenum].edges[edgenum];
        tempintersection = linedefinition.SolveSimEquations(edgeline, cut);
        if (((tempintersection.x >= start.x) && (tempintersection.x <= end.x)) ||
            ((tempintersection.x <= start.x) && (tempintersection.x >= end.x)))
        {
            if (counter == 0)
            {
                inter1 = tempintersection;
                edge1 = edgenum;
            }
            else if (counter == 1)
            {
                inter2 = tempintersection;
                edge2 = edgenum;
            }
            else Debug.Log("oh no. more than 2 intersections found");
            return true;
        }
        else return false;
    }

    void MakeShapes(int i)
    {
        relinter1 = inter1 - fragmentmanager.shapes[i].gameObject.transform.position;
        relinter2 = inter2 - fragmentmanager.shapes[i].gameObject.transform.position;
        ModifyCurrentShape(i);
        InstantiateNewShape(i);
    }

    void ModifyCurrentShape(int i)
    {
        int iterator = 0;
        fragment temp = fragmentmanager.shapes[i];
        Mesh tempmesh = temp.gameObject.GetComponent<MeshFilter>().mesh;
        int arrsize;

        if (edge2 > edge1)
        {
            arrsize = temp.edges.Count - edge2;
        }
        else arrsize = edge1 - edge2;

        Vector3[] vertices = new Vector3[arrsize];
        int[] triangles = new int[arrsize - 2];

        for (int j = 0; j < arrsize; j++)
        {
            if (j == 0)
            {
                vertices[j] = relinter1;
            }
            else if (j == 1)
            {
                vertices[j] = relinter2;
            }
            else vertices[j] = tempmesh.vertices[edge2 + i - 1];
        }

        for (int j = 0; j < arrsize - 2; j += 3)
        {
            triangles[j] = 0;
            triangles[j + 1] = iterator + 1;
            triangles[j + 2] = iterator + 2;
            iterator++;
        }
        Vector2[] pathpoints = new Vector2[vertices.Length];
        for (int j = 0; j < vertices.Length; j++)
        {
            pathpoints[j].x = vertices[j].x;
            pathpoints[j].y = vertices[j].y;
        }
        temp.gameObject.GetComponent<PolygonCollider2D>().SetPath(0, pathpoints);
        //tempmesh.vertices = vertices;
        //tempmesh.triangles = triangles;
    }

    void InstantiateNewShape(int i)
    {
        int iterator = 0;
        int arrsize;
        fragment temp = fragmentmanager.shapes[i];
        Mesh tempmesh = temp.gameObject.GetComponent<MeshFilter>().mesh;

        if (edge2 > edge1)
        {
            arrsize = edge1 - edge2;
        }
        else arrsize = temp.edges.Count - edge2;


        Vector3[] vertices = new Vector3[arrsize];
        int[] triangles = new int[arrsize - 2];

        for (int j = 0; j < arrsize; j++)
        {
            if (j == 0)
            {
                vertices[j] = inter1;
            }
            else if (j == 1)
            {
                vertices[j] = inter2;
            }
            else vertices[j] = tempmesh.vertices[edge2 + j - 1] + temp.transform.position;
        }

        for (int j = 0; j < arrsize - 2; j += 3)
        {
            triangles[j] = 0;
            triangles[j + 1] = iterator + 1;
            triangles[j + 2] = iterator + 2;
            iterator++;
        }
        
        GameObject newshape = Instantiate(copiable, new Vector3(0, 0, 0), Quaternion.identity);
        Mesh newmesh = newshape.GetComponent<MeshFilter>().mesh;
        newmesh.Clear();
        newmesh.vertices = vertices;
        newmesh.triangles = triangles;
        newshape.AddComponent<PolygonCollider2D>();
        Vector2[] pathpoints = new Vector2[vertices.Length];
        for (int j = 0; j < vertices.Length; j++)
        {
            pathpoints[j].x = vertices[j].x;
            pathpoints[j].y = vertices[j].y;
        }
        newshape.GetComponent<PolygonCollider2D>().SetPath(0, pathpoints);
        newshape.AddComponent<Rigidbody2D>();
    }
}
