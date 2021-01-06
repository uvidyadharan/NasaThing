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
    public Terrain theTerrain;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(theTerrain.terrainData.bounds);
        showPath(AgentType);


    }
    public void showPath(string optim)
    {
        int ctr = 1;
        switch(optim)
        {
            case "flat":

                Debug.Log("flat checkpoints");
                lr.positionCount = 11;
                //lr.numCornerVertices = 20;
                lr.SetPosition(0, new Vector3(-7770.566f, 6088.1f, 11000.91f));
                ctr = 1;
                foreach (Vector3 loc in checkpointsFlat)
                {
                    float CorrectY = theTerrain.SampleHeight(loc);
                    Vector3 finalLoc = new Vector3(loc.x, CorrectY, loc.z);
                    Vector3 trailLoc = new Vector3(loc.x-30, CorrectY+5, loc.z);
                    float x = finalLoc.x;
                    float y = finalLoc.y - -1731707;
                    float z = finalLoc.z;
                    float height;
                    lr.SetPosition(ctr, trailLoc);
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
                    ObjectCreator.createInstance(finalLoc, aziumuth, elev);
                    ctr++;
                }          
                break;
            case "dist":
                Debug.Log("dist checkpoints");
                lr.positionCount = 11;
                //lr.numCornerVertices = 5;
                lr.SetPosition(0, new Vector3(-7770.566f, 6088.1f, 11000.91f));
                ctr = 1;
                foreach (Vector3 loc in checkpointsDist)
                {
                    float CorrectY = theTerrain.SampleHeight(loc);
                    Vector3 finalLoc = new Vector3(loc.x, CorrectY, loc.z);
                    Vector3 trailLoc = new Vector3(loc.x-30, CorrectY + 10, loc.z);
                    float x = finalLoc.x;
                    float y = finalLoc.y - -1731707;
                    float z = finalLoc.z;
                    float height;
                    lr.SetPosition(ctr, trailLoc);
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
                    ObjectCreator.createInstance(finalLoc, aziumuth, elev);
                    ctr++;
                }
                break;

        }
    }
}