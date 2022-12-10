using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.UI;  // 追加しましょう

public class keyManager2 : MonoBehaviour
{

    // sphereのunityにおける座標を取得する．
    // これは MovePointer でも行われているが，
    // 別のスクリプトに頼るのは少し心もとない，


    public GameObject sphere;
    public TextMeshProUGUI textobject;
    public float feed_back_time = 0;
    public bool debug = true;

    public GameObject a, k, s, t, n, h, m, y, r, w, hen;
    key keyscript;

    private float ux, uy;
    private GameObject[] keylist;
    private GameObject
        nowkey = null, // 押下されている子音キー
        //consonant = null, // 子音のキー（押下時に他の子音キーを押さないように）
        priorkey = null; // 一つ前のキー（色の変化時に用いる）
    private bool onoff = false;
    private int son = 0;
    private string keep_word = null;

    // Start is called before the first frame update
    void Start()
    {
        ux = sphere.transform.position.x;
        uy = sphere.transform.position.y;

        keylist = new GameObject[] { a, k, s, t, n, h, m, y, r, w, hen };

    }

    // Update is called once per frame
    void Update()
    {

        if(Time.time <= 3)
        {
            if(debug == true)
            {
            Debug.Log("<=3");
            }

            
            return;
        }

        // ux,uyで座標を取得
        ux = sphere.transform.localPosition.x;
        uy = sphere.transform.localPosition.y;

        // onoff（接触しているかどうか）を取得
        Material mat = sphere.GetComponent<Renderer>().material;
        if (mat.color == Color.red)
        {
            onoff = true;
        }
        else if (mat.color == Color.blue)
        {
            onoff = false;
        }

        if(onoff == true)
        {
            // タッチ中

            if(nowkey == null)
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
            if (nowkey == null)
            {

                foreach (GameObject key in keylist)
                {
                    // 指座標がキー領域内にあるかどうか，なければ次のキーを探す
                    if (key.GetComponent<key>().isin(ux, uy) == true)
                    {
                        // priorkeyがkey（現在のkey）ではない
                        // && priorkeyのnull判定をしないとnull参照する
                        if (priorkey != key && priorkey != null)
                        {
                            priorkey.GetComponent<key>().rmcolor();
                        }

                        priorkey = key;
                        priorkey.GetComponent<key>().takecolor();
                        
                        continue;
                    }

                }
            }    // 指を離したとき
            else
            {
                // 指を離すとき，場所領域に応じて文字入力を完了
                textobject.text += keep_word;

                // 母音キーの非表示化
                nowkey.GetComponent<key>().rmcolor(son);
                nowkey.GetComponent<key>().in_visible_key();

                //// 色をもとに戻す
                //priorkey.GetComponent<key>().rmcolor();
                //nowkey.GetComponent<key>().rmcolor();

                nowkey = null;

            }
        }

    }

    private void touch_action()
    {
        float cx = nowkey.GetComponent<key>().get_cx();
        float cy = nowkey.GetComponent<key>().get_cy();

        if (nowkey.GetComponent<key>().isin(ux, uy) == true)
        {
            if (son != 0)
            {
                nowkey.GetComponent<key>().rmcolor(son);
            }
            son = 0;
        }
        else if (ux <= cx && uy <= cy) // 第三象限
        {
            if (cx - ux < cy - uy) // 南南西
            {
                if(son != 4)
                {
                    nowkey.GetComponent<key>().rmcolor(son);
                }
                son = 4;
            }
            else // 西南西
            {
                if (son != 1)
                {
                    nowkey.GetComponent<key>().rmcolor(son);
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
                    nowkey.GetComponent<key>().rmcolor(son);
                }
                son = 4;
            }
            else // 東南東
            {
                if (son != 3)
                {
                    nowkey.GetComponent<key>().rmcolor(son);
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
                    nowkey.GetComponent<key>().rmcolor(son);
                }
                son = 2;
            }
            else // 東北東
            {
                if (son != 3)
                {
                    nowkey.GetComponent<key>().rmcolor(son);
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
                    nowkey.GetComponent<key>().rmcolor(son);
                }
                son = 2;
            }
            else // 西北西
            {
                if (son != 1)
                {
                    nowkey.GetComponent<key>().rmcolor(son);
                }
                son = 1;
                //nowkey.GetComponent<key>().takecolor(Color.red, 1);
            }
        }

        keep_word = nowkey.GetComponent<key>().takeword(son);

        // if time.timeでここだけタイミングを遅らせることも可能
        nowkey.GetComponent<key>().takecolor(Color.red, son);

        if (debug == true)
        {
        Debug.Log("keep_word ;" + keep_word);
        }

    }

    private void invoke()
    {
        nowkey.GetComponent<key>().visible_key();
    }

    private void set_touch()
    {
        foreach (GameObject key in keylist)
        {
            if (key.GetComponent<key>().isin(ux, uy) == true)
            {

                nowkey = key;
                //nowkey.GetComponent<key>().takecolor();

                if (feed_back_time == 0)
                {
                    nowkey.GetComponent<key>().visible_key();
                }else
                {
                    Invoke("invoke", feed_back_time);
                }

                //if (priorkey != key && priorkey != null)
                //{
                //    priorkey.GetComponent<key>().rmcolor();
                //}
                if (priorkey != null)
                {
                    priorkey.GetComponent<key>().rmcolor();
                }
                
                
                continue;
            }

        }
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


