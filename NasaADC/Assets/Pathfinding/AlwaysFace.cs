using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFace : MonoBehaviour
{
    public Transform lookingAt;
    void Update()
    {
        this.transform.rotation = Quaternion.LookRotation(this.transform.position - lookingAt.position);
    }
}
