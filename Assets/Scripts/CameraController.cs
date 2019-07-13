using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    
    private float distance = 10.0f;

    private float sensivityX = 0.0f;
    private float sensivityY = 0.0f;
    [SerializeField]
    private GameObject leJoueur;

    
    // Start is called before the first frame update
    /*void Start()
    {
        cam = Camera.main;


        if (cam != null)
        {
            Debug.Log("Camera instancie");
        }

        
    }*/

    // Update is called once per frame


    
    private void LateUpdate()
    {
        
            Debug.Log("activation cam");
            if (PhotonNetwork.player == leJoueur.GetComponent<PhotonView>().owner)
            {

                    cam.gameObject.SetActive(true);
                    
                
            }

            else
            {
               
            }
        
        
        //Vector3 direction = new Vector3(0,0,-distance);
        //Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        //cam.GetComponent<Transform>().position = leJoueur.GetComponent<Transform>().position +  direction;
        //cam.GetComponent<Transform>().SetParent(leJoueur.GetComponent<Transform>());
        //cam.GetComponent<Transform>().position = new Vector3(0,1.393f,-2.808f);
        //cam.GetComponent<Transform>().LookAt(leJoueur.GetComponent<Transform>().position);
        
    }
    
    
}
