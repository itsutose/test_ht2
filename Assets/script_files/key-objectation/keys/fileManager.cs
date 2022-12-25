using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//// 対象ディレクトリのパス
//string path = @"C:\test\directoryinfo";
//// インスタンス生成
//DirectoryInfo directoryInfo = new(path);

//// FileInfo[] files = directoryInfo.GetFiles();
//IEnumerable<FileInfo> files = directoryInfo.EnumerateFiles();

//foreach (FileInfo file in files)
//{
//    Console.WriteLine(file.Name);
//}

public class fileManager
{
    public void Main()
    {
        string[] names = Directory.GetFiles(@".\\Assets\\CSV_output", "*");
        foreach (string name in names)
        {
            //Console.WriteLine(name);
            Debug.Log(name);
        }
    }

}