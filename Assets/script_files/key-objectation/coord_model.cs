using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;

public class coord_model : MonoBehaviour
{
    public ServerManager server;

    public ONNX_multi_classification model_class;
    public ONNX_Regression model_reg;

    public GameObject sphere;
    public GameObject preSphere;


    public Boolean UtoP;
    public Boolean model;

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
    float ux = 100, uy = 100;
    float px = -1, py = -1;
    float subx, suby;
    int label = -1;
    Boolean onoff = false;
    private Boolean onrunning = false;
    private Boolean Pointer;
    private Boolean HoverPointer;
    private Boolean Hover = false;
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

            //Debug.Log(string.Format("coord_model server message {0}",));
            andpos = server.get_coordinates();

            if (andpos != null)
            {
                string[] splitData = andpos.Split(' ');

                if (splitData.Length > 0)
                {
                    string firstWord = splitData[0];

                    if (firstWord == "1" || firstWord == "0")
                    {
                        //Debug.Log(string.Format("ServerMessage OnMessage {0}, Type: {1}", andpos, andpos.GetType()));
                    }
                    else {

                    
                        Debug.Log(string.Format("coord_model {0}, Type: {1}", andpos, andpos.GetType()));
                        //if (firstWord == "TOUCH_DOWN")
                        //{
                        //    Debug.Log(string.Format("coord_model {0}", server.get_coordinates()));
                        //}
                        //else if (firstWord == "TOUCH_UP")
                        //{
                        //    Debug.Log("coord_model TOUCH_UP");
                        //}
                        //else
                        //{
                        //    //Debug.Log(string.Format("ServerMessage OnMessage {0}", andpos));
                        //}
                    }
                }


                string[] result = Regex.Split(andpos, " ");


                ////////////////////////  ここからはpointerを表示するかどうか


                // 信号が一定時間送られなかったとき
                //if (count_times(now_time, last_time) == true)
                if (Hover == false)
                {
                    if (result[0] == "TOUCH_UP")
                    {

                        Debug.Log("cooed_model TOUCH_UP");
                        sphere.SetActive(false);
                        onoff = false;
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
                    else if (result[0] == "TOUCH_DOWN")
                    {
                        Debug.Log("cooed_model TOUCH_DOWN");
                        if (Pointer == true)
                        {
                            sphere.SetActive(true);
                            onrunning = true;
                            onoff = true;
                        }
                        else
                        {
                            sphere.SetActive(false);
                            onrunning = true;
                            onoff = true;
                        }
                    }
                }

                

                if (result.Length != 5 || result[0] == "TOUCH_DOWN" || result[0] == "TOUCH_UP")
                {
                    ux = 100;
                    uy = 100;
                    return;
                }

                Material mat1 = sphere.GetComponent<Renderer>().material;

                if (result[0] == "0")
                {
                    if (HoverPointer == true)
                    {
                        mat1.color = new Color32(0, 0, 255, 255);
                    }
                    else
                    {
                        mat1.color = new Color32(0, 0, 0, 0);
                        //Debug.Log("coord_model  color is transparent");
                    }

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

                float ux0 = -1 * (xx / sizex - (float)0.5) * 2 * (float)0.0375 * (sizex / sizey) * magnification;
                float uy0 = -1 * (yy / sizey - (float)0.5) * 2 * (float)0.0375 * magnification;

                //float[] uxuy = pass_ux_uy_from_coord();
                //float ux0 = uxuy[0];
                //float uy0 = uxuy[1];

                ////////////////////////////////////////  補正のモデル用


                qx.Insert(0, ux0);
                qy.Insert(0, uy0);


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

                // 回帰で補正をする場合にUtoP == true，それ以外はfalse
                if (UtoP == true)
                {

                    if (onoff == true)
                    {
                        ux = ux0 + subx;
                        uy = uy0 + suby;
                    }
                    else
                    {
                        Debug.Log(string.Format("coord_model  ux0  {0}  uy0 {1}", ux0, uy0));

                        float[] pxpy = model_reg.GetComponent<ONNX_Regression>().ModelPredict(ux0,  uxs[1], uxs[2], uxs[3],uxs[4],uxs[5], uy0, uys[1], uys[2], uys[3],  uys[4],  uys[5]);

                        px = pxpy[0];
                        py = pxpy[1];

                        //ローカル座標を基準に、座標を取得
                        Vector3 prelocalPos = preSphere.transform.localPosition;

                        Debug.Log(string.Format("coord_model  before  {0}   {1}", prelocalPos.x, prelocalPos.y));

                        prelocalPos.x = px;
                        prelocalPos.y = py;

                        preSphere.transform.localPosition = prelocalPos;

                        Debug.Log(string.Format("coord_model  after   {0}   {1}", prelocalPos.x, prelocalPos.y));

                        subx = px - ux0;
                        suby = py - uy0;

                        ux = px;
                        uy = py;
                    }
                }
                else
                {
                    ux = ux0;
                    uy = uy0;

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

        //Debug.Log(model_class.GetComponent<ONNX_multi_classification>().ModelPredict(ux, uy, uxs[1], uys[1], uxs[2], uys[2], uxs[3], uys[3], uxs[4], uys[4], uxs[5], uys[5]));
        return model_class.GetComponent<ONNX_multi_classification>().ModelPredict(ux, uxs[1], uxs[2], uxs[3], uxs[4], uxs[5], uy, uys[1], uys[2], uys[3], uys[4], uys[5]);

    }

    public float[] pass_ux_uy_from_coord()
    {

        string andpos = server.get_coordinates();
        float startTime = Time.time;

        if (andpos != null)
        {

            while (startTime - Time.time <= 0.01) {

                string[] splitData = andpos.Split(' ');
                string[] result = Regex.Split(andpos, " ");

                if (result.Length == 5)
                {
                    float xx = Convert.ToSingle(result[1]);
                    float yy = Convert.ToSingle(result[2]);

                    // x : 倍率3.5でちょうど画面いっぱいでキーボードを網羅する ((x / maxx) - (float)0.5) * (float)3.5;
                    // y : 右式でちょうど画面いっぱいでキーボードを網羅する  (y / maxy - (float)0.5) * (float)5 - (float)0.5; 
                    //ux = (xx / maxx - (float)0.3) * (float)6;

                    float ux0 = -1 * (xx / sizex - (float)0.5) * 2 * (float)0.0375 * (sizex / sizey) * magnification;
                    float uy0 = -1 * (yy / sizey - (float)0.5) * 2 * (float)0.0375 * magnification;

               
                    return new float[] {ux0, uy0};
                }
            }
            
        }

        return new float[] { getUX(), getUY() };

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

    public void setHoverPointer(Boolean b)
    {
        HoverPointer = b;
    }

    public void setORT(int i)
    {
        out_range_times = i;
    }

    public void setMagnification(float f)
    {
        magnification = f;
    }


    //public void setPreCoordsSphere(float ux, float uy)
    //{

    //    PreCoordsSphere.SetActive(true);
    //    //ローカル座標を基準に、座標を取得
    //    Vector3 localPos = PreCoordsSphere.transform.localPosition;

    //    localPos.x = ux;
    //    localPos.y = uy;

    //    PreCoordsSphere.transform.localPosition = localPos;
    //}

    //public void setPreCoordsSphereColor(Color32 color32)
    //{
    //    Material mat = PreCoordsSphere.GetComponent<Renderer>().material;
    //    mat.color = color32;

    //}
}
