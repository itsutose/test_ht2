using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;

public class coord_model_class : MonoBehaviour
{
    public ServerManager server;

    public ONNX_multi_classification onnxloader;
    public GameObject sphere;
    public GameObject preSphere;
    public GameObject PreCoordsSphere;

    public Boolean UtoP;


    //public Boolean Pointer = true;
    //public int out_range_times = 50;
    //public float magnification = (float)1.2;

    // serverから座標とかをうけとる
    // 主にandroidからのデータの処理に用いる
    string andpos;
    string now_time = "00:00:00.000", last_time = "00:00:00.000";
    int same_times_count = 0;

    float sizex = 1900, sizey = 1072; // 横（x）は良さそう

    // 他のスクリプトからのアクセスに対しての返す変数
    float ux = 0, uy = 0;
    float px = -1, py = -1;
    int label = -1;
    Boolean onoff = false;
    private Boolean onrunning = false;
    private Boolean Pointer;
    private int out_range_times;
    private float magnification;


    private float maxx = -100, maxy = -100, minx = 100, miny = 100;

    private List<float> qx, qy;

    private float[] uxs;
    private float[] uys;

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


                    for (int i = 0; i < qx.Count; i++)
                    {

                        if (i >= 40)
                        {
                            break;
                        }
                        qx[i] = -1.0f;
                        qy[i] = -1.0f;
                    }

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

                ux = -1 * (xx / sizex - (float)0.5) * 2 * (float)0.0375 * (sizex / sizey) * magnification;
                uy = -1 * (yy / sizey - (float)0.5) * 2 * (float)0.0375 * magnification;

                ////////////////////////////////////////  補正のモデル用

                qx.Insert(0, ux);
                qy.Insert(0, uy);


                int maxLength = 35;
                qx.RemoveAll(x => qx.IndexOf(x) >= maxLength);
                qy.RemoveAll(x => qy.IndexOf(x) >= maxLength);

                uxs = new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
                float sumx = 0;
                int nn = 0;

                for (int i = 0; i < 6; i++)
                {

                    if (qx[i * 5] != -1)
                    {
                        sumx += qx[i * 5];
                        uxs[i] = qx[i * 5];
                        nn = i + 1;
                    }
                    else
                    {
                        uxs[i] = sumx / (float)nn;
                    }
                }

                uys = new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
                float sumy = 0;
                nn = 0;
                for (int i = 0; i < 6; i++)
                {

                    if (qy[i * 5] != -1)
                    {
                        sumy += qy[i * 5];
                        uys[i] = qy[i * 5];
                        nn = i + 1;
                    }
                    else
                    {
                        uys[i] = sumy / (float)nn;
                    }
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

    public int getLabel()
    {
        //label = onnxloader.GetComponent<ONNX_multi_classification>().ModelPredict(ux, uy, uxs[1], uys[1], uxs[2], uys[2], uxs[3], uys[3], uxs[4], uys[4], uxs[5], uys[5]);

        //return label;
        Debug.Log(string.Format("ux  {0}, uy  {1}", ux, uy));

        for (int i = 1; i < 6; i++)
        {
            Debug.Log(string.Format("ux{0}  {1}, uy{2}  {3}",i, uxs[i],i, uys[i]));
        }

        Debug.Log(onnxloader.GetComponent<ONNX_multi_classification>().ModelPredict(ux, uy, uxs[1], uys[1], uxs[2], uys[2], uxs[3], uys[3], uxs[4], uys[4], uxs[5], uys[5]));
        return onnxloader.GetComponent<ONNX_multi_classification>().ModelPredict(ux,  uxs[1], uxs[2], uxs[3], uxs[4], uxs[5], uy, uys[1], uys[2], uys[3], uys[4], uys[5]);

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

    public void setPointer(Boolean b)
    {
        Pointer = b;
    }

    public void setORT(int i)
    {
        out_range_times = i;
    }

    public void setMagnification(float f)
    {
        magnification = f;
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
