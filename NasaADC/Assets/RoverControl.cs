using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Axle {
    Front,
    Rear
}

[Serializable]
public struct Wheel {
    public GameObject model;
    public WheelCollider collider;
    public Axle axle;
    public bool brake;
}


public class RoverControl : MonoBehaviour {


    public float movementForce = 1000f;
    public float brakeForce = 1000f;
    public float brakeSpeed = 500f;
    public Vector3 roverCenterOfMass;

    [SerializeField]
    private float maxAcceleration = 200.0f;
    [SerializeField]
    private float keyboardTurnSensitivity = 0.3f;
    [SerializeField]
    private float maxSteerAngle = 45.0f;
    [SerializeField]
    private List<Wheel> wheels;

    private float inputX;
    private float inputY;
    private Rigidbody rb;
    private float currentAngle;
    private Vector3 wheelModelOffset;
    private float currentBrakeForce;
    private float thrustPower;
    private float turnPower;

    public DefaultControl controls;
    
    // Start is called before the first frame update
    
    private void Awake() {

        controls = new DefaultControl();
        controls.Player.Throttle.performed += throt => thrustPower = (throt.ReadValue<float>());
        controls.Player.Steer.performed += str => turnPower = (str.ReadValue<float>());
    }

    private void OnEnable() {

        controls.Enable();
    }

    private void OnDisable() {

        controls.Disable();
    }

    private void Start() {

        wheelModelOffset = wheels[0].model.transform.position - wheels[0].collider.transform.position;
    }

    // Update is called once per frame

    private void FixedUpdate() {

        Move();
        Turn();
        AnimateWheels();
    }

    private void Move() {

        if (thrustPower !=0f) {
            // Power all wheels
            foreach (var wheel in wheels) {
                wheel.collider.brakeTorque = 0f;
                wheel.collider.motorTorque = thrustPower * maxAcceleration * movementForce * Time.deltaTime;
                // Debug.Log(wheel.collider.motorTorque);
                // Debug.Log(wheel.collider.brakeTorque);
            }
        }
        else {
            // Brake all wheels
            foreach (var wheel in wheels) {
                if (wheel.brake) {
                    if (wheel.collider.brakeTorque < brakeForce) {
                        wheel.collider.brakeTorque += brakeSpeed * Time.deltaTime;
                        // Debug.Log(wheel.collider.brakeTorque);
                    }
                }
            }
        }
    }

    private void Turn() {

        foreach (var wheel in wheels) {
            if (wheel.axle == Axle.Front) {
                currentAngle = Mathf.Clamp(currentAngle + Time.deltaTime * keyboardTurnSensitivity * turnPower, -1, 1);
                // Debug.Log(currentAngle);
                if (turnPower == 0f) {
                    currentAngle -= Time.deltaTime * keyboardTurnSensitivity / 2f * Mathf.Sign(wheel.collider.steerAngle);
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
