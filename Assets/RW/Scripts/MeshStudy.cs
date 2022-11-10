using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MeshStudy : MonoBehaviour
{
    Mesh _originalMesh;
    Mesh _clonedMesh;
    MeshFilter _meshFilter;
    int[] triangles;

    [HideInInspector]
    public Vector3[] Vertices;

    [HideInInspector]
    public bool IsCloned = false;

    // For Editor
    public float Radius = 0.2f;
    public float Pull = 0.3f;
    public float HandleSize = 0.03f;
    public List<int>[] ConnectedVertices;
    public List<Vector3[]> AllTriangleList;
    public bool MoveVertexPoint = true;

    public void InitMesh()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _originalMesh = _meshFilter.sharedMesh; //1
        _clonedMesh = new Mesh(); //2

        _clonedMesh.name = "clone";
        _clonedMesh.vertices = _originalMesh.vertices;
        _clonedMesh.triangles = _originalMesh.triangles;
        _clonedMesh.normals = _originalMesh.normals;
        _clonedMesh.uv = _originalMesh.uv;
        _meshFilter.mesh = _clonedMesh;  //3

        Vertices = _clonedMesh.vertices; //4
        triangles = _clonedMesh.triangles;
        IsCloned = true; //5
        Debug.Log("Init & Cloned");
    }

    public void Reset()
    {
        if (_clonedMesh != null && _originalMesh != null)
        {
            _clonedMesh.vertices = _originalMesh.vertices;
            _clonedMesh.triangles = _originalMesh.triangles;
            _clonedMesh.normals = _originalMesh.normals;
            _clonedMesh.uv = _originalMesh.uv;
            _meshFilter.mesh = _clonedMesh;

            Vertices = _clonedMesh.vertices;
            triangles = _clonedMesh.triangles;
        }
        else
        {
            Debug.Log("there is something wrong with resetting, probably you need to clone the mesh");
        }
    }

    public void GetConnectedVertices()
    {
        ConnectedVertices = new List<int>[Vertices.Length];
    }

    public void DoAction(int index, Vector3 localPos)
    {
        PullSimilarVertices(index, localPos);
    }

    private void PullSimilarVertices(int index, Vector3 newPos)
    {
        Vector3 targetVertexPos = Vertices[index]; //1
        List<int> relatedVertices = FindRelatedVertices(targetVertexPos, false); //2
        foreach (int i in relatedVertices) //3
        {
            Vertices[i] = newPos;
        }
        ApplyChanges();
    }

    void ApplyChanges()
    {
        _clonedMesh.vertices = Vertices; //2
        _clonedMesh.RecalculateNormals(); //3
    }

    // returns List of int that is related to the targetPt.
    private List<int> FindRelatedVertices(Vector3 targetPt, bool findConnected)
    {
        // list of int
        List<int> relatedVertices = new List<int>();

        int idx = 0;
        Vector3 pos;

        // loop through triangle array of indices
        for (int t = 0; t < triangles.Length; t++)
        {
            // current idx return from tris
            idx = triangles[t];
            // current pos of the vertex
            pos = Vertices[idx];
            // if current pos is same as targetPt
            if (pos == targetPt)
            {
                // add to list
                relatedVertices.Add(idx);
                // if find connected vertices
                if (findConnected)
                {
                    // min
                    // - prevent running out of count
                    if (t == 0)
                    {
                        relatedVertices.Add(triangles[t + 1]);
                    }
                    // max 
                    // - prevent running out of count
                    if (t == triangles.Length - 1)
                    {
                        relatedVertices.Add(triangles[t - 1]);
                    }
                    // between 1 ~ max-1 
                    // - add idx from triangles before t and after t 
                    if (t > 0 && t < triangles.Length - 1)
                    {
                        relatedVertices.Add(triangles[t - 1]);
                        relatedVertices.Add(triangles[t + 1]);
                    }
                }
            }
        }
        // return compiled list of int
        return relatedVertices;
    }

    public void BuildTriangleList()
    {
    }

    public void ShowTriangle(int idx)
    {
    }

    // // Pulling only one vertex pt, results in broken mesh.
    // private void PullOneVertex(int index, Vector3 newPos)
    // {
    //     Vertices[index] = newPos; //1
    //     ApplyChanges();
    // }

    // To test Reset function
    public void EditMesh()
    {
        Vertices[2] = new Vector3(2, 3, 4);
        Vertices[3] = new Vector3(1, 2, 4);
        _clonedMesh.vertices = Vertices;
        _clonedMesh.RecalculateNormals();
    }
}
