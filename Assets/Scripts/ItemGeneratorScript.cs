using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.SceneManagement;


public class ItemGeneratorScript : MonoBehaviour
{
    public static ItemGeneratorScript instance;
    private int flag = 0;
    [Header ("Références")]
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject bananaPrefab;
    [SerializeField] private PhotonView photonView;

    [Header("Pooling des items")]
    private ItemExposerScript[] AppleInstanciees;
    private Queue<ItemExposerScript> appleLibres = new Queue<ItemExposerScript>(5);
    private List<ItemExposerScript> applePlacees = new List<ItemExposerScript>(5);

    private ItemExposerScript[] BananaInstanciees;
    private Queue<ItemExposerScript> bananaLibres = new Queue<ItemExposerScript>(5);
    private List<ItemExposerScript> bananaPlacees = new List<ItemExposerScript>(5);


    //génération du pooling des items
    public void Awake()
    {
        instance = this;
        GameObject apple;
        GameObject banana;

        //instancier toutes les armes possibles au cours d'une partie
        //pour les armes rouges, il n'y en a qu'une par biome donc 5 armes rouges possibles
        AppleInstanciees = new ItemExposerScript[5];
        BananaInstanciees = new ItemExposerScript[5];

        //génération des 5 armes rouges
        for(int i = 0; i < 5; i++)
        {
            apple = (GameObject)Instantiate(applePrefab);
			apple.GetComponent<ItemExposerScript>().SetId(i);
            AppleInstanciees[i] = apple.GetComponent<ItemExposerScript>();
            appleLibres.Enqueue(AppleInstanciees[i]);
        }

        //génération des bananes
        for(int i = 0; i < 5; i++)
        {
            banana = (GameObject)Instantiate(bananaPrefab);
            banana.GetComponent<ItemExposerScript>().SetId(i);
            BananaInstanciees[i] = banana.GetComponent<ItemExposerScript>();
            bananaLibres.Enqueue(BananaInstanciees[i]);
        }
        
        /*if (PhotonNetwork.isMasterClient)
        {
            float x = -19;
            while (appleLibres.Count != 0) {
                    
                x = x + 3;
                Vector3 position = new Vector3(x, -0.9f, -20.0f);
                photonView.RPC("GenererAppleRPC", PhotonTargets.All, position);
                
                Debug.Log("GenererAppleRPC appelé taille de apple item  " + appleLibres.Count);
                    
            }
        }*/
        

    }

    public void Update()
    {
        
        if (PhotonNetwork.isMasterClient && PlayerNetwork.instance.allConnected == 1 && flag == 0)
        {
            float x = -19;
            while (appleLibres.Count != 0) {
                    
                x = x + 3;
                Vector3 position = new Vector3(x, -0.9f, -20.0f);
                photonView.RPC("GenererAppleRPC", PhotonTargets.All, position);
                Debug.Log("GenererAppleRPC appelé taille de apple item  " + appleLibres.Count);
                
                //position = new Vector3(x, -3f, -40.0f);
                //photonView.RPC("GenererBananaRPC", PhotonTargets.All, position);
                //Debug.Log("GenererBananaRPC appelé taille de apple item  " + bananaLibres.Count);
                
                
            }

            
            flag = 1;
        }

        
    }

    private void FixedUpdate()
    {
        /*if (controlleurVaisseauxScript.getGameStarted() && PhotonNetwork.IsMasterClient)
        {
            //vérification du pooling des armes bleues
            for (int i = 0; i < armesBleuesPlacees.Count; i++)
            {
                //si une des armes bleues placées a été ramassée, on la remet dans le pooling
                if (armesBleuesPlacees[i].getRamasse())
                {
                    //remise de l'item dans la queue des armes libres
                    armesBleuesLibres.Enqueue(armesBleuesPlacees[i]);

                    //retrait de l'item de la liste des armes placées
                    armesBleuesPlacees.Remove(armesBleuesPlacees[i]);
                }
            }

            //vérification du pooling des armes vertes
            for (int i = 0; i < armesVertesPlacees.Count; i++)
            {
                //si une des armes vertes placées a été ramassée, on la remet dans le pooling
                if (armesVertesPlacees[i].getRamasse())
                {
                    //remise de l'item dans la queue des armes libres
                    armesVertesLibres.Enqueue(armesVertesPlacees[i]);

                    //retrait de l'item de la liste des armes placées
                    armesVertesPlacees.Remove(armesVertesPlacees[i]);
                }
            }

            //vérification du pooling des armes rouges
            for (int i = 0; i < armesRougesPlacees.Count; i++)
            {
                //si une des armes rouges placées a été ramassée, on la remet dans le pooling
                if (armesRougesPlacees[i].getRamasse())
                {
                    //remise de l'item dans la queue des armes libres
                    armesRougesLibres.Enqueue(armesRougesPlacees[i]);

                    //retrait de l'item de la liste des armes placées
                    armesRougesPlacees.Remove(armesRougesPlacees[i]);
                }
            }
        }*/
    }


    public void GenererBanana(Vector3 position)
    {
        if(PhotonNetwork.isMasterClient)
        {
            photonView.RPC("GenererBananaRPC", PhotonTargets.All, position);
        }
    }

    [PunRPC]
    private void GenererBananaRPC(Vector3 position)
    {
        //faire spawn une arme verte
        var banana = bananaLibres.Dequeue();

        //activation de l'item
        banana.ActivationItem();
        banana.SetPosition(position);

        //ajout de l'item dans la liste des items placés
        bananaPlacees.Add(banana);
        
        Debug.Log("la banane est placée");
    }
    
    public void destroyBanana(ItemExposerScript uneBanana)
    {
        //faire spawn une arme verte
        bananaLibres.Enqueue(uneBanana);

        //activation de l'item
        uneBanana.DesactivationItem();
        

        //ajout de l'item dans la liste des items placés
        bananaPlacees.Remove(uneBanana);
        
        Debug.Log("la banane est détruite");
    }
    
    




    public void GenererApple(Vector3 position)
    {
        if(PhotonNetwork.isMasterClient)
        {
            photonView.RPC("GenererAppleRPC", PhotonTargets.All, position);
        }
    }

    [PunRPC]
    private void GenererAppleRPC(Vector3 position)
    {
        //faire spawn une arme verte
        var apple = appleLibres.Dequeue();

        //activation de l'item
        apple.ActivationItem();
        apple.SetPosition(position);

        //ajout de l'item dans la liste des items placés
        applePlacees.Add(apple);
        
        Debug.Log("la pomme est placée");
    }
    
    public void destroyApple(ItemExposerScript uneApple)
    {
        //faire spawn une arme verte
        appleLibres.Enqueue(uneApple);

        //activation de l'item
        uneApple.DesactivationItem();
        

        //ajout de l'item dans la liste des items placés
        applePlacees.Remove(uneApple);
        
        Debug.Log("la pomme est détruite");
    }
    
}
