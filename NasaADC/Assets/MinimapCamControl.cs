using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapCamControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform rover;
    public Vector3 minimapCameraOffset;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = rover.position + minimapCameraOffset;
        transform.eulerAngles = new Vector3(90,0, -rover.rotation.eulerAngles.y);
    }
}
