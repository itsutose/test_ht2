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
    public coordinates coords;

    public Boolean color_feedback = true;
    public Boolean sphere_feedback = true;
  
    public string pr = "center";

    public TextMeshProUGUI textobject;
    public float feed_back_time = 0;

    public GameObject a, k, s, t, n, h, m, y, r, w, hen, backspace, space, point, enter;
    key keyscript;

    
    // キーボード入力時のフィードバックとか文字入力関係
    private float ux, uy;
    private GameObject[] keylist;
    private GameObject
        nowkey = null, // 押下されている子音キー
        priorkey = null; // 一つ前のキー（色の変化時に用いる）
    private bool onoff = false,onrunning = false;
    private int son = 0;
    private string keep_word = null;

    private Boolean preonoff = false;

    // Start is called before the first frame update
    void Start()
    {

        keylist = new GameObject[] { a, k, s, t, n, h, m, y, r, w, hen, backspace, space, point, enter};

    }

    // Update is called once per frame
    void Update()
    {

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

        if (Time.time <= 3)
        {
            Debug.Log("<=3");
            return;
        }

        if (sphere_feedback == true)
        {

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
}