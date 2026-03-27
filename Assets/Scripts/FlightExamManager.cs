using UnityEngine;
using TMPro;

public class FlightExamManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI warningText;

    [Header("State")]
    public bool hasEnteredDangerZone { get; private set; }
    public bool isInsideDangerZone { get; private set; }
    public bool countdownActive { get; private set; }
    public bool missileActive { get; private set; }
    public bool threatCleared { get; private set; }

    private float dangerZoneTimer = 0f;
    private float missileSpawnDelay = 5f;

    private void Awake()
    {
        HideWarning();
    }

    private void Update()
    {
        if (isInsideDangerZone && !missileActive)
        {
            dangerZoneTimer += Time.deltaTime;

            if (dangerZoneTimer >= missileSpawnDelay)
            {
                countdownActive = false;
                missileActive = true;
                ShowWarning("MISSILE INBOUND!");
            }
        }
    }

    public void EnterDangerZone()
    {
        isInsideDangerZone = true;
        hasEnteredDangerZone = true;

        if (!missileActive)
        {
            countdownActive = true;
            dangerZoneTimer = 0f;
            ShowWarning("Entered a Dangerous Zone!");
        }
    }

    public void ExitDangerZone()
    {
        isInsideDangerZone = false;
        countdownActive = false;
        dangerZoneTimer = 0f;

        if (missileActive)
        {
            missileActive = false;
            threatCleared = true;
            ShowWarning("Threat Cleared!");
            CancelInvoke();
            Invoke(nameof(HideWarning), 2f);
        }
        else
        {
            HideWarning();
        }
    }

    public void ShowWarning(string message)
    {
        if (warningText == null) return;

        warningText.gameObject.SetActive(true);
        warningText.text = message;
    }

    public void HideWarning()
    {
        if (warningText == null) return;

        warningText.text = "";
        warningText.gameObject.SetActive(false);
    }
}