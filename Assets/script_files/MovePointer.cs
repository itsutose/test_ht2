using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using TMPro;

using UnityEngine.UI;  // ’Ç‰Á‚µ‚Ü‚µ‚å‚¤

public class MovePointer : MonoBehaviour
{
    public coordinates coords;
    private GameObject sphere;

    private float ux = 100, uy = 100;
    private Boolean onrun = false, onoff = false;


    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {

        

        //if(coords != null)
        //{
        //    ux = coords.getUX();
        //    uy = coords.getUY();

        //    Debug.Log("MovePointer gets coords :" + coords.getUX());
        //}
        //else
        //{
        //    Debug.Log("coord‚Í‚È‚¢");
        //}

        if (coords.getOnrunning() == true)
        {
            Debug.Log("MovePointer gets coords :" + coords.getUX());
        }
    }

    //public string get_now_time()
    //{
    //    return now_time;
    //}
}
