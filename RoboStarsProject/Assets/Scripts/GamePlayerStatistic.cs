using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePlayerStatistic : PlayerStatistic
{
    private const int LOOSE_COEF = 1, WIN_COEF = 3, STANDAR_EXP_VALUE = 50;

    [SerializeField]
    private TMP_Text titleText;
    [SerializeField]
    private TMP_Text levelText;
    [SerializeField]
    private TMP_Text currentExpText;
    [SerializeField]
    private Image fillExpArea;

    private void OnEnable()
    {
        //GameNetworkManager.Instance.OnGameWon.AddListener(ShowWinInfo);
    }

    private void OnDisable()
    {
        GameNetworkManager.Instance.OnGameWon.RemoveAllListeners();
    }

    private void UpdateText()
    {
        levelText.text = $"Level: {playerLevel}";
        currentExpText.text = $"{currentExpText} / {EXPtoNextLevel}";
        fillExpArea.fillAmount = (float)currentEXP / (float)EXPtoNextLevel;
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("PlayerLevel", playerLevel);
        PlayerPrefs.SetInt("EXP", currentEXP);
    }

    public void ShowWinInfo(int coef, string title)
    {
        int value = (playerLevel * STANDAR_EXP_VALUE) * WIN_COEF;
        if (currentEXP + value > EXPtoNextLevel)
        {
            int bufferValue = (currentEXP + value) - EXPtoNextLevel;
            playerLevel++;
            currentEXP = bufferValue;
            UpdateExpToNextLevel();
        }
        else
        {
            currentEXP += value;
        }
        titleText.text = "You Win!";
        UpdateText();
        SaveData();
    }

    public void ShowLooseInfo()
    {
        int value = (playerLevel * STANDAR_EXP_VALUE) * LOOSE_COEF;
        if (currentEXP + value > EXPtoNextLevel)
        {
            int bufferValue = (currentEXP + value) - EXPtoNextLevel;
            playerLevel++;
            currentEXP = bufferValue;
            UpdateExpToNextLevel();
        }
        else
        {
            currentEXP += value;
        }
        titleText.text = "You Loose!";
        UpdateText();
        SaveData();
    }
}
