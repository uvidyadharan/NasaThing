using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    // Start is called before the first frame update
    public Text speedometer;
    public string speedometerText;
    public List<Transform> speedometerSegments;

    private GameObject roverObject;
    private RoverControl roverAttributes;
    private int speedometerSegmentsShown;
    static Vector3 shown = new Vector3(1f, 1f, 1f);
    static Vector3 hidden = new Vector3(0f, 0f, 0f);

    void Start()
    {
        roverObject = GameObject.Find("MoonRover");
        roverAttributes = roverObject.GetComponent<RoverControl>();
    }


    // Update is called once per frame
    void Update()
    {
        float x = roverAttributes.transform.position.x;
        float y = roverAttributes.transform.position.y;
        float z = roverAttributes.transform.position.z;
        float height;

        if(x < 0f && y <0f && z < 0f)
        {
            height = Mathf.Pow (x*x + y*y + z*z, .5f) ;
        }else{
            height = -1f*Mathf.Pow (x*x + y*y + z*z, .5f) ;
        }
        
        //Debug.Log(height);

        float lat = Mathf.Asin(z/(height+1737400f));
        float lon = Mathf.Acos(y/((height+1737400f)*Mathf.Cos(lat)));


        //Elevation

        float rangeAb = Mathf.Pow((361000000f - x)*(361000000f - x) + y*y + (-42100000f - z)*(-42100000f - z), .5f);
        float rz = (361000000f - x)*Mathf.Cos(lat)*Mathf.Cos(lon) + y*Mathf.Cos(lat)*Mathf.Sin(lon)+(-42100000f - z)*Mathf.Sin(lat);

        float elev = Mathf.Asin(rz/rangeAb);
        

        Debug.Log("Elev: " + elev + "Lat: " + lat + "Lon: " + lon + "Height: " + height);

        //Azimuth 
        






        speedometer.text = Mathf.Round(roverAttributes.currentSpeed / 3.6f).ToString() + speedometerText;
        speedometerSegmentsShown = Mathf.RoundToInt((roverAttributes.currentSpeed / roverAttributes.maxSpeed) * 10f);
        for (int segment = 0; segment < 10; segment++) {
            if (segment < speedometerSegmentsShown) {
                speedometerSegments[segment].localScale = shown;
            }
            else {
                speedometerSegments[segment].localScale = hidden;
            }
        }

        

    }

}