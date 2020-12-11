using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderDisplay : MonoBehaviour
{
    public Transform dest;
    public string AgentType;
    public Vector3[] checkpointsDist;
    public Vector3[] checkpointsFlat;
   public CreateObject ObjectCreator;
    public LineRenderer lr;


    // Start is called before the first frame update
    void Start()
    {
        showPath(AgentType);


    }
    public void showPath(string optim)
    {
        switch(optim)
        {
            case "flat":

                Debug.Log("flat checkpoints");

                foreach (Vector3 loc in checkpointsFlat)
                {
                    float x = loc.x;
                    float y = loc.y - -1731707;
                    float z = loc.z;
                    float height;
                    if (x < 0f && y < 0f && z < 0f)
                    {
                        height = Mathf.Pow(x * x + y * y + z * z, .5f) + 1737400;

                    }
                    else
                    {
                        height = -1f * Mathf.Pow(x * x + y * y + z * z, .5f) + 1737400;
                    }
                    if (x < 0f)
                    {
                        height = height * -1f;
                    }

                    //Debug.Log(height);
                    float lat = (Mathf.Asin(z / (height + 1737400f)));
                    float lon = (Mathf.Acos(y / ((height + 1737400f) * Mathf.Cos(lat))));
                    float aziumuth = Mathf.Atan2(Mathf.Sin(-lon), ((Mathf.Cos(lat) * 0) - (Mathf.Sin(lat) * 1 * Mathf.Cos(-lon))));
                    float rangeAb = Mathf.Pow((361000000f - x) * (361000000f - x) + y * y + (-42100000f - z) * (-42100000f - z), .5f);
                    float rz = (361000000f - x) * Mathf.Cos(lat) * Mathf.Cos(lon) + y * Mathf.Cos(lat) * Mathf.Sin(lon) + (-42100000f - z) * Mathf.Sin(lat);
                    float elev = Mathf.Asin(rz / rangeAb);
                    lon += -90;
                    lat += 54.794f;
                    ObjectCreator.createInstance(loc, aziumuth, elev);
                }
                
                lr.positionCount = 10;
                lr.SetPositions(checkpointsFlat);
                
                break;
            case "dist":

                Debug.Log("dist checkpoints");
                foreach (Vector3 loc in checkpointsDist)
                {
                    float x = loc.x;
                    float y = loc.y - -1731707;
                    float z = loc.z;
                    float height;
                    if (x < 0f && y < 0f && z < 0f)
                    {
                        height = Mathf.Pow(x * x + y * y + z * z, .5f) + 1737400;

                    }
                    else
                    {
                        height = -1f * Mathf.Pow(x * x + y * y + z * z, .5f) + 1737400;
                    }
                    if (x < 0f)
                    {
                        height = height * -1f;
                    }

                    //Debug.Log(height);
                    float lat = (Mathf.Asin(z / (height + 1737400f)));
                    float lon = (Mathf.Acos(y / ((height + 1737400f) * Mathf.Cos(lat))));
                    float aziumuth = Mathf.Atan2(Mathf.Sin(-lon), ((Mathf.Cos(lat) * 0) - (Mathf.Sin(lat) * 1 * Mathf.Cos(-lon))));
                    float rangeAb = Mathf.Pow((361000000f - x) * (361000000f - x) + y * y + (-42100000f - z) * (-42100000f - z), .5f);
                    float rz = (361000000f - x) * Mathf.Cos(lat) * Mathf.Cos(lon) + y * Mathf.Cos(lat) * Mathf.Sin(lon) + (-42100000f - z) * Mathf.Sin(lat);
                    float elev = Mathf.Asin(rz / rangeAb);
                    lon += -90;
                    lat += 54.794f;
                    ObjectCreator.createInstance(loc, aziumuth, elev);
                }
                lr.positionCount = 10;
                lr.SetPositions(checkpointsDist);
                break;

        }
    }
}