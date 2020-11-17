using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMove : MonoBehaviour
{
    [SerializeField]
    Transform _destination;

    NavMeshAgent _navMeshAgent;
    public int NumCheckpoints;
    public string AgentType;
    public int radiusOfSearch;
    public float distTravelled = 0;
    public float totalDist;
    public float distBetCheckpoints = 0;
    public Vector3 lastPosition, distStart, origPosition;
    public bool isReturn = false;
    public bool firstTime = true;
    // Start is called before the first frame update
    void Start()
    {
        resetDist();
        origPosition = transform.position;
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        if(_navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent is not attached to " + gameObject.name);
        }
        else
        {
            if (_destination != null)
                SetDestination(_destination.transform.position);
        }
    }
    void Update()
    {
        distTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
        if(isReturn)
        {
            //Debug.Log(distTravelled);
            if (distTravelled >= distBetCheckpoints)
            {
                //Vector3 closest = findLowestSlopePoint(transform.position, 3);
                Debug.Log("Checkpoint at: " + transform.position);
                resetDist();
            }
        }
        if(firstTime && _navMeshAgent.remainingDistance == 0 && distTravelled > 2)
        {
            firstTime = false;
            Debug.Log("reached dest");
            totalDist = distTravelled;
            distBetCheckpoints = totalDist / NumCheckpoints;
            Debug.Log("DBC: " + distBetCheckpoints);
            resetDist();
            Debug.Log(origPosition);
            isReturn = true;
            SetDestination(origPosition);
            
        }
        //Debug.Log("Distance Left: " + _navMeshAgent.remainingDistance + " Distance went: " + distTravelled);
        //Debug.Log(_navMeshAgent.remainingDistance);
        
        //Debug.Log("Distance traveled: "+distTravelled);
    }
    private void resetDist()
    {
        distTravelled = 0;
        lastPosition = transform.position;
        distStart = transform.position;
    }
    private void SetDestination(Vector3 theDestination)
    {
            
            if (AgentType == "dist")
            {
                _navMeshAgent.agentTypeID = -1372625422;
                Debug.Log("cube Going shortest distance");
            }
            else if (AgentType == "flat")
            {
                _navMeshAgent.agentTypeID = 0;
                Debug.Log("cube Going flat land");
            }
            else
                Debug.Log("Invalid agent type");
            //Vector3 targetVector = theDestination.transform.position;
            _navMeshAgent.SetDestination(theDestination);

    }
}
