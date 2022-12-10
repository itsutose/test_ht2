using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using TMPro;

using UnityEngine.UI;  // í«â¡ÇµÇ‹ÇµÇÂÇ§

public class Test_d : MonoBehaviour
{
    public ServerManager server;
    public string debug = null;

    private GameObject cube;

    private string andpos;

    private float maxx = 1900, maxy = 1072; // â°ÅixÅjÇÕó«Ç≥ÇªÇ§
    private Text score_text;
    private Boolean flag = false;
    private string prior1 = null, prior2 = null;

    private Material mat;
    private Color color; 

    // Start is called before the first frame update
    void Start()
    {
        mat = this.GetComponent<Renderer>().material;
        //color = this.GetComponent<Image>().color;
        //color.r = 0.8f;
        //color.g = 0.3f;
        //color.b = 0.1f;
        //color.a = 0.5f;
        //gameObject.GetComponent<Image>().color = color;
        cube = this.gameObject;

        if (server != null)
        {
            andpos = server.get_coordinates();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (server != null)
        {
            andpos = server.get_coordinates();
            if (andpos != null)
            {
                string[] result = Regex.Split(andpos, " ");

                if (result.Length == 1 || result.Length != 5)
                {
                    //Debug.Log(andpos);
                    return;
                }
                
                if (debug.Contains("1"))
                    {
                        Debug.Log("result[4] : " + result[4] + " , prior1 : " + prior1 + " , prior2 : " + prior2);
                    }
                
                if (result[4] == prior1 && result[0] == "0")
                {
                    

                    Material mat = this.GetComponent<Renderer>().material;
                    mat.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
                    //mat.color = Color.blue;
                    return;
                }

                prior2 = prior1;
                prior1 = result[4];

                if (result[0] == "0")
                {
                    Material mat = this.GetComponent<Renderer>().material;
                    //mat.color = new Color(1.0f, 0.85f, 0.0f, 1.0f);
                    mat.color = Color.blue;
                }
                else if(result[0] == "1")
                {
                    Material mat = this.GetComponent<Renderer>().material;
                    mat.color = Color.red;
                }

                //if (andpos.Contains("UP"))
                //{
                //    Debug.Log(andpos);

                //    Color color = this.GetComponent<Renderer>().material.color;
                //    color.a = 1.0f;

                //    this.GetComponent<Renderer>().material.color = color;

                //    //Material mat = this.GetComponent<Renderer>().material;
                //    //mat.color = Color.blue;


                //    //var c = mat.color;
                //    //mat.color = new Color32(c.1, c.2, c.3);
                //    //c = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                //    //this.GetComponent<Image>().color = c;
                //}

                //if (andpos.Contains("HOVER_ENTER"))
                //{
                //    Debug.Log(andpos);
                //    Material mat = this.GetComponent<Renderer>().material;
                //    mat.color = Color.blue;

                //    // = new Color(0.0f, 0.0f, 1.0f, 1.0f);
                //    //this.GetComponent<Image>().color = c;
                //}

                //if (andpos.Contains("DOWN"))
                //{
                //    Debug.Log(andpos);
                //    Material mat = this.GetComponent<Renderer>().material;
                //    mat.color = Color.red;
                //    //c = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                //    //this.GetComponent<Image>().color = c;
                //}

                //if (andpos.Contains("HOVER_EXIT"))
                //{
                //    Debug.Log(andpos);

                //    Color color = this.GetComponent<Renderer>().material.color;
                //    color.a = 0.0f;

                //    this.GetComponent<Renderer>().material.color = color;

                //    //var c = mat.color;
                //    //mat.color = new Color32(c.1, c.2, c.3, 0f);
                //    //color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                //    //this.GetComponent<Image>().color = color;
                //}

            }
        }

        
    }
}
