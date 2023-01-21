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

    private Boolean HoverColorFeedback;
    private Boolean KeyBoardFeedback;
    private String KeyColor;
    private int HowTransparent;
    private int CloverTransparent;

  
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

    private Boolean preonoff = false;

    private float xKeySize, yKeySize;

    // Start is called before the first frame update
    public void SStart()
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

    }

    // Update is called once per frame
    void Update()
    {
        ux = coords.getUX();
        uy = coords.getUY();
        onoff = coords.getOnoff();
        onrunning = coords.getOnrunning();

        if(onrunning == false)
        {
            onoff = false;
        }


        if(HoverColorFeedback == true && KeyBoardFeedback == true)
        {
            if (nowkey != null && onrunning == false)
            {
                up_touch(true);

                return;
            }

            if (onrunning == false)
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
            // ホバー中
            else
            {
                // nowkeyは今押されている（若しくは，1 flame前まで押されていた）キー
                if (preonoff == false)
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
                                //priorkey.GetComponent<key2>().rmcolor();
                                rmcolor(priorkey, HowTransparent);
                            }

                            priorkey = key;

                            if (HoverColorFeedback == true)
                            {
                                priorkey.GetComponent<key2>().takecolor();
                            }
                            return;
                        }
                    }

                    rmcolor(priorkey, HowTransparent);
                }
                // 指を離したとき, 1フレーム前まではonなのでpreonoff == true
                else
                {

                    if (nowkey == null)
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
                //nowkey.GetComponent<key2>().rmcolor(son);
                takecolor(nowkey, CloverTransparent, son);
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
                    //nowkey.GetComponent<key2>().rmcolor(son);
                    if (son == 0)
                    {
                        takecolor(nowkey, 80, son);
                    }
                    else
                    {
                        takecolor(nowkey, CloverTransparent, son);
                    }
                }
                son = 4;
            }
            else // 西南西
            {
                if (son != 1)
                {
                    //nowkey.GetComponent<key2>().rmcolor(son);
                    if (son == 0)
                    {
                        takecolor(nowkey, 80, son);
                    }
                    else
                    {
                        takecolor(nowkey, CloverTransparent, son);
                    }
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
                    //nowkey.GetComponent<key2>().rmcolor(son);
                    if (son == 0)
                    {
                        takecolor(nowkey, 80, son);
                    }
                    else
                    {
                        takecolor(nowkey, CloverTransparent, son);
                    }
                }
                son = 4;
            }
            else // 東南東
            {
                if (son != 3)
                {
                    //nowkey.GetComponent<key2>().rmcolor(son);
                    if (son == 0)
                    {
                        takecolor(nowkey, 80, son);
                    }
                    else
                    {
                        takecolor(nowkey, CloverTransparent, son);
                    }
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
                    //nowkey.GetComponent<key2>().rmcolor(son);
                    if (son == 0)
                    {
                        takecolor(nowkey, 80, son);
                    }
                    else
                    {
                        takecolor(nowkey, CloverTransparent, son);
                    }
                }
                son = 2;
            }
            else // 東北東
            {
                if (son != 3)
                {
                    //nowkey.GetComponent<key2>().rmcolor(son);
                    if (son == 0)
                    {
                        takecolor(nowkey, 80, son);
                    }
                    else
                    {
                        takecolor(nowkey, CloverTransparent, son);
                    }
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
                    //nowkey.GetComponent<key2>().rmcolor(son);
                    if (son == 0)
                    {
                        takecolor(nowkey, 80, son);
                    }
                    else
                    {
                        takecolor(nowkey, CloverTransparent, son);
                    }
                }
                son = 2;
            }
            else // 西北西
            {
                if (son != 1)
                {
                    //nowkey.GetComponent<key2>().rmcolor(son);
                    if (son == 0)
                    {
                        takecolor(nowkey, 80, son);
                    }
                    else
                    {
                        takecolor(nowkey, CloverTransparent, son);
                    }
                }
                son = 1;
            }
        }

        keep_word = nowkey.GetComponent<key2>().takeword(son, textobject.text);

        if(HoverColorFeedback == true)
        {
            // Color32(255,255,0,255) は黄色
            nowkey.GetComponent<key2>().takecolor(new Color32(255, 255, 0, 255), son);
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
                    takecolor(nowkey, CloverTransparent, -1);
                }
                else
                {
                    Invoke("invoke", feed_back_time);
                }
               
                // priorkeyはホバーでの入力を念頭に入れている
                if (priorkey != null)
                {
                    rmcolor(priorkey, HowTransparent);
                }
                break;
            }
            //else
            //{
            //    // 特定のキーを押した際の他のキーの色の変化に関する
            //    takecolor(key, HowTransparent);
            //}

            //takecolor(key, HowTransparent);
        }

        foreach (GameObject key in keylist)
        {
            takecolor(key, HowTransparent);
        }
    }

    private void rmcolor(GameObject k, int alpha)
    {
        Color32 white = new Color32(255, 255, 255, 250);
        Color32 Trans = new Color32(255, 255, 255, Convert.ToByte(alpha));

        // 透明にならない，明度が下がるだけ
        if (KeyColor == "TP0" || KeyColor == "TP1")
        {
            k.GetComponent<key2>().takecolor(white);
        }
        // 半透明になる
        else if (KeyColor == "TP2" || KeyColor == "TP3")
        {
            k.GetComponent<key2>().takecolor(Trans);

        }
    }

    private void takecolor(GameObject k, int alpha, int a = -2)
    {
        Color32 white = new Color32(255, 255, 255, 250);
        Color32 gray = new Color32(191, 191, 191, 250);
        Color32 Trans = new Color32(255, 255, 255, Convert.ToByte(alpha));
        Color32 Full = new Color32(255, 255, 255, 0);

        if (a != -2)
        {
            //if(KeyColor == "TP3")
            //{
            //    k.GetComponent<key2>().takecolor(Trans, all);
            //}
            // 透明にならない，明度が下がるだけ
            if (KeyColor == "TP0" || KeyColor == "TP1" || KeyColor == "TP2")
            {
                k.GetComponent<key2>().takecolor(white, a);
            }
            // 半透明になる
            else if (KeyColor == "TP3")
            {
                //k.GetComponent<key2>().takecolor(Full);
                k.GetComponent<key2>().takecolor(Trans, a);
            }
        }
        else
        {
            // 透明にならない，明度が下がるだけ
            if (KeyColor == "TP0")
            {
                k.GetComponent<key2>().takecolor(gray);
            }
            // 半透明になる
            else if (KeyColor == "TP1" || KeyColor == "TP2")
            {
                k.GetComponent<key2>().takecolor(Trans);
            }
            // 透明になる
            else if (KeyColor == "TP3")
            {
                k.GetComponent<key2>().takecolor(Full);
            }
        }
    }

    private void up_touch(Boolean oo)
    {
        foreach (GameObject key in keylist)
        {
            rmcolor(key, HowTransparent);
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

    //public Boolean getPreTest()
    //{
    //    return PreTest;
    //}

    //public void setPreTest(Boolean tf)
    //{
    //    PreTest = tf;
    //}

    public Boolean getHoverColorFeedback()
    {
        return HoverColorFeedback;
    }

    public void setHoverColorFeedback(Boolean tf)
    {
        HoverColorFeedback = tf;
    }

    public Boolean setKeyBoardFeedback()
    {
        return KeyBoardFeedback;
    }

    public void setKeyBoardFeedback(Boolean tf)
    {
        KeyBoardFeedback = tf;
    }

    public String getKeyColor()
    {
        return KeyColor;
    }

    public void setKeyColor(String s)
    {
        KeyColor = s;
    }

    public int getHowTransparent()
    {
        return HowTransparent;
    }

    public void setHowTransparent(int i)
    {
        HowTransparent = i;
    }

    public int getCloverTransparent()
    {
        return CloverTransparent;
    }

    public void setCloverTransparent(int i)
    {
        CloverTransparent = i;
    }

    public void refresh()
    {
        foreach (GameObject key in keylist)
        {
            rmcolor(key, HowTransparent);
        }
    }
}