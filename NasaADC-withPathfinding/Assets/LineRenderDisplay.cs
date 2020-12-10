using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderDisplay : MonoBehaviour
{
    public Transform dest;
    public Vector3[] checkpointsDist;
    public Vector3[] checkpointsFlat;
    public CreateObject ObjectCreator;
    public LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        showPath("flat");
    }
    public void showPath(string optim)
    {
        switch(optim)
        {
            case "flat":
                Debug.Log("flat checkpoints");
                foreach(Vector3 loc in checkpointsFlat)
                {
                    ObjectCreator.createInstance(loc, 2.0f, 4.0f);
                    lr.SetPositions(checkpointsFlat);
                }

                break;
            case "dist":
                Debug.Log("dist checkpoints");
                break;

        }
    }
}
