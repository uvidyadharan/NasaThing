using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHitScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.CompareTag("Player"))
        {
            transform.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
