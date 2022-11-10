using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshStudy))]
public class MeshInspector : Editor
{
    private MeshStudy mesh;
    private Transform handleTransform;
    private Quaternion handleRotation;
    string triangleIdx;

    void OnSceneGUI()
    {
        mesh = target as MeshStudy;
        if(mesh.IsCloned) EditMesh();
    }

    void EditMesh()
    {
        handleTransform = mesh.transform; //1
        handleRotation = Tools.pivotRotation == PivotRotation.Local ?
            handleTransform.rotation : Quaternion.identity; //2
        for (int i = 0; i < mesh.Vertices.Length; i++) //3
        {
            ShowPoint(i);
        }

    }

    private void ShowPoint(int index)
    {
        if (mesh.MoveVertexPoint)
        {
            //draw dot
            Vector3 point = handleTransform.TransformPoint(mesh.Vertices[index]); //1
            Handles.color = Color.blue;
            point = Handles.FreeMoveHandle(point, handleRotation, mesh.HandleSize,
                Vector3.zero, Handles.DotHandleCap); //2

            //drag
            if (GUI.changed) //3
            {
                mesh.DoAction(index, handleTransform.InverseTransformPoint(point)); //4
            }

        }
        else
        {
            //click
        }
    }


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        mesh = target as MeshStudy;

        if (GUILayout.Button("Clone"))
        {
            mesh.InitMesh();
        }

        if (GUILayout.Button("Reset"))
        {
            mesh.Reset();
        }



        // For testing Reset function
        if (mesh.IsCloned)
        {
            if (GUILayout.Button("Test Edit"))
            {
                mesh.EditMesh();
            }
        }
    }


}
