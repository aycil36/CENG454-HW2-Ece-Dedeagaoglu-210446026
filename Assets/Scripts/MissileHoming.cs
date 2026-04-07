using UnityEngine;

public class MissileHoming : MonoBehaviour
{
    [Header("Hareket")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float turnSpeed = 3f;

    [Header("Çarpışma")]
    [SerializeField] private float hitRadius = 1.5f;

    private Transform target;
    private FlightExamManager examManager;
    private bool hasHit = false;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;

        if (target != null)
        {
            Vector3 dir = (target.position - transform.position).normalized;
            if (dir != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    public void SetExamManager(FlightExamManager manager)
    {
        examManager = manager;
    }

private void Update()
{
    if (target == null || hasHit) return;

    Debug.Log("examManager: " + (examManager == null ? "NULL" : "OK"));

    float distance = Vector3.Distance(transform.position, target.position);

    if (distance <= hitRadius)
    {
        hasHit = true;
        if (examManager != null)
            examManager.OnMissileHit();
        Destroy(gameObject);
        return;
    }

        Vector3 direction = (target.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}