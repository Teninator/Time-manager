using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public Light sun;
    public float dayLengthInSeconds = 60f;

    [Header("Time of Day Settings")]
    [Range(0f, 1f)]
    public float currentTime = 0f;

    [Header("Light Intensity")]
    [Range(0f, 1f)]
    public float maxIntensity = 1f;
    [Range(0f, 1f)]
    public float minIntensity = 0.1f;

    [Header("Skybox Material")]
    public Material daySkybox;
    public Material nightSkybox;

    [Header("Ambient Light")]
    public Color dayAmbientColor = Color.yellow;
    public Color nightAmbientColor = new Color(0.1f, 0.1f, 0.2f);

    // Fields to represent different times of day
    public bool isMorning = false;
    public bool isAfternoon = false;
    public bool isEvening = false;
    public bool isDawn = false;
    public bool isDusk = false;
    public bool isMidnight = false;

    // Field to represent the pause state
    [Header("Pause time")]
    public bool isTimePaused = false;

    void Update()
    {
        if (!isTimePaused)
        {
            // Toggle for different times of day based on field values
            if (isMorning) currentTime = 0.1f;
            else if (isAfternoon) currentTime = 0.4f;
            else if (isEvening) currentTime = 0.7f;
            else if (isDawn) currentTime = 0.05f;
            else if (isDusk) currentTime = 0.55f;
            else if (isMidnight) currentTime = 0f;

            // Updates time based on the day length
            currentTime += Time.deltaTime / dayLengthInSeconds;

            // Wraps around time
            if (currentTime > 1f)
                currentTime -= 1f;
        }

        // Updates the sun's rotation based on the current time
        float angle = currentTime * 360f;
        sun.transform.rotation = Quaternion.Euler(new Vector3(angle, 0, 0));

        // Updates the sun's intensity based on the time
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.Abs(currentTime - 0.5f) / 0.5f);
        sun.intensity = intensity;

        // Updates the skybox material and ambient light based on the time
        RenderSettings.skybox = (currentTime < 0.5f) ? daySkybox : nightSkybox;
        RenderSettings.ambientLight = Color.Lerp(dayAmbientColor, nightAmbientColor, Mathf.Abs(currentTime - 0.5f) / 0.5f);
    }

    // Function to toggle pause/play
    public void ToggleTimePause()
    {
        isTimePaused = !isTimePaused;
    }

    // Methods to set different times of day
    public void SetMorning() { ResetTimes(); isMorning = true; }
    public void SetAfternoon() { ResetTimes(); isAfternoon = true; }
    public void SetEvening() { ResetTimes(); isEvening = true; }
    public void SetDawn() { ResetTimes(); isDawn = true; }
    public void SetDusk() { ResetTimes(); isDusk = true; }
    public void SetMidnight() { ResetTimes(); isMidnight = true; }

    // Helper method to reset all times of day
    
    private void ResetTimes()
    {
        isMorning = false;
        isAfternoon = false;
        isEvening = false;
        isDawn = false;
        isDusk = false;
        isMidnight = false;
    }
}
