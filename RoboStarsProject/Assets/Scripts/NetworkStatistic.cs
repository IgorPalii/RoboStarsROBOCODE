using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkStatistic : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(UpdateConnectedPlayerCount());
    }

    private IEnumerator UpdateConnectedPlayerCount()
    {
        yield return new WaitForSeconds(3);
        while (true)
        {            
            UIManager.Instance.ChangeConnectedPlayersText(PhotonNetwork.CountOfPlayers);
            yield return new WaitForSeconds(6);
        }        
    }
}
