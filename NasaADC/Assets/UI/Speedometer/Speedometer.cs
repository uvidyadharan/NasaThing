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

    public Text statusDisplay;

    private GameObject roverObject;
    private RoverControl roverAttributes;

    private GameObject moonCenter;
     

    private int speedometerSegmentsShown;
    static Vector3 shown = new Vector3(1f, 1f, 1f);
    static Vector3 hidden = new Vector3(0f, 0f, 0f);
    private float roundingConstant = 1000f;

    void Start()
    {
        roverObject = GameObject.Find("RoverNew");
        roverAttributes = roverObject.GetComponent<RoverControl>();
    }


    // Update is called once per frame
    void Update()
    {
        float x = roverAttributes.transform.position.x;
        float y = roverAttributes.transform.position.y - -1731707;
        float z = roverAttributes.transform.position.z;
        float height;

        if(x < 0f && y <0f && z < 0f )
        {
            height = Mathf.Pow (x*x + y*y + z*z, .5f) + 1737400;
;
        }else{
            height = -1f*Mathf.Pow (x*x + y*y + z*z, .5f) + 1737400;
;
        }
        if(x < 0f)
        {
            height = height * -1f;
        }
        
        //Debug.Log(height);

        float lat = (Mathf.Asin(z/(height+1737400f)));
        float lon = (Mathf.Acos(y/((height+1737400f)*Mathf.Cos(lat))));

        float aziumuth = Mathf.Atan2(Mathf.Sin(-lon), ((Mathf.Cos(lat)*0)-(Mathf.Sin(lat)*1*Mathf.Cos(-lon))));
        //Elevation

        float rangeAb = Mathf.Pow((361000000f - x)*(361000000f - x) + y*y + (-42100000f - z)*(-42100000f - z), .5f);
        float rz = (361000000f - x)*Mathf.Cos(lat)*Mathf.Cos(lon) + y*Mathf.Cos(lat)*Mathf.Sin(lon)+(-42100000f - z)*Mathf.Sin(lat);

        float elev = Mathf.Asin(rz/rangeAb);
        lon += -90;
        lat += 54.794f;

        lat = (Mathf.Round(lat * roundingConstant) / roundingConstant);
        lon = (Mathf.Round(lon * roundingConstant) / roundingConstant);
        height = (Mathf.Round(height * roundingConstant) / roundingConstant);
        elev = (Mathf.Round(elev * roundingConstant) / roundingConstant);
        aziumuth = (Mathf.Round(aziumuth * roundingConstant) / roundingConstant);
        

        statusDisplay.text = lat  + "\n" + lon + "\n" + height + "\n" + elev + "\n" +aziumuth;
        // Debug.Log("Azimuth: " + aziumuth+"Elev: " + elev + "Lat: " + lat + "Lon: " + lon + "Height: " + height + "X: " + x + "Y: " + y + "Z: " + z);

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