using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SplineTools : MonoBehaviour
{
    // How many points you want on the curve

    // Parametric constant: 0.0 for the uniform spline, 0.5 for the centripetal spline, 1.0 for the chordal spline

    public static List<Vector2> DrawCatmullSegment(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3,int numberOfPoints, float alpha=0.5f)
    {
        List<Vector2> newPoints = new List<Vector2>();

		float t0 = 0.0f;
		float t1 = GetT(t0, p0, p1, alpha);
		float t2 = GetT(t1, p1, p2, alpha);
		float t3 = GetT(t2, p2, p3, alpha);

		for (float t = t1; t < t2; t += ((t2 - t1) / (float) numberOfPoints))
		{
			Vector2 A1 = (t1 - t) / (t1 - t0) * p0 + (t - t0) / (t1 - t0) * p1;
			Vector2 A2 = (t2 - t) / (t2 - t1) * p1 + (t - t1) / (t2 - t1) * p2;
			Vector2 A3 = (t3 - t) / (t3 - t2) * p2 + (t - t2) / (t3 - t2) * p3;

			Vector2 B1 = (t2 - t) / (t2 - t0) * A1 + (t - t0) / (t2 - t0) * A2;
			Vector2 B2 = (t3 - t) / (t3 - t1) * A2 + (t - t1) / (t3 - t1) * A3;

			Vector2 C = (t2 - t) / (t2 - t1) * B1 + (t - t1) / (t2 - t1) * B2;

			newPoints.Add(C);
		}

		return newPoints;
    }

    public static List<Vector2> DrawLerpLine(Vector2 p0, Vector2 p1, int numberOfPoints)
    {
	    float increment = 1f / numberOfPoints;
	    float counter = 0;
	    List <Vector2> points = new List<Vector2>();
	    for (int i = 0; i < numberOfPoints; i++)
	    {
		    points.Add(Vector2.Lerp(p0, p1, counter));
		    counter += increment;
	    }

	    return points;

    }

    public static float GetT(float t, Vector2 p0, Vector2 p1, float alpha)
    {
        float a = Mathf.Pow((p1.x - p0.x), 2.0f) + Mathf.Pow((p1.y - p0.y), 2.0f);
        float b = Mathf.Pow(a, alpha * 0.5f);

        return (b + t);
    }
    
}