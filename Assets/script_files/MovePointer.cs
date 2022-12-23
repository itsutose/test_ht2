using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using TMPro;

using UnityEngine.UI;  // í«â¡ÇµÇ‹ÇµÇÂÇ§

public class MovePointer : MonoBehaviour
{
    //public GameObject moveobject;
    public ServerManager server;
    public GameObject f1, f2, f3, f4;
    public Boolean Flame = false;

    private GameObject sphere;

    private string andpos;

    private float maxx = 1900, maxy = 1072; // â°ÅixÅjÇÕó«Ç≥ÇªÇ§
    private Text score_text;
    private Boolean flag = false;
    private string now_time = null;

    private float leftx = 0, rightx = 0, upy = 0, downy = 0;

    // Start is called before the first frame update
    void Start()
    {

        //sphere = this.gameObject;

        //Vector3 pf1 = f1.transform.localPosition;
        //Vector3 pf2 = f2.transform.localPosition;
        //Vector3 pf3 = f3.transform.localPosition;
        //Vector3 pf4 = f4.transform.localPosition;

        //if(Flame == false)
        //{
        //    f1.SetActive(false);
        //    f2.SetActive(false);
        //    f3.SetActive(false);
        //    f4.SetActive(false);
        //}
    


        //if (server != null)
        //{
        //    andpos = server.get_coordinates();
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string get_now_time()
    {
        return now_time;
    }


}
