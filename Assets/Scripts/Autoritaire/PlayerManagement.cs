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
            listeInfoJoueurs.Add(new PlayerStats(unPhotonPlayer, 100, 100, 50, 50));
        }
    }






//Cette classe sera le type utilisé pour stocker nos informations de joueurs dans le serveur
//On aura une liste de PlayerStats
   
}

public class PlayerStats
{

    public readonly PhotonPlayer photonPlayerJoueur;
    public int health;
    public int maxHealth;
    public int energy;
    public int maxEnergy;

    public int[] tabSlots;


    public PlayerStats(PhotonPlayer unPhotonPLayer, int vieInitiale, int maxHealthInitiale, int energieInitiale,
        int maxEnergieInitiale)
    {

        photonPlayerJoueur = unPhotonPLayer;
        health = vieInitiale;
        maxHealth = maxHealthInitiale;
        energy = energieInitiale;
        maxEnergy = maxEnergieInitiale;
        tabSlots = tabSlots = new int[6];


    }
}