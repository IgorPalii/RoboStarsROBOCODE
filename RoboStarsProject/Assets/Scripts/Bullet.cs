using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private BulletInfo info;
    [SerializeField]
    private PhotonView pView;
    [SerializeField]
    private Rigidbody rb;

    private void Awake()
    {
        info.render = gameObject;
    }
}
