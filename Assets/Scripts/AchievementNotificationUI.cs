using TMPro;
using UnityEngine;

public class AchievementNotificationUI : MonoBehaviour
{
    public TextMeshProUGUI notificationText; // Assign this in the inspector
    public float displayTime = 5.0f; // How long to display the notification

    private void Awake()
    {
        // Initially hide the notification
        notificationText.gameObject.SetActive(false);
    }

    public void DisplayAchievement(string achievementId)
    {
        notificationText.text = $"Achievement Unlocked: {achievementId}";
        notificationText.gameObject.SetActive(true);
        Invoke("HideNotification", displayTime);
    }

    private void HideNotification()
    {
        notificationText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        AchievementManager.OnAchievementUnlocked += DisplayAchievement;
    }

    private void OnDisable()
    {
        AchievementManager.OnAchievementUnlocked -= DisplayAchievement;
    }
}