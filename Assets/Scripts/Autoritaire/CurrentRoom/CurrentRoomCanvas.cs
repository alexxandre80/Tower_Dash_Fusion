using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
    public void onClickStartSync()
    {
        if (!PhotonNetwork.isMasterClient)
        {
            return;
        }
        PhotonNetwork.LoadLevel(1);
    }

    public void onClickStartDelayed()
    {
        
        if (!PhotonNetwork.isMasterClient)
        {
            return;
        }
        
        PhotonNetwork.room.IsOpen = false;
        PhotonNetwork.room.IsVisible = false;
        PhotonNetwork.LoadLevel(1);
        
    }
    
    
}
