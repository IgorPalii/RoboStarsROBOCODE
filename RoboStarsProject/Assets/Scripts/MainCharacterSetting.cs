using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;

public class MainCharacterSetting : MonoBehaviourPunCallbacks
{
    private const int MAX_HEALTH = 100;

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
    }

    private void OnDisable()
    {
        OnTakeDamage -= TakeDamage;
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
            health = MAX_HEALTH;
            transform.GetComponentInChildren<MainCharacter>().Respawn();
        }

        healthBar.fillAmount = health;
    }
}
