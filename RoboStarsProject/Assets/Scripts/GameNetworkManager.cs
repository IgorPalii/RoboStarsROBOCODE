using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Photon.Pun;

public class GameNetworkManager : MonoBehaviour
{
    public UnityEvent OnGameOver;
    public UnityEvent OnGameWon;
    [SerializeField] private GameObject allPlayerUI;
    [SerializeField] private PhotonView pv;

    void Start()
    {
        if (!pv.IsMine)
        {
            allPlayerUI.SetActive(false);
        }
    }

    public void OutOfBattle()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }

}
