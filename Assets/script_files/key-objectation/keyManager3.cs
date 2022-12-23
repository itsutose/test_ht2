using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;

using UnityEngine.UI;  // 追加しましょう

public class keyManager3 : MonoBehaviour
{

    // sphereのunityにおける座標を取得する．
    // これは MovePointer でも行われているが，
    // 別のスクリプトに頼るのは少し心もとない，
    public ServerManager server;
    public Boolean color_feedback = true;
    public Boolean sphere_feedback = true;
  
    public string pr = "center";

    public TextMeshProUGUI textobject;
    public float feed_back_time = 0;
    public int out_range_times = 50;

    public GameObject a, k, s, t, n, h, m, y, r, w, hen, backspace, space, point, enter;
    key keyscript;

    
    // キーボード入力時のフィードバックとか文字入力関係
    private float ux, uy;
    private GameObject[] keylist;
    private GameObject
        nowkey = null, // 押下されている子音キー
        priorkey = null; // 一つ前のキー（色の変化時に用いる）
    private bool onoff = false;
    private int son = 0;
    private string keep_word = null;
    private float maxx = 1900, maxy = 1072; // 横（x）は良さそう

    // serverから座標とかをうけとる
    private string andpos;
    private string now_time = "00:00:00.000", last_time = "00:00:00.000";
    private int same_times_count = 0;

    private Boolean preonoff = false;
    
    public GameObject sphere;

    // Start is called before the first frame update
    void Start()
    {
        ux = sphere.transform.position.x;
        uy = sphere.transform.position.y;

        keylist = new GameObject[] { a, k, s, t, n, h, m, y, r, w, hen, backspace, space, point, enter};

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

                Debug.Log(andpos);

                if (result.Length != 5)
                {
                    return;
                }

                last_time = now_time;
                now_time = result[4];

                // 信号が一定時間送られなかったとき
                if (count_times(now_time, last_time) == true)
                {
                    sphere.SetActive(false);

                    //Debug.Log("");

                    if (nowkey != null)
                    {
                        up_touch(true);
                    }

                    return;

                }
                else
                {
                    sphere.SetActive(true);
                }

                Material mat1 = sphere.GetComponent<Renderer>().material;

                if (result[0] == "0")
                {
                    mat1.color = Color.blue;
                    onoff = false;
                }
                else if (result[0] == "1")
                {
                    mat1.color = Color.red;
                    onoff = true;
                }


                float xx = Convert.ToSingle(result[1]);
                float yy = Convert.ToSingle(result[2]);

                // x : 倍率3.5でちょうど画面いっぱいでキーボードを網羅する ((x / maxx) - (float)0.5) * (float)3.5;
                // y : 右式でちょうど画面いっぱいでキーボードを網羅する  (y / maxy - (float)0.5) * (float)5 - (float)0.5; 
                //ux = (xx / maxx - (float)0.3) * (float)6;

                if (pr == "center")
                {
                    ux = (xx / maxx - (float)0.5) * (float)0.01;
                    uy = (yy / maxy - (float)0.5) * (float)0.01;
                }else if(pr == "right")
                {
                    ux = (xx / maxx - (float)0.65) * (float)5.8;
                    uy = (yy / maxy - (float)0.5) * (float)6 - (float)0.5;
                }
                // ローカル座標を基準に、座標を取得
                Vector3 localPos = sphere.transform.localPosition;

                localPos.x = ux;
                localPos.y = uy;

                sphere.transform.localPosition = localPos;

            }

        }




        if (Time.time <= 3)
        {
            Debug.Log("<=3");
            return;
        }

        if (sphere_feedback == true)
        {
            
            last_time = now_time;

            if (Time.time <= 3)
            {
                Debug.Log("<=3");
                return;
            }

            if (onoff == true)
            {
                // タッチ中
                preonoff = true;

                if (nowkey == null)
                {
                    set_touch();
                }
                else
                {
                    touch_action();
                }
            }
            else
            {
                // ホバー中


                // nowkeyは今押されている（若しくは，1 flame前まで押されていた）キー
                if(preonoff == false)
                {

                    foreach (GameObject key in keylist)
                    {
                        // 指座標がキー領域内にあるかどうか，なければ次のキーを探す
                        if (key.GetComponent<key2>().isin(ux, uy) == true)
                        {
                            // priorkeyがkey（現在のkey）ではない
                            // && priorkeyのnull判定をしないとnull参照する
                            if (priorkey != key && priorkey != null)
                            {
                                priorkey.GetComponent<key2>().rmcolor();
                            }

                            priorkey = key;

                            if (color_feedback == true)
                            {
                                priorkey.GetComponent<key2>().takecolor();
                            }


                            continue;
                        }

                    }
                }    // 指を離したとき
                else
                {
                    //nowkey = null;
                    if(nowkey == null)
                    {
                        up_touch(false);
                    }
                    else
                    {
                        up_touch(true);
                    }

                    preonoff = false;

                }
            }
        }

    }

    private void touch_action()
    {
        float cx = nowkey.GetComponent<key2>().get_cx();
        float cy = nowkey.GetComponent<key2>().get_cy();

        if (nowkey.GetComponent<key2>().isin(ux, uy) == true)
        {
            if (son != 0)
            {
                nowkey.GetComponent<key2>().rmcolor(son);
            }
            son = 0;
        }

        // 上下左右のどこにタッチしているかを判断し，sonという変数
        else if (ux <= cx && uy <= cy) // 第三象限
        {
            if (cx - ux < cy - uy) // 南南西
            {
                if (son != 4)
                {
                    nowkey.GetComponent<key2>().rmcolor(son);
                }
                son = 4;
            }
            else // 西南西
            {
                if (son != 1)
                {
                    nowkey.GetComponent<key2>().rmcolor(son);
                }
                son = 1;
            }
        }
        else if (ux > cx && uy <= cy) // 第二象限
        {
            if (ux - cx < cy - uy) // 南南東
            {
                if (son != 4)
                {
                    nowkey.GetComponent<key2>().rmcolor(son);
                }
                son = 4;
            }
            else // 東南東
            {
                if (son != 3)
                {
                    nowkey.GetComponent<key2>().rmcolor(son);
                }
                son = 3;
            }
        }
        else if (ux > cx && uy > cy) // 第一象限
        {
            if (ux - cx < uy - cy) // 北北東
            {
                if (son != 2)
                {
                    nowkey.GetComponent<key2>().rmcolor(son);
                }
                son = 2;
            }
            else // 東北東
            {
                if (son != 3)
                {
                    nowkey.GetComponent<key2>().rmcolor(son);
                }
                son = 3;
            }
        }
        else if (ux <= cx && uy > cy) // 第四象限
        {
            if (cx - ux < uy - cy) // 北北西
            {
                if (son != 2)
                {
                    nowkey.GetComponent<key2>().rmcolor(son);
                }
                son = 2;
            }
            else // 西北西
            {
                if (son != 1)
                {
                    nowkey.GetComponent<key2>().rmcolor(son);
                }
                son = 1;
            }
        }

        keep_word = nowkey.GetComponent<key2>().takeword(son, textobject.text);

        if(color_feedback == true)
        {
            nowkey.GetComponent<key2>().takecolor(Color.yellow, son);
        }
    }

    private void invoke()
    {
        nowkey.GetComponent<key2>().visible_key();
    }

    private void set_touch()
    {
        foreach (GameObject key in keylist)
        {
            Renderer mesh_obj = key.GetComponent<Renderer>();

            if (key.GetComponent<key2>().isin(ux, uy) == true)
            {

                nowkey = key;
                //nowkey.GetComponent<key>().takecolor();

                if (feed_back_time == 0)
                {
                    nowkey.GetComponent<key2>().visible_key();
                }
                else
                {
                    Invoke("invoke", feed_back_time);
                }

                if (priorkey != null)
                {
                    priorkey.GetComponent<key2>().rmcolor();
                }


                continue;
            }
            else
            {
                //mesh_obj.material.color = new Color32(255, 255, 255, 80);
                key.GetComponent<MeshRenderer>().material.color = new Color32(150, 150, 150, 20);
            }

        }
    }

    private void up_touch(Boolean oo)
    {
        foreach (GameObject key in keylist)
        {
            key.GetComponent<MeshRenderer>().material.color = new Color32(255, 255, 255, 0);
        }

        if (oo == true)
        {
            // 指を離すとき，場所領域に応じて文字入力を完了
            textobject.text = keep_word;

            // 母音キーの非表示化
            nowkey.GetComponent<key2>().rmcolor(son);
            nowkey.GetComponent<key2>().in_visible_key();

            //// 色をもとに戻す
            //priorkey.GetComponent<key>().rmcolor();
            //nowkey.GetComponent<key>().rmcolor();
        }
        nowkey = null;

    }

    bool range(float a, float b, float c)
    {
        if (b <= a && a <= c)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool count_times(string nt, string lt)
    {

        Debug.Log(same_times_count);
        if (nt == lt)
        {
            same_times_count += 1;
            if (same_times_count >= out_range_times)
            {
                return true;
            }
        }
        else
        {

            same_times_count = 0;
        }

        return false;
    }
}