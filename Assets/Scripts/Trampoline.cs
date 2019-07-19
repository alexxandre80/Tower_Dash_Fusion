using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Trampoline : MonoBehaviour
{
    [SerializeField]
    private float rebound;
    
    [SerializeField]
    public int codeSpringBoard;
    
    [SerializeField]
    public int nbUtilisation;
    
    [SerializeField]
    private PhotonView photonView;
    
    [SerializeField]
    public Text textNBU;
    
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
     
		
    }
    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Joueur")
        {
            other.GetComponent<Rigidbody>().AddForce(Vector3.up * rebound);
            nbUtilisation = nbUtilisation + 1;
        }

        if (PhotonNetwork.isMasterClient)
        {
            photonView.RPC("RPC_AddDataSpringBoard",PhotonTargets.All,  codeSpringBoard);
            
        }
    }
    
    
    [PunRPC]
    private void RPC_AddDataSpringBoard( int code)
    {
	    
        DataCollector.RegisterSpringBoardUse(code);
	    
    }
}
