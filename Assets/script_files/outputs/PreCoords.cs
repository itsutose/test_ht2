using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System;
using System.Linq;
using System.Security.Cryptography;
using TMPro;

public class PreCoords : MonoBehaviour
{
    //public TextMeshProUGUI textobject;
    //public TextMeshProUGUI textsub;

    // file name path, others : file container
    private string folder_path;
    private string file_name;
    private string _ID;
    private int TestTimes = 10;


    // 実行時に制御するための変数
    private StreamWriter sw;

    private Boolean isFirst = true;
    private Boolean isEnd = false;

    private string file_path;
    private float preTime = 0;
    private float SumTime = 0;

    private bool started = false;

    private char word;
    private int i = 0;

    //private float[] rx;
    //private float[] ry;


// Start is called before the first frame update
//void Start()
public void SStart()
    {
        Debug.Log(string.Format("PreTest_output.SStart(): folder_path {0}, file_name {1}", folder_path, file_name));



        //////////////////////////// ランダムな float rx, float ry の行列を作成

        //rx = new float[TestTimes];
        //ry = new float[TestTimes];

        //System.Random randx = new System.Random();
        //System.Random randy = new System.Random();


        //for (int i = 0; i < rx.Length; i++)
        //{
        //    rx[i] = (float)(randx.NextDouble() * 0.16) - (float)0.08;
        //}

        //foreach (float num in rx)
        //{
        //    Console.WriteLine(num);
        //}

        //for (int i = 0; i < ry.Length; i++)
        //{
        //    ry[i] = (float)(rand.NextDouble() * 0.089) - 0.0445f;
        //}

        //foreach (float num in ry)
        //{
        //    Console.WriteLine(num);
        //}

        ////////////////////////////


        file_name = FileNameCheck(folder_path, file_name);
        file_name = file_name + ".csv";
        file_path = folder_path + "\\" + file_name;

        Debug.Log(string.Format("PreTest_output.SStart(): file_path {0}, ", file_path));

        // 第二引数をtrueにすると追加で書き込む
        // falseにすると全ての文章が書き換えられる
        sw = new StreamWriter(@file_path, true, Encoding.GetEncoding("Shift_JIS"));

        string[] s1 = { "ID", "rx", "ry", "ux", "uy" };
        string s2 = string.Join(",", s1);
        Debug.Log(s2);
        sw.WriteLine(s2);

        //Debug.Log(string.Format("ID {0}, rx {1}, ry {2}, ux {3}, uy {4}",
        //    ID, rx, ry, ux, uy));

    }

    void Update()
    {
        started = Application.isPlaying;

        //エンターキーが入力された場合「true」
        if (Input.GetKey(KeyCode.Backspace))
        {
            //オブジェクトを削除
            sw.WriteLine("interrupt");
            UnityEditor.EditorApplication.isPlaying = false;
            sw.Close();
        }
    }

    void OnDestroy()
    {
        if (started)
        {
            if (!Application.isPlaying)
            {
                // エディタ終了時の処理
                sw.WriteLine("interrupt");
                sw.Close();
                UnityEditor.EditorApplication.isPlaying = false;

            }
            else
            {
                // 普通のプレイ終了時の処理
                sw.WriteLine("interrupt");
                sw.Close();
                UnityEditor.EditorApplication.isPlaying = false;

            }
        }
    }

    public void Begin(float rx, float ry, float ux, float uy)
    {

        if (isFirst == true)
        {
            // この時点では i == 0
            //word = words2[i];
            //textobject.text = word.ToString();
            isFirst = false;

        }
        else
        {
            string[] s1 = { _ID, rx.ToString(), ry.ToString(),ux.ToString(), uy.ToString() };
            string s2 = string.Join(",", s1);

            sw.WriteLine(s2);

        }
    }

    private void Next()
    {
        //word = words2[i];
        // 次の文字を表示
        //textobject.text = word.ToString();
        //textsub.text = "Not touched yet.";
    }

    public void End()
    {

        if (isFirst == true)
        {

        }
        else
        {
            //string[] s1 = { _ID, KC, P.ToString(), KBF.ToString(), HCF.ToString(), _Distance.ToString(), "end", word.ToString(), ux.ToString(), uy.ToString(), "" };
            ////SumTime += DeltaTime;
            //string s2 = string.Join(",", s1);
            //sw.WriteLine(s2);

            i += 1;
            if (i <= TestTimes)
            {
                //word = words2[i];
                //// 次の文字を表示
                //textobject.text = word.ToString();

                //textsub.text = "Good!";

                Invoke("Next", 1);

            }
            else
            {
                // 普通のプレイ終了時の処理
                sw.WriteLine("End");
                //textobject.text = "End";
                sw.Close();
                UnityEditor.EditorApplication.isPlaying = false;

            }
        }
    }

    private string FileNameCheck(string folder_path, string file_name)
    {
        Boolean exist = false;
        int i = 1;
        string file_path = folder_path + "\\" + file_name + ".csv";

        int max_num = -1;
        string max_file = "";

        string[] files = Directory.GetFiles(folder_path, file_name + "*.csv");

        foreach (string file in files)
        {


            //Match match = Regex.Match(Path.GetFileName(file), @"test(\d+)\.csv");
            //Match match = Regex.Match(Path.GetFileName(file), @"{file_name}(\d+)\.csv");
            Match match = Regex.Match(Path.GetFileName(file), file_name + "(\\d+)\\.csv");
            if (match.Success)
            {
                int num = int.Parse(match.Groups[1].Value);
                if (num > max_num)
                {
                    max_num = num;
                    max_file = file;
                }
            }

            Debug.Log(string.Format("filecheck {0} , max_num {1}, max_file {2}", file, max_num, max_file));
        }

        if (max_num != -1)
        {
            Debug.Log(string.Format("{0}", folder_path + "\\" + file_name + (max_num + 1).ToString() + ".csv"));

            string result = file_name + (max_num + 1).ToString();

            return result;

        }
        else
        {
            Debug.Log("file_name is not exist");
            return file_name + "0";
        }

    }

    //public float getDistace()
    //{
    //    return _Distance;
    //}

    //public void setDistance(float f)
    //{
    //    _Distance = f;
    //}
    public void setfolder_path(string s)
    {
        folder_path = s;
    }
    public void setfile_name(string s)
    {
        file_name = s;
    }
    public void setID(string s)
    {
        _ID = s;
    }
    //public void setKC(string s)
    //{
    //    KC = s;
    //}
    //public void setP(Boolean tf)
    //{
    //    P = tf;
    //}
    //public void setKBF(Boolean tf)
    //{
    //    KBF = tf;
    //}
    //public void setHCF(Boolean tf)
    //{
    //    HCF = tf;
    //}
    public void setTestTimes(int i)
    {
        TestTimes = i;
    }
}
