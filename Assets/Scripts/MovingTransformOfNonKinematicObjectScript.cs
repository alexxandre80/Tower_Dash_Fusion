using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MovingTransformOfNonKinematicObjectScript : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObject;
    
    //type de variable propre à photon, il va servire à corriger un bug qui fait que quand on déplace un personnage, on les déplace tous
    // Elle permet d'associer au script un seul joueur en gros
    PhotonView view;

    private bool wantToMoveLeft;

    private bool wantToMoveRight;
    
    private bool wantToMoveForward;
    
    private bool wantToMoveBack;


    void Start()
    {
        // On récupere le composant photon view propre à notre joueur actuel
        //On va faire des test dessus, si le joueur possède ma vue alors je le déplace, sinon on ne fait rien
        //Ainsi, seul le joueur dont ce script possède la photon view sera déplacé et le problème est réglé
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(view.isMine)
                wantToMoveLeft = true;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            if(view.isMine)
                wantToMoveLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if(view.isMine)
                wantToMoveRight = true;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            if(view.isMine)
                wantToMoveRight = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(view.isMine)
                wantToMoveForward = true;
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            if(view.isMine)
                wantToMoveForward = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if(view.isMine)
                wantToMoveBack = true;
        }    

        if (Input.GetKeyUp(KeyCode.S))
        {
            if(view.isMine)
                wantToMoveBack = false;
        }
        
        /*if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            
            if(view.isMine)
                if (Inventory2.inventory2.items[0] != null)
                {
                    Inventory2.inventory2.items[0].Use();
                }
            
        }
        
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            if(view.isMine)
                if (Inventory2.inventory2.items[1] != null)
                {
                    Inventory2.inventory2.items[1].Use();
                }
            
        }
        
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            if(view.isMine)
                if (Inventory2.inventory2.items[2] != null)
                {
                    Inventory2.inventory2.items[2].Use();
                }
        }
        
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            if(view.isMine)
                if (Inventory2.inventory2.items[3] != null)
                {
                    Inventory2.inventory2.items[3].Use();
                }
        }*/
        
        
		
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (wantToMoveLeft)
        {
            playerGameObject.transform.position += Vector3.left * 5f * Time.deltaTime;
        }

        if (wantToMoveRight)
        {
            playerGameObject.transform.position += Vector3.right * 5f * Time.deltaTime;
        }
        
        if (wantToMoveForward)
        {
            playerGameObject.transform.position += Vector3.forward * 5f * Time.deltaTime;
        }
        
        if (wantToMoveBack)
        {
            playerGameObject.transform.position += Vector3.back * 5f * Time.deltaTime;
        }
        
		
    }
}
