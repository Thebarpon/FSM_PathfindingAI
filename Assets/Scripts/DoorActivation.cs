using UnityEngine;

public class DoorActivation : MonoBehaviour
{
    public Vector3 offSetPosition;
    public float speed;
    private bool hasReachedDestination = false;
    public DoorKey DoorKey;

    Vector3 startPosition = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        offSetPosition = startPosition + offSetPosition;
    }

    void MoveDoor()
    {
        if (!hasReachedDestination)
        {
            transform.position = Vector3.MoveTowards(transform.position, offSetPosition, speed * Time.deltaTime);
            if (transform.position == offSetPosition)
            {
                hasReachedDestination = true;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            if (transform.position == startPosition)
            {
                hasReachedDestination = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (DoorKey.isOpen)
        {
            MoveDoor();
        }
    }
}
