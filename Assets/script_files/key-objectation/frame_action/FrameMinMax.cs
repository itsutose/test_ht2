using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;
public class FrameMinMax : MonoBehaviour
{

    public ServerManager server;
    public GameObject xbar, ybar, up, down, right, left;
    public string position = "center";

    private float ux, uy;
    private float sizex = 1900, sizey = 1072; // 横（x）は良さそう
    private string andpos;
    private float maxx = -100, maxy = -100, minx = 100, miny = 100;

    // Start is called before the first frame update
    void Start()
    {
        // ローカル座標を基準に、座標を取得
        Vector3 localPosXbar = xbar.transform.localPosition;
        Vector3 localPosYbar = ybar.transform.localPosition;

        localPosXbar.y = 0;
        localPosYbar.x = 0;

        xbar.transform.localPosition = localPosXbar;
        ybar.transform.localPosition = localPosYbar;

        ////ゲームオブジェクトのサイズ変更
        //gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2((gameObject.GetComponent<RectTransform>().sizeDelta.x - 10), gameObject.GetComponent<RectTransform>().sizeDelta.y);

        //xbar.GetComponent<RectTransform>().sizeDelta

        ////ゲームオブジェクトの横幅サイズを取得
        //if (gameObject.GetComponent<RectTransform>().sizeDelta.x <= 0)
        //{
        //    //横幅0以下だったら0にする
        //    gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, gameObject.GetComponent<RectTransform>().sizeDelta.y);

        //}
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


                float xx = Convert.ToSingle(result[1]);
                float yy = Convert.ToSingle(result[2]);

                // x : 倍率3.5でちょうど画面いっぱいでキーボードを網羅する ((x / maxx) - (float)0.5) * (float)3.5;
                // y : 右式でちょうど画面いっぱいでキーボードを網羅する  (y / maxy - (float)0.5) * (float)5 - (float)0.5; 
                //ux = (xx / maxx - (float)0.3) * (float)6;

                if (position == "center")
                {
                    ux = (xx / sizex - (float)0.5) * (float)0.065;
                    uy = (yy / sizey - (float)0.5) * (float)0.035;
                }
                else if (position == "right")
                {
                    ux = (xx / sizex - (float)0.65) * (float)5.8;
                    uy = (yy / sizey - (float)0.5) * (float)6 - (float)0.5;
                }


                //// ローカル座標を基準に、座標を取得
                Vector3 upPos = up.transform.localPosition;
                Vector3 downPos = down.transform.localPosition;
                Vector3 rightPos = right.transform.localPosition;
                Vector3 leftPos = left.transform.localPosition;

                if(ux > maxx)
                {
                    maxx = ux;
                    rightPos.x = ux;
                    right.transform.localPosition = rightPos;
                }

                if (ux < minx)
                {
                    minx = ux;
                    leftPos.x = ux;
                    left.transform.localPosition = leftPos;
                }

                if (uy > maxy)
                {
                    uy = maxy;
                    upPos.y = uy;
                    up.transform.localPosition = upPos;
                }

                if (uy < miny)
                {
                    uy = miny;
                    downPos.y = uy;
                    down.transform.localPosition = downPos;
                }

                //localPos.x = ux;
                //localPos.y = uy;

                //sphere.transform.localPosition = localPos;

            }

        }
    }
}
