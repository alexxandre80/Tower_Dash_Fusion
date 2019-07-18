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
    private int codeSpringBoard;
    
    [SerializeField]
    private int nbUtilisation;
    
    [SerializeField]
    private PhotonView photonView;
    
    [SerializeField]
    private Text textNBU;
    
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        
        if (DataCollector.Instance().isActivated() && PhotonNetwork.isMasterClient)
        {
            string path = "SpringBoard.txt";
            if (File.Exists(path))
            {

                string dataAsJson = File.ReadAllText(path);
                string[] split = dataAsJson.Split('*');
            
                foreach (string springBoardData in split)
                {
                    SpringBoardData laSpringBoardData = JsonUtility.FromJson<SpringBoardData>(springBoardData);

                    if (laSpringBoardData.code == codeSpringBoard)
                    {
                        nbUtilisation = nbUtilisation + 1;
                    }

                    
                }
            }
        }
		
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
            photonView.RPC("RPC_AddDataSpringBoard",PhotonTargets.All, other.gameObject.transform.position,  codeSpringBoard);
            
        }
    }
    
    
    [PunRPC]
    private void RPC_AddDataSpringBoard(Vector3 pos, int code)
    {
	    
        DataCollector.RegisterSpringBoardUse(pos,  code);
	    
    }
}
