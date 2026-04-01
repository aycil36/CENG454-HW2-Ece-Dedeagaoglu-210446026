using UnityEngine;

public class MissionLandingController : MonoBehaviour
{
    [SerializeField] private FlightExamManager examManager;
    [SerializeField] private float takeoffHeightThreshold = 3f;

    private bool takeoffRegistered = false;

    private void Update()
    {
        if (examManager == null) return;

        // Takeoff algılama
        if (!takeoffRegistered && transform.position.y >= takeoffHeightThreshold)
        {
            takeoffRegistered = true;
            examManager.RegisterTakeoff();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (examManager == null) return;

        // Landing area'ya giriş
        if (!other.CompareTag("LandingArea")) return;

        // Mission complete şartları
        if (examManager.hasTakenOff &&
            examManager.threatCleared &&
            !examManager.missionComplete)
        {
            examManager.CompleteMission();
        }
    }
}