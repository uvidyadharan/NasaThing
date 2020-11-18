using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    static Vector3 shown = new Vector3(1f, 1f, 1f);
    static Vector3 hidden = new Vector3(0f, 0f, 0f);

    public Transform mapView;
    public Transform blurLayer;
    public GameObject rover;
    public Projector heightMapProjector;
    public Projector slopeMapProjector;
    
    private bool mapShown = false;
    private bool slopeMapShown = true;
    private bool heightMapShown = true;

    public DefaultControl controls;

    void Start()
    {
        controls = new DefaultControl();
        controls.Enable();
        controls.UI.MapShow.performed += shmp => ShowMap();
        
        mapView.localScale = hidden;
        blurLayer.localScale = hidden;
        mapShown = false;
        SlopeMapToggle();
        HeightMapToggle();
    }

    public void SlopeMapToggle() {
        if (slopeMapShown) {
            slopeMapShown = false;
            slopeMapProjector.orthographic = false;
        }
        else {
            slopeMapShown = true;
            slopeMapProjector.orthographic = true;
        }
    }

    public void HeightMapToggle() {
        if (heightMapShown) {
            heightMapShown = false;
            heightMapProjector.orthographic = false;
        }
        else {
            heightMapShown = true;
            heightMapProjector.orthographic = true;
        }
    }

    void ShowMap() {
        if (mapShown) {
            mapView.localScale = hidden;
            blurLayer.localScale = hidden;
            rover.SetActive(true);
            mapShown = false;
        }
        else {
            mapView.localScale = shown;
            blurLayer.localScale = shown;
            rover.SetActive(false);
            mapShown = true;
        }
    }
}
