using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class NickNameDisplay : MonoBehaviour
{
    [SerializeField]
    private PhotonView pView;
    [SerializeField]
    private TMP_Text nickName;

    private void Awake()
    {
        if (pView.IsMine) Destroy(nickName);
    }

    private void Start()
    {
        nickName.text = pView.Owner.NickName;
    }
}
