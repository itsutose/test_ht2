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

    public GameObject a, k, s, t, n, h, m, y, r, w, hen, point, enter,space, backspace,  dummy = null;


    // file name path, others : file container
    private string folder_path;
    private string file_name;
    private string _ID;
    private string KC;
    private Boolean P;
    private Boolean KBF;
    private Boolean HCF;
    private float _Distance;
    private int TestTimes = 10;


    // ���s���ɐ��䂷�邽�߂̕ϐ�
    private StreamWriter sw;

    private Boolean isFirst = true;
    private Boolean isEnd = false;

    private string file_path;
    private float preTime = 0;
    private float SumTime = 0;

    private bool started = false;

    private char word;
    private int i = 0;

    private char[] words = { '��','��','��','��','��',
                            '��','��','��','��','��',
                            '��','��','��','��','��',
                            '��','��','��','��','��',
                            '��','��','��','��','��',
                            '��','��','��','��','��',
                            '��','��','��','��','��',
                            '��','��','��',
                            '��','��','��','��','��',
                            '��','��','��',
                            '��','�[','�A','�B','�I','�H',
                            'E', 'S', 'B'}; // 55

    private char[] words1;
    private char[] words2;

    //private string[] aa = { "��", "��", "��", "��", "��" };
    //private string[] kk = { "��", "��", "��", "��", "��" };
    //private string[] ss = { "��", "��", "��", "��", "��" };
    //private string[] tt = { "��", "��", "��", "��", "��" };
    //private string[] nn = { "��", "��", "��", "��", "��" };
    //private string[] hh = { "��", "��", "��", "��", "��" };
    //private string[] mm = { "��", "��", "��", "��", "��" };
    //private string[] yy = { "��", "��", "��" };
    //private string[] rr = { "��", "��", "��", "��", "��" };
    //private string[] ww = { "��", "��", "��", "�[" };
    //private string[] hhen = { "��" };
    //private string[] pp = { "�A", "�B", "�I", "�H" };
    //private string[] EE = { "E" };
    //private string[] SS = { "S" };
    //private string[] BB = { "B" }; // 55

    private string[,] boin = {{ "��", "��", "��", "��", "��" },
                             { "��", "��", "��", "��", "��" },
                             { "��", "��", "��", "��", "��" },
                             { "��", "��", "��", "��", "��" },
                             { "��", "��", "��", "��", "��" },
                             { "��", "��", "��", "��", "��" },
                             { "��", "��", "��", "��", "��" },
                             { "��", "��", "��", "��", "��" },
                             { "��", "��", "��", "��", "��" },
                             { "��", "��", "��", "�[", "�[" },
                             { "��", "��", "��", "��", "��" },
                             { "�A", "�B", "�I", "�H", "?"},
                             { "E" , "E" , "E" , "E" , "E"  },
                             { "S","S","S","S","S" },
                             { "B","B","B","B","B" } };
        
    private GameObject[] keylist;


    // Start is called before the first frame update
    //void Start()
    public void SStart()
    {

        keylist = new GameObject[] { a, k, s, t, n, h, m, y, r, w, hen, point, enter, space, backspace };


        //Debug.Log(string.Format("PreTest_output.SStart(): folder_path {0}, file_name {1}", folder_path, file_name));

        words1 = words.Concat(words).ToArray();
        words2 = words1.Concat(words1).ToArray();

        //////////////////// words2�̓����������

        Dictionary<string, int> wordlist = new Dictionary<string, int>();

        foreach (string word in boin)
        {
            wordlist[word] = 0;
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

        //Console.WriteLine(wordlist.Count);
        Debug.Log(wordlist.Count);
        foreach (KeyValuePair<string, int> item in wordlist.OrderBy(i => i.Key))
        {
            //Console.WriteLine("{0}: {1}", item.Key, item.Value);
            Debug.Log(string.Format("{0}: {1}", item.Key, item.Value));
        }

        ////////////////////

        System.Random random = new System.Random();
        words2 = words2.OrderBy(x => random.Next()).ToArray();
        
        file_name = FileNameCheck(folder_path, file_name);
        file_name = file_name + ".csv";
        file_path = folder_path + "\\" + file_name;

        Debug.Log(string.Format("PreTest_output.SStart(): file_path {0}, ",file_path));
        
        // ��������true�ɂ���ƒǉ��ŏ�������
        // false�ɂ���ƑS�Ă̕��͂�������������
        sw = new StreamWriter(@file_path, true, Encoding.GetEncoding("Shift_JIS"));

        string[] s1 = { "ID", "KC", "P", "KBF", "HCF", "Distance","BorE", "word", "ux", "uy", "delta time"};
        string s2 = string.Join(",", s1);
        Debug.Log(s2);
        sw.WriteLine(s2);

        //Debug.Log(string.Format("KC {0}, P {1}, KBF {2}, HCF {3}, Distance {4}, TTs {5}, FP {6}, FN {7}",
        //    KC, P, KBF, HCF, _Distance, TestTimes, file_path, file_name));

    }

    void Update()
    {
        started = Application.isPlaying;

        //�G���^�[�L�[�����͂��ꂽ�ꍇ�utrue�v
        if (Input.GetKey(KeyCode.Backspace))
        {
            //�I�u�W�F�N�g���폜
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
                // �G�f�B�^�I�����̏���
                sw.WriteLine("interrupt");
                sw.Close();
                UnityEditor.EditorApplication.isPlaying = false;

            }
            else
            {
                // ���ʂ̃v���C�I�����̏���
                sw.WriteLine("interrupt");
                sw.Close();
                UnityEditor.EditorApplication.isPlaying = false;

            }
        }
    }

    public void Begin(float ux, float uy)
    {
        float DeltaTime;

        if (isFirst == true)
        {
            // ���̎��_�ł� i == 0
            word = words2[i];
            textobject.text = word.ToString();
            DeltaTime = 0;
            preTime = Time.time;
            isFirst = false;

        }
        else
        {
            DeltaTime = Time.time - preTime;
            preTime = Time.time;
            string[] s1 = { _ID, KC, P.ToString(), KBF.ToString(), HCF.ToString(), _Distance.ToString(), "begin", word.ToString(), ux.ToString(), uy.ToString(), DeltaTime.ToString() };
            SumTime += DeltaTime;
            string s2 = string.Join(",", s1);

            sw.WriteLine(s2);

        }   
    }

    private void Next()
    {
        word = words2[i];
        // ���̕�����\��
        textobject.text = word.ToString();


        // ���̉����L�[�̐F��ς���
        NextKey(word.ToString());

        textsub.text = "Not touched yet.";
    }

    private void NextKey(string word)
    {

        foreach (GameObject key in keylist)
        {
            //key.GetComponent<key2>().takecolor(new Color32(255,255,255,250)," ");
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
                        keylist[i].GetComponent<key2>().takecolor(new Color32(255, 255, 0, 255), "�E");
                    }else if(j == 1)
                    {
                        keylist[i].GetComponent<key2>().takecolor(new Color32(255, 255, 0, 255), "��");
                    }
                    else if (j == 2)
                    {
                        keylist[i].GetComponent<key2>().takecolor(new Color32(255, 255, 0, 255), "��");
                    }
                    else if (j == 3)
                    {
                        keylist[i].GetComponent<key2>().takecolor(new Color32(255, 255, 0, 255), "��");
                    }
                    else if (j == 4)
                    {
                        keylist[i].GetComponent<key2>().takecolor(new Color32(255, 255, 0, 255), "��");
                    }
                    break;
                }
            }
        }
    }

    public void End(float ux, float uy)
    {
        float DeltaTime;

        if (isFirst == true)
        {
         
        }
        else
        {
            //DeltaTime = Time.time - preTime;
            //preTime = Time.time;   
            string[] s1 = { _ID, KC, P.ToString(), KBF.ToString(), HCF.ToString(), _Distance.ToString(), "end", word.ToString(), ux.ToString(), uy.ToString(), "" };
            //SumTime += DeltaTime;
            string s2 = string.Join(",", s1);
            sw.WriteLine(s2);
            
            i += 1;
            if (i <= TestTimes)
            {
                //word = words2[i];
                //// ���̕�����\��

                textsub.text = "Good!";
                Invoke("Next", 1);

            }
            else
            {
                // ���ʂ̃v���C�I�����̏���
                sw.WriteLine("End");
                textobject.text = "End";
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
            

            Match match = Regex.Match(Path.GetFileName(file), @"test(\d+)\.csv");
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

            //int num;

            //try
            //{
            //    string match = Regex.Match(max_file, @"[0-9]+").Value;

            //    num = int.Parse(match) + 1;
            //}
            //catch (Exception e)
            //{
            //    //UnityEditor.EditorApplication.isPlaying = false;
            //    //return e.ToString();
            //    num = 0;
            //}

            //Debug.Log("exist");
            //Debug.Log(num);
            //Debug.Log("num : " + num + " , num.Tostring() :" + num.ToString());
            //Debug.Log(file_name + num.ToString());
            //return file_name + num.ToString();

        }
        else
        {
            Debug.Log("file_name is not exist");
            return file_name + "0";
        }
        
        //catch (Exception e)
        //{
        //    UnityEditor.EditorApplication.isPlaying = false;
        //    return e.ToString();

        //}
    }

    public float getDistace()
    {
        return _Distance;
    }

    public void setDistance(float f)
    {
        _Distance = f;
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
