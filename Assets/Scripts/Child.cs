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
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collision collision)
    {
        

        if(collision.gameObject.tag == "plateforme")
        {
            joueurTransform.parent = collision.transform;
        }


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


    void OnTriggerExit(Collision collision)
    {
        
        if (collision.gameObject.tag == "ColliderHautBas")
        {
            joueurTransform.parent = null;
        }
        
    }


    // Update is called once per frame
    void Update()
    {


    }
}

