using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;
    
    [SerializeField]
    private Text stoneDamage;
    
    [SerializeField]
    private Text scissorDamage;
    
    [SerializeField]
    private Text paperDamage;
    
    [SerializeField]
    private Text movementSpeed;
    
    [SerializeField]
    private Text maxHealth;
    
    [SerializeField]
    private Text currentHealth;


    
    
    
    
    
    void Awake()
    {
        healthBar = GetComponent<Slider>();
       
    }

    // Update is called once per frame
    void Update()
    {
        GameObject leJoueur;
        
        foreach (var joueur in  GameObject.FindGameObjectsWithTag("Joueur"))
        {
            if (PhotonNetwork.player == joueur.GetComponent<PhotonView>().owner)
            {
                leJoueur = joueur;
                healthBar.value = leJoueur.GetComponent<PlayerMovement>().health;
                healthBar.maxValue = leJoueur.GetComponent<PlayerMovement>().maxHealth;

                // toutes les autres stats du HUD
                stoneDamage.text = "Stone Damage: " + leJoueur.GetComponent<PlayerMovement>().damageStone.ToString();
                scissorDamage.text = "Scissor Damage: " + leJoueur.GetComponent<PlayerMovement>().damageScissor.ToString();
                paperDamage.text = "Paper Damage: " + leJoueur.GetComponent<PlayerMovement>().damagePaper.ToString();
                movementSpeed.text = "Movement Speed: " +
                                     leJoueur.GetComponent<PlayerMovement>().movementSpeed.ToString();
                maxHealth.text = leJoueur.GetComponent<PlayerMovement>().health.ToString();
                currentHealth.text = leJoueur.GetComponent<PlayerMovement>().maxHealth.ToString();
            }
        }
        
    }
}
