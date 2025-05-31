using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class MainCharacterSetting : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameNetworkManager gameManager;
    private const int MAX_HEALTH = 100;
    private const byte GAME_OVER_CODE = 112;

    [SerializeField]
    private PhotonView pView;
    [SerializeField]
    private Image healthBar;

    private int health;

    public Action<int> OnTakeDamage;

    private void Start()
    {
        health = MAX_HEALTH;
        healthBar.fillAmount = health;
    }

    private void OnEnable()
    {
        OnTakeDamage += TakeDamage;
        PhotonNetwork.NetworkingClient.EventReceived += OnNetworkEventCome;
    }

    private void OnDisable()
    {
        OnTakeDamage -= TakeDamage;
        PhotonNetwork.NetworkingClient.EventReceived -= OnNetworkEventCome;
    }

    private void OnNetworkEventCome(EventData data)
    {
        if (data.Code == GAME_OVER_CODE)
        {
            if (!pView.IsMine) return;
            gameManager.OnGameWon.Invoke();
        }
    }

    private void SendWinEvent()
    {
        object[] data = null;
        PhotonNetwork.RaiseEvent(GAME_OVER_CODE, data, RaiseEventOptions.Default, SendOptions.SendUnreliable);
    }

    private void TakeDamage(int value)
    {
        pView.RPC("UpdateHealth", RpcTarget.All, value);
    }

    [PunRPC]
    private void UpdateHealth(int value)
    {
        health -= value;

        if (health <= 0)
        {
            //health = MAX_HEALTH;
            //transform.GetComponentInChildren<MainCharacter>().Respawn();
            if (pView.IsMine)
            {
                SendWinEvent();
                gameManager.OnGameOver.Invoke();
            }
        }

        healthBar.fillAmount = health;
    }
}
