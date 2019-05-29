using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatRefresher : MonoBehaviour
{
    
    private float health;
    private float maxHealth;
    private float notoriety;
    private float damageStone;
    private float damagePaper;
    private float damageScissor;
    private int inFight;
    private float movementSpeed;
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
                    maxHealth = PlayerManagement.Instance.listeInfoJoueurs[index].maxHealth;
                    notoriety = PlayerManagement.Instance.listeInfoJoueurs[index].notoriety;
                    damageStone = PlayerManagement.Instance.listeInfoJoueurs[index].damageStone;
                    damagePaper = PlayerManagement.Instance.listeInfoJoueurs[index].damagePaper;
                    damageScissor = PlayerManagement.Instance.listeInfoJoueurs[index].damageScissor;
                    inFight = PlayerManagement.Instance.listeInfoJoueurs[index].inFight;
                    movementSpeed = PlayerManagement.Instance.listeInfoJoueurs[index].movementSpeed;
                
                    photonView.RPC("RPC_UpdateStats", PhotonTargets.All,  playerGameObject.GetPhotonView().owner, health, maxHealth, notoriety, damageStone, damagePaper, damageScissor, inFight, movementSpeed);
                
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
    private void RPC_UpdateStats(PhotonPlayer unPhotonPlayer, float health, float maxHealth, float notoriety, float damageStone, float damagePaper, float damageScissor, int inFight, float movementSpeed)
    {

        int count = 0;

        foreach (var playerGameObject in GameObject.FindGameObjectsWithTag("Joueur"))
        {
            
            if (playerGameObject.GetPhotonView().owner ==  unPhotonPlayer)
            {

                playerGameObject.GetComponent<PlayerMovement>().health = health;
                playerGameObject.GetComponent<PlayerMovement>().maxHealth = maxHealth;
                playerGameObject.GetComponent<PlayerMovement>().notoriety = notoriety;
                playerGameObject.GetComponent<PlayerMovement>().damageStone = damageStone;
                playerGameObject.GetComponent<PlayerMovement>().damagePaper = damagePaper;
                playerGameObject.GetComponent<PlayerMovement>().damageScissor = damageScissor;
                playerGameObject.GetComponent<PlayerMovement>().inFight = inFight;
                playerGameObject.GetComponent<PlayerMovement>().movementSpeed = movementSpeed;
                
                UnityEngine.Debug.Log("le joueur " + unPhotonPlayer.ID + " est présent");
                

            }

            
        }

    }

}
