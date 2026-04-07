using UnityEngine;

public class MissionLandingController : MonoBehaviour
{
    [SerializeField] private FlightExamManager examManager;
    [SerializeField] private float takeoffHeightThreshold = 5f;

    private bool takeoffRegistered = false;

    private void Update()
    {
        if (examManager == null) return;

        if (!takeoffRegistered && transform.position.y >= takeoffHeightThreshold)
        {
            takeoffRegistered = true;
            examManager.RegisterTakeoff();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (examManager == null) return;

        if (!other.CompareTag("LandingArea")) return;

        if (examManager.hasTakenOff &&
            examManager.threatCleared &&
            !examManager.missionComplete)
        {
            examManager.CompleteMission();
        }
        else if (!examManager.hasTakenOff)
        {
            examManager.ShowWarning("You must take off first!");
        }
        else if (!examManager.threatCleared)
        {
            examManager.ShowWarning("Clear the threat before landing!");
        }
    }
}