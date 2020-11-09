using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{

    public Transform rover;
    public Vector3 cameraOffset;
    public DefaultControl controls;
    
    private bool lookMode;
    // Start is called before the first frame update
    private void OnEnable() {

        controls.Enable();
    }

    private void OnDisable() {

        controls.Disable();
    }
    private void Awake() {
        
        controls = new DefaultControl();
        // controls.Player.Look.performed += look => Debug.Log(look.ReadValue<Vector2>());
        controls.Player.LookMode.performed += lookmode => Debug.Log("Pressed");
    }
    void Start() {

        transform.position = rover.position;
        transform.position += cameraOffset[2] * transform.TransformDirection(rover.forward);
        transform.position += cameraOffset[1] * transform.TransformDirection(rover.up);
        transform.position += cameraOffset[0] * transform.TransformDirection(rover.right);
    }

    // Update is called once per frame

    private void Rotate(Vector2 direction) {


    }
    void Update() {


        transform.position = rover.position;
        transform.position += cameraOffset[2] * transform.TransformDirection(rover.forward);
        transform.position += cameraOffset[1] * transform.TransformDirection(rover.up);
        transform.position += cameraOffset[0] * transform.TransformDirection(rover.right);
    }
}
