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

[Serializable]
public struct PLoop {
    public float h; // minimum brake value applied if over speed limit
    public float b; // minimum adjustment
    public float a;
    public float speedCorrectionCoefficient;
    public float speedOver;
}

public class RoverControl : MonoBehaviour {


    public float brakeForce = 1000f;
    public float brakeSpeed = 500f;
    public Vector3 roverCenterOfMass;
    public Vector3 wheelColliderOffset;
    public Rigidbody rb;
    public float turnRebound;
    public float maxSpeed = 50f;
    public float speedControlForce;
    public PLoop pl;
    public float currentSpeed;

    [SerializeField]
    private float acceleration = 200.0f;
    [SerializeField]
    private float keyboardTurnSensitivity = 0.3f;
    [SerializeField]
    private float maxSteerAngle = 45.0f;
    [SerializeField]
    private List<Wheel> wheels;

    private float inputX;
    private float inputY;
    private float currentAngle;
    private Vector3 wheelModelOffset;
    private float currentBrakeForce;
    private float thrustPower; // between -1 and 1
    private float turnPower; // between -1 and 1
    private bool brakeControl;

    //P loop variables

    public DefaultControl controls;
    
    // Start is called before the first frame update
    
    private void Awake() {

        controls = new DefaultControl();
        controls.Player.Throttle.performed += throt => thrustPower = (throt.ReadValue<float>());
        controls.Player.Brake.performed += bra => BrakeControlControl();
        controls.Player.Steer.performed += str => turnPower = (str.ReadValue<float>());
        rb.centerOfMass = roverCenterOfMass;
        maxSpeed *= 3.6f;
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

    private void BrakeControlControl() {
        if (brakeControl) {
            brakeControl = false;
        }
        else {
            brakeControl = true;
        }
    }

    // Update is called once per frame

    private void FixedUpdate() {

        Move();
        Turn();
        AnimateWheels();
    }

    private void Move() {
        currentSpeed = Mathf.Sqrt(Mathf.Pow(rb.velocity[0], 2f)  + Mathf.Pow(rb.velocity[1], 2f) + Mathf.Pow(rb.velocity[2], 2f));
        // Debug.Log(currentSpeed);

        pl.speedOver = currentSpeed - maxSpeed;

        if (thrustPower !=0f) {
            // Power all wheels

            foreach (var wheel in wheels) {
                wheel.collider.motorTorque = thrustPower * acceleration * Time.deltaTime;
                // Debug.Log(wheel.collider.motorTorque);
                // Debug.Log(wheel.collider.brakeTorque);
            }
        }
        if (brakeControl) {
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
        else {
            foreach (var wheel in wheels) {

                if (pl.speedOver > 0) {
                    if (pl.speedOver / pl.a + pl.b >= pl.h) {
                        wheel.collider.brakeTorque = pl.speedOver / pl.a + pl.b;
                    }
                    else {
                        wheel.collider.brakeTorque = pl.h;
                    }
                }
                else {
                        wheel.collider.brakeTorque = 0;
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
                    currentAngle -= Time.deltaTime  * turnRebound * Mathf.Sign(wheel.collider.steerAngle);
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
            wheel.model.transform.position = _pos + wheelColliderOffset;
        }
    }
}
