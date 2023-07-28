using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;

public class textSet : MonoBehaviour
{

    public Boolean random = false;
    public TextMeshProUGUI ExampleText;
    public TextMeshProUGUI textobject;
    public csv_output csvOP;
    //public int until = 26;

    private int i = 0;
    private Boolean isFirst = true;

    string[,] practice = {
        //{"Please input your name", "���O����͂��Ă�������" },
          {"0","�v���؂���K�v", "������������Ђ悤"},
          {"1","�������O��", "���傤���Ă�"},
          {"2","�}�g�R�ɓo��", "���΂���ɂ̂ڂ�"},
          {"3","���΃G�N�X�v���X", "���΂������Ղꂷ"},
          {"4","���[���𑗐M", "�߁[�����������"},
          {"5","��������肢", "��������˂���" },
          {"6","�������C�ɂȂ�", "�Â������ɂȂ�" },
          {"7","�݂�ȂŃ~�j�Q�[��","�݂�Ȃł݂ɂ��[��" },
          {"8","�v���O���~���O", "�Ղ낮��݂�" },
          {"9","���\��ɗՂ�", "�͂��҂傤�����ɂ̂���" },
          {"10","�y�[�W���߂���", "�؁[�����߂���"},
          {"11","���֗��s", "���񂾂��ւ�傱��" },
          {"12","�S�~��", "�ЂႭ���񂾂�" },
          {"13","����ς肢����","����ς肢����"},
          {"14","�����[����", "�݂����ق��イ����"},
          {"15","���s������", "�ȂׂԂ��傤������" },
          {"16","�p�\�R���͍���", "�ς�����͂�����"},
          {"17","���肪�Ƃ��Ƃ����܂�","���肪�Ƃ��������܂�"},
          {"18","�S���s���{��","���񂱂��Ƃǂ��ӂ���"},
          {"19","�L�^���k�߂�","���낭�������߂�"},
          {"20","�͂��ɂƂǂ܂�","�킸���ɂƂǂ܂�"},
          {"21","�}�b�g�^��������", "�܂��Ƃ���ǂ����Ƃ���"},
          {"22","����߂���","�䂴�߂���"},
          {"23","���Ղ����ǂ�", "�������Ƃ����ǂ�" },
          {"24","�V�����[�𗁂т�","�����[�����т�"},
          {"25","�������ʂ邭�Ȃ�","���䂪�ʂ邭�Ȃ�"},
          {"26","���ƎU��","���ʂƂ����"}};

    //{"�����K�i���̂ڂ�","�点�񂩂�������̂ڂ�"}

    //Start is called before the first frame update
    void Start()
    {
        //Debug.Log(practice.Length); // 54�Əo�Ă���
        //for(int i = 0 ;i < practice.Length / 2; i++)
        //{
        //    Debug.Log(practice[i, 1]);
        //}
        //Debug.Log(practice.Length/2);

        //System.Random random = new System.Random();
        //words2 = .OrderBy(x => random.Next()).ToList();

        System.Random rnd = new System.Random();
        int rows = practice.GetLength(0);
        int cols = practice.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            int randomIndex = rnd.Next(rows);
            Debug.Log("randomIndex : "+randomIndex);
            for (int j = 0; j < cols; j++)
            {
                string temp = practice[i, j];
                practice[i, j] = practice[randomIndex, j];
                practice[randomIndex, j] = temp;
            }
        }

        //foreach(var line in practice)
        //{
        //    Debug.Log(string.Join(",", line));
        //}

        for (int i = 0; i < practice.GetLength(0); i++)
        {
            for (int j = 0; j < practice.GetLength(1); j++)
            {
                string element = practice[i, j];
                Debug.Log(element);
            }
        }

    }


    //// update is called once per frame
    //void update()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        NextText();
    //    }
    //}

    //public IEnumerable NextText()
    //{
    //    foreach(var phrases in practice)
    //    {
    //        ExampleText.text = phrases[0] + "\n" + phrases[1];
    //        yield return null;
    //    }
    //}

    public void InputWord(char word, float ux, float uy)
    {
        //if (i >= 1)
        //{
        //    string phrase = practice[i - 1, 1];
        //    Debug.Log("InputWord,  phrase:" + phrase + ", word: " + word + ", ux: " + ux + ", uy: " + uy);
        //    csvOP.KeyInputSave(phrase, word, ux, uy);
        //}
        if (isFirst == true)
        {

        }
        else
        {
            if (i < practice.GetLength(0))
            {
                string phrase = practice[i, 2];
                string original_num = practice[i, 0];
                Debug.Log("InputWord,  phrase:" + phrase + ", word: " + word + ", ux: " + ux + ", uy: " + uy);
                csvOP.KeyInputSave(phrase, word, ux, uy, original_num);
            }
            else
            {
                csvOP.KeyInputSave("dummy", 'd', 'd', 'd', "dummy");
            }
        }
    }

    public void NextText(char word, float ux, float uy)
    {
      
        Debug.Log(string.Format("textSet.NextText : {0}",Time.time));

        string phrase = "s";

        if (i < practice.GetLength(0))
        {
            phrase = practice[i, 2];
        }
        
        if (isFirst == true)
        {
            Debug.Log("NextText,  phrase " + phrase + ", word :" + word + ", ux :" + ux + ", uy :" + uy);
            csvOP.EnterSave(phrase, word, ux, uy);
            ExampleText.text = i.ToString() + ". " + practice[i, 1] + "\n" + practice[i, 2];
            textobject.text = "";

            isFirst = false;
        }
        else
        {

            if (i < practice.GetLength(0))
            {
                phrase = practice[i, 2];
                csvOP.EnterSave(phrase, word, ux, uy);
            }
            // ======================= �����܂ł�i��ڂ̍�� ========================

 

            //if (until == i)
            //{
            //    csvOP.csvClose();
            //}


            if (i >= practice.GetLength(0) && i <= practice.GetLength(0)+10)
            { 
                 ExampleText.text = "END";
                csvOP.EnterSave("dummy", 'd', 'd', 'd');

                return;
            }
            else if(i>= practice.GetLength(0)+5)
            { 
                csvOP.csvClose();
                return;
            }
            
            i += 1;
            
            if (i < practice.GetLength(0))
            {
                
                ExampleText.text = i.ToString() + ". " + practice[i, 1] + "\n" + practice[i, 2];
                textobject.text = "";
            }

        }
    }

}