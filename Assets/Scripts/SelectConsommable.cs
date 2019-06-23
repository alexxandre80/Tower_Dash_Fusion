using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectConsommable : MonoBehaviour
{

	Inventory Inventory_script;


	void Start(){
		
		Inventory_script = GameObject.Find("Inventory").GetComponent<Inventory>();

	}
    
    public void SelectionConso()
    {
        //nr slot
        int nrSlot = transform.parent.GetSiblingIndex();
        
        //nb d'item ne peut descendre en dessous de 0
        if(Inventory_script.tabSlots[nrSlot]>0){
        	
        	//decremente 1
        	Inventory_script.tabSlots[nrSlot] -= 1;
        	Inventory_script.UpdateTXT(nrSlot, Inventory_script.tabSlots[nrSlot].ToString());
    		
        	switch(nrSlot){

        		case 0: //apple
        		GameObject.Find("Health Canvas").GetComponent<Energy>().energy += 5;
        		break;

        		case 1: //banana
        		GameObject.Find("Health Canvas").GetComponent<Energy>().energy += 10;
        		break;

        		case 2: //greenpotion
        		GameObject.Find("Health Canvas").GetComponent<Energy>().energy += 50;
        		break;

        		case 3: //orangepotion
        		GameObject.Find("Health Canvas").GetComponent<Energy>().energy += 15;
        		break;

        		case 4: //redpotion
        		GameObject.Find("Health Canvas").GetComponent<Energy>().energy -= 50;
        		break;

        		//case 0: //supergun
        		
        		//break;
        	}

    	}

    }

}
