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
    private string practice_or_test = "test";
    //public int until = 26;

    private int i = 0;
    private Boolean isFirst = true;
    private Boolean Tpressed = false;

    string[,] phrases;

    //string[,] practice = {
    //    //{"Please input your name", "���O����͂��Ă�������" },
    //      {"0","�v���؂���K�v", "������������Ђ悤"}, // 10, 10
    //      {"1","�������O��", "���傤���Ă�"}, // 7, 8 
    //      {"2","�}�g�R�ɓo��", "���΂���ɂ̂ڂ�"}, // 9, 11
    //      {"3","���΃G�N�X�v���X", "���΂������Ղꂷ"}, // 9, 12
    //      {"4","���[���𑗐M", "�߁[�����������"}, // 8. 8
    //      {"5","��������肢", "��������˂���" }, // 7, 8
    //      {"6","�������C�ɂȂ�", "�Â������ɂȂ�" },
    //      {"7","�݂�ȂŃ~�j�Q�[��","�݂�Ȃł݂ɂ��[��" },
    //      {"8","�v���O���~���O", "�Ղ낮��݂�" },
    //      {"9","���\��ɗՂ�", "�͂��҂傤�����ɂ̂���" },
    //      {"10","�y�[�W���߂���", "�؁[�����߂���"},
    //      {"11","���֗��s", "���񂾂��ւ�傱��" },
    //      {"12","�S�~��", "�ЂႭ���񂾂�" },
    //      {"13","����ς肢����","����ς肢����"},
    //      {"14","�����[����", "�݂����ق��イ����"},
    //      {"15","���s������", "�ȂׂԂ��傤������" },
    //      {"16","�p�\�R���͍���", "�ς�����͂�����"},
    //      {"17","���肪�Ƃ��Ƃ����܂�","���肪�Ƃ��������܂�"},
    //      {"18","�S���s���{��","���񂱂��Ƃǂ��ӂ���"},
    //      {"19","�L�^���k�߂�","���낭�������߂�"},
    //      {"20","�͂��ɂƂǂ܂�","�킸���ɂƂǂ܂�"},
    //      {"21","�}�b�g�^��������", "�܂��Ƃ���ǂ����Ƃ���"},
    //      {"22","����߂���","�䂴�߂���"},
    //      {"23","���Ղ����ǂ�", "�������Ƃ����ǂ�" },
    //      {"24","�V�����[�𗁂т�","�����[�����т�"},
    //      {"25","�������ʂ邭�Ȃ�","���䂪�ʂ邭�Ȃ�"},
    //      {"26","���ƎU��","���ʂƂ����"}};


    string[,] practice = {
            {"0", "�ʖڂȂ�" ,"�߂�ڂ��Ȃ�" },
            {"1", "����ς肢����" ,"����ς肢����" },
            {"2", "��l���炵��" ,"�ЂƂ�ւ炵��" },
            {"3", "����������" ,"�������Ƃ�����" },
            {"4", "���΃G�N�X�v���X" ,"���΂������Ղꂷ" },
            {"5", "��c��" ,"���������イ" },
            {"6", "�v���؂���K�v" ,"������������Ђ悤" },
            {"7", "���肪�Ƃ��Ƃ����܂�" ,"���肪�Ƃ��������܂�" },
            {"8", "�S�~��" ,"�ЂႭ���񂾂�" },
            {"9", "�킩��܂���" ,"�킩��܂���" },
            {"10", "Uber Eats" ,"���[�΁[���[��" },
            {"11", "�}�g�R�ɓo��" ,"���΂���ɂ̂ڂ�" },
            {"12", "��������肢" ,"��������˂���" },
            {"13", "���|�[�g���ߐ؂�" ,"��ہ[�Ƃ��߂���" },
            {"14", "�e���Ȃ�" ,"���܂��Ȃ�" },
            {"15", "�����ɍs���܂�" ,"�����ɂ����܂�" },
            {"16", "���֗��s" ,"���񂾂��ւ�傱��" },
            {"17", "�v���O���~���O" ,"�Ղ낮��݂�" },
            {"18", "�����x��܂�" ,"������������܂�" },
            {"19", "���΂Ƀ��o��" ,"�������ɂ�΂�" },
            {"20", "���ꌂ����" ,"���ꂤ����" },
            {"21", "���[���𑗐M" ,"�߁[�����������" },
            {"22", "�ߌ�x�u" ,"�������イ����" },
            {"23", "�������C�ɂȂ�" ,"�Â������ɂȂ�" },
            {"24", "�C���^���N�V����" ,"���񂽂炭�����" },
            {"25", "�K�o�G�C�������" ,"���΂����ނ����" },
            {"26", "�}�g�ɂ��܂�" ,"���΂ɂ��܂�" },
            {"27", "�݂�ȂŃ~�j�Q�[��" ,"�݂�Ȃł݂ɂ��[��" },
            {"28", "�����Ƃ�" ,"�ق��Ƃ�" },
            {"29", "�����ł�" ,"��傤�����ł�" },
            {"30", "���\��" ,"�͂��҂傤����" },
            {"31", "�r�f�I�ԋp" ,"�тł��ւ񂫂Ⴍ" },
            {"32", "�_����ǂ�" ,"���Ԃ�����" },
            {"33", "�����C�͂�����" ,"���ӂ�͂�����" },
            {"34", "�y�j���݉�" ,"�ǂ悤�݂̂���"}};
        
    string[,] test = {
            {"0","�����낤", "���������Ƃ낤"},
            {"1","�V�����[�𗁂т�", "�����[�����т�"},
            {"2","�є������܂�", "�߂��ʂ����܂�"},
            {"3","���ƎU��", "���ʂƂ����"},
            {"4","���o��ł�", "���܂ł����ł�"},
            {"5","�񕜂��Ă�", "�����ӂ����Ă�"},
            {"6","���߂�", "�˂ނ���"},
            {"7","�X�}�u���ɏ�����", "���܂Ԃ�ɂ�����"},
            {"8","���₷�݂Ȃ���", "���₷�݂Ȃ���"},
            {"9","�����͋x��", "�������͂₷��"},
            {"10","�G������", "�Ă�������"},
            {"11","�������ʂ邭�Ȃ�", "���䂪�ʂ邭�Ȃ�"},
            {"12","������Ƒ҂���", "������Ƃ܂���"},
            {"13","�L�^���k�߂�", "���낭�������߂�"},
            {"14","���Ղ����ǂ�", "�������Ƃ����ǂ�"},
            {"15","���߂�Ȃ���", "���߂�Ȃ���"},
            {"16","���R�C������", "�肱���邹������"},
            {"17","��ɐH�ׂ�", "�����ɂ��ׂ�"},
            {"18","����߂���", "�䂴�߂���"},
            {"19","�}�b�g�^��������", "�܂��Ƃ���ǂ����Ƃ���"},
            {"20","�y�[�W���߂���", "�؁[�����߂���"},
            {"21","�������g���؂���", "�Ȃ����̂���������"},
            {"22","������", "�����ǂ�����"},
            {"23","���K���Ă���", "��񂵂イ���Ă���"},
            {"24","�e���Ȃ����Ă�", "���イ�����܂����Ă�"}};

    public void setIsPractice(Boolean io)
    {
        // practice_or_test �̒l�Ɋ�Â��ēK�؂Ȕz���I��
        if (io == true)
        {
            phrases = practice;
        }
        else
        {
            phrases = test;
        }
    }

    //void SomeFunction() // �K�؂Ȋ֐����ɕύX���Ă�������
    //{


    //    // practice_or_test �̒l�Ɋ�Â��ēK�؂Ȕz���I��
    //    if (practice_or_test == "practice")
    //    {
    //        phrases = practice;
    //    }
    //    else if (practice_or_test == "test")
    //    {
    //        phrases = test;
    //    }
    //    else
    //    {
    //        throw new Exception("Invalid value for practice_or_test");
    //    }

    //}

    //Start is called before the first frame update
    void Start()
    {
        //SomeFunction();


        System.Random rnd = new System.Random();
        int rows = phrases.GetLength(0);
        int cols = phrases.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            int randomIndex = rnd.Next(rows);
            Debug.Log("randomIndex : "+randomIndex);
            for (int j = 0; j < cols; j++)
            {
                string temp = phrases[i, j];
                phrases[i, j] = phrases[randomIndex, j];
                phrases[randomIndex, j] = temp;
            }
        }

        for (int i = 0; i < phrases.GetLength(0); i++)
        {
            for (int j = 0; j < phrases.GetLength(1); j++)
            {
                string element = phrases[i, j];
                Debug.Log(element);
            }
        }

    }


    public void InputWord(char word, float ux, float uy)
    {
        if (isFirst == true)
        {

        }
        else
        {
            if (i < phrases.GetLength(0))
            {
                string phrase = phrases[i, 2];
                string original_num = phrases[i, 0];
                Debug.Log("InputWord,  phrase:" + phrase + ", word: " + word + ", ux: " + ux + ", uy: " + uy);
                csvOP.KeyInputSave(phrase, word, ux, uy, original_num);
            }
            else
            {
                csvOP.KeyInputSave("dummy", 'd', 'd', 'd', "dummy");
            }
        }
    }

    // Unity��Update���\�b�h����T�L�[�������ꂽ���`�F�b�N
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // T�L�[�������ꂽ���`�F�b�N
        {
            Tpressed = true;
        }
    }

    public void NextText(char word, float ux, float uy)
    {
      
        Debug.Log(string.Format("textSet.NextText : {0}",Time.time));

        string phrase = "s";

        if (i < phrases.GetLength(0))
        {
            phrase = phrases[i, 2];
        }
        
        if (isFirst == true)
        {
            if (Tpressed == true)
            {
                Debug.Log("NextText,  phrase " + phrase + ", word :" + word + ", ux :" + ux + ", uy :" + uy);
                csvOP.EnterSave(phrase, word, ux, uy);
                ExampleText.text = i.ToString() + ". " + phrases[i, 1] + "\n" + phrases[i, 2];
                textobject.text = "";

                isFirst = false;

            }
        }
        else
        {

            if (i < phrases.GetLength(0))
            {
                phrase = phrases[i, 2];
                csvOP.EnterSave(phrase, word, ux, uy);
            }
            // ======================= �����܂ł�i��ڂ̍�� ========================

            if (i >= phrases.GetLength(0) && i <= phrases.GetLength(0)+10)
            { 
                 ExampleText.text = "END";
                csvOP.EnterSave("dummy", 'd', 'd', 'd');

                return;
            }
            else if(i>= phrases.GetLength(0)+5)
            { 
                csvOP.csvClose();
                return;
            }
            
            i += 1;
            
            if (i < phrases.GetLength(0))
            {
                
                ExampleText.text = i.ToString() + ". " + phrases[i, 1] + "\n" + phrases[i, 2];
                textobject.text = "";
            }

        }
    }

}