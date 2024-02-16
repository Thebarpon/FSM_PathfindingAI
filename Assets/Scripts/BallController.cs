using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 1.0f;
    public float jumpForce = 5.0f;
    private float distanceToGround = 0.0f;
    private bool isJumping = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Collider collider = rb.GetComponent<Collider>();
        distanceToGround = collider.bounds.extents.y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJumping = false;
            //Debug.Log("Touched some grass");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJumping = true;
            //Debug.Log("To the moon");
        }

    }
    void Update()
    {
        float torqueForce = speed * Time.deltaTime;
        Vector3 vector3 = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            vector3 += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            vector3 += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            vector3 += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            vector3 += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
            isJumping = true;
            Debug.Log("Jumped once");
        }

        vector3.Normalize();
        if (vector3.magnitude <= 0)
        {
            return;
        }

        rb.AddTorque(vector3.normalized * torqueForce, ForceMode.Force);
    }
}