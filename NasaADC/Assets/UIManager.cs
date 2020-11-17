using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    static Vector3 shown = new Vector3(1f, 1f, 1f);
    static Vector3 hidden = new Vector3(0f, 0f, 0f);
    private bool mapShown = false;
    public Transform mapView;
    public Transform blurLayer;
    public GameObject rover;

    public DefaultControl controls;

    void Start()
    {
        controls = new DefaultControl();
        controls.Enable();
        controls.UI.MapShow.performed += shmp => ShowMap();
        
        mapView.localScale = hidden;
        blurLayer.localScale = hidden;
        mapShown = false;
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
