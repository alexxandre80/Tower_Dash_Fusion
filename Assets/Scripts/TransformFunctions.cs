using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformFunctions : MonoBehaviour
{
    /*public float moveSpeed = 10f;
    public float turnSpeed = 50f;
    
    
    void Update ()
    {
        if(Input.GetKey(KeyCode.UpArrow))
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        
        if(Input.GetKey(KeyCode.DownArrow))
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
        
        if(Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        
        if(Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
    }*/


    public float lookSpeed = 10;
    private Vector3 curLoc;
    private Vector3 prevLoc;
     
    void Update (){

        InputListen();
        transform.rotation = Quaternion.Lerp (transform.rotation,  Quaternion.LookRotation(transform.position - prevLoc), Time.fixedDeltaTime * lookSpeed);
    }
     
    private void InputListen(){

        prevLoc = curLoc;
        curLoc = transform.position;
         
        if(Input.GetKey(KeyCode.A))
            curLoc.x -= 1 * Time.fixedDeltaTime;
        if(Input.GetKey(KeyCode.D))
            curLoc.x += 1 * Time.fixedDeltaTime;
        if(Input.GetKey(KeyCode.W))
            curLoc.z += 1 * Time.fixedDeltaTime;
        if(Input.GetKey(KeyCode.S))
            curLoc.z -= 1 * Time.fixedDeltaTime;
         
        transform.position = curLoc;
         
    }
}
