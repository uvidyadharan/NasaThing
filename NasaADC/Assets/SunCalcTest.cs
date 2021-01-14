using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SunCalcNet;
using SunCalcNet.Model;

public class SunCalcTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SunPosition ps = SunCalc.GetSunPosition(DateTime.Now, 0, 0);
        Debug.Log("" + (ps.Altitude * Mathf.Rad2Deg) + " " + (ps.Azimuth * Mathf.Rad2Deg));
        MoonPosition mp = MoonCalc.GetMoonPosition(DateTime.Now, 0, 0);
        Debug.Log("" + (mp.Altitude * Mathf.Rad2Deg) + " " +  (mp.Azimuth * Mathf.Rad2Deg)  + " "+ mp.Distance);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
