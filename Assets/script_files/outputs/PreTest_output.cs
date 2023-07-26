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
using System.Linq;

public class PreTest_output : MonoBehaviour
{
    public TextMeshProUGUI textobject;
    public TextMeshProUGUI textsub;

    public GameObject a, k, s, t, n, h, m, y, r, w, hen, point, enter, space, backspace, dummy = null;


    // file name path, others : file container
    private string folder_path;
    private string file_name;
    private string _ID;
    private int Mode;
    private string KC;
    private Boolean P;
    private Boolean HP;
    private Boolean KBF;
    private Boolean HCF;
    private float _Distance;
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

    private List<char> words = new List<char>{
                            'あ','い','う','え','お',
                            'か','き','く','け','こ',
                            'さ','し','す','せ','そ',
                            'た','ち','つ','て','と',
                            'な','に','ぬ','ね','の',
                            'は','ひ','ふ','へ','ほ',
                            'ま','み','む','め','も',
                            'や','ゆ','よ',
                            'ら','り','る','れ','ろ',
                            'わ','を','ん',
                            '変','ー','、','。','！','？',
                            'E', 'S', 'B'
    };

    private List<char> words75 = new List<char>{
                            'あ','い','う','え','お',
                            'か','き','く','け','こ',
                            'さ','し','す','せ','そ',
                            'た','ち','つ','て','と',
                            'な','に','ぬ','ね','の',
                            'は','ひ','ふ','へ','ほ',
                            'ま','み','む','め','も',
                            'や','ゆ','よ','や','ゆ',
                            'ら','り','る','れ','ろ',
                            'わ','を','ん','ー','ー',
                            '変','変','変','変','変',
                            '、','。','！','？','？',
                            'E', 'E', 'E', 'E', 'E',
                            'S', 'S', 'S', 'S', 'S',
                            'B', 'B', 'B', 'B', 'B'
    };

    //private List<char> words75 = new List<char>{
    //                        'れ','れ','れ','れ','れ',
    //                        'れ','れ','れ','れ','れ',
    //                        'れ','れ','れ','れ','れ',
    //                        'れ','れ','れ','れ','れ',
    //                        'れ','れ','れ','れ','れ',
    //                        'れ','れ','れ','れ','れ'
    //};

    //private List<char> words = new List<char> { 'な', 'に', 'わ', 'を', 'ん', 'ー', '、', '。', '！', '？', 'E', 'E', 'E', 'S', 'B', 'E', 'S', 'B', 'E', 'S','E', 'E', 'E', 'S', 'B', 'E', 'S', 'B', 'E', 'S', 'S', 'S', 'B', '変', '変', '変', '変', '変', '変', '変', '変', '変', 'や', 'ゆ', 'よ', 'や', 'ゆ', 'よ' };


    private List<char> words1;
    private List<char> words2;


    private string[,] boin = {{ "あ", "い", "う", "え", "お" },
                             { "か", "き", "く", "け", "こ" },
                             { "さ", "し", "す", "せ", "そ" },
                             { "た", "ち", "つ", "て", "と" },
                             { "な", "に", "ぬ", "ね", "の" },
                             { "は", "ひ", "ふ", "へ", "ほ" },
                             { "ま", "み", "む", "め", "も" },
                             { "や", "ゆ", "よ", "よ", "よ" },
                             { "ら", "り", "る", "れ", "ろ" },
                             { "わ", "を", "ん", "ー", "ー" },
                             { "変", "変", "変", "変", "変" },
                             { "、", "。", "！", "？", "？"},
                             { "E" , "E" , "E" , "E" , "E"  },
                             { "S","S","S","S","S" },
                             { "B","B","B","B","B" } };

    private float[,] keyCoords;
        
    private GameObject[] keylist;

    private float prerx, prery;

    // Start is called before the first frame update
    //void Start()
    public void SStart()
    {

        keylist = new GameObject[] { a, k, s, t, n, h, m, y, r, w, hen, point, enter, space, backspace };
        //GameObject[,] keylist2 = { { a, k, s, backspace }, { t, n, h, space }, { m, y, r, enter }, { hen, w, point, dummy } };
        //keyCoords = new float[,] {}

        //if (Mode == 0) {
        //    keyCoords = new float[,] { };
        //}
        //else if (Mode == 1)
        //{
        //    keyCoords = new float[,] { };
        //}
        //else(Mode == 2)
        //{
        //    keyCoords = new float[,] { };
        //}

        ///////////////////// 全体のキーの割合をそろえるための措置

        words1 = words.Concat(words).ToList();
        
        words2 = words1.Concat(words1).ToList();
        words2 = words2.Concat(words2).ToList();

        for (int i = 0; i < 1; i++)
        {
            words2.Add((char)('わ'));
            words2.Add((char)('を'));
            words2.Add((char)('ん'));
            words2.Add((char)('ー'));
            words2.Add((char)('。'));
            words2.Add((char)('、'));
            words2.Add((char)('？'));
            words2.Add((char)('！'));
        }


        for (int i = 0; i < 16; i++)
        {
            words2.Add((char)('変'));
            words2.Add((char)('S'));
            words2.Add((char)('B'));
            words2.Add((char)('E'));
        }

        //////////////////// words2の内訳を見たい

        Dictionary<string, int> wordlist = new Dictionary<string, int>();

        foreach (string word in boin)
        {
            wordlist[word.ToString()] = 0;
        }

        foreach (char word in words2)
        {
            if (!wordlist.ContainsKey(word.ToString()))
            {
                wordlist[word.ToString()] = 1;
            }
            else
            {
                wordlist[word.ToString()]++;
            }
        }

        Debug.Log(wordlist.Count);
        foreach (KeyValuePair<string, int> item in wordlist.OrderBy(i => i.Key))
        {
            Debug.Log(string.Format("{0}: {1}", item.Key, item.Value));
        }

        //////////////////// ランダムなword列を作成

        System.Random random = new System.Random();
        //words2 = words2.OrderBy(x => random.Next()).ToList();
        words2 = words75.OrderBy(x => random.Next()).ToList();


        file_name = FileNameCheck(folder_path, file_name);
        file_name = file_name + ".csv";
        file_path = folder_path + "\\" + file_name;

        Debug.Log(string.Format("PreTest_output.SStart(): file_path {0}, ",file_path));
        
        // 第二引数をtrueにすると追加で書き込む
        // falseにすると全ての文章が書き換えられる
        sw = new StreamWriter(@file_path, true, Encoding.GetEncoding("Shift_JIS"));

        string[] s1 = { "ID", 
            "Mode", 
            "KC", 
            "P",
            "HP",
            "KBF",
            "HCF",
            "Distance",
            "BorE",
            "word",
            "rx", 
            "ry",
            "ux",
            "ux1",
            "ux2",
            "ux3",
            "ux4",
            "ux5",
            "uy",
            "uy1",
            "uy2",
            "uy3",
            "uy4",
            "uy5",
            "px",
            "py",
            "delta time",
            "pre_rx",
            "pre_ry"};
        string s2 = string.Join(",", s1);
        Debug.Log(s2);
        sw.WriteLine(s2);



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

    public void Begin(float ux, float uy, float ux1, float uy1, float ux2, float uy2, float ux3, float uy3, float ux4, float uy4, float ux5, float uy5, float px, float py)
    {
        float DeltaTime;

        if (isFirst == true)
        {
            // この時点では i == 0
            word = words2[i];
            textobject.text = word.ToString();
            DeltaTime = 0;
            preTime = Time.time;
            isFirst = false;
            //prerx = ux;
            //prery = uy;

        }
        else
        {
            DeltaTime = Time.time - preTime;
            preTime = Time.time;

            float rx = getKeyRX(word);
            float ry = getKeyRY(word);
            
            string[] s1 = { _ID, Mode.ToString(), KC, P.ToString(),HP.ToString(), KBF.ToString(), HCF.ToString(), _Distance.ToString(), "begin", word.ToString(), 
                rx.ToString(),ry.ToString(),
                ux.ToString(),
                ux1.ToString(),
                ux2.ToString(),
                ux3.ToString(),
                ux4.ToString(),
                ux5.ToString(),
                uy.ToString(),
                uy1.ToString(),
                uy2.ToString(),
                uy3.ToString(),
                uy4.ToString(),
                uy5.ToString(),
                px.ToString(),
                py.ToString(),
                DeltaTime.ToString(),
                prerx.ToString(),
                prery.ToString()};

            SumTime += DeltaTime;
            string s2 = string.Join(",", s1);

            sw.WriteLine(s2);

        }   
    }



    public void End(float ux, float uy, float px, float py)
    {
        float DeltaTime;

        if (isFirst == true)
        {
            prerx = ux;
            prery = uy;
        }
        else
        {
            float rx = getKeyRX(word);
            float ry = getKeyRY(word);

            string[] s1 = { _ID, Mode.ToString(), KC, P.ToString(),HP.ToString(), KBF.ToString(), HCF.ToString(), _Distance.ToString(), "end", word.ToString(),
                rx.ToString(),ry.ToString(),
                ux.ToString(),
                "",
                "",
                "",
                "",
                "",
                uy.ToString(),
                "",
                "",
                "",
                "",
                "",
                px.ToString(),
                py.ToString(),
                "",
                prerx.ToString(),
                prery.ToString()
            };


            prerx = ux;
            prery = uy;

            string s2 = string.Join(",", s1);
            sw.WriteLine(s2);
            
            i += 1;
            if (i <= TestTimes)
            {
                //word = words2[i];
                //// 次の文字を表示

                textsub.text = "Good!";
                Invoke("Next", 1);

            }
            else
            {
                // 普通のプレイ終了時の処理
                sw.WriteLine("End");
                textobject.text = "End";
                sw.Close();
                UnityEditor.EditorApplication.isPlaying = false;

            }
        }       
    }

    private void Next()
    {
        word = words2[i];
        // 次の文字を表示
        textobject.text = word.ToString();


        // 次の押すキーの色を変える
        NextKey(word.ToString());

        textsub.text = "Not touched yet.";
    }

    private void NextKey(string word)
    {

        foreach (GameObject key in keylist)
        {
            key.GetComponent<key2>().takecolor(new Color32(255, 255, 255, 80)," ");
        }

        for (int i = 0; i < boin.GetLength(0); i++)
        {
            for (int j = 0; j < boin.GetLength(1); j++)
            {
                if (boin[i, j] == word)
                {
                    if (j == 0)
                    {
                        keylist[i].GetComponent<key2>().takecolor(new Color32(255, 255, 0, 255), "・");
                    }else if(j == 1)
                    {
                        keylist[i].GetComponent<key2>().takecolor(new Color32(255, 255, 0, 255), "←");
                    }
                    else if (j == 2)
                    {
                        keylist[i].GetComponent<key2>().takecolor(new Color32(255, 255, 0, 255), "↑");
                    }
                    else if (j == 3)
                    {
                        keylist[i].GetComponent<key2>().takecolor(new Color32(255, 255, 0, 255), "→");
                    }
                    else if (j == 4)
                    {
                        keylist[i].GetComponent<key2>().takecolor(new Color32(255, 255, 0, 255), "↓");
                    }
                    break;
                }
            }
        }
    }

    private float getKeyRX(char word)
    {
        for (int i = 0; i < boin.GetLength(0); i++)
        {
            for (int j = 0; j < boin.GetLength(1); j++)
            {
                if (boin[i, j] == word.ToString())
                {
                    return (float)keylist[i].GetComponent<key2>().get_cx();
                    //Debug.Log(keylist[i].GetComponent<key2>().get_cx());
                    //return ;
                }
            }
        }

        return 0f;
    }

    private float getKeyRY(char word)
    {
        for (int i = 0; i < boin.GetLength(0); i++)
        {
            for (int j = 0; j < boin.GetLength(1); j++)
            {
                if (boin[i, j] == word.ToString())
                {
                    return (float)keylist[i].GetComponent<key2>().get_cy();
                    //Debug.Log(keylist[i].GetComponent<key2>().get_cy());
                    //return 1f;
                }
            }
        }

        return 0f;
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

    public float getDistace()
    {
        return _Distance;
    }

    public void setDistance(float f)
    {
        _Distance = f;
    }
    public void setMode(int i)
    {
        Mode = i;
    }
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
    public void setKC(string s)
    {
        KC = s;
    }
    public void setP(Boolean tf)
    {
        P = tf;
    }
    public void setHP(Boolean tf)
    {
        HP = tf;
    }
    public void setKBF(Boolean tf)
    {
        KBF = tf;
    }
    public void setHCF(Boolean tf)
    {
        HCF = tf;
    }
    public void setTestTimes(int i)
    {
       TestTimes =  i;
    }
}
