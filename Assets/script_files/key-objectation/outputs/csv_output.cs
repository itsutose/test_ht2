using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//public class csv_output : MonoBehaviour
//{
//    // Use this for initialization
//    void Start()
//    {
//        string filePath = Application.dataPath + @"\Scripts\File\WriteText1.txt";


//        "Assets\\script_files\\key-objectation\\outputs"

//        string myString1 = "かきくけこ\nさしすせそ\n";
//        File.AppendAllText(filePath, myString1);

//        string[] myStringArray = { "あいうえお", "かきくけこ", "さしすせそ" };
//        File.WriteAllLines(filePath, myStringArray);

//        string myString2 = "あいうえお\nかきくけこ\nさしすせそ\nたちつてと\nなにぬねの";
//        File.WriteAllText(filePath, myString2);

//    }
    
//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}

public class csv_output : MonoBehaviour
{
    int[] num = { 0, 1, 2, 3, 4 };

    public string folder_path = "./Assets/CSV_output/";
    public string file_name = "ScapeGoat.txt";

    private string file_path;
    // Start is called before the first frame update
    void Start()
    {
        file_path = folder_path + file_name;

        //StreamWriter sw = new StreamWriter(file_path, false); // TextData.txtというファイルを新規で用意
        //StreamWriter sw = new StreamWriter("./Assets/TextData.txt", false); // TextData.txtというファイルを新規で用意
        StreamWriter sw = new StreamWriter("./Assets/CSV_output/ScapeGoat.txt", false); // TextData.txtというファイルを新規で用意

        foreach (int i in num)
        {
            sw.WriteLine(i);// ファイルに書き出したあと改行
        }

        sw.WriteLine("おわり");

        sw.Flush();
        sw.Close();

    }

    void Update()
    {
        //if()
    }


}