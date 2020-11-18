﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public RawImage HMToggle;
    public RawImage SMToggle;

    //map assets
    public RawImage HMToggleMap;
    public RawImage SMToggleMap;
    
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

        slopeMapShown = false;
        slopeMapProjector.orthographic = false;
        SMToggle.color = new Color(0.3f, 0.3f, 0.3f,1f);
        SMToggleMap.color = new Color(0.3f, 0.3f, 0.3f,1f);

        heightMapShown = false;
        heightMapProjector.orthographic = false;
        HMToggle.color = new Color(0.3f, 0.3f, 0.3f,1f);
        HMToggleMap.color = new Color(0.3f, 0.3f, 0.3f,1f);
    }

    public void SlopeMapToggle() {
        if (slopeMapShown) {
            slopeMapShown = false;
            slopeMapProjector.orthographic = false;
            SMToggle.color = new Color(0.3f, 0.3f, 0.3f,1f);
            SMToggleMap.color = new Color(0.3f, 0.3f, 0.3f,1f);

        }
        else {
            if (heightMapShown) {
                HeightMapToggle();
            }
            slopeMapShown = true;
            slopeMapProjector.orthographic = true;
            SMToggle.color = new Color(0.185f, 1f, 0f, 1f);
            SMToggleMap.color = new Color(0.185f, 1f, 0f, 1f);

        }
    }

    public void HeightMapToggle() {
        Debug.Log(heightMapShown);
        if (heightMapShown) {
            heightMapShown = false;
            heightMapProjector.orthographic = false;
            HMToggle.color = new Color(0.3f, 0.3f, 0.3f,1f);
            HMToggleMap.color = new Color(0.3f, 0.3f, 0.3f,1f);
        }
        else {
            if (slopeMapShown) {
                SlopeMapToggle();
            }
            heightMapShown = true;
            heightMapProjector.orthographic = true;
            HMToggle.color = new Color(0.185f, 1f, 0f, 1f);
            HMToggleMap.color = new Color(0.185f, 1f, 0f, 1f);
        }
    }

    void ShowMap() {
        if (mapShown) {
            mapView.localScale = hidden;
            blurLayer.localScale = hidden;
            mapShown = false;
        }
        else {
            mapView.localScale = shown;
            blurLayer.localScale = shown;
            mapShown = true;
        }
    }
}
