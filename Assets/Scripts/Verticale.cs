using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verticale : MonoBehaviour
{
    [SerializeField]
    private Transform plateformeT;
    
    [SerializeField]
    private float moveSpeed;
    
    [SerializeField]
    private float hauteurMax;
    
    [SerializeField]
    private float hauteurMin;
    
    [SerializeField]
    private PhotonView photonView;

    private Boolean drap = false;

    
    

    // Update is called once per frame
    
    
    
    void FixedUpdate()
    {

        if (PhotonNetwork.isMasterClient)
        {
            if (plateformeT.position.y >= hauteurMax)
            {
                
                Debug.Log("position y >= hauteur max " + plateformeT.position.y);

                drap = false;
            

            }
            
            if (plateformeT.position.y <= hauteurMin)
            {

                drap = true;
            
            }

            if (drap)
            {
                photonView.RPC("RPC_up", PhotonTargets.All);
            }

            else
            {
                photonView.RPC("RPC_down", PhotonTargets.All);
            }
            
        }
        
        
        
        
        
        
        
    }
    
    
    [PunRPC]
    private void RPC_up()
    {
        Debug.Log("elle monte");
        plateformeT.position += Vector3.up * Time.deltaTime * moveSpeed;

    }
    
    [PunRPC]
    private void RPC_down()
    {
        
        Debug.Log("elle descend");
        
        plateformeT.position += Vector3.down * Time.deltaTime * moveSpeed;

    }
    
}
