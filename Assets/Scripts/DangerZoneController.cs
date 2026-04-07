using UnityEngine;
using System.Collections;

public class DangerZoneController : MonoBehaviour
{
    [SerializeField] private FlightExamManager examManager;
    [SerializeField] private MissileLauncher missileLauncher;
    [SerializeField] private float missileDelay = 5f;

    private Coroutine activeCountdown;
    private bool playerInside = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (playerInside) return;

        playerInside = true;

        if (examManager != null)
            examManager.EnterDangerZone();

        if (activeCountdown == null)
            activeCountdown = StartCoroutine(StartMissileCountdown(collision.transform));
    }

    private void OnTriggerExit(Collider collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (!playerInside) return;

        playerInside = false;

        if (activeCountdown != null)
        {
            StopCoroutine(activeCountdown);
            activeCountdown = null;
        }

        if (missileLauncher != null)
            missileLauncher.DestroyActiveMissile();

        if (examManager != null)
            examManager.ExitDangerZone();
    }

    private IEnumerator StartMissileCountdown(Transform playerTarget)
    {
        yield return new WaitForSeconds(missileDelay);

        if (missileLauncher != null && playerTarget != null)
        {
            missileLauncher.Launch(playerTarget);

            if (examManager != null)
                examManager.MissileLaunched();
        }

        activeCountdown = null;
    }
}