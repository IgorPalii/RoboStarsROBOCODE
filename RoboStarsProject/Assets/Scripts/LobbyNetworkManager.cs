using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyNetworkManager : MonoBehaviourPunCallbacks
{
    public static LobbyNetworkManager Instance;

    [SerializeField] private TMP_Text waitBattleText;
    [SerializeField] private Button findBattleButton;
    [SerializeField] private Button stopFindBattleButton;
 
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        UIManager.Instance.OpenPanel("LoadingPanel");
    }

    private void OnEnable()
    {
        findBattleButton.onClick.AddListener(ToBattleButton);
        stopFindBattleButton.onClick.AddListener(StopFindBattleButton);
    }

    private void OnDisable()
    {
        findBattleButton.onClick.RemoveAllListeners();
        stopFindBattleButton.onClick.RemoveAllListeners();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        UIManager.Instance.OpenPanel("MenuPanel");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        if (returnCode == (short)ErrorCode.NoRandomMatchFound)
        {
            waitBattleText.text = "No matches found. Creating new room...";
            CreateNewRoom();
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        if (returnCode == (short)ErrorCode.GameIdAlreadyExists)
        {
            CreateNewRoom();
        }
    }

    public override void OnCreatedRoom()
    {
        waitBattleText.text = "Waiting for the second player...";
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        if(PhotonNetwork.IsMasterClient) return;

        waitBattleText.text = "The battle begins. Get ready!";
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        Room currentRoon = PhotonNetwork.CurrentRoom;
        currentRoon.IsOpen = false;

        waitBattleText.text = "The battle begins. Get ready!";
        Invoke("LoadingGameMap", 3f);
    }

    public override void OnLeftRoom()
    {
        UIManager.Instance.OpenPanel("MenuPanel");
    }

    private void LoadingGameMap()
    {
        PhotonNetwork.LoadLevel(1);
    }

    private string GenerateRoomCode()
    {
        short codeLength = 12;
        string roomCode = string.Empty;
        for (short i = 0; i < codeLength; i++)
        {
            char c = (char)Random.Range(65, 91);
            roomCode += c;
        }
        return roomCode;
    }

    private void CreateNewRoom()
    {
        RoomOptions currentRoom = new RoomOptions();
        currentRoom.IsOpen = true;
        currentRoom.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(GenerateRoomCode(), currentRoom);
    }

    private void StopFindBattleButton()
    {
        PhotonNetwork.LeaveRoom();
    }

    private void ToBattleButton()
    {
        UIManager.Instance.OpenPanel("QuickBattlePanel");
        PhotonNetwork.JoinRandomRoom();
    }
}
