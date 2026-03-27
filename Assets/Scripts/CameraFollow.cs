using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private float distance = 10f;
    [SerializeField] private float height = 3f;
    [SerializeField] private float lookAhead = 8f;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private bool useRightAxisAsForward = true;
    [SerializeField] private bool invertAxis = false;

    private Transform target;

    private void Start()
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);

        if (targetObject != null)
        {
            target = targetObject.transform;
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 forwardAxis = useRightAxisAsForward ? target.right : target.forward;

        if (invertAxis)
        {
            forwardAxis = -forwardAxis;
        }

        Vector3 desiredPosition = target.position - forwardAxis * distance + Vector3.up * height;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        Vector3 lookPoint = target.position + forwardAxis * lookAhead + Vector3.up * 1.2f;
        Quaternion desiredRotation = Quaternion.LookRotation(lookPoint - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothSpeed * Time.deltaTime);
    }
}