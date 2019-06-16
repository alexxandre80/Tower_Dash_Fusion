using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
   
    private Camera cam;
    
    private float distance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensivityX = 0.0f;
    private float sensivityY = 0.0f;
    private GameObject leJoueur;

    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;


        if (cam != null)
        {
            Debug.Log("Camera instancie");
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");
    }

    
    private void LateUpdate()
    {
        
        foreach (var joueur in  GameObject.FindGameObjectsWithTag("Joueur"))
        {
            if (PhotonNetwork.player == joueur.GetComponent<PhotonView>().owner)
            {
                leJoueur = joueur;
                
            }

            else
            {
               
            }
        }
        
        Vector3 direction = new Vector3(0,0,-distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        cam.GetComponent<Transform>().position = leJoueur.GetComponent<Transform>().position + rotation * direction;
        cam.GetComponent<Transform>().LookAt(leJoueur.GetComponent<Transform>().position);
    }
    
    
}
