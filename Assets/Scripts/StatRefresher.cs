using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatRefresher : MonoBehaviour
{
    
    private int health;
    private int energy;
    private PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {

        if (PhotonNetwork.isMasterClient)
        {
            foreach (var playerGameObject in GameObject.FindGameObjectsWithTag("Joueur"))
            {
                int index = PlayerManagement.Instance.listeInfoJoueurs.FindIndex(x => x.photonPlayerJoueur == playerGameObject.GetPhotonView().owner);
                if (index != -1)
                {
                
                    health = PlayerManagement.Instance.listeInfoJoueurs[index].health;
                    energy = PlayerManagement.Instance.listeInfoJoueurs[index].energy;


                    photonView.RPC("RPC_UpdateStats", PhotonTargets.All,  playerGameObject.GetPhotonView().owner, health, energy);
                
                    UnityEngine.Debug.Log("taille de la liste de joueur : " + PlayerManagement.Instance.listeInfoJoueurs.Count);

                }

                else
                {
                    print("l'HUD marche pas");
                }
            }
            
        }
        
        
        
    }
    
    [PunRPC]
    private void RPC_UpdateStats(PhotonPlayer unPhotonPlayer, int health, int energy)
    {

        int count = 0;

        foreach (var playerGameObject in GameObject.FindGameObjectsWithTag("Joueur"))
        {
            
            if (playerGameObject.GetPhotonView().owner ==  unPhotonPlayer)
            {

                playerGameObject.GetComponent<Health>().currentHealth = health;
                
                playerGameObject.GetComponent<Energy>().energy = energy;
                
               
                
                UnityEngine.Debug.Log("le joueur " + unPhotonPlayer.ID + " est présent");
                

            }

            
        }

    }

}
