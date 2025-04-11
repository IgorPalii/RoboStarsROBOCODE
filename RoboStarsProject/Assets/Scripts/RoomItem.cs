using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public RoomInfo roomInfo {private get; set; }

    [SerializeField]
    private TMP_Text roomNameText;
    [SerializeField]
    private Button roomButton;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        roomNameText.text = roomInfo.Name;
        roomButton.onClick.AddListener(JoinRoom);
    }

    private void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomInfo.Name);
    }
}
