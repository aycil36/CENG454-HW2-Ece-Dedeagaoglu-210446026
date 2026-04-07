using UnityEngine;
using TMPro;

public class FlightExamManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private TextMeshProUGUI missionText;

    [Header("Audio")]
    [SerializeField] private AudioClip dangerZoneClip;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip escapeClip;

    private AudioSource audioSource;

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
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        HideWarning();
        HideMissionText();
    }

    public void RegisterTakeoff()
    {
        hasTakenOff = true;
    }

    public void EnterDangerZone()
    {
        if (isInsideDangerZone) return;
        isInsideDangerZone = true;
        hasEnteredDangerZone = true;
        countdownActive = true;
        ShowWarning("Entered a Dangerous Zone!");
        PlayClip(dangerZoneClip);
    }

    public void ExitDangerZone()
    {
        if (!isInsideDangerZone) return;
        isInsideDangerZone = false;
        countdownActive = false;

        if (missileActive)
        {
            missileActive = false;
            threatCleared = true;
            ShowWarning("Threat Cleared!");
            CancelInvoke();
            Invoke(nameof(HideWarning), 2f);
            PlayClip(escapeClip);
        }
        else
        {
            HideWarning();
            PlayClip(escapeClip);
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

public void OnMissileHit()
{
    missileActive = false;
    threatCleared = false;
    PlayClip(hitClip);
    ShowWarning("MISSILE HIT! Restarting...");
    CancelInvoke();
    Invoke(nameof(RestartScene), 2f);
}

    private void RestartScene()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void CompleteMission()
    {
        if (missionComplete) return;
        missionComplete = true;
        ShowMissionText("Mission Complete!");
        PlayClip(escapeClip);
    }

    private void PlayClip(AudioClip clip)
    {
        if (clip != null && audioSource != null)
            audioSource.PlayOneShot(clip);
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