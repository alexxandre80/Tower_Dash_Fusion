using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timer;
    private int timerint;
    public Text timertext;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timerint = Mathf.RoundToInt(timer);
        timertext.text = (timerint + " secondes");
        if (timer <=0)
        {
            timer = 0;
            Debug.Log("Timer à 0");
            ;
        }
    }

    //void ONGUI()
    //{
    //    GUI.Box(new Rect(10,10,50,25 ),timer.ToString("0"));
    //}
    
}
