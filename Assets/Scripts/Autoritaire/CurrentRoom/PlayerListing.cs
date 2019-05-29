using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviour
{
    public PhotonPlayer photonPlayer { get; private set; }

    [SerializeField] 
    private Text _playerName;

    private Text playerName
    {
        get { return _playerName; }
    }

    public void applyPhotonPlayer(PhotonPlayer unPhotonPlayer)
    {
        photonPlayer = unPhotonPlayer;
        playerName.text = unPhotonPlayer.NickName;
        
    }
    
}
