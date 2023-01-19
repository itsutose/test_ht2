using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;

public class coordinates : MonoBehaviour
{
    public ServerManager server;
    public ONNXLoader onnxloader;
    public GameObject sphere;
    public GameObject PreCoordsSphere;



    public Boolean model;
    public Boolean Pointer = true;
    public int out_range_times = 50;
    public float magnification = 1;

    // serverから座標とかをうけとる
    // 主にandroidからのデータの処理に用いる
    string andpos;
    string now_time = "00:00:00.000", last_time = "00:00:00.000";
    int same_times_count = 0;

    float sizex = 1900, sizey = 1072; // 横（x）は良さそう

    // 他のスクリプトからのアクセスに対しての返す変数
    float ux = 0, uy = 0;
    Boolean onoff = false;
    private Boolean onrunning = false;

    private float maxx = -100, maxy = -100, minx = 100, miny = 100;

    private List<float> qx,qy;

    // Start is called before the first frame update
    void Start()
    {
        //ratio = keypos.getRatio();

        qx = new List<float>() { };
        qy = new List<float>() { };

        for (int i = 0; i < 40; i++)
        {
            qx.Add(-1.0f);
            qy.Add(-1.0f);
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

                if (result.Length != 5)
                {
                    return;
                }

                last_time = now_time;
                now_time = result[4];

                if (result[0] == "0")
                {
                    onoff = false;
                }
                else if (result[0] == "1")
                {
                    onoff = true;
                }


        ////////////////////////  ここからはpointerを表示するかどうか

         
                // 信号が一定時間送られなかったとき
                if (count_times(now_time, last_time) == true)
                {
                    sphere.SetActive(false);

                    onrunning = false;

                    ux = -1;
                    uy = -1;

                    return;

                }
                else
                {
                    if (Pointer == true)
                    {
                        sphere.SetActive(true);
                        onrunning = true;
                    }
                    else
                    {
                        sphere.SetActive(false);
                        onrunning = true;
                    }
                }

                Material mat1 = sphere.GetComponent<Renderer>().material;

                if (result[0] == "0")
                {
                    mat1.color = Color.blue;
                }
                else if (result[0] == "1")
                {
                    mat1.color = Color.red;
                }
            
        ////////////////////////////// ここまで

                float xx = Convert.ToSingle(result[1]);
                float yy = Convert.ToSingle(result[2]);

                // x : 倍率3.5でちょうど画面いっぱいでキーボードを網羅する ((x / maxx) - (float)0.5) * (float)3.5;
                // y : 右式でちょうど画面いっぱいでキーボードを網羅する  (y / maxy - (float)0.5) * (float)5 - (float)0.5; 
                //ux = (xx / maxx - (float)0.3) * (float)6;

                ux = -1*(xx / sizex - (float)0.5) * 2 * (float)0.0375 * (sizex/sizey)*magnification;
                uy = -1*(yy / sizey - (float)0.5) * 2 * (float)0.0375 * magnification;

                ////////////////////////////////////////  補正のモデル用
                if (model == true) {
                    qx.Insert(0, ux);
                    qy.Insert(0, uy);

                    float ux0 = qx[0];
                    float ux1 = qx[4];
                    float ux2 = qx[9];
                    float ux3 = qx[14];
                    float ux4 = qx[19];
                    float ux5 = qx[24];

                    float uy0 = qy[0];
                    float uy1 = qy[4];
                    float uy2 = qy[9];
                    float uy3 = qy[14];
                    float uy4 = qy[19];
                    float uy5 = qy[24];


                    float[] pxpy = onnxloader.GetComponent<ONNXLoader>().ModelPredict(ux0, uy0, ux1, uy1, ux2, uy2, ux3, uy3, ux4, uy4, ux5, uy5);

                    //Debug.Log()

                    ux = pxpy[0];
                    uy = pxpy[1];
                }
                
                //ローカル座標を基準に、座標を取得
                Vector3 localPos = sphere.transform.localPosition;

                localPos.x = ux;
                localPos.y = uy;

                sphere.transform.localPosition = localPos;

            }

        }
    }

    bool count_times(string nt, string lt)
    {

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

    public bool getOnoff()
    {
        return onoff;
    }

    public bool getOnrunning()
    {
        return onrunning;
    }

    public float getUX()
    {
        return ux;
    }

    public float getUY()
    {
        return uy;
    }

    public void setPreCoordsSphere(float ux, float uy)
    {

        PreCoordsSphere.SetActive(true);
        //ローカル座標を基準に、座標を取得
        Vector3 localPos = PreCoordsSphere.transform.localPosition;

        localPos.x = ux;
        localPos.y = uy;

        PreCoordsSphere.transform.localPosition = localPos;
    }

    public void setPreCoordsSphereColor(Color32 color32)
    {
        Material mat = PreCoordsSphere.GetComponent<Renderer>().material;
        mat.color = color32;

    }
}
