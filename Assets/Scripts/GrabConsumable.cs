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
	private int index;
	
	
	
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

				    index = PlayerManagement.Instance.listeInfoJoueurs.FindIndex(x => x.photonPlayerJoueur == photonView.owner);
					PlayerManagement.Instance.listeInfoJoueurs[index].health += 30;
				    
				break;
			    
			    case "bullet": //bullets

					index = PlayerManagement.Instance.listeInfoJoueurs.FindIndex(x => x.photonPlayerJoueur == photonView.owner);
					PlayerManagement.Instance.listeInfoJoueurs[index].health -= 10;

				    if (PlayerManagement.Instance.listeInfoJoueurs[index].health <= 0)
				    {
					    destruction();
				    }

				break;
				

				case "banana": //banana

					photonView.RPC("RPC_AddInInventory",PhotonTargets.MasterClient, photonView.owner, 1);
				    
				    photonView.RPC("RPC_returnToPool",PhotonTargets.All, col.GetComponent<ItemExposerScript>().GetId(), "banana");

	                //Inventory_script.tabSlots[1] = Inventory_script.tabSlots[1] + 1; 
	                //Inventory_script.UpdateTXT(1,Inventory_script.tabSlots[1].ToString());
	                //Destroy (col.gameObject);

				    index = PlayerManagement.Instance.listeInfoJoueurs.FindIndex(x => x.photonPlayerJoueur == photonView.owner);
					PlayerManagement.Instance.listeInfoJoueurs[index].health += 5;

	            break;


	            case "greenP": //greenPotion

	            	photonView.RPC("RPC_AddInInventory",PhotonTargets.MasterClient, photonView.owner, 2);
				    
				    photonView.RPC("RPC_returnToPool",PhotonTargets.All, col.GetComponent<ItemExposerScript>().GetId(), "greenP");

				    index = PlayerManagement.Instance.listeInfoJoueurs.FindIndex(x => x.photonPlayerJoueur == photonView.owner);
					PlayerManagement.Instance.listeInfoJoueurs[index].health -= 30;
	            break;

	            case "orangeP": //orangePotion

	            	photonView.RPC("RPC_AddInInventory",PhotonTargets.MasterClient, photonView.owner, 3);
				    
				    photonView.RPC("RPC_returnToPool",PhotonTargets.All, col.GetComponent<ItemExposerScript>().GetId(), "orangeP");

				    index = PlayerManagement.Instance.listeInfoJoueurs.FindIndex(x => x.photonPlayerJoueur == photonView.owner);
					PlayerManagement.Instance.listeInfoJoueurs[index].health += 15;

	            break;

	            case "redP": //redPotion

	            	photonView.RPC("RPC_AddInInventory",PhotonTargets.MasterClient, photonView.owner, 4);
				    
				    photonView.RPC("RPC_returnToPool",PhotonTargets.All, col.GetComponent<ItemExposerScript>().GetId(), "redP");

				    index = PlayerManagement.Instance.listeInfoJoueurs.FindIndex(x => x.photonPlayerJoueur == photonView.owner);
					PlayerManagement.Instance.listeInfoJoueurs[index].health += 30;

	            break;

	            case "superGun": //superGun

	            	photonView.RPC("RPC_AddInInventory",PhotonTargets.MasterClient, photonView.owner, 5);
				    
				    photonView.RPC("RPC_returnToPool",PhotonTargets.All, col.GetComponent<ItemExposerScript>().GetId(), "superGun");

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
}

