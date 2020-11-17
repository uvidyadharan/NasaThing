using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Text speedometer;

    private GameObject roverObject;
    private RoverControl roverAttributes;


    
    void Start()
    {
        roverObject = GameObject.Find("MoonRover");
        roverAttributes = roverObject.GetComponent<RoverControl>();
    }

    // Update is called once per frame
    void Update()
    {
        speedometer.text = Mathf.Round(roverAttributes.currentSpeed).ToString() + " MPH";

    }

}