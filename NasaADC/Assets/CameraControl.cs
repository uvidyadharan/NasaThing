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
    public float smoothAmount;
    
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
    }
    void Start() {

    }
    // Update is called once per frame
    void FixedUpdate() {
        Vector3 desiredPosition = rover.TransformPoint(cameraOffset);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothAmount);
        transform.position = smoothedPosition;
        transform.eulerAngles = rover.rotation.eulerAngles;

    }
}
