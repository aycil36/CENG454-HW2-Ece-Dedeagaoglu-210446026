using UnityEngine;

public class AircraftThreatHandler : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.CompareTag("Missile")) return;
        Destroy(collision.gameObject);
    }
}