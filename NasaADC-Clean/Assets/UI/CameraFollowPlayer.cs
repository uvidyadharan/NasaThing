using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform player;
    public Vector3 offset;
    public float horizontalSpeed = 2.0f;
    public float verticalSpeed = 2.0f;
    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;
        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        float v = verticalSpeed * Input.GetAxis("Mouse Y");
        transform.Rotate(-v, h, 0);
    }
}
