using UnityEngine;

public abstract class PlayerStatistic : MonoBehaviour
{
    protected const int START_EXP_VALUE = 500;
    protected int playerLevel, currentEXP, EXPtoNextLevel;

    protected virtual void Awake()
    {
        playerLevel = PlayerPrefs.GetInt("PlayerLevel", 1);
        currentEXP = PlayerPrefs.GetInt("EXP");
        UpdateExpToNextLevel();
    }

    protected void UpdateExpToNextLevel()
    {
        EXPtoNextLevel = playerLevel * START_EXP_VALUE;
    }
}
