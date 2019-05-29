using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayoutGroup : MonoBehaviour
{
    
    //Ce script gère la liste de joueur que contient une room quand on attend dans une room du lobby
    [SerializeField] 
    private GameObject _playerListingPrefab;

    private GameObject playerListingPrefab
    {
        get { return _playerListingPrefab; }
    }

    private List<PlayerListing> _listeDeJoueur = new List<PlayerListing>();

    private List<PlayerListing> listeDeJoueur
    {
        get { return _listeDeJoueur; }
    }

    private void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        PhotonNetwork.LeaveRoom();
    }
    
    //Appelée auto par photon quand un joueur rejoin la room
    private void OnJoinedRoom()
    {

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        MainCanvasManager.instance.currentRoomCanvas.transform.SetAsLastSibling();

        
        PhotonPlayer[] photonPlayers = PhotonNetwork.playerList;

        for (int i = 0; i < photonPlayers.Length; i = i + 1)
        {
            playerJoinedRoom(photonPlayers[i]);
        }

    }
    
    //Appelée auto par photon quand un joueur quitte la room
    private void OnPhotonPlayerDisconnected(PhotonPlayer unPhotonPLayer)
    {
        playerLeftRoom(unPhotonPLayer);
    }
    
    private void OnPhotonPlayerConnected(PhotonPlayer unPhotonPLayer)
    {
        playerJoinedRoom(unPhotonPLayer);
    }


    public void playerJoinedRoom(PhotonPlayer unPhotonPlayer)
    {
        if (unPhotonPlayer == null)
        {
            return;
        }

        playerLeftRoom(unPhotonPlayer);

        GameObject playerListingObj = Instantiate(playerListingPrefab);
        playerListingObj.transform.SetParent(transform,false);

        PlayerListing playerListing = playerListingObj.GetComponent<PlayerListing>();
        playerListing.applyPhotonPlayer(unPhotonPlayer);
        
        listeDeJoueur.Add(playerListing);



    }

    public void playerLeftRoom(PhotonPlayer unPhotonPlayer)
    {
        int index = listeDeJoueur.FindIndex(x => x.photonPlayer == unPhotonPlayer);
        if (index != -1)
        {
            Destroy(listeDeJoueur[index].gameObject);
            listeDeJoueur.RemoveAt(index);
        }
    }

    public void OnClickRoomState()
    {
        if (!PhotonNetwork.isMasterClient)
        {
            return;
        }

        PhotonNetwork.room.IsOpen = !PhotonNetwork.room.IsOpen;
        PhotonNetwork.room.IsVisible = PhotonNetwork.room.IsOpen;
    }

    public void onClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    
}
