using UnityEngine;

public class LandingZone : MonoBehaviour
{
    [SerializeField] private FlightExamManager examManager;
    [SerializeField] private float landingHeightThreshold = 1.5f;
    [SerializeField] private float maxLandingSpeed = 8f;

    private Collider landingCollider;

    private void Awake()
    {
        landingCollider = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (examManager == null) return;

        // Uçağın alt noktası
        float aircraftBottomY = other.bounds.min.y;

        // Landing alanının üst yüzeyi
        float landingTopY = landingCollider.bounds.max.y;

        // Zemine yakın mı?
        bool closeToGround = aircraftBottomY <= landingTopY + landingHeightThreshold;

        // Hızı düşük mü?
        Rigidbody rb = other.attachedRigidbody;
        bool slowEnough = true;

        if (rb != null)
        {
            slowEnough = rb.linearVelocity.magnitude <= maxLandingSpeed;
        }

        if (closeToGround && slowEnough)
        {
            examManager.TryCompleteMission();
        }
    }
}