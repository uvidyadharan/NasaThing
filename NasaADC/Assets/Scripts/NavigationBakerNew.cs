using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBakerNew : MonoBehaviour
{
    public NavMeshSurface surface;
    public string AgentType;
    // Start is called before the first frame update
    void Start()
    {
        //bakeMesh(AgentType);
    }
    public void bakeMesh(string agent)
    {
        //short distance ID is -1372625422
        //flatLand ID is 0
        //no clue why
        if (agent == "ret")
        {
            surface.agentTypeID = -334000983;
            Debug.Log("Returning");
        }
        else if (agent == "dist")
        {
            surface.agentTypeID = -1372625422;
            Debug.Log("Going shortest distance");
        }
        else if (agent == "flat")
        {
            surface.agentTypeID = 0;
            Debug.Log("Going flat land");
        }
        else
            Debug.Log("Invalid agent type");
        surface.BuildNavMesh();
    }

}
