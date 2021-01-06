using UnityEngine;

public class Move : MonoBehaviour
{

    public Rigidbody rb;
    public Transform cameraMain;
    
    public Vector3 cameraOffset = new Vector3(0, 0, -100);


    public float moveSpeedForward = 1000f;
    public float moveSpeedHorizontal = 1000f;
    public float horizontalSpeed = 2.0f;
    public float verticalSpeed = 2.0f;
    public float horizontalTorque = 1000f;
    public float verticalTorque = 1000f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        float v = verticalSpeed * Input.GetAxis("Mouse Y");

        // Middle mouse for free orbit
        if (Input.GetKey(KeyCode.Mouse2)) {
            cameraMain.RotateAround(transform.position, transform.right, -v * 2.0f);
            cameraMain.LookAt(transform, transform.up);
            cameraMain.RotateAround(transform.position, transform.up, h);
        }
        // Else mouse controls look and rotation
        else {
            rb.AddTorque(transform.up * horizontalTorque * h);
            cameraMain.RotateAround(transform.position, transform.right, -v * 2.0f);
        }
        
        // Basic moveement
        if (Input.GetKey("d")) {
            rb.AddForce(transform.right * Time.deltaTime * moveSpeedForward);
        }
        else if (Input.GetKey("a")) {
            rb.AddForce(-transform.right * Time.deltaTime * moveSpeedForward);
        }
        else if (Input.GetKey("w")) {
            rb.AddForce(transform.forward * Time.deltaTime * moveSpeedForward);
        }
        else if (Input.GetKey("s")) {
            rb.AddForce(-transform.forward * Time.deltaTime * moveSpeedForward);
        }

        // Right the player
        if (Input.GetKey("r")) {
        }
    }
}