using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class SplineManager : MonoBehaviour
{
    // Start is called before the first frame update

    // Path objects
    public Transform pointProjector;
    public Transform startingPoint;
    public Transform endingPoint;
    public Transform pointContainer;

    private Vector3 _staticStartPoint;
    // Path parameters
    public int pathResolution;
    public Quaternion projectorAngle;
    public float projectorHeight;

    private int numPoints;
    
    // Move point stuff
    public float moveDuration;
    
    private List<Vector2> _controlPoints = new List<Vector2>();
    private List<Vector2> _splinePoints = new List<Vector2>();
    private List<bool> _coroutinePermission = new List<bool>();

    private void Start()
    {

        _staticStartPoint = startingPoint.position;
        
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
            _coroutinePermission.Add(true);
        }
        numPoints = _splinePoints.Count;
    }
    
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _splinePoints.Count; i++)
        {
            if (_coroutinePermission[i])
            {
                StartCoroutine(MoveAlongPath(pointContainer.GetChild(i), i));
            }
            
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

    private void RotatePoints()
    {
        print(_splinePoints.Count);
        print(pointContainer.childCount);
        Vector2 vDiff = (Vector2) MathC.FlipYZ(endingPoint.position) - _splinePoints[numPoints-1];
        float atan2 = Mathf.Atan2(vDiff.x, vDiff.y) * Mathf.Rad2Deg;
        pointContainer.GetChild(numPoints-1).rotation = Quaternion.Euler(90, atan2, 0);
        
        for (int i = numPoints - 2; i >= 0; i--)
        {
            vDiff = _splinePoints[i+1] - _splinePoints[i];
            atan2 = Mathf.Atan2(vDiff.x, vDiff.y) * Mathf.Rad2Deg;
            pointContainer.GetChild(i).rotation = Quaternion.Euler(90, atan2, 0);
        }
    }

    private IEnumerator MoveAlongPath(Transform arrow, int startIndex)
    {
        _coroutinePermission[startIndex] = false;
        Vector2 pos;
        float _timeElapsed;

        
        for (int i = startIndex; i < _splinePoints.Count-1; i++)
        {
            // Lerp between 2 points
            _timeElapsed = 0;
            while (_timeElapsed < moveDuration)
            {
                pos = Vector2.Lerp(_splinePoints[i], _splinePoints[i + 1], _timeElapsed / moveDuration);
                arrow.position = new Vector3(pos.x, projectorHeight, pos.y);
                _timeElapsed += Time.deltaTime;
                yield return null;
            }
            pos = _splinePoints[i + 1];
            arrow.position = new Vector3(pos.x, projectorHeight, pos.y);
        }

        pos = _splinePoints[0];
        arrow.position = new Vector3(pos.x, projectorHeight, pos.y);
        
        for (int i = 0; i < startIndex-1; i++)
        {
            // Lerp between 2 points
            _timeElapsed = 0;
            while (_timeElapsed < moveDuration)
            {
                pos = Vector2.Lerp(_splinePoints[i], _splinePoints[i + 1], _timeElapsed / moveDuration);
                arrow.position = new Vector3(pos.x, projectorHeight, pos.y);
                _timeElapsed += Time.deltaTime;
                yield return null;
            }
            pos = _splinePoints[i + 1];
        }
        _coroutinePermission[startIndex] = true;
    }
}