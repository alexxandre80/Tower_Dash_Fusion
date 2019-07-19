using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class chute : MonoBehaviour
{

    private PhotonView photonView;

    public void Start()
    {
        photonView = GetComponent<PhotonView>();
    }
    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Joueur")
        {
            if (PhotonNetwork.isMasterClient)
            {
                UnityEngine.Debug.Log("CHUTE");
                photonView.RPC("RPC_AddDataDeath",PhotonTargets.All,  other.transform.position, Time.timeSinceLevelLoad, other.GetComponent<PhotonView>().ownerId,"fall");
            
            }
        }

        
    }
    
    [PunRPC]
    private void RPC_AddDataDeath(Vector3 pos, float timeStamp,  int player, string death)
    {
	    
        DataCollector.RegisterDeath(pos, timeStamp,  player, death);

    }
}



