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

    void OnCollisionEnter(Collision collision)
    {
        

        if(collision.gameObject.tag == "plateforme")
        {
            joueurTransform.parent = collision.transform;
        }


    }

    void OnCollisionExit(Collision collision)
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

