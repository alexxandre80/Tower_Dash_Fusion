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
    [SerializeField] private GameObject greenPotionPrefab;
    [SerializeField] private GameObject orangePotionPrefab;
    [SerializeField] private GameObject redPotionPrefab;
    [SerializeField] private GameObject superGunPrefab;
    [SerializeField] private PhotonView photonView;

    [Header("Pooling des items")]
    private ItemExposerScript[] AppleInstanciees;
    private Queue<ItemExposerScript> appleLibres = new Queue<ItemExposerScript>(5);
    private List<ItemExposerScript> applePlacees = new List<ItemExposerScript>(5);

    private ItemExposerScript[] BananaInstanciees;
    private Queue<ItemExposerScript> bananaLibres = new Queue<ItemExposerScript>(5);
    private List<ItemExposerScript> bananaPlacees = new List<ItemExposerScript>(5);

    private ItemExposerScript[] GreenPotionInstanciees;
    private Queue<ItemExposerScript> greenPotionLibres = new Queue<ItemExposerScript>(5);
    private List<ItemExposerScript> greenPotionPlacees = new List<ItemExposerScript>(5);

    private ItemExposerScript[] OrangePotionInstanciees;
    private Queue<ItemExposerScript> orangePotionLibres = new Queue<ItemExposerScript>(5);
    private List<ItemExposerScript> orangePotionPlacees = new List<ItemExposerScript>(5);

    private ItemExposerScript[] RedPotionInstanciees;
    private Queue<ItemExposerScript> redPotionLibres = new Queue<ItemExposerScript>(5);
    private List<ItemExposerScript> redPotionPlacees = new List<ItemExposerScript>(5);

    private ItemExposerScript[] SuperGunInstanciees;
    private Queue<ItemExposerScript> superGunLibres = new Queue<ItemExposerScript>(5);
    private List<ItemExposerScript> superGunPlacees = new List<ItemExposerScript>(5);


    //génération du pooling des items
    public void Awake()
    {
        instance = this;
        GameObject apple;
        GameObject banana;
        GameObject greenP;
        GameObject orangeP;
        GameObject redP;
        GameObject superGun;

        //instancier toutes les armes possibles au cours d'une partie
        //pour les armes rouges, il n'y en a qu'une par biome donc 5 armes rouges possibles
        AppleInstanciees = new ItemExposerScript[5];
        BananaInstanciees = new ItemExposerScript[5];
        GreenPotionInstanciees = new ItemExposerScript[5];
        OrangePotionInstanciees = new ItemExposerScript[5];
        RedPotionInstanciees = new ItemExposerScript[5];
        SuperGunInstanciees = new ItemExposerScript[5];

        //génération des 5 pommes rouges
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


        //génération des p. vertes
        for(int i = 0; i < 5; i++)
        {
            greenP = (GameObject)Instantiate(greenPotionPrefab);
            greenP.GetComponent<ItemExposerScript>().SetId(i);
            GreenPotionInstanciees[i] = greenP.GetComponent<ItemExposerScript>();
            greenPotionLibres.Enqueue(GreenPotionInstanciees[i]);
        }
        
        //génération des p. oranges
        for(int i = 0; i < 5; i++)
        {
            orangeP = (GameObject)Instantiate(orangePotionPrefab);
            orangeP.GetComponent<ItemExposerScript>().SetId(i);
            OrangePotionInstanciees[i] = orangeP.GetComponent<ItemExposerScript>();
            orangePotionLibres.Enqueue(OrangePotionInstanciees[i]);
        }


        //génération des p. rouges
        for(int i = 0; i < 5; i++)
        {
            redP = (GameObject)Instantiate(redPotionPrefab);
            redP.GetComponent<ItemExposerScript>().SetId(i);
            RedPotionInstanciees[i] = redP.GetComponent<ItemExposerScript>();
            redPotionLibres.Enqueue(RedPotionInstanciees[i]);
        }

        //génération du supergun
        for(int i = 0; i < 5; i++)
        {
            superGun = (GameObject)Instantiate(superGunPrefab);
            superGun.GetComponent<ItemExposerScript>().SetId(i);
            SuperGunInstanciees[i] = superGun.GetComponent<ItemExposerScript>();
            superGunLibres.Enqueue(SuperGunInstanciees[i]);
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


                foreach (GameObject pommeSpawner in GameObject.FindGameObjectsWithTag("PommeSpawner"))
                {
                    Vector3 position = pommeSpawner.transform.position;
                    photonView.RPC("GenererAppleRPC", PhotonTargets.All, position);
                    Debug.Log("GenererAppleRPC appelé taille de apple item  " + appleLibres.Count);
                }
                
                
            }

            x = 0 ;
            while (bananaLibres.Count != 0) {

                x = x + 3;
                Vector3 position = new Vector3(x, -0.2f, -20.0f);
                photonView.RPC("GenererBananaRPC", PhotonTargets.All, position);
                Debug.Log("GenererBananaRPC appelé taille de banana item  " + bananaLibres.Count);

            }


            x = 0 ;
            while (greenPotionLibres.Count != 0) {

                x = x + 3;
                Vector3 position = new Vector3(x, -0.3f, -20.2f);
                photonView.RPC("GenererGreenPRPC", PhotonTargets.All, position);
                Debug.Log("GenererGreenPRPC appelé taille de p. green item  " + greenPotionLibres.Count);

            }



            x = 0 ;
            while (orangePotionLibres.Count != 0) {

                x = x + 3;
                Vector3 position = new Vector3(x, -0.5f, -21.0f);
                photonView.RPC("GenererOrangePRPC", PhotonTargets.All, position);
                Debug.Log("GenererOrangePRPC appelé taille de p. orange item  " + orangePotionLibres.Count);

            }

            x = 0 ;
            while (redPotionLibres.Count != 0) {

                x = x + 3;
                Vector3 position = new Vector3(x, -0.4f, -21.5f);
                photonView.RPC("GenererRedPRPC", PhotonTargets.All, position);
                Debug.Log("GenererRedPRPC appelé taille de p. red item  " + redPotionLibres.Count);

            }

            x = 0 ;
            while (superGunLibres.Count != 0) {

                x = x + 3;
                Vector3 position = new Vector3(x, -0.7f, -22.0f);
                photonView.RPC("GenererSuperGunRPC", PhotonTargets.All, position);
                Debug.Log("GenererSuperGunRPC appelé taille de supergun item  " + superGunLibres.Count);

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




    public void GenererGreenP(Vector3 position)
    {
        if(PhotonNetwork.isMasterClient)
        {
            photonView.RPC("GenererGreenPRPC", PhotonTargets.All, position);
        }
    }

    [PunRPC]
    private void GenererGreenPRPC(Vector3 position)
    {
        //faire spawn une arme verte
        var greenP = greenPotionLibres.Dequeue();

        //activation de l'item
        greenP.ActivationItem();
        greenP.SetPosition(position);

        //ajout de l'item dans la liste des items placés
        greenPotionPlacees.Add(greenP);
        
        Debug.Log("la p. verte est placée");
    }
    
    public void destroyGreenP(ItemExposerScript uneGreenP)
    {
        //faire spawn une arme verte
        greenPotionLibres.Enqueue(uneGreenP);

        //activation de l'item
        uneGreenP.DesactivationItem();
        

        //ajout de l'item dans la liste des items placés
        greenPotionPlacees.Remove(uneGreenP);
        
        Debug.Log("la p.verte est détruite");
    }

    
    public void GenererOrangeP(Vector3 position)
    {
        if(PhotonNetwork.isMasterClient)
        {
            photonView.RPC("GenererOrangePRPC", PhotonTargets.All, position);
        }
    }

    [PunRPC]
    private void GenererOrangePRPC(Vector3 position)
    {
        //faire spawn une arme verte
        var orangeP = orangePotionLibres.Dequeue();

        //activation de l'item
        orangeP.ActivationItem();
        orangeP.SetPosition(position);

        //ajout de l'item dans la liste des items placés
        orangePotionPlacees.Add(orangeP);
        
        Debug.Log("la p. orange est placée");
    }
    
    public void destroyOrangeP(ItemExposerScript uneOrangeP)
    {
        //faire spawn une arme verte
        orangePotionLibres.Enqueue(uneOrangeP);

        //activation de l'item
        uneOrangeP.DesactivationItem();
        

        //ajout de l'item dans la liste des items placés
        orangePotionPlacees.Remove(uneOrangeP);
        
        Debug.Log("la p. orange est détruite");
    }


    public void GenererRedP(Vector3 position)
    {
        if(PhotonNetwork.isMasterClient)
        {
            photonView.RPC("GenererRedPRPC", PhotonTargets.All, position);
        }
    }

    [PunRPC]
    private void GenererRedPRPC(Vector3 position)
    {
        //faire spawn une arme verte
        var redP = redPotionLibres.Dequeue();

        //activation de l'item
        redP.ActivationItem();
        redP.SetPosition(position);

        //ajout de l'item dans la liste des items placés
        redPotionPlacees.Add(redP);
        
        Debug.Log("la p. red est placée");
    }
    
    public void destroyRedP(ItemExposerScript uneRedP)
    {
        //faire spawn une arme verte
        redPotionLibres.Enqueue(uneRedP);

        //activation de l'item
        uneRedP.DesactivationItem();
        

        //ajout de l'item dans la liste des items placés
        redPotionPlacees.Remove(uneRedP);
        
        Debug.Log("la p. red est détruite");
    }


    public void GenererSuperGun(Vector3 position)
    {
        if(PhotonNetwork.isMasterClient)
        {
            photonView.RPC("GenererSuperGunRPC", PhotonTargets.All, position);
        }
    }

    [PunRPC]
    private void GenererSuperGunRPC(Vector3 position)
    {
        //faire spawn une arme verte
        var superGun = superGunLibres.Dequeue();

        //activation de l'item
        superGun.ActivationItem();
        superGun.SetPosition(position);

        //ajout de l'item dans la liste des items placés
        superGunPlacees.Add(superGun);
        
        Debug.Log("le supergun est placé");
    }
    
    public void destroySupergun(ItemExposerScript unSuperGun)
    {
        //faire spawn une arme verte
        superGunLibres.Enqueue(unSuperGun);

        //activation de l'item
        unSuperGun.DesactivationItem();
        

        //ajout de l'item dans la liste des items placés
        superGunPlacees.Remove(unSuperGun);
        
        Debug.Log("le supergun est détruit");
    }
    
    
}
