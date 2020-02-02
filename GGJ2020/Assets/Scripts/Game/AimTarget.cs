﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTarget : MonoBehaviour
{
    public Transform slime;
    public float lineHeight = 2;
    public float midPointPosition = 0;
    private LineRenderer lr;

    public Vector3[] Points { get; private set; }

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        DrawLine();
    }

    private void DrawLine()
    {
        Vector3 p0 = transform.position;
        Vector3 p2 = SlimeManager.Instance.CurrentSlime.transform.position + (Vector3.up / 3) * SlimeManager.Instance.CurrentSlime.SlimeSize / 2;
        Vector3 p1 = (p0 / 2) + (p2 / 2);

        p1.y += lineHeight;
        p1.x += midPointPosition;
        p1.z += midPointPosition;
        List<Vector3> _points = new List<Vector3>();
        for (float i = 0; i < 1; i += 0.02f)
        {
            _points.Add(CalcQuadraticBezierCurve(i, p0, p1, p2));
        }
        Points = _points.ToArray();
        lr.SetPositions(Points);
    }

    private Vector3 CalcQuadraticBezierCurve(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        //Vector3 point = (1 - t) * 2 * p0 + 2 * (1 - t) * t * p1 + t * 2 * p2;
        //Vector3 point = Mathf.Pow(1 - t, 2) * p0 + 2 * t * (1 - t) * p1 + Mathf.Pow(t,2) * p2;
        //return point;


        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;

    }
}
