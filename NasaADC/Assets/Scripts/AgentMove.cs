using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMove : MonoBehaviour
{
    [SerializeField]
    Transform _destination;

    NavMeshAgent _navMeshAgent;
    public NavigationBakerNew baker;
    public TrailRenderer tr;
    public int NumCheckpoints;
    public string AgentType;
    public int radiusOfSearch;
    public float distTravelled = 0;
    public float totalDist;
    public float distBetCheckpoints = 0;
    public Vector3 lastPosition, distStart, origPosition;
    public bool isReturn = false;
    public bool firstTime = true;
    public bool supposedToStay = false;
    private NavMeshPath path;
    private float elapsed = 0.0f;
    public List<Vector3> checkpoints = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        tr.time = 100;
        baker.bakeMesh(AgentType);
        //setupTrail();
        //this.GetComponent<MeshRenderer>().enabled = false;
        resetDist();
        origPosition = transform.position;
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (_navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent is not attached to " + gameObject.name);
        }
        else
        {
            if (_destination != null)
            {
                tr.emitting = true;
                SetDestination(_destination.transform.position);
            }
        }
    }
    void Update()
    {
        //tr.transform.position = new Vector3(Mathf.Sin(Time.time * 1.51f) * 7.0f, Mathf.Cos(Time.time * 1.27f) * 4.0f, 0.0f);
        //timer -= Time.deltaTime();
        distTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
        //Debug.Log(transform.position);
        //Debug.Log(_navMeshAgent.remainingDistance);
        if(isReturn)
        {

            //Debug.Log("Am in isret");
            //Debug.Log(distTravelled);
            if (distTravelled >= distBetCheckpoints && checkpoints.Count <= NumCheckpoints)
            {

                //Vector3 closest = findLowestSlopePoint(transform.position, 3);

                Debug.Log("Checkpoint at: " + transform.position);
                checkpoints.Add(transform.position);
                resetDist();
            }
        }
        if(firstTime && _navMeshAgent.remainingDistance <= 0.2 && distTravelled > 2)
            tr.emitting = false;
        if (firstTime && _navMeshAgent.remainingDistance == 0 && distTravelled > 2)

        {
            //_navMeshAgent.path = null;
            //_navMeshAgent.isStopped = true;
            //_navMeshAgent.ResetPath();
            //_navMeshAgent.updatePosition = false;
            //this.GetComponent<MeshRenderer>().enabled = false;
            _navMeshAgent.speed = 15;
            _navMeshAgent.acceleration = 100;
            _navMeshAgent.angularSpeed = 500;
            firstTime = false;
            Debug.Log("reached destNew");
            totalDist = distTravelled;
            Debug.Log("reached calculating totalDist");
            Debug.Log("TotalDist: " + totalDist + " ; NumCheckpoints: " + NumCheckpoints);
            distBetCheckpoints = totalDist / NumCheckpoints;
            Debug.Log("DBC: " + distBetCheckpoints);
            resetDist();
            Debug.Log("Orig: "+origPosition);
            isReturn = true;
            this.transform.position = origPosition;
            this.transform.localPosition = origPosition;
            _navMeshAgent.Warp(origPosition);
            _navMeshAgent.updatePosition = true;
            _navMeshAgent.isStopped = false;
            resetDist();
            _navMeshAgent.ResetPath();
            //setChecks();
            // _navMeshAgent.ResetPath();




            //resetDist();
            Debug.Log("Setting Destination to go again");
            SetDestination(_destination.transform.position);
        }
        /*if(supposedToStay)
        {
            gameObject.transform.localPosition = origPosition;
        }*/
        //Debug.Log("Distance Left: " + _navMeshAgent.remainingDistance + " Distance went: " + distTravelled);
        //Debug.Log("Remaining: "+_navMeshAgent.remainingDistance+" ; Pos: "+transform.position);
        //Debug.Log(transform.position);
        //Debug.Log("Distance traveled: "+distTravelled);
    }
    /*private void setupTrail()
    {
        tr = GetComponent<TrailRenderer>();
        tr.material = new Material(Shader.Find("Sprites/Default"));

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.green, 0.0f), new GradientColorKey(Color.red, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        tr.colorGradient = gradient;
    }*/
    private void resetDist()
    {
        distTravelled = 0;
        lastPosition = transform.position;
        distStart = transform.position;
    }
    private int SetDestination(Vector3 theDestination)
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
        Debug.Log("Current position is: " + transform.position);
        Debug.Log("Destination position is: " + theDestination);
            //Vector3 targetVector = theDestination.transform.position;

            _navMeshAgent.SetDestination(theDestination);

        /*switch (_navMeshAgent.pathStatus)
        {
            case NavMeshPathStatus.PathComplete:
                Debug.Log("Complete");
                break;

            case NavMeshPathStatus.PathPartial:
                Debug.Log("Partial");
                break;

            case NavMeshPathStatus.PathInvalid:
                Debug.Log("Invalid");
                break;
        }*/
        return 0;
    }
    private void setChecks()
    {
        Debug.Log("Got into setchecks");
        this.transform.position = origPosition;
        this.transform.localPosition = origPosition;
        _navMeshAgent.updatePosition = false;
        _navMeshAgent.isStopped = true;
        /*for (int x = 0; x < 2000; x++)
        {
            Debug.Log(x);
        }*/
        SetDestination(_destination.transform.position);

    }
}
