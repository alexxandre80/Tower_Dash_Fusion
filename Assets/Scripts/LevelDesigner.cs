using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDesigner : MonoBehaviour
{
    [SerializeField] private Text warning;
    
    [SerializeField] private PhotonView photonview;
    void Update()
    {
        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("compteur destruction : " + Time.timeSinceLevelLoad);
            
            
            //phase 1
            if (Time.timeSinceLevelLoad > 20 && Time.timeSinceLevelLoad < 40)
            {

                photonview.RPC("RPC_WarningPlateforme",PhotonTargets.All, "plateforme1", "la zone 1", 40, Time.timeSinceLevelLoad);
            }
            
            if (Time.timeSinceLevelLoad >= 40 && Time.timeSinceLevelLoad < 42)
            {
                photonview.RPC("RPC_DestructionPlateforme",PhotonTargets.All, "plateforme1");
            }
            
            // phase 2
            
            if (Time.timeSinceLevelLoad > 80 && Time.timeSinceLevelLoad < 100)
            {

                photonview.RPC("RPC_WarningPlateforme",PhotonTargets.All, "plateforme2", "la zone 2", 100, Time.timeSinceLevelLoad);
            }
            
            if (Time.timeSinceLevelLoad >= 100)
            {
                photonview.RPC("RPC_DestructionPlateforme",PhotonTargets.All, "plateforme2");
            }
            
            //phase 3
            
            if (Time.timeSinceLevelLoad > 340 && Time.timeSinceLevelLoad < 360)
            {

                photonview.RPC("RPC_WarningPlateforme",PhotonTargets.All, "plateforme3", "la zone finale", 360, Time.timeSinceLevelLoad);
            }
            
            if (Time.timeSinceLevelLoad >= 360)
            {
                photonview.RPC("RPC_DestructionPlateforme",PhotonTargets.All, "plateforme3");
            }
        }
        
    }
    
    
    [PunRPC]
    private void RPC_DestructionPlateforme(String tag) {
    
        foreach (GameObject decor in GameObject.FindGameObjectsWithTag(tag)){

            decor.SetActive(false);
            Debug.Log("Destruction " + tag);
        }

        if (tag == "plateforme3")
        {
            foreach (GameObject decor in GameObject.FindGameObjectsWithTag("Plateforme")){

                decor.SetActive(false);
                Debug.Log("Destruction " + tag);
            }
        }

        warning.text = "";


    }
    
    [PunRPC]
    private void RPC_WarningPlateforme(String tag, string zone, int secDisparition, float timerMasterClient) {

            warning.text = "Attention, les plateformes de " + zone + " disparaissent dans: " + ((int)(secDisparition - timerMasterClient)).ToString();


    }
}

