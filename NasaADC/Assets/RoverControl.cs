using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum Axle {
    Front,
    Rear
}

[Serializable]
public struct Wheel {
    public GameObject model;
    public WheelCollider collider;
    public Axle axle;
}


public class RoverControl : MonoBehaviour {
    public float movementForce = 500f;

    [SerializeField]
    private float maxAcceleration = 20.0f;
    [SerializeField]
    private float keyboardTurnSensitivity = 0.1f;
    [SerializeField]
    private float maxSteerAngle = 45.0f;
    [SerializeField]
    private List<Wheel> wheels;

    private float inputX;
    private float inputY;
    private Rigidbody rb;
    private float currentAngle;
    private Vector3 wheelModelOffset;
    
    // Start is called before the first frame update
    private void Start() {
        wheelModelOffset = wheels[0].model.transform.position - wheels[0].collider.transform.position;
    }

    // Update is called once per frame
    private void Update() {
        GetInputs();
    }

    private void GetInputs() {
        // Keyboard input WASD
        if (Input.GetKey("w")) {
            inputY = 1f;
        }
        else if (Input.GetKey("s")) {
            inputY = -1f;
        }
        else {
            inputY = 0;
        }
        if (Input.GetKey("a")) {
            inputX = -1f;
        }
        else if (Input.GetKey("d")) {
            inputX = 1f;
        }
        else {
            inputX = 0;
        }
        // Debug.Log(inputY);
        // Debug.Log(inputX);
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
        AnimateWheels();
    }

    private void Move() {
        // Apply torque to wheels if acceleration is held down
        foreach (var wheel in wheels) {
            wheel.collider.motorTorque = inputY * maxAcceleration * movementForce * Time.deltaTime;

        }
    }

    private void Turn() {
        foreach (var wheel in wheels) {
            if (wheel.axle == Axle.Front) {

                // Keyboard Input Specific Code
                // Is turn held down?
                // Yes, start turning
                if (inputX != 0f) {
                    currentAngle += Time.deltaTime * keyboardTurnSensitivity * inputX;
                }
                // No, return to center
                else {
                    currentAngle -= Time.deltaTime * keyboardTurnSensitivity / 0.5f * Mathf.Sign(wheel.collider.steerAngle);
                }
                wheel.collider.steerAngle = Mathf.Lerp(-maxSteerAngle, maxSteerAngle, currentAngle / 2f + 0.5f);
            }
        }
    }

    private void AnimateWheels() {
        foreach (var wheel in wheels) {
            Quaternion _rot;
            Vector3 _pos;
            wheel.collider.GetWorldPose(out _pos, out _rot);
            wheel.model.transform.rotation = _rot;
            wheel.model.transform.position = _pos + new Vector3(0, 0.3f, 0);
        }
    }
}
