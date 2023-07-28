using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System;

using UnityEngine.UI; 

public class csv_output : MonoBehaviour
{
    private string folder_path;
    private string file_name;
    private string _ID;
    private string _InputType;
    private string _Distance;

    private StreamWriter sw;

    private Boolean isFirst = true;
    private string file_path;
    private float preTime = 0;
    private float SumTime = 0;

    private int i = 0;
    private int now_time = 0;

    // Start is called before the first frame update
    public void SStart()
    {
        //file_path = folder_path + "\\" + file_name;

        file_name = FileNameCheck(folder_path, file_name);
        file_name = file_name + ".csv";
        file_path = folder_path + "\\" + file_name;

        //Debug.Log(file_path);

        // 第二引数をtrueにすると追加で書き込む
        // falseにすると全ての文章が書き換えられる
        //sw = new StreamWriter(@".\\Assets\\CSV_output\\SaveData4.csv", true, Encoding.GetEncoding("Shift_JIS"));
        sw = new StreamWriter(@file_path, true, Encoding.GetEncoding("Shift_JIS"));

        string[] s1 = { "participant","method", "session","isPractice", "phrase", "phraseNumber", "phraseCount", "keystroke", "timestamp"};
        string s2 = string.Join(",", s1);
        Debug.Log(s2);
        sw.WriteLine(s2);

    }

    void Update()
    {

        //エンターキーが入力された場合「true」
        if (Input.GetKey(KeyCode.Space))
        {
            //オブジェクトを削除
            sw.WriteLine("interrupt");
            csvClose();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    //public void KeyInputSave(string id, string InputType, string distance, string phrase, string word, string detectedKey, string ux, string uy)
    public void KeyInputSave(string phrase, char word, float ux, float uy, string original_num)
    {
        float DeltaTime;
        int millitime = (int)((Time.time - preTime) * 1000);


        string wo = word.ToString();

        if(wo == "o")
        {
            wo = "option";
        }else if(wo == "D")
        {
            wo = "backspace";
        }

        //string[] s1 = { id, InputType, distance, phrase, word, detectedKey, ux, uy, DeltaTime.ToString(), " "};
        //string[] s1 = { _ID, _InputType, _Distance, phrase, word.ToString(), ux.ToString(), uy.ToString(), DeltaTime.ToString(), " " };

        string[] s1 = { _ID, _InputType, "3" , "FALSE", phrase, original_num, now_time.ToString(), wo , millitime.ToString()};
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);
        sw.Flush();

    }

    //public void EnterSave(string id, string InputType, string distance, string phrase, string word, string detectedKey, string ux, string uy)
    public void EnterSave(string phrase, char word, float ux, float uy)
    {
        Debug.Log(string.Format("Enter.Save : {0}", Time.time));

        //sw.WriteLine();
        //sw.Flush();

        now_time += 1;
        preTime = Time.time;
        SumTime = 0;
        //isFirst = true;
    }

    void OnApplicationQuit()
    {
        csvClose();
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

            Debug.Log(string.Format("filecheck {0} , max_num {1}", file, max_num));
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


    public void csvClose()
    {
        sw.Close();
    }

    public void setFolderPath(string s)
    {
        folder_path = s;
    }

    public void setFileName(string s)
    {
        file_name = s;
    }

    public void setID(string s)
    {
        _ID = s;
    }

    public void setInputType(string s)
    {
        _InputType = s;
    }

    public void setDistance(string s)
    {
        _Distance = s;
    }

}