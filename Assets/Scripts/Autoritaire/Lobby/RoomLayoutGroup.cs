using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLayoutGroup : MonoBehaviour
{
    [SerializeField] 
    private GameObject _roomListingPrefab;

    public GameObject RoomListingPrefab
    {
        get => _roomListingPrefab;
        
    }
    
    private List<RoomListing> _listeDeRoom = new List<RoomListing>();

    private List<RoomListing> listeDeRoom
    {
        get { return _listeDeRoom; }
    }

    private void OnReceivedRoomListUpdate()
    {
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();

        foreach (RoomInfo room in rooms)
        {
            roomReceived(room);
        }

        removeOldRooms();
    }

    private void roomReceived(RoomInfo room)
    {
        int index = listeDeRoom.FindIndex(x => x.roomName == room.Name);

        if (index == -1)
        {
            if (room.IsVisible && room.PlayerCount < room.MaxPlayers)
            {
                GameObject roomListingObj = Instantiate(RoomListingPrefab);
                roomListingObj.transform.SetParent(transform, false);

                RoomListing roomListing = roomListingObj.GetComponent<RoomListing>();
                
                listeDeRoom.Add(roomListing);

                index = listeDeRoom.Count - 1;


            }
            
        }

        if (index != -1)
        {
            RoomListing roomListing = listeDeRoom[index];
            roomListing.setRoomNameText(room.Name);
            roomListing.updated = true;
        }
    }

    private void removeOldRooms()
    {
        List<RoomListing> roomsToRemove = new List<RoomListing>();

        foreach (RoomListing room in listeDeRoom)
        {
            if (!room.updated)
            {
                roomsToRemove.Add(room);
            }

            else
            {
                room.updated = false;
            }
        }

        foreach (RoomListing room in roomsToRemove)
        {
            GameObject roomListingObj = room.gameObject;
            listeDeRoom.Remove(room);
            Destroy(roomListingObj);
        }
        
      

    }
}
