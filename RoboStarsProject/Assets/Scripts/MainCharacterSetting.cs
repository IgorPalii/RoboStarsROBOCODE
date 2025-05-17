using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MainCharacterSetting : MonoBehaviourPunCallbacks
{
    private const int MAX_HEALTH = 100;

    [SerializeField]
    private PhotonView pView;
    [SerializeField]
    private Image healthBar;

    private int health;

    private void Start()
    {
        health = MAX_HEALTH;
        healthBar.fillAmount = health;
    }

    public void TakeDamage(int value)
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
