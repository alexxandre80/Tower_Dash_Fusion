using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 

public class FightTrigger : MonoBehaviour
{


    [SerializeField]
    private int inFight;

    
    
       
    void Start () {
        
        

    }
	
    // Update is called once per frame
    void Update () {


    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Joueur") // dans le cas où heurte un joueur
        {
            inFight = 1;
            Application.LoadLevel ("_MainScene");

        }

    }

}
