using UnityEngine;
using TMPro;

public class FlightExamManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private TextMeshProUGUI missionText;

    [Header("Mission State")]
    public bool hasTakenOff { get; private set; }
    public bool hasEnteredDangerZone { get; private set; }
    public bool isInsideDangerZone { get; private set; }
    public bool countdownActive { get; private set; }
    public bool missileActive { get; private set; }
    public bool threatCleared { get; private set; }
    public bool missionComplete { get; private set; }

    private void Awake()
    {
        HideWarning();
        HideMissionText();
    }

    public void RegisterTakeoff()
    {
        hasTakenOff = true;
    }

    public void EnterDangerZone()
    {
        isInsideDangerZone = true;
        hasEnteredDangerZone = true;
        countdownActive = true;
        ShowWarning("Entered a Dangerous Zone!");
    }

    public void ExitDangerZone()
    {
        isInsideDangerZone = false;
        countdownActive = false;

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

    public void MissileLaunched()
    {
        missileActive = true;
        countdownActive = false;
        ShowWarning("MISSILE INBOUND!");
    }

    public void MissileDestroyed()
    {
        missileActive = false;
        threatCleared = true;
        ShowWarning("Threat Cleared!");
        CancelInvoke();
        Invoke(nameof(HideWarning), 2f);
    }

    public void CompleteMission()
    {
        if (missionComplete) return;
        missionComplete = true;
        ShowMissionText("Mission Complete!");
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

    public void ShowMissionText(string message)
    {
        if (missionText == null) return;
        missionText.gameObject.SetActive(true);
        missionText.text = message;
    }

    public void HideMissionText()
    {
        if (missionText == null) return;
        missionText.text = "";
        missionText.gameObject.SetActive(false);
    }
}