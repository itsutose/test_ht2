using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System;

using UnityEngine.UI;  // �ǉ�����
//public class csv_output : MonoBehaviour
//{
//    // Use this for initialization
//    void Start()
//    {
//        string filePath = Application.dataPath + @"\Scripts\File\WriteText1.txt";


//        "Assets\\script_files\\key-objectation\\outputs"

//        string myString1 = "����������\n����������\n";
//        File.AppendAllText(filePath, myString1);

//        string[] myStringArray = { "����������", "����������", "����������" };
//        File.WriteAllLines(filePath, myStringArray);

//        string myString2 = "����������\n����������\n����������\n�����Ă�\n�Ȃɂʂ˂�";
//        File.WriteAllText(filePath, myString2);

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}

public class csv_output : MonoBehaviour
{
    int[] num = { 0, 1, 2, 3, 4,5,5,56,7 };
    //C:\Users\t-yamaguchi\unity\test_ht2\Assets
    public string folder_path = ".\\Assets\\CSV_output";
    public string file_name = "ScapeGoat.txt";
    public string _ID;
    public string _InputType;
    public string _Distance;

    private StreamWriter sw;

    private Boolean isFirst = true;
    private string file_path;
    private float preTime = 0;
    private float SumTime = 0;

    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        file_path = folder_path + "\\" + file_name;

        //Debug.Log(file_path);

        // ��������true�ɂ���ƒǉ��ŏ�������
        // false�ɂ���ƑS�Ă̕��͂�������������
        //sw = new StreamWriter(@".\\Assets\\CSV_output\\SaveData4.csv", true, Encoding.GetEncoding("Shift_JIS"));
        sw = new StreamWriter(@file_path, true, Encoding.GetEncoding("Shift_JIS"));

        string[] s1 = { "ID","input type", "distance", "Phrase", "word", "ux", "uy", "delta time", "sum time" };
        string s2 = string.Join(",", s1);
        Debug.Log(s2);
        sw.WriteLine(s2);
        //foreach (int i in num)
        //{
        //    sw.WriteLine(i);// �t�@�C���ɏ����o�������Ɖ��s
        //}

        //sw.WriteLine("�����");

        //sw.Flush();
        //sw.Close();

    }

    void Update()
    {

        //�G���^�[�L�[�����͂��ꂽ�ꍇ�utrue�v
        if (Input.GetKey(KeyCode.Space))
        {
            //�I�u�W�F�N�g���폜
            sw.WriteLine("interrupt");
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    //public void KeyInputSave(string id, string InputType, string distance, string phrase, string word, string detectedKey, string ux, string uy)
    public void KeyInputSave(string phrase, char word, float ux, float uy)
    {
        float DeltaTime;

        if (isFirst == true)
        {
            DeltaTime = 0;
            preTime = Time.time;
            isFirst = false;
        }
        else
        {
            DeltaTime = Time.time - preTime;
            preTime = Time.time;
        }


        //string[] s1 = { id, InputType, distance, phrase, word, detectedKey, ux, uy, DeltaTime.ToString(), " "};
        string[] s1 = { _ID, _InputType, _Distance, phrase, word.ToString(), ux.ToString(), uy.ToString(), DeltaTime.ToString(), " " };
        SumTime += DeltaTime;
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);

        //i += 1;
        //if (i == 5)
        //{
        //    sw.Close();
        //}
    }

    //public void EnterSave(string id, string InputType, string distance, string phrase, string word, string detectedKey, string ux, string uy)
    public void EnterSave(string phrase, char word, float ux, float uy)
    {
        Debug.Log(string.Format("Enter.Save : {0}", Time.time));
        float DeltaTime = Time.time - preTime;

        SumTime += DeltaTime;
        string[] s1 = { _ID, _InputType, _Distance, phrase, word.ToString(), ux.ToString(), uy.ToString(), DeltaTime.ToString(), SumTime.ToString() };
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);

        preTime = Time.time;
        SumTime = 0;
        isFirst = true;
    }

    public void csvClose()
    {
        sw.Close();
    }

}