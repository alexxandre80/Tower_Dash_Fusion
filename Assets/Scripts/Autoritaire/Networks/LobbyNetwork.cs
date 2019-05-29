using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNetwork : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        print("connecting to server");
        PhotonNetwork.ConnectUsingSettings("0.1");
    }
    
    
    // Au moment où l'on rejoint le lobby on associe le nom du joueur généré dans le script PlayerNetwork au PhotonNetwork
    private void OnConnectedToMaster()
    {
        print("Connected to master");
        
        //Cette ligne fait en sorte que quand on se connect à une room, on est automatiquement synchronisé à la scène du master client
        PhotonNetwork.automaticallySyncScene = false;
        
        print("la sync avec le master est à : " + PhotonNetwork.automaticallySyncScene);

        PhotonNetwork.playerName = PlayerNetwork.instance.name;

        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    private void OnJoinedLobby()
    {
        print("JoinedLobby");
        if (!PhotonNetwork.inRoom)
        {
            MainCanvasManager.instance.lobbyCanvas.transform.SetAsLastSibling();
        }
        
    }

}
