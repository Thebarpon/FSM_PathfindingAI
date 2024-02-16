using UnityEngine;

public class DoorKey : MonoBehaviour
{
    public GameObject door;
    public bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        Collider collider = GetComponent<Collider>();
        GameObject door = GetComponent<GameObject>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected");
        if (collision.gameObject.name == "Character")
        {
            isOpen = true;
            Destroy(this.gameObject);
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
