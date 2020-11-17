using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapCameraControl : MonoBehaviour
{
    public Slider scaleSlider;

    public Camera mapCamera;
    public Transform rover;
    public Terrain moonSurface;
    public DefaultControl controls;

    public Vector3 camPosition;
    public float mapSenseX;
    public float mapSenseY;
    public bool cameraIsOrtho;

    private float controlDirX;
    private float controlDirY;
    private bool goToRoverCommand = false;
    public Vector3 desiredGotoPosition;
    private float distanceFromSurface;



    public void OrthoSelector() {
        if (cameraIsOrtho) {
            cameraIsOrtho = false;
            mapCamera.orthographic = false;
            mapCamera.fieldOfView = 60f;
        }
        else {
            cameraIsOrtho = true;
            mapCamera.orthographic = true;
            mapCamera.orthographicSize = (transform.position.y - moonSurface.SampleHeight(camPosition)) / Mathf.Sqrt(3);
        }
    }

    public void GoToPosition() {
        goToRoverCommand = true;
        distanceFromSurface = transform.position.y - moonSurface.SampleHeight(transform.position) + moonSurface.SampleHeight(rover.position);
        desiredGotoPosition = new Vector3(rover.position.x, distanceFromSurface, rover.position.z);
    }

    private void Awake() {
        controls = new DefaultControl();

        controls.UI.Enable();
        controls.UI.MapPanX.performed += mpanx => controlDirX = (mpanx.ReadValue<float>());
        controls.UI.MapPanY.performed += mpany => controlDirY = (mpany.ReadValue<float>());
    }


    void Start()
    {
        camPosition = new Vector3(rover.position.x, 7100f + Mathf.Pow(10, (scaleSlider.value * 10)), rover.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        camPosition.y = 100f + Mathf.Pow(4, (scaleSlider.value * 8)) + moonSurface.SampleHeight(camPosition);
        camPosition.x += controlDirX * (camPosition.y - moonSurface.SampleHeight(camPosition)) * mapSenseX;
        camPosition.z += controlDirY * (camPosition.y - moonSurface.SampleHeight(camPosition)) * mapSenseY;

        mapCamera.farClipPlane = camPosition.y + 4000;
        mapCamera.nearClipPlane = Mathf.Max(1f, camPosition.y - 10000);
        mapCamera.orthographicSize = (transform.position.y - moonSurface.SampleHeight(camPosition)) / Mathf.Sqrt(3);

        moonSurface.basemapDistance = Mathf.Max(camPosition.y + 1000);

        if (goToRoverCommand) {
            // camPosition = Vector3.Lerp(transform.position, desiredGotoPosition, 0.125f * Time.deltaTime);

            camPosition = Vector3.Lerp(transform.position, desiredGotoPosition, 6f * Time.deltaTime);
            if (Vector3.Distance(desiredGotoPosition, camPosition) < 0.0001 * (distanceFromSurface)) {
                goToRoverCommand = false;
            }
        }
        transform.position = camPosition;
        // Debug.Log(controlDirX);
        // Debug.Log(controlDirY);



    }
}