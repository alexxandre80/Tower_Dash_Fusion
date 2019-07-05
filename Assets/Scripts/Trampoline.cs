using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField]
    private float rebound;
    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Joueur")
        {
            other.GetComponent<Rigidbody>().AddForce(Vector3.up * rebound);
        }
    }
}
