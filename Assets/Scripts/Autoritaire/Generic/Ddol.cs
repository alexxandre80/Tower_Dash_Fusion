using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ddol : MonoBehaviour
{
    //Ce script va être attaché à un empty game object du même nom, tous les enfant de ce gameobject seront conservé lors des changements de scène
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }


}
