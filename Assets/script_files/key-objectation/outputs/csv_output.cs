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
    int[] num = { 0, 1, 2, 3, 4 };

    public string folder_path = "./Assets/CSV_output/";
    public string file_name = "ScapeGoat.txt";

    private string file_path;
    // Start is called before the first frame update
    void Start()
    {
        file_path = folder_path + file_name;

        //StreamWriter sw = new StreamWriter(file_path, false); // TextData.txt�Ƃ����t�@�C����V�K�ŗp��
        //StreamWriter sw = new StreamWriter("./Assets/TextData.txt", false); // TextData.txt�Ƃ����t�@�C����V�K�ŗp��
        StreamWriter sw = new StreamWriter("./Assets/CSV_output/ScapeGoat.txt", false); // TextData.txt�Ƃ����t�@�C����V�K�ŗp��

        foreach (int i in num)
        {
            sw.WriteLine(i);// �t�@�C���ɏ����o�������Ɖ��s
        }

        sw.WriteLine("�����");

        sw.Flush();
        sw.Close();

    }

    void Update()
    {
        //if()
    }


}