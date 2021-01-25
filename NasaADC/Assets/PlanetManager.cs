using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering.HighDefinition;

public class PlanetManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform earth;
    public Transform sun;
    public Transform moon;
    public HDAdditionalLightData sunLight;
    void Start()
    {
        StartCoroutine(UpdatePlanets());
        
    }

    // Update is called once per frame
    // void Update()
    // {
    //     
    // }
    IEnumerator UpdatePlanets()
    {
        WWWForm form = new WWWForm();
        form.AddField("key", "TRUMP2020");
        UnityWebRequest rq = UnityWebRequest.Post("http://52.186.47.67:80/get_position", form);
        // Request and wait for the desired page.
        yield return rq.SendWebRequest();

        if (rq.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error: " + rq.error);
        }
        else
        {
            // PlanetPositions pos = new PlanetPositions();
            PlanetPositions pos = JsonUtility.FromJson<PlanetPositions>(rq.downloadHandler.text);
            pos.ToVector3();
            print(pos.EarthPosition);
            print(pos.SunPosition);
            earth.position = pos.EarthPosition;
            sun.position = pos.SunPosition;
            sun.LookAt(moon);
            sunLight.distance = Vector3.Distance(pos.SunPosition, Vector3.zero);

        }
        
    }
}

[Serializable]
public class PlanetPositions
{
    public List<float> earth;
    public List<float> sun;
    public Vector3 EarthPosition;
    public Vector3 SunPosition;
    
    public void ToVector3()
    {
        EarthPosition = new Vector3(earth[0], -earth[1], earth[2]);
        SunPosition = new Vector3(sun[0], -sun[1], sun[2]);
    }
}