using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using TMPro;

using UnityEngine.UI;  // 追加しましょう

public class MovePointer : MonoBehaviour
{
    //public GameObject moveobject;
    public ServerManager server;
    public GameObject f1, f2, f3, f4;
    public Boolean Flame = false;

    private GameObject sphere;

    private string andpos;

    private float maxx = 1900, maxy = 1072; // 横（x）は良さそう
    private Text score_text;
    private Boolean flag = false;
    private string now_time = null;

    private float leftx = 0, rightx = 0, upy = 0, downy = 0;

    // Start is called before the first frame update
    void Start()
    {

        sphere = this.gameObject;

        Vector3 pf1 = f1.transform.localPosition;
        Vector3 pf2 = f2.transform.localPosition;
        Vector3 pf3 = f3.transform.localPosition;
        Vector3 pf4 = f4.transform.localPosition;

        if(Flame == false)
        {
            f1.SetActive(false);
            f2.SetActive(false);
            f3.SetActive(false);
            f4.SetActive(false);
        }
    


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
                //Debug.Log("Move Update:" + andpos);

                //if(andpos == "TOUCH_DOWN")
                //{
                //    Debug.Log("touch off if called aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

                //    //Material mat = this.GetComponent<Renderer>().material;
                //    //mat.color = Color.;


                //    return;
                //}

                //if (andpos.Contains("UP"))
                //{
                //    Debug.Log(andpos);
                //}

                //if (andpos == "hover_enter")
                //{
                //    flag = true;
                //    Debug.Log(andpos);
                //}

                //if (andpos == "hover_exit")
                //{
                //    flag = true;
                //    Debug.Log(andpos);
                //}

                //if (andpos == "touch_up")
                //{
                //    flag = true;
                //    Debug.Log(andpos);
                //}

                //if (andpos == "touch_down")
                //{
                //    flag = true;
                //    Debug.Log(andpos);
                //}

                //if (flag == true)
                //{
                //    return;
                //    flag = false;
                //}


                string[] result = Regex.Split(andpos, " ");

                if (result.Length != 5)
                {
                    //Debug.Log(andpos);
                    return;
                }

                //if(result[4] != prior_time)
                //{
                //    Debug.Log(result)
                //}

                //if(result[4] == prior_time)
                //{
                //    Material mat = this.GetComponent<Renderer>().material;
                //    //mat.color = new Color(1.0f, 0.0f, 0.0f, 0.0f);
                //    mat.color = Color.blue;
                //    return;
                //}

                now_time = result[4];

                //Debug.Log(now_time);

                if (result[0] == "0")
                {
                    Material mat = this.GetComponent<Renderer>().material;
                    //mat.color = new Color(1.0f, 0.85f, 0.0f, 1.0f);
                    mat.color = Color.blue;
                    //mat.color = new Color(0.0f, 0.2f, 1.0f);
                }
                 else if(result[0] == "1")
                {
                    Material mat = this.GetComponent<Renderer>().material;
                    mat.color = Color.red;
                }

                float x = Convert.ToSingle(result[1]);
                float y = Convert.ToSingle(result[2]);



                //obj = transform.position;
                // ローカル座標を基準に、座標を取得
                Vector3 localPos = this.transform.localPosition;

                // x : 倍率3.5でちょうど画面いっぱいでキーボードを網羅する ((x / maxx) - (float)0.5) * (float)3.5;
                // y : 右式でちょうど画面いっぱいでキーボードを網羅する  (y / maxy - (float)0.5) * (float)5 - (float)0.5; 

                localPos.x = ((x / maxx) - (float)0.5) * (float)4.5;
                localPos.y = (y / maxy - (float)0.5) * (float)6 - (float)0.5;

                this.transform.localPosition = localPos;

            }
        }
    }

    public string get_now_time()
    {
        return now_time;
    }
}
