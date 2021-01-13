using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    static Vector3 shown = new Vector3(1f, 1f, 1f);
    static Vector3 hidden = new Vector3(0f, 0f, 0f);

    public Transform mapView;
    public Transform blurLayer;
    public GameObject rover;
    public DecalProjector heightMapProjector;
    public DecalProjector slopeMapProjector;
    public RawImage HMToggle;
    public RawImage SMToggle;

    //map assets
    public RawImage HMToggleMap;
    public RawImage SMToggleMap;
    
    private bool mapShown = false;
    private bool slopeMapShown = true;
    private bool heightMapShown = true;
    private bool UIShown = true;

    public DefaultControl controls;

    void Start()
    {
        controls = new DefaultControl();
        controls.Enable();
        controls.UI.MapShow.performed += shmp => ShowMap();
        controls.UI.UIHide.performed += hide => HideUI();
        
        mapView.localScale = hidden;
        blurLayer.localScale = hidden;
        mapShown = false;

        slopeMapShown = false;
        slopeMapProjector.fadeFactor = 0f;
        SMToggle.color = new Color(0.3f, 0.3f, 0.3f,1f);
        SMToggleMap.color = new Color(0.3f, 0.3f, 0.3f,1f);

        heightMapShown = false;
        heightMapProjector.fadeFactor = 0f;
        HMToggle.color = new Color(0.3f, 0.3f, 0.3f,1f);
        HMToggleMap.color = new Color(0.3f, 0.3f, 0.3f,1f);
    }

    void HideUI() {
        if(UIShown) {
            transform.localScale = new Vector3(0, 0, 0);
            UIShown = false;
        }
        else {
            transform.localScale = new Vector3(1, 1, 1);
            UIShown = true;
        }

    }

    public void SlopeMapToggle() {
        if (slopeMapShown) {
            slopeMapShown = false;
            slopeMapProjector.fadeFactor = 0f;
            SMToggle.color = new Color(0.3f, 0.3f, 0.3f,1f);
            SMToggleMap.color = new Color(0.3f, 0.3f, 0.3f,1f);

        }
        else {
            if (heightMapShown) {
                HeightMapToggle();
            }
            slopeMapShown = true;
            slopeMapProjector.fadeFactor = 1f;
            SMToggle.color = new Color(0.185f, 1f, 0f, 1f);
            SMToggleMap.color = new Color(0.185f, 1f, 0f, 1f);

        }
    }

    public void HeightMapToggle() {
        Debug.Log(heightMapShown);
        if (heightMapShown) {
            heightMapShown = false;
            heightMapProjector.fadeFactor = 0f;
            HMToggle.color = new Color(0.3f, 0.3f, 0.3f,1f);
            HMToggleMap.color = new Color(0.3f, 0.3f, 0.3f,1f);
        }
        else {
            if (slopeMapShown) {
                SlopeMapToggle();
            }
            heightMapShown = true;
            heightMapProjector.fadeFactor = 1f;
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
