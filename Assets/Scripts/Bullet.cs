using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("la balle doit se détruire");
        Destroy(gameObject);
        
    }
}
