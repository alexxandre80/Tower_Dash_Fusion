using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespwanZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Players")
        {
            other.transform.position = new Vector3(-17.19f,0.81f,-23.03f);
        }
    }
}

