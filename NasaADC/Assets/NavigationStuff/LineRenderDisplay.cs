using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderDisplay : MonoBehaviour
{
    //REFACTORED 1/15/2021 SAM
    public Transform dest;

    public Transform checkpointParent;

    public SplineManager splineManager;
    public string AgentType;
    public Vector3[] checkpointsDist;
    public Vector3[] checkpointsFlat;

    public CreateObject ObjectCreator;

    public Terrain theTerrain;
    public float moveDown;
    public string optimization;

    public RenderTexture rt;

    // Start is called before the first frame update
    void Awake()
    {
        //Debug.Log(theTerrain.terrainData.bounds);
        optimization = AgentType;
        showPath(optimization);
    }

    public void TogglePathOptimization()
    {   
        splineManager.ResetPath();
        splineManager.RemoveAllChildren(splineManager.transform);
        if (optimization == "flat")
        {
            optimization = "dist";
        }
        else
        {
            optimization = "flat";
        }
        showPath(optimization);
        splineManager.CreateNewPath();
    }


    public void showPath(string optim)
    {
        int ctr = 1;
        switch(optim)
        {
            case "flat":
                // ProjSetter.setProjMat(false);
                Debug.Log("flat checkpoints");
                // lr.positionCount = 11;
                // lr.numCornerVertices = 20;
                // lr.SetPosition(0, new Vector3(-7770.566f, 6088.1f-moveDown, 11000.91f));
                ctr = 1;
                foreach (Vector3 loc in checkpointsFlat)
                {
                     float CorrectY = theTerrain.SampleHeight(loc);
                     Vector3 finalLoc = new Vector3(loc.x, CorrectY, loc.z);
                     Vector3 trailLoc = new Vector3(loc.x-120, CorrectY-moveDown, loc.z);
                     float x = finalLoc.x;
                     float y = finalLoc.y - -1731707;
                     float z = finalLoc.z;
                     float height;
                     // lr.SetPosition(ctr, trailLoc);
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

                    Debug.Log(height);
                    float lat = (Mathf.Asin(z / (height + 1737400f)));
                    float lon = (Mathf.Acos(y / ((height + 1737400f) * Mathf.Cos(lat))));
                    float aziumuth = Mathf.Atan2(Mathf.Sin(-lon), ((Mathf.Cos(lat) * 0) - (Mathf.Sin(lat) * 1 * Mathf.Cos(-lon))));
                    float rangeAb = Mathf.Pow((361000000f - x) * (361000000f - x) + y * y + (-42100000f - z) * (-42100000f - z), .5f);
                    float rz = (361000000f - x) * Mathf.Cos(lat) * Mathf.Cos(lon) + y * Mathf.Cos(lat) * Mathf.Sin(lon) + (-42100000f - z) * Mathf.Sin(lat);
                    float elev = Mathf.Asin(rz / rangeAb);
                    lon += -90;
                    lat += 54.794f;
                    ObjectCreator.createInstance(finalLoc, aziumuth, elev, checkpointParent);
                    ctr++;
                }
               // ImageSaver.setProj(rt);
                break;
            case "dist":
                // ProjSetter.setProjMat(true);
                Debug.Log("dist checkpoints");
                // lr.positionCount = 11;
                //lr.numCornerVertices = 5;
                // lr.SetPosition(0, new Vector3(-7770.566f, 6088.1f-moveDown, 11000.91f));
                ctr = 1;
                foreach (Vector3 loc in checkpointsDist)
                {
                    float CorrectY = theTerrain.SampleHeight(loc);
                    Vector3 finalLoc = new Vector3(loc.x, CorrectY, loc.z);
                    Vector3 trailLoc = new Vector3(loc.x-30, CorrectY-moveDown, loc.z);
                    float x = finalLoc.x;
                    float y = finalLoc.y - -1731707;
                    float z = finalLoc.z;
                    float height;
                    // lr.SetPosition(ctr, trailLoc);
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
                    ObjectCreator.createInstance(finalLoc, aziumuth, elev, checkpointParent);
                    ctr++;
                }
                break;

        }
    }
}