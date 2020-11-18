using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{

    public Transform rover;
    public Vector3 cameraOffset1;
    public Vector3 cameraOffset2;
    public Vector3 cameraRotation1;
    public Vector3 cameraRotation2;
    public DefaultControl controls;
    public float smoothAmount;

    private Vector3 cameraOffset;
    private Vector3 cameraRotation;
    
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
        controls.Player.CAM1.performed += c1 => Camera1();
        controls.Player.CAM2.performed += c2 => Camera2();
        cameraOffset = cameraOffset1;
        cameraRotation = cameraRotation1;
    }
    private void Camera1() {
        cameraOffset = cameraOffset1;
        cameraRotation = cameraRotation1;
    }
    private void Camera2() {
        cameraOffset = cameraOffset2;
        cameraRotation = cameraRotation2;
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector3 desiredPosition = rover.TransformPoint(cameraOffset);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothAmount);
        transform.position = smoothedPosition;
        transform.eulerAngles = rover.rotation.eulerAngles + cameraRotation;

    }
}
