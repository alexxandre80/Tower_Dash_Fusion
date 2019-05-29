using UnityEngine;
using UnityEngine.UI;

namespace PUN
{
    public class GUIController : MonoBehaviour
    {
        Text statusText;
        Text isMaster;
    
        // Start is called before the first frame update
        void Start()
        {
            statusText = GameObject.Find("StatusText").GetComponent<Text>();
            isMaster = GameObject.Find("IsMaster").GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            statusText.text = "Status : " + PhotonNetwork.connectionStateDetailed.ToString();
            isMaster.text = "is master client: " + PhotonNetwork.isMasterClient;
        }
    }
}
