using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;

public class keyManager4 : MonoBehaviour
{

    // sphere��unity�ɂ�������W���擾����D
    // ����� MovePointer �ł��s���Ă��邪�C
    // �ʂ̃X�N���v�g�ɗ���̂͏����S���ƂȂ��C
    public coordinates coords;
    public PreTest_output pretest;
    //public textSet textset;
    private Boolean PreTest; //
    private Boolean HoverColorFeedback; //
    private Boolean KeyBoardFeedback; //
    private String KeyColor; //
    private int HowTransparent; //
    private int CloverTransparent; //

    public string pr = "center"; 

    public TextMeshProUGUI textobject;
    public float feed_back_time = 0;

    public GameObject a, k, s, t, n, h, m, y, r, w, hen, backspace, space, point, enter, dummy = null;

    // �L�[�{�[�h���͎��̃t�B�[�h�o�b�N�Ƃ��������͊֌W
    private float ux, uy;
    private GameObject[] keylist;
    private GameObject[,] keylist2;

    private GameObject
        nowkey = null, // ��������Ă���q���L�[
        priorkey = null; // ��O�̃L�[�i�F�̕ω����ɗp����j

    private bool onoff = false, onrunning = false;
    private int son = 0;
    private string keep_word = null;

    private Boolean preonoff = false;

    private float xKeySize, yKeySize;

    private Boolean pretestonoff = false;
    private Boolean pretestonrunning = false;

    private Boolean sstart = false;
    private String state;

    private float ux1, ux2, ux3, ux4, ux5;
    private float uy1, uy2, uy3, uy4, uy5;
    private float px = -1, py = -1;
    private int hovergap = 0;

    // Start is called before the first frame update
    public void SStart()
    {
        if (dummy != null)
        {
            keylist = new GameObject[] { a, k, s, t, n, h, m, y, r, w, hen, backspace, space, enter, point, dummy };
            keylist2 = new GameObject[,] { { a, t, m, hen }, { k, n, y, w }, { s, h, r, point }, { backspace, space, enter, dummy } };
        }
        else
        {
            keylist = new GameObject[] { a, k, s, t, n, h, m, y, r, w, hen, backspace, space, enter, point };
        }


        refresh();

        sstart = true;

        Debug.Log(string.Format("SStart   keyManager4   Pretest {0}, HCF {1}, KBF {2}, KC {3}, HT {4}, CT {5}",
            PreTest, HoverColorFeedback, KeyBoardFeedback, KeyColor, HowTransparent, CloverTransparent));
    }

    // Update is called once per frame
    void Update()
    {
        if(sstart == false)
        {

            return;
        }


        if (hovergap >= 5)
        {
            hovergap = 0;
        }
        else
        {
            hovergap += 1;
        }

        if (hovergap % 5 == 0)
        {
            ux5 = ux4;
            ux4 = ux3;
            ux3 = ux2;
            ux2 = ux1;
            ux1 = ux;

            uy5 = uy4;
            uy4 = uy3;
            uy3 = uy2;
            uy2 = uy1;
            uy1 = uy;
        }


        ux = coords.getUX();
        uy = coords.getUY();
        px = coords.getPX();
        py = coords.getPY();
        onoff = coords.getOnoff();
        onrunning = coords.getOnrunning();


        if (onrunning == false)
        {
            onoff = false;
        }

        if (PreTest == true)
        {
            //Debug.Log(string.Format("PTonoff : {0}, onrunning : {1}, onoff : {2}", pretestonoff, onrunning, onoff));

            // touch
            if (onrunning == true && onoff == true)
            {
                if(state == "hover")
                {
                    pretest.Begin(ux, uy, ux1, uy1, ux2, uy2, ux3, uy3, ux4, uy4, ux5, uy5, px, py);
                }
                else if(state == "out")
                {
                    pretest.Begin(ux, uy, ux1, uy1, ux2, uy2, ux3, uy3, ux4, uy4, ux5, uy5, px, py);
                }

                state = "touch";
            }
            // out
            else if (onrunning == false && onoff == false)
            {
                // touch -> out
                if(state == "touch")
                {
                    pretest.End(ux,uy, px,py);
                }
                
                // touch -> hover
                else if(state == "hover")
                {
                    // �����Ȃ�
                }

                state = "out";

                ux5 = -1;
                ux4 = -1;
                ux3 = -1;
                ux2 = -1;
                ux1 = -1;

                uy5 = -1;
                uy4 = -1;
                uy3 = -1;
                uy2 = -1;
                uy1 = -1;
            }
            // hover
            else if (onrunning == true && onoff == false)
            {
                // touch -> hover
                if(state == "touch")
                {
                    pretest.End(ux,uy,px,py);

                }
                // out -> hover
                else if(state == "out"){
                    // �������Ȃ�
                }
                state = "hover";
            }
        }

        if (HoverColorFeedback == true && KeyBoardFeedback == true)
        {
            if (nowkey != null && onrunning == false)
            {
                up_touch(true);

                return;
            }

            if (onrunning == false)
            {
                return;
            }

            if (onoff == true)
            {
                // �^�b�`��
                // 1�t���[���O�܂ŗ���Ă����w��on�Ƃ���
                preonoff = true;

                if (nowkey == null)
                {
                    set_touch();
                }
                else
                {
                    touch_action();
                }
            }
            // �z�o�[��
            else
            {
                // nowkey�͍�������Ă���i�Ⴕ���́C1 flame�O�܂ŉ�����Ă����j�L�[
                if (preonoff == false)
                {
                    foreach (GameObject key in keylist)
                    {
                        // �w���W���L�[�̈���ɂ��邩�ǂ����C�Ȃ���Ύ��̃L�[��T��
                        if (key.GetComponent<key2>().isin(ux, uy) == true)
                        {
                            // priorkey��key�i���݂�key�j�ł͂Ȃ�
                            // && priorkey��null��������Ȃ���null�Q�Ƃ���
                            if (priorkey != key && priorkey != null)
                            {
                                //priorkey.GetComponent<key2>().rmcolor();
                                rmcolor(priorkey, HowTransparent);
                            }

                            priorkey = key;

                            if (HoverColorFeedback == true)
                            {
                                priorkey.GetComponent<key2>().takecolor();
                            }
                            return;
                        }
                    }

                    //rmcolor(priorkey, HowTransparent);
                }
                // �w�𗣂����Ƃ�, 1�t���[���O�܂ł�on�Ȃ̂�preonoff == true
                else
                {

                    if (nowkey == null)
                    {
                        up_touch(false);
                    }
                    else
                    {
                        up_touch(true);
                    }
                    preonoff = false;
                }
            }
        }
    }

    private void touch_action()
    {
        float cx = nowkey.GetComponent<key2>().get_cx();
        float cy = nowkey.GetComponent<key2>().get_cy();

        if (nowkey.GetComponent<key2>().isin(ux, uy) == true)
        {
            if (son != 0)
            {
                //nowkey.GetComponent<key2>().rmcolor(son);
                takecolor(nowkey, CloverTransparent, son);
            }
            son = 0;
        }

        // �㉺���E�̂ǂ��Ƀ^�b�`���Ă��邩�𔻒f���Cson�Ƃ����ϐ�
        else if (ux <= cx && uy <= cy) // ��O�ی�
        {
            if (cx - ux < cy - uy) // ��쐼
            {
                if (son != 4)
                {
                    //nowkey.GetComponent<key2>().rmcolor(son);
                    if (son == 0)
                    {
                        takecolor(nowkey, 80, son);
                    }
                    else
                    {
                        takecolor(nowkey, CloverTransparent, son);
                    }
                }
                son = 4;
            }
            else // ���쐼
            {
                if (son != 1)
                {
                    //nowkey.GetComponent<key2>().rmcolor(son);
                    if (son == 0)
                    {
                        takecolor(nowkey, 80, son);
                    }
                    else
                    {
                        takecolor(nowkey, CloverTransparent, son);
                    }
                }
                son = 1;
            }
        }
        else if (ux > cx && uy <= cy) // ���ی�
        {
            if (ux - cx < cy - uy) // ��쓌
            {
                if (son != 4)
                {
                    //nowkey.GetComponent<key2>().rmcolor(son);
                    if (son == 0)
                    {
                        takecolor(nowkey, 80, son);
                    }
                    else
                    {
                        takecolor(nowkey, CloverTransparent, son);
                    }
                }
                son = 4;
            }
            else // ���쓌
            {
                if (son != 3)
                {
                    //nowkey.GetComponent<key2>().rmcolor(son);
                    if (son == 0)
                    {
                        takecolor(nowkey, 80, son);
                    }
                    else
                    {
                        takecolor(nowkey, CloverTransparent, son);
                    }
                }
                son = 3;
            }
        }
        else if (ux > cx && uy > cy) // ���ی�
        {
            if (ux - cx < uy - cy) // �k�k��
            {
                if (son != 2)
                {
                    //nowkey.GetComponent<key2>().rmcolor(son);
                    if (son == 0)
                    {
                        takecolor(nowkey, 80, son);
                    }
                    else
                    {
                        takecolor(nowkey, CloverTransparent, son);
                    }
                }
                son = 2;
            }
            else // ���k��
            {
                if (son != 3)
                {
                    //nowkey.GetComponent<key2>().rmcolor(son);
                    if (son == 0)
                    {
                        takecolor(nowkey, 80, son);
                    }
                    else
                    {
                        takecolor(nowkey, CloverTransparent, son);
                    }
                }
                son = 3;
            }
        }
        else if (ux <= cx && uy > cy) // ��l�ی�
        {
            if (cx - ux < uy - cy) // �k�k��
            {
                if (son != 2)
                {
                    //nowkey.GetComponent<key2>().rmcolor(son);
                    if (son == 0)
                    {
                        takecolor(nowkey, 80, son);
                    }
                    else
                    {
                        takecolor(nowkey, CloverTransparent, son);
                    }
                }
                son = 2;
            }
            else // ���k��
            {
                if (son != 1)
                {
                    //nowkey.GetComponent<key2>().rmcolor(son);
                    if (son == 0)
                    {
                        takecolor(nowkey, 80, son);
                    }
                    else
                    {
                        takecolor(nowkey, CloverTransparent, son);
                    }
                }
                son = 1;
            }
        }

        keep_word = nowkey.GetComponent<key2>().takeword(son, textobject.text);

        if (HoverColorFeedback == true)
        {
            // Color32(255,255,0,255) �͉��F
            nowkey.GetComponent<key2>().takecolor(new Color32(255, 255, 0, 255), son);
        }
    }

    private void invoke()
    {
        nowkey.GetComponent<key2>().visible_key();
    }

    private void set_touch()
    {
        foreach (GameObject key in keylist)
        {
            if (key.GetComponent<key2>().isin(ux, uy) == true)
            {
                nowkey = key;

                if (feed_back_time == 0)
                {
                    nowkey.GetComponent<key2>().visible_key();
                    takecolor(nowkey, CloverTransparent, -1);
                }
                else
                {
                    Invoke("invoke", feed_back_time);
                }

                // priorkey�̓z�o�[�ł̓��͂�O���ɓ���Ă���
                if (priorkey != null)
                {
                    rmcolor(priorkey, HowTransparent);
                }
                break;
            }
            //else
            //{
            //    // ����̃L�[���������ۂ̑��̃L�[�̐F�̕ω��Ɋւ���
            //    takecolor(key, HowTransparent);
            //}

            //takecolor(key, HowTransparent);
        }

        foreach (GameObject key in keylist)
        {
            takecolor(key, HowTransparent);
        }
    }

    private void rmcolor(GameObject k, int alpha)
    {
        Color32 white = new Color32(255, 255, 255, 250);
        Color32 Trans = new Color32(255, 255, 255, Convert.ToByte(alpha));

        // �����ɂȂ�Ȃ��C���x�������邾��
        if (KeyColor == "TP0" || KeyColor == "TP1")
        {
            k.GetComponent<key2>().takecolor(white);
        }
        // �������ɂȂ�
        else if (KeyColor == "TP2" || KeyColor == "TP3")
        {
            k.GetComponent<key2>().takecolor(Trans);

        }
    }

    private void takecolor(GameObject k, int alpha, int a = -2)
    {
        Color32 white = new Color32(255, 255, 255, 250);
        Color32 gray = new Color32(191, 191, 191, 250);
        Color32 Trans = new Color32(255, 255, 255, Convert.ToByte(alpha));
        Color32 Full = new Color32(255, 255, 255, 0);

        if (a != -2)
        {
            //if(KeyColor == "TP3")
            //{
            //    k.GetComponent<key2>().takecolor(Trans, all);
            //}
            // �����ɂȂ�Ȃ��C���x�������邾��
            if (KeyColor == "TP0" || KeyColor == "TP1" || KeyColor == "TP2")
            {
                k.GetComponent<key2>().takecolor(white, a);
            }
            // �������ɂȂ�
            else if (KeyColor == "TP3")
            {
                //k.GetComponent<key2>().takecolor(Full);
                k.GetComponent<key2>().takecolor(Trans, a);
            }
        }
        else
        {
            // �����ɂȂ�Ȃ��C���x�������邾��
            if (KeyColor == "TP0")
            {
                k.GetComponent<key2>().takecolor(gray);
            }
            // �������ɂȂ�
            else if (KeyColor == "TP1" || KeyColor == "TP2")
            {
                k.GetComponent<key2>().takecolor(Trans);
            }
            // �����ɂȂ�
            else if (KeyColor == "TP3")
            {
                k.GetComponent<key2>().takecolor(Full);
            }
        }
    }

    private void up_touch(Boolean oo)
    {
        foreach (GameObject key in keylist)
        {
            rmcolor(key, HowTransparent);
        }

        if (oo == true)
        {

            // �w�𗣂��Ƃ��C�ꏊ�̈�ɉ����ĕ������͂�����
            textobject.text = keep_word;

            int s_leng = keep_word.Length;
            if (s_leng >= 1 && PreTest == false)
            {
                char last_word = keep_word[s_leng - 1];
                nowkey.GetComponent<key2>().InputWordtoCSV(last_word);
            }

            // �ꉹ�L�[�̔�\����
            nowkey.GetComponent<key2>().rmcolor(son);
            nowkey.GetComponent<key2>().in_visible_key();
        }

        nowkey = null;

    }

    bool range(float a, float b, float c)
    {
        if (b <= a && a <= c)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Boolean getPreTest()
    {
        return PreTest;
    }

    public void setPreTest(Boolean tf)
    {
        PreTest = tf;
    }

    public Boolean getHoverColorFeedback()
    {
        return HoverColorFeedback;
    }

    public void setHoverColorFeedback(Boolean tf)
    {
        HoverColorFeedback = tf;
    }

    public Boolean setKeyBoardFeedback()
    {
        return KeyBoardFeedback;
    }

    public void setKeyBoardFeedback(Boolean tf)
    {
        KeyBoardFeedback = tf;
    }

    public String getKeyColor()
    {
        return KeyColor;
    }

    public void setKeyColor(String s)
    {
        KeyColor = s;
    }

    public int getHowTransparent()
    {
        return HowTransparent;
    }

    public void setHowTransparent(int i)
    {
        HowTransparent = i;
    }

    public int getCloverTransparent()
    {
        return CloverTransparent;
    }

    public void setCloverTransparent(int i)
    {
        CloverTransparent = i;
    }

    public void refresh()
    {
        foreach (GameObject key in keylist)
        {
            rmcolor(key, HowTransparent);
        }
    }
}