using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabConsumable : MonoBehaviour
{
	[SerializeField]
	private Inventory Inventory_script;
	
	[SerializeField]
	private GameObject quitMenu;
	
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
			    case "apple": //apple
				    photonView.RPC("RPC_AddInInventory",PhotonTargets.MasterClient, photonView.owner, 0);
				    
				    photonView.RPC("RPC_returnToPool",PhotonTargets.All, col.GetComponent<ItemExposerScript>().GetId(), "apple");
				    if (DataCollector.Instance().isActivated())
				    {
					    DataCollector.RegisterObjectPickedUp(col.gameObject.transform.position,  Time.timeSinceLevelLoad, col.gameObject.tag, photonView.OwnerActorNr);
					    photonView.RPC("RPC_AddDataObject",PhotonTargets.Others, col.gameObject.transform.position,  Time.timeSinceLevelLoad, col.gameObject.tag, photonView.OwnerActorNr);
				    }
				break;
			    
			    case "bullet": //bullets

					int index = PlayerManagement.Instance.listeInfoJoueurs.FindIndex(x => x.photonPlayerJoueur == photonView.owner);
					PlayerManagement.Instance.listeInfoJoueurs[index].health -= 10;

				    if (PlayerManagement.Instance.listeInfoJoueurs[index].health <= 0)
				    {
					    if (DataCollector.Instance().isActivated()) {
						    DataCollector.RegisterDeath(transform.position, Time.timeSinceLevelLoad,
						    photonView.OwnerActorNr, "shot");
						    photonView.RPC("RPC_AddDataDeath",PhotonTargets.Others, transform.position, Time.timeSinceLevelLoad,
							    photonView.OwnerActorNr, "shot");
					    }
					    
					    destruction();
				    }

				break;
				

				case "banana": //banana

					photonView.RPC("RPC_AddInInventory",PhotonTargets.MasterClient, photonView.owner, 1);
				    
				    photonView.RPC("RPC_returnToPool",PhotonTargets.All, col.GetComponent<ItemExposerScript>().GetId(), "banana");
					
					// Collecte de données sur le ramassage d'objet
					if (DataCollector.Instance().isActivated())
						if (DataCollector.Instance().isActivated())
						{
							DataCollector.RegisterObjectPickedUp(col.gameObject.transform.position,  Time.timeSinceLevelLoad, col.gameObject.tag, photonView.OwnerActorNr);
							photonView.RPC("RPC_AddDataObject",PhotonTargets.Others, col.gameObject.transform.position,  Time.timeSinceLevelLoad, col.gameObject.tag, photonView.OwnerActorNr);
						}
					
	                
	            break;


	            case "greenP": //greenPotion

	            	photonView.RPC("RPC_AddInInventory",PhotonTargets.MasterClient, photonView.owner, 2);
				    
				    
	                photonView.RPC("RPC_returnToPool",PhotonTargets.All, col.GetComponent<ItemExposerScript>().GetId(), "greenP");
					
	                if (DataCollector.Instance().isActivated())
	                {
		                DataCollector.RegisterObjectPickedUp(col.gameObject.transform.position,  Time.timeSinceLevelLoad, col.gameObject.tag, photonView.OwnerActorNr);
		                photonView.RPC("RPC_AddDataObject",PhotonTargets.Others, col.gameObject.transform.position,  Time.timeSinceLevelLoad, col.gameObject.tag, photonView.OwnerActorNr);
	                }
	            break;
	            

	            case "orangeP": //orangePotion

	            	photonView.RPC("RPC_AddInInventory",PhotonTargets.MasterClient, photonView.owner, 3);
				    
				    photonView.RPC("RPC_returnToPool",PhotonTargets.All, col.GetComponent<ItemExposerScript>().GetId(), "orangeP");
					
	                if (DataCollector.Instance().isActivated())
	                {
		                DataCollector.RegisterObjectPickedUp(col.gameObject.transform.position,  Time.timeSinceLevelLoad, col.gameObject.tag, photonView.OwnerActorNr);
		                photonView.RPC("RPC_AddDataObject",PhotonTargets.Others, col.gameObject.transform.position,  Time.timeSinceLevelLoad, col.gameObject.tag, photonView.OwnerActorNr);
	                }
	            break;

	            case "redP": //redPotion

	            	photonView.RPC("RPC_AddInInventory",PhotonTargets.MasterClient, photonView.owner, 4);
				    
				    photonView.RPC("RPC_returnToPool",PhotonTargets.All, col.GetComponent<ItemExposerScript>().GetId(), "redP");
					
	                if (DataCollector.Instance().isActivated())
	                {
		                DataCollector.RegisterObjectPickedUp(col.gameObject.transform.position,  Time.timeSinceLevelLoad, col.gameObject.tag, photonView.OwnerActorNr);
		                photonView.RPC("RPC_AddDataObject",PhotonTargets.Others, col.gameObject.transform.position,  Time.timeSinceLevelLoad, col.gameObject.tag, photonView.OwnerActorNr);
	                }
	            break;

	            case "superGun": //superGun

	            	photonView.RPC("RPC_AddInInventory",PhotonTargets.MasterClient, photonView.owner, 5);
				    
				    photonView.RPC("RPC_returnToPool",PhotonTargets.All, col.GetComponent<ItemExposerScript>().GetId(), "superGun");
					
	                if (DataCollector.Instance().isActivated())
	                {
		                DataCollector.RegisterObjectPickedUp(col.gameObject.transform.position,  Time.timeSinceLevelLoad, col.gameObject.tag, photonView.OwnerActorNr);
		                photonView.RPC("RPC_AddDataObject",PhotonTargets.Others, col.gameObject.transform.position,  Time.timeSinceLevelLoad, col.gameObject.tag, photonView.OwnerActorNr);
	                }
	            
	            break;


        	
		    }
	    }
		
        
    }

    public void destruction()
    {
	    photonView.RPC("RPC_Destruction",PhotonTargets.All);
    }
    
    [PunRPC]
    private void RPC_Destruction()
    {
	    Debug.Log("un joueur a été détruit");
	    
	    int index = PlayerManagement.Instance.listeInfoJoueurs.FindIndex(x => x.photonPlayerJoueur == photonView.owner);

	    PlayerManagement.Instance.listeInfoJoueurs.Remove(PlayerManagement.Instance.listeInfoJoueurs[index]);

	    
	    PlayerNetwork.instance.setnbJoueur(PlayerNetwork.instance.getnbJoueur() - 1);
	    if (PhotonNetwork.player == photonView.owner)
	    {
		    PhotonNetwork.Destroy(gameObject);
		    Debug.Log("taille de la liste de panel :" + GameObject.FindGameObjectsWithTag("TopPanel").Length);
		    quitMenu = GameObject.FindGameObjectsWithTag("TopPanel")[0].GetComponent<EndGameUI>().topPanel;
		    GameObject.FindGameObjectsWithTag("TopPanel")[0].GetComponent<EndGameUI>().text.text = "Vous avez été tué";
		    Debug.Log("on rentre dans le if d'affichage de menu");
		    quitMenu.SetActive(true);
	    }

	    else
	    {
		    
		    
		    if (PlayerManagement.Instance.listeInfoJoueurs.Count == 1)
		    {
			    
			    Debug.Log("On detecte bien qu'il ne reste qu'un joueur et que je n'ai pas été rekt");
			    

				    if (PlayerManagement.Instance.listeInfoJoueurs[0].photonPlayerJoueur == PhotonNetwork.player)
				    {
					    Debug.Log("taille de la liste de panel :" + GameObject.FindGameObjectsWithTag("TopPanel").Length);
					    quitMenu = GameObject.FindGameObjectsWithTag("TopPanel")[0].GetComponent<EndGameUI>().topPanel;
					    GameObject.FindGameObjectsWithTag("TopPanel")[0].GetComponent<EndGameUI>().text.text = "Winner !";
					    Debug.Log("on rentre dans le if d'affichage de menu");
					    quitMenu.SetActive(true);

					    DataCollector.Instance().CreateText();
				    }
			    
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
    private void RPC_returnToPool(int id, string tag)
    {
	    

	    int i = 0;
	    int flag = 0;

	    switch(tag){
	    	case "apple":
			    while (i <  GameObject.FindGameObjectsWithTag(tag).Length && flag!=1)
			    {
				    
				    if (GameObject.FindGameObjectsWithTag(tag)[i].GetComponent<ItemExposerScript>().GetId() == id){

					    ItemGeneratorScript.instance.destroyApple(GameObject.FindGameObjectsWithTag(tag)[i]
						    .GetComponent<ItemExposerScript>());

					    //Debug.Log("on trouve la pomme à détruire");
					    flag = 1;
					    
				    }

				    i = i + 1;
			    }
			break;

			case "banana":
			    while (i <  GameObject.FindGameObjectsWithTag(tag).Length && flag!=1)
			    {
				    
				    if (GameObject.FindGameObjectsWithTag(tag)[i].GetComponent<ItemExposerScript>().GetId() == id){

					    ItemGeneratorScript.instance.destroyBanana(GameObject.FindGameObjectsWithTag(tag)[i]
						    .GetComponent<ItemExposerScript>());

					    flag = 1;
					    
				    }

				    i = i + 1;
			    }
			break;

			case "greenP":
			    while (i <  GameObject.FindGameObjectsWithTag(tag).Length && flag!=1)
			    {
				    
				    if (GameObject.FindGameObjectsWithTag(tag)[i].GetComponent<ItemExposerScript>().GetId() == id){

					    ItemGeneratorScript.instance.destroyGreenP(GameObject.FindGameObjectsWithTag(tag)[i]
						    .GetComponent<ItemExposerScript>());

					    flag = 1;
					    
				    }

				    i = i + 1;
			    }
			break;

			case "orangeP":
			    while (i <  GameObject.FindGameObjectsWithTag(tag).Length && flag!=1)
			    {
				    
				    if (GameObject.FindGameObjectsWithTag(tag)[i].GetComponent<ItemExposerScript>().GetId() == id){

					    ItemGeneratorScript.instance.destroyOrangeP(GameObject.FindGameObjectsWithTag(tag)[i]
						    .GetComponent<ItemExposerScript>());

					    flag = 1;
					    
				    }

				    i = i + 1;
			    }
			break;

			case "redP":
			    while (i <  GameObject.FindGameObjectsWithTag(tag).Length && flag!=1)
			    {
				    
				    if (GameObject.FindGameObjectsWithTag(tag)[i].GetComponent<ItemExposerScript>().GetId() == id){

					    ItemGeneratorScript.instance.destroyRedP(GameObject.FindGameObjectsWithTag(tag)[i]
						    .GetComponent<ItemExposerScript>());

					    flag = 1;
					    
				    }

				    i = i + 1;
			    }
			break;

			case "superGun":
			    while (i <  GameObject.FindGameObjectsWithTag(tag).Length && flag!=1)
			    {
				    
				    if (GameObject.FindGameObjectsWithTag(tag)[i].GetComponent<ItemExposerScript>().GetId() == id){

					    ItemGeneratorScript.instance.destroySupergun(GameObject.FindGameObjectsWithTag(tag)[i]
						    .GetComponent<ItemExposerScript>());

					    flag = 1;
					    
				    }

				    i = i + 1;
			    }
			break;

		}
    }

    [PunRPC]
    private void RPC_AddDataObject(Vector3 pos, float timeStamp, string objectTag, int player)
    {
	    
	    DataCollector.RegisterObjectPickedUp(pos,  timeStamp, objectTag, player);
	    
    }
    
	
	[PunRPC]
    private void RPC_AddDataDeath(Vector3 pos, float timeStamp, int player, string death)
    {
	    
	    DataCollector.RegisterDeath(pos,  timeStamp, player, death);
	    
    }

}

