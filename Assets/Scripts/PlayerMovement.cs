using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Photon.MonoBehaviour
{
    private PhotonView photonview;


    private Vector3 targetPosition;
    private Quaternion TargetRotation;
    public float health;
    public float maxHealth;
    public float notoriety;
    public float damageStone;
    public float damagePaper;
    public float damageScissor;
    public int inFight;
    public float movementSpeed;
    
    void Awake()
    {
        photonview = GetComponent<PhotonView>();
    }

    // Update is called once per frame
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
        float moveSpeed = 100f;
        float rotateSpeed = 500f;

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        transform.position += transform.forward * (vertical * moveSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0,horizontal* rotateSpeed * Time.deltaTime, 0));

    }
}
