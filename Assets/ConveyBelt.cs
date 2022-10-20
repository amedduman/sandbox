using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyBelt : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] Transform package;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float rotationSpeed = 10;
    // [SerializeField] float time = 1;
    void Start()
    {
        points = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            var p = transform.GetChild(i);
            points[i] = p;
        }
        StartCoroutine(Move());
    }

    // IEnumerator Move()
    // {
    //     for (int i = 0; i < points.Length; i++)
    //     {
    //         Transform p = points[i];
    //         while(Vector3.Distance(p.position, package.position) > .1f)
    //         {
    //             Vector3 pos = package.position;
    //             Vector3 dir = (p.position - package.position).normalized;
    //             package.forward = dir;
    //             pos +=  dir * Time.deltaTime * moveSpeed;
    //             package.position = pos;
    //             yield return null;
    //         }
    //     }
    // }

    IEnumerator Move()
    {
        for (int i = 0; i < points.Length; i++)
        {
            Transform p = points[i];

            Vector3 pos = package.position;
            Vector3 targetPos = p.position;

            Quaternion rot = package.rotation;
            Vector3 dirToLook = p.position - package.position;
            Quaternion targetRot = Quaternion.LookRotation(dirToLook, Vector3.up);

            float tp = 0;
            float tr = 0;
            float distance = Vector3.Distance(p.position, package.position);

            while (tp < 1)
            {
                package.position = Vector3.Lerp(pos, targetPos, tp);
                package.rotation = Quaternion.Lerp(rot, targetRot, tr);


                tp += (Time.deltaTime * moveSpeed) / distance;
                tr += Time.deltaTime * rotationSpeed;

                yield return null;
            }
        }
    }
}