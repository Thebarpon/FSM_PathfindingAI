using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;

    private HashSet<string> unlockedAchievements = new HashSet<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public bool UnlockAchievement(string achievementId)
    {
        if (!unlockedAchievements.Contains(achievementId))
        {
            unlockedAchievements.Add(achievementId);
            Debug.Log($"Achievement unlocked: {achievementId}");

            // Notify any listeners that an achievement has been unlocked
            OnAchievementUnlocked?.Invoke(achievementId);

            return true;
        }
        return false;
    }

    public bool IsAchievementUnlocked(string achievementId)
    {
        return unlockedAchievements.Contains(achievementId);
    }

    public static event Action<string> OnAchievementUnlocked;


}