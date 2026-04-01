using UnityEngine;

public class MissileHoming : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 18f;
    [SerializeField] private float turnSpeed = 4f;

    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void Update()
    {
        if (target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            turnSpeed * Time.deltaTime
        );

        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
