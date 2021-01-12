using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alwaysFace : MonoBehaviour
{
    public Transform lookingAt;
    // Update is called once per frame
    void Start()
    {

    }
    void Update()
    {
        this.transform.rotation = Quaternion.LookRotation(this.transform.position - lookingAt.position);
    }
}
