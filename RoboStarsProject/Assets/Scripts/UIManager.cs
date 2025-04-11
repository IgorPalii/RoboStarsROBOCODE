using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField]
    private GameObject[] panels;
    //[SerializeField]
    private TMP_Text connectedPlayersText;

    private void Awake()
    {
        Instance = this;
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }        
    }

    private void Start()
    {
        OpenPanel("LoadingPanel");
    }

    public void OpenPanel(string panelName)
    {
        foreach (GameObject panel in panels)
        {
            if (panel.name == panelName) 
            {
                panel.SetActive(true);
            }
            else
            {
                panel.SetActive(false);
            }
        }
    }

    public void ChangeConnectedPlayersText(int playersCount)
    {
        connectedPlayersText.text = $"Connected players: {playersCount}";
    }
}
