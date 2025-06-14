using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Photon.Pun;
using UnityEngine.UI;

public class GameNetworkManager : MonoBehaviour
{
    public static GameNetworkManager Instance;

    public UnityEvent OnGameOver;
    public UnityEvent OnGameWon;
    [SerializeField] private GameObject allPlayerUI;
    [SerializeField] private PhotonView pv;
    [SerializeField] private Button outBattleButton;

    private void Awake()
    {
        pv = gameObject.GetPhotonView();
    }

    private void Start()
    {
        if (!pv.IsMine)
        {
            allPlayerUI.SetActive(false);
            return;
        }
    }

    private void OnEnable()
    {
        outBattleButton.onClick.AddListener(OutOfBattle);
    }

    private void OnDisable()
    {
        outBattleButton.onClick.RemoveAllListeners();
    }

    private void OutOfBattle()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }

}
