using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;

public class keyManager5 : MonoBehaviour
{

    // sphereのunityにおける座標を取得する．
    // これは MovePointer でも行われているが，
    // 別のスクリプトに頼るのは少し心もとない，
    public PreCoords precoords;
    public coordinates coords;


    private Boolean HoverColorFeedback; //
    private Boolean KeyBoardFeedback; //
    private String KeyColor; //
    private int HowTransparent; //
    private int CloverTransparent; //

    //public TextMeshProUGUI textobject;


    // キーボード入力時のフィードバックとか文字入力関係
    private float ux, uy;
    private float[] rx, ry;
    private int TestTimes;

    private bool onoff = false, onrunning = false;

    private Boolean preonoff = false;

    private float xKeySize, yKeySize;

    private Boolean sstart = false;
    private String state;

    private int now = 0;

    // Start is called before the first frame update
    public void SStart()
    {

        ////////////////////////// ランダムな float rx, float ry の行列を作成

        rx = new float[500];
        ry = new float[500];

        System.Random randx = new System.Random();
        System.Random randy = new System.Random();

        //Debug.Log("keyManager5 SStart()");

        for (int i = 0; i < rx.Length; i++)
        {
            if (i % 10 == 0 && i <= 70)
            {
                rx[i] = 0;
            }
            else
            {
                //Debug.Log(string.Format("random number {0}", (float)(randx.NextDouble() * 0.16) - (float)0.08));
                rx[i] = (float)(randx.NextDouble() * 0.16) - (float)0.08;
            }
        }

        foreach (float num in rx)
        {
            //Console.WriteLine(num);
            //Debug.Log(string.Format("keyManager5 SStart rx {0}", num));
        }

        for (int i = 0; i < ry.Length; i++)
        {
            if (i % 10 == 0 && i <= 70)
            {
                ry[i] = 0;
            }
            else
            {
                ry[i] = (float)(randy.NextDouble() * 0.089) - 0.0445f;
            }
        }

        foreach (float num in ry)
        {
            //Console.WriteLine(num);
            //Debug.Log(string.Format("keyManager5 SStart ry {0}", num));
        }

        Debug.Log(string.Format("keyManager5 SStart  rx[0] {0}, ry[0] {1}", rx[0],ry[0]));

        //////////////////////////

        //coords.sphere.SetActive(true);
        coords.setPreCoordsSphere(rx[0], ry[0]);
        coords.setPreCoordsSphereColor(new Color32(0, 255, 0, 255));

        sstart = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (sstart == false)
        {

            return;
        }

        ux = coords.getUX();
        uy = coords.getUY();
        onoff = coords.getOnoff();
        onrunning = coords.getOnrunning();

        if (onrunning == false)
        {
            onoff = false;
        }

        //Debug.Log(string.Format("keyManager5 state {0}",state));

        // touch
        if (onrunning == true && onoff == true)
        {
            if (state == "hover")
            {
                precoords.Begin(rx[now], ry[now], ux, uy);
                now++;
            }
            else if (state == "out")
            {
                
                precoords.Begin(rx[now], ry[now], ux, uy);
                now++;
            }

            state = "touch";
        }
        // out
        else if (onrunning == false && onoff == false)
        {
            // touch -> out
            if (state == "touch")
            {
                coords.setPreCoordsSphereColor(new Color32(255, 0, 0, 255));
                precoords.End();

                // 
                Invoke("Next", 1);

                coords.setPreCoordsSphere(rx[now], ry[now]);
                coords.setPreCoordsSphereColor(new Color32(0, 255, 0, 255));
            }

            // touch -> hover
            else if (state == "hover")
            {
                // 何もなし
            }

            state = "out";
        }
        // hover
        else if (onrunning == true && onoff == false)
        {
            // touch -> hover
            if (state == "touch")
            {
                coords.setPreCoordsSphereColor(new Color32(255, 0, 0, 255));
                precoords.End();

                // 
                Invoke("Next", 1);

                coords.setPreCoordsSphere(rx[now], ry[now]);
                coords.setPreCoordsSphereColor(new Color32(0, 255, 0, 255));

            }
            // out -> hover
            else if (state == "out")
            {
                // 何もしない
            }
            state = "hover";
        }
    }

    private void Next()
    {
        //word = words2[i];
        // 次の文字を表示
        //textobject.text = word.ToString();
        //textsub.text = "Not touched yet.";
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

    public void setTestTimes(int i)
    {
        TestTimes = i;
    }

    //public Boolean getPreTest()
    //{
    //    return PreTest;
    //}

    //public void setPreTest(Boolean tf)
    //{
    //    PreTest = tf;
    //}

    //public Boolean getHoverColorFeedback()
    //{
    //    return HoverColorFeedback;
    //}

    //public void setHoverColorFeedback(Boolean tf)
    //{
    //    HoverColorFeedback = tf;
    //}

    //public Boolean setKeyBoardFeedback()
    //{
    //    return KeyBoardFeedback;
    //}

    //public void setKeyBoardFeedback(Boolean tf)
    //{
    //    KeyBoardFeedback = tf;
    //}

    //public String getKeyColor()
    //{
    //    return KeyColor;
    //}

    //public void setKeyColor(String s)
    //{
    //    KeyColor = s;
    //}

    //public int getHowTransparent()
    //{
    //    return HowTransparent;
    //}

    //public void setHowTransparent(int i)
    //{
    //    HowTransparent = i;
    //}

    //public int getCloverTransparent()
    //{
    //    return CloverTransparent;
    //}

    //public void setCloverTransparent(int i)
    //{
    //    CloverTransparent = i;
    //}

}