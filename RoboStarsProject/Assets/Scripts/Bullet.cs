using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private BulletInfo info;
    [SerializeField]
    private PhotonView pView;
    [SerializeField]
    private Rigidbody rb;

    public Action<Vector3> OnSpawned;

    private void Awake()
    {
        info.render = gameObject;
    }

    private void OnEnable()
    {
        OnSpawned += StartMove;
    }

    private void OnDisable()
    {
        OnSpawned -= StartMove;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!pView.IsMine) return;

        if (col.CompareTag("Player"))
        {
            col.GetComponentInParent<MainCharacterSetting>().OnTakeDamage?.Invoke(info.damage);
            PhotonNetwork.Destroy(gameObject);
        }
    }

    private void StartMove(Vector3 dir)
    {
        rb.linearVelocity = info.speed * dir;
    }
}
