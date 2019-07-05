using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{

    [SerializeField]
    Transform joueurTransform;

    [SerializeField]
    private MoveCubeScript move;
    
    
    

    // Start is called before the first frame update
    void Awake()
    {
        
    }


    void OnTriggerEnter(Collider surface)

    {
        

        if(surface.gameObject.tag == "Plateforme")
        {
            joueurTransform.parent = surface.transform;
            
        }
        move.setAllowToJump(true);


    }
    /*
    void OnTriggerStay(Collision collision)
    {
        
        if (Input.GetKeyDown (KeyCode.Space)) {

            if (joueurTransform.position.y <= 1.05f) {

                //GetComponent<Rigidbody>().AddForce (Vector3.up * 700);
                joueurTransform.parent.gameObject.GetComponent<Rigidbody>().AddForce (Vector3.up * 700);
            }
        }

    }*/


    void OnTriggerExit(Collider surface)

    {
        
        if (surface.gameObject.tag == "Plateforme")
        {
            joueurTransform.parent = null;
        }
        
        move.setAllowToJump(false);
        
    }
    
    void OnTriggerStay(Collider surface)
    {

        move.setAllowToJump(true);

    }


    // Update is called once per frame
    void Update()
    {


    }
}

