using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;

public class keyManager3 : MonoBehaviour
{

    // sphereのunityにおける座標を取得する．
    // これは MovePointer でも行われているが，
    // 別のスクリプトに頼るのは少し心もとない，
    public coordinates coords;
    //public textSet textset;

    public Boolean color_feedback = true;
    public String KeyColor = "white";
  
    public string pr = "center";

    public TextMeshProUGUI textobject;
    public float feed_back_time = 0;

    public GameObject a, k, s, t, n, h, m, y, r, w, hen, backspace, space, point, enter, dummy = null;
    
    // キーボード入力時のフィードバックとか文字入力関係
    private float ux, uy;
    private GameObject[] keylist;
    private GameObject[,] keylist2;

    private GameObject
        nowkey = null, // 押下されている子音キー
        priorkey = null; // 一つ前のキー（色の変化時に用いる）
 
    private bool onoff = false,onrunning = false;
    private int son = 0;
    private string keep_word = null;
    //private string word = null;

    private Boolean preonoff = false;

    private float xKeySize, yKeySize;

    // Start is called before the first frame update
    void Start()
    {
        if (dummy != null)
        {
            keylist = new GameObject[] { a, k, s, t, n, h, m, y, r, w, hen, backspace, space, enter, point, dummy };
            keylist2 = new GameObject[,] { { a, t, m, hen }, { k, n, y, w }, { s, h, r, point }, { backspace, space, enter, dummy } };
        }
        else
        {
            keylist = new GameObject[] { a, k, s, t, n, h, m, y, r, w, hen, backspace, space, enter, point};
        }
        float ncx = n.GetComponent<key2>().get_cx();
        float ncy = n.GetComponent<key2>().get_cy();

        float hcx = h.GetComponent<key2>().get_cx();
        float hcy = h.GetComponent<key2>().get_cy();

        float ycx = y.GetComponent<key2>().get_cx();
        float ycy = y.GetComponent<key2>().get_cy();

        xKeySize = hcx - ncx;
        yKeySize = ncy - ycy;

        //Debug.Log(string.Format("ncx : {0}, hcx : {1}", ncx, hcx));
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time <= 0.2 && Time.time > 0)
        {
            float ncx = n.GetComponent<key2>().get_cx();
            float ncy = n.GetComponent<key2>().get_cy();

            float hcx = h.GetComponent<key2>().get_cx();
            float hcy = h.GetComponent<key2>().get_cy();

            float ycx = y.GetComponent<key2>().get_cx();
            float ycy = y.GetComponent<key2>().get_cy();

            xKeySize = hcx - ncx;
            yKeySize = ncy - ycy;

            //Debug.Log(string.Format("ncx : {0}, hcx : {1}", ncx, hcx));
        }

        ux = coords.getUX();
        uy = coords.getUY();
        onoff = coords.getOnoff();
        onrunning = coords.getOnrunning();


        if (nowkey != null && onrunning == false)
        {
            up_touch(true);
            return;
        }

        if(onrunning == false)
        {
            return;
        }

        if (Time.time <= 1)
        {
            return;
        }


        if (onoff == true)
        {
            // タッチ中
            // 1フレーム前まで離れていた指をonとする
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

                        break;
                    }
                }
            }    // 指を離したとき, 1フレーム前まではonなのでpreonoff == true
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
           
            if (key.GetComponent<key2>().isin(ux, uy) == true)
            {
                nowkey = key;

                if (feed_back_time == 0)
                {
                    nowkey.GetComponent<key2>().visible_key();
                }
                else
                {
                    Invoke("invoke", feed_back_time);
                }


                //if (KeyColor == "white")
                //{
                //    key.GetComponent<key2>().takecolor(new Color32(150, 150, 150, 250));
                //}
                //// 半透明になる
                //else if (KeyColor == "transparent1" || KeyColor == "transparent2")
                //{
                //    key.GetComponent<key2>().takecolor(new Color32(255, 255, 255, 80));

                //}
                //// 透明になる
                //else if (KeyColor == "transparent3")
                //{
                //    key.GetComponent<key2>().takecolor(new Color32(255, 255, 255, 0));
                //}

                // priorkeyはホバーでの入力を念頭に入れている
                if (priorkey != null)
                {
                    priorkey.GetComponent<key2>().rmcolor();

                }

            }
            else
            {
                // 特定のキーを押した際の他のキーの色の変化に関する


                //Renderer mesh_obj = key.GetComponent<Renderer>();

                // color.aの値を0.6fで半透明，1.0fにすると完全に透明

                // 以下のコードだと視界全体を覆うrectに色がついた
                //key.GetComponent<MeshRenderer>().material.color = new Color32(150, 150, 150, 250);
                
                // 透明にならない，明度が下がるだけ
                if (KeyColor == "white")
                {
                    key.GetComponent<key2>().takecolor(new Color32(150, 150, 150, 250));
                    //mesh_obj.material.color = new Color32(150, 150, 150, 255);
                }
                // 半透明になる
                else if (KeyColor == "transparent1" || KeyColor == "transparent2")
                {
                    key.GetComponent<key2>().takecolor(new Color32(255, 255, 255, 80));

                }
                // 透明になる
                else if(KeyColor == "transparent3")
                {
                    key.GetComponent<key2>().takecolor(new Color32(255, 255, 255, 0));
                }
            }

        }
    }

    private void up_touch(Boolean oo)
    {
        foreach (GameObject key in keylist)
        {
            if (KeyColor == "white" || KeyColor == "transparent1")
            {
                key.GetComponent<key2>().takecolor(new Color32(255, 255, 255, 250));
            }
            // 半透明になる
            else if (KeyColor == "transparent2")
            {
                key.GetComponent<key2>().takecolor(new Color32(255, 255, 255, 80));

            }
            // 透明になる
            else if (KeyColor == "transparent3")
            {
                key.GetComponent<key2>().takecolor(new Color32(255, 255, 255, 80));
            }

            //key.GetComponent<MeshRenderer>().material.color = new Color32(255, 255, 255, 0);
        }

        if (oo == true)
        {
            // 指を離すとき，場所領域に応じて文字入力を完了
            textobject.text = keep_word;

            int s_leng = keep_word.Length;
            if (s_leng >= 1)
            {
                char last_word = keep_word[s_leng - 1];
                nowkey.GetComponent<key2>().InputWordtoCSV(last_word);
            }


            // 母音キーの非表示化
            nowkey.GetComponent<key2>().rmcolor(son);
            nowkey.GetComponent<key2>().in_visible_key();
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
}