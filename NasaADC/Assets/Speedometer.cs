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