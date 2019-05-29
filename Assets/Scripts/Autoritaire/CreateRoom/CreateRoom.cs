using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour
{
    
    // C'est le script qui sera rattaché à notre bouton create room du lobby
    [SerializeField]
    private Text _roomName;
    
    //Création d'un getter, on peut le récupérer mais pas le changer
    private Text roomName{get{ return _roomName; } }

    
    // La fonction qui sera utilisée quand on cliquera sur le bouton "create room" du lobby
    public void OnClick_CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions() {IsVisible = true, IsOpen = true, MaxPlayers = 10};
        
        if (PhotonNetwork.CreateRoom(roomName.text, roomOptions, TypedLobby.Default))
        {
            print("room successfully created");
        }

        else
        {
            print("create room failed to send");
        }
    }
    
    
    //Appelé automatiquement quand la création  de la room a échoué 
    private void OnPhotonCreateRoomFailed(object[] codeAndMessage )
    {
        print("create room failed : " + codeAndMessage[1]);
    }
    
    //Appelé automatiquement quand la création  de la room s'est bien passée
    private void OnCreatedRoom()
    {
        print("room created successfully" );
    }

}
