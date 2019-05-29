using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCanvas : MonoBehaviour
{

    [SerializeField] 
    private RoomLayoutGroup _roomLayoutGroup;

    public RoomLayoutGroup roomLayoutGroup
    {
        get { return _roomLayoutGroup; }
    }

    public void onClickJoinRoom(string roomName)
    {
        if (PhotonNetwork.JoinRoom(roomName))
        {
            
        }

        else
        {
            print("join room failed");
        }
    }
}
