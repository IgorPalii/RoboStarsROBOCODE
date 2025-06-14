using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuPlayerStatistic : PlayerStatistic
{
    [SerializeField]
    private TMP_Text levelText;
    [SerializeField]
    private TMP_Text currentExpText;
    [SerializeField]
    private Image fillExpArea;

    private void Start()
    {
        levelText.text = $"Level: {playerLevel}";
        currentExpText.text = $"{currentExpText} / {EXPtoNextLevel}";
        fillExpArea.fillAmount = (float)currentEXP / (float)EXPtoNextLevel;
    }

    void Update()
    {
        
    }
}
