using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
   public float speed;
   public VariableJoystick variableJoystick;
    
    
    void Awake()
    {
        photonview = GetComponent<PhotonView>();
        variableJoystick = GameObject.FindWithTag("Joystick").GetComponent<VariableJoystick>();

    }
    
        void Update()
    {
        
        //Quand il s'agit de mon joueur on check nos propres input pour le faire bouger à l'aide de la fonction checkInput
        if (photonview.isMine)
        {
            checkInput();
        }
        
        //Pour tous les autres on utilise smoothSyncMovement qui s'occupe de rendre les mouvement des autres joueurs fluides
        else
        {
            smoothSyncMovement();
        }

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
		Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
		playerRigidBody.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
		
        if (Input.GetKey(KeyCode.S))
        {
            targetTransform.position +=
                Vector3.back * Time.deltaTime * moveSpeed;
        }

        if (Input.GetKey(KeyCode.Z))
        {
            targetTransform.position +=
                Vector3.forward * Time.deltaTime * moveSpeed;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            targetTransform.position +=
                Vector3.left * Time.deltaTime * moveSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            targetTransform.position +=
                Vector3.right * Time.deltaTime * moveSpeed;
        }
		
		
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            photonview.RPC("RPC_AskToFire", PhotonTargets.MasterClient, photonview.owner);
        }

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            photonview.RPC("RPC_AskToJump", PhotonTargets.MasterClient, photonview.owner);
			
		}


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
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 18;
        
            //Destruction de la balle apres 2 seconde
            Destroy(bullet, 0.5f);
        }
        
        
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
            playerRigidBody.AddForce(Vector3.up * 500.0f);
                
        }

    }

}
