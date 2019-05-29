using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    
    //L'instance de DameControl qui sera utilisée
    //Elle est statique donc on peut l'utiliser partout dans le code
    //très pratique
    public static GameControl player;

    public float maxHealth;
    public float health;
    public float notoriety;
    public float damageStone;
    public float damagePaper;
    public float damageScissor;
    public int inFight;
    public float movementSpeed;
    
    public GameObject playerModel;
    
    
    void Awake()
    {
        // si "player" n'a pas encore été créé alors "player" = this et il sera conservé de scène en scène
        if (player == null)
        {
            //permet de conserver l'objet quand on passe de chaine en chaines, sans le détruire
            DontDestroyOnLoad(gameObject);
            player = this;
            
        }
        
        //si un objet "player" a déjà été créé avant celui là alors on le conserve et on detruit celui là
        else
        {
            if (player != this)
            {
                Destroy(gameObject);
            }
        }
        
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Joueur")) {
            if(go.GetComponent<PhotonView>().owner == PhotonNetwork.player)
            {
                playerModel = go;
                    
                break;
            }
        }
        
    }

}
