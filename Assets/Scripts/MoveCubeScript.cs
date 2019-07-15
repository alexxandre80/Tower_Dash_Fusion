using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering;

public class MoveCubeScript : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform;

    [SerializeField]
    private float moveSpeed;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    
    private PhotonView photonview;
    
    [SerializeField]
    private Boolean allowToJump;

    [SerializeField] 
    private Rigidbody playerRigidBody;
    
    private Vector3 targetPosition;
    private Quaternion TargetRotation;
   //public float health;
   //public float maxHealth;
   //public float notoriety;
   //public float damageStone;
   //public float damagePaper;
   //public float damageScissor;
   //public int inFight;
   //public float movementSpeed;
   
   [SerializeField]
   private float speed;
   public VariableJoystick variableJoystick;
   public Button JumpButton;
   public Button FireButton;
   public int flagbutton = 0;


    /*public float lookSpeed = 10;
   private Vector3 curLoc;
    private Vector3 prevLoc;
    private Vector3 previousLocation;*/

    //public float turnSpeed = 50f;


    
    
    void Awake()
    {
        photonview = GetComponent<PhotonView>();
        variableJoystick = GameObject.FindWithTag("Joystick").GetComponent<VariableJoystick>();
        JumpButton = GameObject.FindWithTag("JumpButton").GetComponent<Button>();
        FireButton = GameObject.FindWithTag("FireButton").GetComponent<Button>();

    }
    
        void Update()
    {
        
        //Quand il s'agit de mon joueur on check nos propres input pour le faire bouger à l'aide de la fonction checkInput
        if (photonview.isMine)
        {
            checkInput();

            if (flagbutton == 0)
            {
                JumpButton.onClick.AddListener(() => jump());
                FireButton.onClick.AddListener(() => fire());

                flagbutton = 1;
            }
            
            
        }
        
        //Pour tous les autres on utilise smoothSyncMovement qui s'occupe de rendre les mouvement des autres joueurs fluides
        else
        {
            smoothSyncMovement();
        }

        //InputListen();
        //transform.rotation = Quaternion.Lerp (transform.rotation,  Quaternion.LookRotation(transform.position - prevLoc), Time.fixedDeltaTime * lookSpeed);

    }
    
    
    //appelé à chaque fois qu'on reçoit un paquet pour l'objet ( comme ce script est attaché à autoritaire player dans notre cas c'est autoritaire player )
    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // Si il s'agit de notre joueur et que donc on écrit les data à envoyer dans notre paquet
        // On envoie notre pisition et notre rotation
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        
        // Dans le cas où on reçoit des données d'autres joueurs
        // On les stock dans les valeur targetPosition et targetRotation
        // On réutilise ses variable dans la fonction smoothSyncMovement
        else
        {
            targetPosition = (Vector3) stream.ReceiveNext();
            TargetRotation = (Quaternion) stream.ReceiveNext();
        }
    }
    
    //Cette fonction permet de rendre les déplacement sur les écran des autres joueurs plus fluides
    private void smoothSyncMovement()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition,0.25f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, 500 * Time.deltaTime);
    }

 
		private void checkInput()
    {
        //moveSpeed = 10f;
        float rotateSpeed = 200;

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        
        transform.position += transform.forward * (variableJoystick.Vertical * moveSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0,variableJoystick.Horizontal* rotateSpeed * Time.deltaTime, 0));

        transform.position += transform.forward * (vertical * moveSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0,horizontal* rotateSpeed * Time.deltaTime, 0));
        

             if (Input.GetKeyDown(KeyCode.K))
        {
            photonview.RPC("RPC_AskToFire", PhotonTargets.MasterClient, photonview.owner);
        }

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            photonview.RPC("RPC_AskToJump", PhotonTargets.MasterClient, photonview.owner);
            
        }


    }
		
		
        
   
        //transform.position = curLoc;


    public void Jump()
    {
        photonview.RPC("RPC_AskToJump", PhotonTargets.MasterClient, photonview.owner);
    }

    [PunRPC]
    private void RPC_AskToFire(PhotonPlayer unPhotonPlayer)
    {

        photonview.RPC("RPC_Fire", PhotonTargets.All, unPhotonPlayer);
        
    }
    

    [PunRPC]
    private void RPC_Fire(PhotonPlayer unPhotonPlayer)
    {
        if (unPhotonPlayer == photonview.owner)
        {
            //Creation de la balle à partir du prefab "Bullet"
            var bullet = (GameObject) Instantiate(
                bulletPrefab,
                bulletSpawn.position,
                bulletSpawn.rotation);
        
            //Ajout de velocite a la ball
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 50;
        
            //Destruction de la balle apres 2 seconde
            Destroy(bullet, 0.5f);
        }
        
        
    }
    
    void jump()
    {
        photonview.RPC("RPC_AskToJump", PhotonTargets.MasterClient, photonview.owner);
    }   
    
    void fire()
    {
        photonview.RPC("RPC_AskToFire", PhotonTargets.MasterClient, photonview.owner);
    }


    public void setAllowToJump(Boolean allow)
    {
        allowToJump = allow;
    }
    
    [PunRPC]
    private void RPC_AskToJump(PhotonPlayer unPhotonPlayer)
    {
        if (allowToJump)
        {
            photonview.RPC("RPC_jump", PhotonTargets.All, unPhotonPlayer);
        }
  
    }
    
    
    [PunRPC]
    private void RPC_jump(PhotonPlayer unPhotonPlayer)
    {
        if (unPhotonPlayer == photonview.owner)
        {
            playerRigidBody.AddForce(Vector3.up * 700.0f);
                
        }

    }

}
