using UnityEngine;

public class AchievementTrigger : MonoBehaviour
{
    private bool hasTriggered = false; // Prevents the achievement from being triggered multiple times

    private void OnCollisionEnter(Collision other)
    {
        // Check if the collider belongs to the player
        // This assumes your player GameObject has a tag labeled "Player"
        if (!hasTriggered && other.gameObject.CompareTag("Player"))
        {
            hasTriggered = true; // Ensure the achievement is only unlocked once
            AchievementManager.Instance.UnlockAchievement("First Win");
        }
    }
}
