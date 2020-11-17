using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Text speedometer;
    public string speedometerText;
    public List<Transform> speedometerSegments;

    private GameObject roverObject;
    private RoverControl roverAttributes;
    private int speedometerSegmentsShown;
    static Vector3 shown = new Vector3(1, 1, 1);
    static Vector3 hidden = new Vector3(0, 0, 0);

    
    void Start()
    {
        roverObject = GameObject.Find("MoonRover");
        roverAttributes = roverObject.GetComponent<RoverControl>();
    }

    // Update is called once per frame
    void Update()
    {
        speedometer.text = Mathf.Round(roverAttributes.currentSpeed).ToString() + speedometerText;
        speedometerSegmentsShown = Mathf.FloorToInt(roverAttributes.maxSpeed / roverAttributes.currentSpeed) * 10;
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