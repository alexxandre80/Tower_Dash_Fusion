using UnityEngine;
using System.Collections;


[RequireComponent( typeof( PhotonView ) )]
public class MaterialPerOwner : Photon.MonoBehaviour
{
    [SerializeField]
    private Renderer m_Renderer;


    // Update is called once per frame
    private void Awake()
    {
        if(GetComponent<PhotonView>().isMine )
        {
            
            m_Renderer.material.color = Color.green;
            
        }

        else
        {
            m_Renderer.material.color = Color.grey;
        }
    }
}
