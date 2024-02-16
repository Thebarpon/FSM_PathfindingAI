using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    public Transform target;
    private Vector3 offSet = Vector3.zero;
    public Vector3 velocity = Vector3.zero;
    public float smoothness = 0.3F;
    // Start is called before the first frame update
    void Start()
    {
        offSet = transform.position - target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 targetPosition = target.position + offSet;


        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition + offSet, smoothness);
        transform.SetPositionAndRotation(smoothedPosition, target.rotation);
        transform.position = smoothedPosition;

        transform.LookAt(targetPosition);
    }
}

