using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class SplineManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform pointProjector;
    public Transform startingPoint;
    public Transform endingPoint;
    public int pathResolution;

    public Transform pointContainer;
    public Quaternion projectorAngle;

    public float projectorHeight;

    private List<Vector2> _controlPoints = new List<Vector2>();
    private List<Vector2> _splinePoints = new List<Vector2>();

    private void Start()
    {
        // Add start point
        _controlPoints.Add(MathC.FlipYZ(startingPoint.position));

        // Add midpoint checkpoints
        foreach (Transform child in transform)
        {
            _controlPoints.Add(MathC.FlipYZ(child.position));
        }

        // Add ending point
        _controlPoints.Add(MathC.FlipYZ(endingPoint.position));

        BuildSpline();

        foreach (Vector2 point in _splinePoints)
        {
            // Debug.Log(point);
            Instantiate(pointProjector, new Vector3(point.x, projectorHeight, point.y), projectorAngle,
                pointContainer);
        }
        RotatePoints();
    }

    public void BuildSpline()
    {
        float distance = Find2DDistance(startingPoint.position, transform.GetChild(0).position);

        _splinePoints.AddRange(SplineTools.DrawLerpLine(MathC.FlipYZ(startingPoint.position),
            MathC.FlipYZ(transform.GetChild(0).position), Mathf.RoundToInt(distance * pathResolution * 0.01f)));
        for (int i = 0; i < _controlPoints.Count - 3; i++)
        {
            distance = Find2DDistance(_controlPoints[i + 1], _controlPoints[i + 2]);
            _splinePoints.AddRange(SplineTools.DrawCatmullSegment(_controlPoints[i], _controlPoints[i + 1],
                _controlPoints[i + 2], _controlPoints[i + 3], Mathf.RoundToInt(distance * pathResolution * 0.01f)));
        }

        distance = Find2DDistance(transform.GetChild(transform.childCount - 1).position, endingPoint.position);
        _splinePoints.AddRange(SplineTools.DrawLerpLine(
            MathC.FlipYZ(transform.GetChild(transform.childCount - 1).position), MathC.FlipYZ(endingPoint.position),
            Mathf.RoundToInt(distance * pathResolution * 0.01f)));
    }

    public float Find2DDistance(Vector3 p0, Vector3 p1)
    {
        return Vector2.Distance(MathC.FlipYZ(p0), MathC.FlipYZ(p1));
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void RotatePoints()
    {
        int numPoints = _splinePoints.Count;
        Vector2 vDiff = MathC.FlipYZ(endingPoint.position) - MathC.FlipYZ(pointContainer.GetChild(numPoints-1).position);
        Debug.Log("rotate");
        float atan2 = Mathf.Atan2(vDiff.x, vDiff.y) * Mathf.Rad2Deg;
        pointContainer.GetChild(numPoints-1).rotation = Quaternion.Euler(90, atan2, 0);
        
        for (int i = numPoints - 2; i >= 0; i--)
        {
            vDiff = MathC.FlipYZ(pointContainer.GetChild(i+1).position) - MathC.FlipYZ(pointContainer.GetChild(i).position);
            atan2 = Mathf.Atan2(vDiff.x, vDiff.y) * Mathf.Rad2Deg;
            pointContainer.GetChild(i).rotation = Quaternion.Euler(90, atan2, 0);
        }
    }

    // private void OnDrawGizmos()
    // {
    //     foreach (Vector2 temp in _splinePoints)
    //     {
    //         Gizmos.DrawSphere(MathC.FlipYZ(temp), 0.3f);
    //     }
    //     
    // }
}