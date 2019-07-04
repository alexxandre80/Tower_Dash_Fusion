using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horizontal : MonoBehaviour
{
    [SerializeField]
    private Transform plateformeT;
    
    [SerializeField]
    private float moveSpeed;
    
    [SerializeField]
    private double rightMax;
    
    [SerializeField]
    private double leftMax;
    
    [SerializeField]
    private PhotonView photonView;

    private Boolean drap = false;

    
    

    // Update is called once per frame
    
    
    
    void FixedUpdate()
    {

        if (PhotonNetwork.isMasterClient)
        {
            if (plateformeT.position.x >= rightMax)
            {
                

                drap = false;
            

            }
            
            if (plateformeT.position.x <= leftMax)
            {

                drap = true;
            
            }

            if (drap)
            {
                photonView.RPC("RPC_right", PhotonTargets.All);
            }

            else
            {
                photonView.RPC("RPC_left", PhotonTargets.All);
            }
            
        }
        
        
        
        
        
        
        
    }
    
    
    [PunRPC]
    private void RPC_left()
    {
        Debug.Log("elle va à gauche");
        plateformeT.position += Vector3.left * Time.deltaTime * moveSpeed;

    }
    
    [PunRPC]
    private void RPC_right()
    {
        
        Debug.Log("elle va à droite");
        
        plateformeT.position += Vector3.right * Time.deltaTime * moveSpeed;

    }
}
