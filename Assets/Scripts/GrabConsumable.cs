﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabConsumable : MonoBehaviour
{
	[SerializeField]
	private Inventory Inventory_script;
	
	private PhotonView photonView;
	
	private void Awake()
	{
		photonView = GetComponent<PhotonView>();
	}

    // Start is called before the first frame update

    // dès qu'il y a un collider
    void OnTriggerEnter(Collider col)
    {
	    
	    if (PhotonNetwork.isMasterClient)
	    {
		    switch(col.gameObject.tag){

			    //en fonction des comsommables et de leur tags cela met a jour le nb d'item et detruit l'objet 
			    //"case" a faire pour chaque consommable
			    case "Slot": //apple
				    photonView.RPC("RPC_AddInInventory",PhotonTargets.MasterClient, photonView.owner, 0);
				    
				    photonView.RPC("RPC_returnToPool",PhotonTargets.All, col.GetComponent<ItemExposerScript>().GetId());
				    
				    break;
        	
		    }
	    }
		
        
    }
    
    [PunRPC]
    private void RPC_AskAddInInventory(PhotonPlayer unPhotonPlayer, int slot)
    {
	    
		    photonView.RPC("RPC_AddInInventory",PhotonTargets.MasterClient, unPhotonPlayer, slot);
		    Debug.Log("GrabConsumable : rpc au serveur envoyé, demande d'add à l'inventaire.");

    }
    
    [PunRPC]
    private void RPC_AddInInventory(PhotonPlayer unPhotonPlayer, int slot)
    {
	    
	    
		    int index = PlayerManagement.Instance.listeInfoJoueurs.FindIndex(x => x.photonPlayerJoueur == unPhotonPlayer);
		    
		    Debug.Log("RPC_AddInInventory : index du joueur dans la liste : " + index);


		    PlayerManagement.Instance.listeInfoJoueurs[index].tabSlots[slot] += 1;


    }
    
    [PunRPC]
    private void RPC_returnToPool(int id)
    {
	    

	    int i = 0;
	    int flag = 0;

	    while (i <  GameObject.FindGameObjectsWithTag("Slot").Length && flag!=1)
	    {
		    
		    if (GameObject.FindGameObjectsWithTag("Slot")[i].GetComponent<ItemExposerScript>().GetId() == id)
		    {
			    ItemGeneratorScript.instance.destroyApple(GameObject.FindGameObjectsWithTag("Slot")[i]
				    .GetComponent<ItemExposerScript>());
			    
			    Debug.Log("on trouve la pomme à détruire");

			    flag = 1;
			    

		    }

		    i = i + 1;
		    
	    }


    }
}

