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
    public int until = 26;

    private int i = 0;
    private Boolean isFirst = true;

    string[,] practice = {
        //{"Please input your name", "���O����͂��Ă�������" },
        {"�v���؂���K�v", "������������Ђ悤"},
          { "�������O��", "���傤���Ă�" },
          { "�}�g�R�ɓo��", "���΂���ɂ̂ڂ�" },
          { "���΃G�N�X�v���X", "���΂������Ղꂷ" },
          { "���[���𑗐M", "�߁[�����������"},
          { "��������肢", "��������˂���" },
          { "�������C�ɂȂ�", "�Â������ɂȂ�" },
          { "�݂�ȂŃ~�j�Q�[��","�݂�Ȃł݂ɂ��[��" },
          { "�v���O���~���O", "�Ղ낮��݂�" },
          { "���\��ɗՂ�", "�͂��҂傤�����ɂ̂���" },
          { "�y�[�W���߂���", "�؁[�����߂���"},
          { "���֗��s", "���񂾂��ւ�傱��" },
          { "�S�~��", "�ЂႭ���񂾂�" },
          { "����ς肢����","����ς肢����"},
          { "�����[����", "�݂����ق��イ����"},
          { "���s������", "�ȂׂԂ��傤������" },
          { "�p�\�R���͍���", "�ς�����͂�����"},
          { "���肪�Ƃ��Ƃ����܂�","���肪�Ƃ��������܂�"},
          { "�S���s���{��","���񂱂��Ƃǂ��ӂ���"},
          { "�L�^���k�߂�","���낭�������߂�"},
          { "�͂��ɂƂǂ܂�","�킸���ɂƂǂ܂�"},
          { "�}�b�g�^��������", "�܂��Ƃ���ǂ����Ƃ���"},
          { "����߂���","�䂴�߂���"},
          { "���Ղ����ǂ�", "�������Ƃ����ǂ�" },
          { "�V�����[�𗁂т�","�����[�����т�"},
          { "�������ʂ邭�Ȃ�","���䂪�ʂ邭�Ȃ�"},
          { "���ƎU��","���ʂƂ����"}};

    //{"�����K�i���̂ڂ�","�点�񂩂�������̂ڂ�"}

    //Start is called before the first frame update
    void Start()
    {
        //Debug.Log(practice.Length); // 54�Əo�Ă���
        //for(int i = 0 ;i < practice.Length / 2; i++)
        //{
        //    Debug.Log(practice[i, 1]);
        //}
        
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
        if(isFirst == true)
        {

        }
        else
        {
            string phrase = practice[i, 1];
            Debug.Log("InputWord,  phrase:" + phrase + ", word: " + word + ", ux: " + ux + ", uy: " + uy);
            csvOP.KeyInputSave(phrase, word, ux, uy);
        }
    }

    public void NextText(char word, float ux, float uy)
    {
        //if (i >= practice.Length / 2)
        //{
        //    ExampleText.text = "END";
        //    textobject.text = "";

        //    csvOP.csvClose();
        //    return;
        //}

        //string phrase = practice[i - 1, 1];
        //csvOP.EnterSave(phrase, word, ux, uy);

        //if(i == 3)
        //{
        //    csvOP.csvClose();
        //}


        //phrase = practice[i, 1];
        //Debug.Log("NextText,  phrase " + phrase + ", word :" + word + ", ux :" + ux + ", uy :" + uy);

        //textobject.text = "";

        //ExampleText.text = i.ToString() +  ". "+ practice[i,0] + "\n" + practice[i,1];

        //i += 1;
        string phrase = practice[i, 1];

        if (isFirst == true)
        {
            //phrase = practice[i, 1];
            Debug.Log("NextText,  phrase " + phrase + ", word :" + word + ", ux :" + ux + ", uy :" + uy);

            ExampleText.text = i.ToString() + ". " + practice[i, 0] + "\n" + practice[i, 1];
            textobject.text = "";

            isFirst = false;
        }
        else
        {
            //string phrase = practice[i, 1];
            csvOP.EnterSave(phrase, word, ux, uy);
            // ======================= �����܂ł�i��ڂ̍�� ========================

            i += 1;
            ExampleText.text = i.ToString() + ". " + practice[i, 0] + "\n" + practice[i, 1];
            textobject.text = "";

            if (until == i)
            {
                csvOP.csvClose();
            }


            if (i >= practice.Length / 2)
            {
                ExampleText.text = "END";
                //textobject.text = "";

                csvOP.csvClose();
                return;
            }
        }
    }

}