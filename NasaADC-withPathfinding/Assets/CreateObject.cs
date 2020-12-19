using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    // Start is called before the first frame update
    public MeshRenderer mesh1;
    public MeshRenderer mesh2;
    public GameObject self;
    public Terrain terrain;
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
        float yDisplace = ((this.GetComponent<Renderer>().bounds.size.y) / 2); // get height of object so it doesn't spawn half-buried
        Vector3 placeAt = new Vector3(location.x, location.y-2, location.z); // determine where to place it
        var obj = Instantiate(this, placeAt, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up)); // create the object
        RaycastHit hit;
        var ray = new Ray(obj.transform.position, Vector3.down); // check for slopes
        if (terrain.GetComponent<Collider>().Raycast(ray, out hit, 1000))
        {
            obj.transform.rotation = Quaternion.FromToRotation(obj.transform.up, hit.normal) * obj.transform.rotation; // adjust for slopes
        }
        /*Instantiate(this, location, Quaternion.identity);
         mesh1.enabled = true;
         mesh2.enabled = true;*/
       
    }
}
