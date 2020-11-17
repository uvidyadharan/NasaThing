using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMove : MonoBehaviour
{
    [SerializeField]
    Transform _destination;

    NavMeshAgent _navMeshAgent;
    public string AgentType;
    // Start is called before the first frame update
    void Start()
    { 
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        if(_navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent is not attached to " + gameObject.name);
        }
        else
        {
            SetDestination();
        }
    }
    private void SetDestination()
    {
        if(_destination != null)
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
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);
        }
    }

}
