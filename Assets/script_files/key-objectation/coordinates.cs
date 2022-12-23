using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;

using UnityEngine.UI;  // 追加しましょう

public class coordinates
{
    public ServerManager server;
    public string pr = "center";
    public float feed_back_time = 0;
    public int out_range_times = 50;

    // キーボード入力時のフィードバックとか文字入力関係
    private float ux, uy;
    private float maxx = 1900, maxy = 1072; // 横（x）は良さそう

    // serverから座標とかをうけとる
    private string andpos;
    private string now_time = "00:00:00.000", last_time = "00:00:00.000";
    private int same_times_count = 0;

    private Boolean preonoff = false;

    // Start is called before the first frame update
    void Start()
    {
        
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

                if (result.Length != 5)
                {
                    return;
                }

                last_time = now_time;
                now_time = result[4];

                // 信号が一定時間送られなかったとき
                if (count_times(now_time, last_time) == true)
                {
                    //sphere.SetActive(false);

                    //if (nowkey != null)
                    //{
                    //    up_touch(true);
                    //}

                    return;

                }
                else
                {
                    //sphere.SetActive(true);
                }

                //Material mat1 = sphere.GetComponent<Renderer>().material;

                //if (result[0] == "0")
                //{
                //    mat1.color = Color.blue;
                //    onoff = false;
                //}
                //else if (result[0] == "1")
                //{
                //    mat1.color = Color.red;
                //    onoff = true;
                //}


                float xx = Convert.ToSingle(result[1]);
                float yy = Convert.ToSingle(result[2]);

                // x : 倍率3.5でちょうど画面いっぱいでキーボードを網羅する ((x / maxx) - (float)0.5) * (float)3.5;
                // y : 右式でちょうど画面いっぱいでキーボードを網羅する  (y / maxy - (float)0.5) * (float)5 - (float)0.5; 
                //ux = (xx / maxx - (float)0.3) * (float)6;

                if (pr == "center")
                {
                    ux = (xx / maxx - (float)0.5) * 2 * (float)2;
                    uy = (yy / maxy - (float)0.5) * (float)6 - (float)0.5;
                }
                else if (pr == "right")
                {
                    ux = (xx / maxx - (float)0.65) * (float)5.8;
                    uy = (yy / maxy - (float)0.5) * (float)6 - (float)0.5;
                }
                // ローカル座標を基準に、座標を取得
                //Vector3 localPos = sphere.transform.localPosition;

                //localPos.x = ux;
                //localPos.y = uy;

                //sphere.transform.localPosition = localPos;

            }

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
