using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class Freecam : MonoBehaviour
{

    public Transform rover;
    public DefaultControl controls;

    public Vector3 camPosition;
    [FormerlySerializedAs("XYSensitivity")] public float xySensitivity;
    public float heightSensitivity;
    public float panSensitivity;

    private Vector2 _controlDir;
    private float _controlHeight;
    private float _controlPan;
    private float _controlVPan;


    private void Awake() {
        controls = new DefaultControl();

        controls.FreeCam.Enable();
        controls.FreeCam.Move.performed += mpan => _controlDir = (mpan.ReadValue<Vector2>());
        controls.FreeCam.Pan.performed += pan => _controlPan = (pan.ReadValue<float>());
        controls.FreeCam.Height.performed += ht => _controlHeight = (ht.ReadValue<float>());
        controls.FreeCam.AddSpeed.performed += ass => xySensitivity += ass.ReadValue<float>() * 0.1f;
        controls.FreeCam.PanSpeed.performed += ps => panSensitivity += ps.ReadValue<float>() * 0.1f;
        controls.FreeCam.VPan.performed += vp => _controlVPan = vp.ReadValue<float>();
    }


    void Start()
    {
        transform.position = rover.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3(_controlDir.x * xySensitivity, _controlHeight * heightSensitivity,
            _controlDir.y * xySensitivity);
        transform.Translate(dir * Time.deltaTime * 200);
        transform.Rotate(_controlVPan * panSensitivity * Time.deltaTime * 60, _controlPan * panSensitivity * Time.deltaTime * 60, 0);
    }

}
