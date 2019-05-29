using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Debug = System.Diagnostics.Debug;


public class PlayerManagement : MonoBehaviour
{
    //On fait de cette classe un singleton 
    public static PlayerManagement Instance;
    private PhotonView serverPhotonView;
    
    // La fameuse liste qui va contenir toutes nos info de joueur dans le serveur
    private List<PlayerStats> _listeInfoJoueurs = new List<PlayerStats>();
    
    private PhotonView photonView;

    public List<PlayerStats> listeInfoJoueurs
    {
        get => _listeInfoJoueurs;
        private set => _listeInfoJoueurs = value;
    }


    private void Awake()
    {
        Instance = this;
        serverPhotonView = GetComponent<PhotonView>();

        photonView = GetComponent<PhotonView>();
    }

    public void addPlayerStats(PhotonPlayer unPhotonPlayer)
    {
        int index = listeInfoJoueurs.FindIndex(x => x.photonPlayerJoueur == unPhotonPlayer);
        if (index == -1)
        {
            listeInfoJoueurs.Add(new PlayerStats(unPhotonPlayer, 100, 100, 30, 30, 30 ,1f));
        }
    }

    // Fonction qui sera utilisée pour tout les objets qui vont altérer les dégats de pierre d'un joueur en particulier
    public void modifyStoneDamage(PhotonPlayer unPhotonPlayer, int value)
    {
        PlayerNetwork.instance.newStoneDamage(unPhotonPlayer, value);
    }
}



//Cette classe sera le type utilisé pour stocker nos informations de joueurs dans le serveur
//On aura une liste de PlayerStats
public class PlayerStats
{
    
    public readonly PhotonPlayer photonPlayerJoueur;
    public float health;
    public float maxHealth;
    public float notoriety;
    public float damageStone;
    public float damagePaper;
    public float damageScissor;
    public int inFight;
    public float movementSpeed;
    
    
    public PlayerStats(PhotonPlayer unPhotonPLayer, float vieInitiale, float maxHealthInitiale, float damagePaperInitiale, float damageScissorInitiale, float damageStoneInitiale, float movementspeedInitiale )
    {

        photonPlayerJoueur = unPhotonPLayer;
        health = vieInitiale;
        maxHealth = maxHealthInitiale;
        notoriety = 0;
        damageStone = damageStoneInitiale;
        damagePaper = damagePaperInitiale;
        damageScissor = damageScissorInitiale;
        inFight = 0;
        movementSpeed = movementspeedInitiale;


    }
}