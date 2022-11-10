using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(HeartMesh))]
public class HeartMeshInspector : Editor
{
    private HeartMesh mesh;
    private Transform handleTransform;
    private Quaternion handleRotation;

    void OnSceneGUI()
    {
        mesh = target as HeartMesh;
        handleTransform = mesh.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

        // ShowHandles on Mesh
        if (mesh.isEditMode)
        {
            if (mesh.originalVertices == null || mesh.normals.Length == 0)
            {
                mesh.Init();
            }
            for (int i = 0; i < mesh.originalVertices.Length; i++)
            {
                ShowHandle(i);
            }
        }

        // Show/ Hide Transform Tool
        if (mesh.showTransformHandle)
        {
            Tools.current = Tool.Move;
        }
        else
        {
            Tools.current = Tool.None;
        }
    }

    void ShowHandle(int index)
    {
        Vector3 point = handleTransform.TransformPoint(mesh.originalVertices[index]);

        // unselected vertex
        if (!mesh.selectedIndices.Contains(index))
        {
            Handles.color = Color.blue;
            if (Handles.Button(point, handleRotation, mesh.pickSize, mesh.pickSize,
                Handles.DotHandleCap)) //1
            {
                mesh.selectedIndices.Add(index); //2
            }

        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        mesh = target as HeartMesh;

        if (mesh.isEditMode || mesh.isMeshReady)
        {
            if (GUILayout.Button("Show Normals"))
            {
                Vector3[] verts = mesh.modifiedVertices.Length == 0 ? mesh.originalVertices : mesh.modifiedVertices;
                Vector3[] normals = mesh.normals; Debug.Log(normals.Length);
                for (int i = 0; i < verts.Length; i++)
                {
                    Debug.DrawLine(handleTransform.TransformPoint(verts[i]), handleTransform.TransformPoint(normals[i]), Color.green, 4.0f, true);
                }
            }
        }

        if (GUILayout.Button("Clear Selected Vertices"))
        {
            mesh.ClearAllData();
        }

    }


}
