using UnityEngine;

public class AircraftTakeoffDetector : MonoBehaviour
{
    [SerializeField] private FlightExamManager examManager;
    [SerializeField] private float takeoffHeight = 3f;

    private bool takeoffSent = false;

    private void Update()
    {
        if (takeoffSent || examManager == null) return;

        if (transform.position.y >= takeoffHeight)
        {
            takeoffSent = true;
            examManager.RegisterTakeoff();
        }
    }
}