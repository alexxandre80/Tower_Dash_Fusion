using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EndGameUI : MonoBehaviour
{
    [SerializeField]
    private GameObject ddol;
    // Start is called before the first frame update


    public GameObject topPanel;
    public Text text;

    
    
    
    public void onClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        foreach (var ddol in GameObject.FindGameObjectsWithTag("ddol"))
        {
            Destroy(ddol);
        }
        SceneManager.LoadScene("Lobby", LoadSceneMode.Single);

        


    }
}
