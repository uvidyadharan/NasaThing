using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    // Start is called before the first frame update
    public MeshRenderer mesh1;
    public MeshRenderer mesh2;
    public GameObject self;
    void Start()
    {

        //mesh1.enabled = false;
        //mesh2.enabled = false;
        self.transform.Rotate(-90.0f, 0.0f, 0.0f, Space.Self);
        //  mod1.GetComponent<MeshRenderer>().enabled = false;
        // mod2.GetComponent<MeshRenderer>().enabled = false;
    }

    public void createInstance(Vector3 location, float azi, float elev)
    {
        
        Instantiate(this, location, Quaternion.identity);
        mesh1.enabled = true;
        mesh2.enabled = true;
    }
}
