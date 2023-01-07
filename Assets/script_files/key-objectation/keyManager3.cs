using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;

public class keyManager3 : MonoBehaviour
{

    // sphere��unity�ɂ�������W���擾����D
    // ����� MovePointer �ł��s���Ă��邪�C
    // �ʂ̃X�N���v�g�ɗ���̂͏����S���ƂȂ��C
    public coordinates coords;
    //public textSet textset;

    public Boolean color_feedback = true;
    public String KeyColor = "white";
  
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
 
    private bool onoff = false,onrunning = false;
    private int son = 0;
    private string keep_word = null;
    //private string word = null;

    private Boolean preonoff = false;

    private float xKeySize, yKeySize;

    // Start is called before the first frame update
    void Start()
    {
        if (dummy != null)
        {
            keylist = new GameObject[] { a, k, s, t, n, h, m, y, r, w, hen, backspace, space, enter, point, dummy };
            keylist2 = new GameObject[,] { { a, t, m, hen }, { k, n, y, w }, { s, h, r, point }, { backspace, space, enter, dummy } };
        }
        else
        {
            keylist = new GameObject[] { a, k, s, t, n, h, m, y, r, w, hen, backspace, space, enter, point};
        }
        float ncx = n.GetComponent<key2>().get_cx();
        float ncy = n.GetComponent<key2>().get_cy();

        float hcx = h.GetComponent<key2>().get_cx();
        float hcy = h.GetComponent<key2>().get_cy();

        float ycx = y.GetComponent<key2>().get_cx();
        float ycy = y.GetComponent<key2>().get_cy();

        xKeySize = hcx - ncx;
        yKeySize = ncy - ycy;

        //Debug.Log(string.Format("ncx : {0}, hcx : {1}", ncx, hcx));
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time <= 0.2 && Time.time > 0)
        {
            float ncx = n.GetComponent<key2>().get_cx();
            float ncy = n.GetComponent<key2>().get_cy();

            float hcx = h.GetComponent<key2>().get_cx();
            float hcy = h.GetComponent<key2>().get_cy();

            float ycx = y.GetComponent<key2>().get_cx();
            float ycy = y.GetComponent<key2>().get_cy();

            xKeySize = hcx - ncx;
            yKeySize = ncy - ycy;

            //Debug.Log(string.Format("ncx : {0}, hcx : {1}", ncx, hcx));
        }

        ux = coords.getUX();
        uy = coords.getUY();
        onoff = coords.getOnoff();
        onrunning = coords.getOnrunning();


        if (nowkey != null && onrunning == false)
        {
            up_touch(true);
            return;
        }

        if(onrunning == false)
        {
            return;
        }

        if (Time.time <= 1)
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
        else
        {
            // �z�o�[��


            // nowkey�͍�������Ă���i�Ⴕ���́C1 flame�O�܂ŉ�����Ă����j�L�[
            if(preonoff == false)
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
                            priorkey.GetComponent<key2>().rmcolor();
                        }

                        priorkey = key;

                        if (color_feedback == true)
                        {
                            priorkey.GetComponent<key2>().takecolor();
                        }

                        break;
                    }
                }
            }    // �w�𗣂����Ƃ�, 1�t���[���O�܂ł�on�Ȃ̂�preonoff == true
            else
            {
                //nowkey = null;
                if(nowkey == null)
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

    private void touch_action()
    {
        float cx = nowkey.GetComponent<key2>().get_cx();
        float cy = nowkey.GetComponent<key2>().get_cy();
        
        if (nowkey.GetComponent<key2>().isin(ux, uy) == true)
        {
            if (son != 0)
            {
                nowkey.GetComponent<key2>().rmcolor(son);
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
                    nowkey.GetComponent<key2>().rmcolor(son);
                }
                son = 4;
            }
            else // ���쐼
            {
                if (son != 1)
                {
                    nowkey.GetComponent<key2>().rmcolor(son);
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
                    nowkey.GetComponent<key2>().rmcolor(son);
                }
                son = 4;
            }
            else // ���쓌
            {
                if (son != 3)
                {
                    nowkey.GetComponent<key2>().rmcolor(son);
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
                    nowkey.GetComponent<key2>().rmcolor(son);
                }
                son = 2;
            }
            else // ���k��
            {
                if (son != 3)
                {
                    nowkey.GetComponent<key2>().rmcolor(son);
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
                    nowkey.GetComponent<key2>().rmcolor(son);
                }
                son = 2;
            }
            else // ���k��
            {
                if (son != 1)
                {
                    nowkey.GetComponent<key2>().rmcolor(son);
                }
                son = 1;
            }
        }

        keep_word = nowkey.GetComponent<key2>().takeword(son, textobject.text);

        if(color_feedback == true)
        {
            nowkey.GetComponent<key2>().takecolor(Color.yellow, son);
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
                }
                else
                {
                    Invoke("invoke", feed_back_time);
                }


                //if (KeyColor == "white")
                //{
                //    key.GetComponent<key2>().takecolor(new Color32(150, 150, 150, 250));
                //}
                //// �������ɂȂ�
                //else if (KeyColor == "transparent1" || KeyColor == "transparent2")
                //{
                //    key.GetComponent<key2>().takecolor(new Color32(255, 255, 255, 80));

                //}
                //// �����ɂȂ�
                //else if (KeyColor == "transparent3")
                //{
                //    key.GetComponent<key2>().takecolor(new Color32(255, 255, 255, 0));
                //}

                // priorkey�̓z�o�[�ł̓��͂�O���ɓ���Ă���
                if (priorkey != null)
                {
                    priorkey.GetComponent<key2>().rmcolor();

                }

            }
            else
            {
                // ����̃L�[���������ۂ̑��̃L�[�̐F�̕ω��Ɋւ���


                //Renderer mesh_obj = key.GetComponent<Renderer>();

                // color.a�̒l��0.6f�Ŕ������C1.0f�ɂ���Ɗ��S�ɓ���

                // �ȉ��̃R�[�h���Ǝ��E�S�̂𕢂�rect�ɐF������
                //key.GetComponent<MeshRenderer>().material.color = new Color32(150, 150, 150, 250);
                
                // �����ɂȂ�Ȃ��C���x�������邾��
                if (KeyColor == "white")
                {
                    key.GetComponent<key2>().takecolor(new Color32(150, 150, 150, 250));
                    //mesh_obj.material.color = new Color32(150, 150, 150, 255);
                }
                // �������ɂȂ�
                else if (KeyColor == "transparent1" || KeyColor == "transparent2")
                {
                    key.GetComponent<key2>().takecolor(new Color32(255, 255, 255, 80));

                }
                // �����ɂȂ�
                else if(KeyColor == "transparent3")
                {
                    key.GetComponent<key2>().takecolor(new Color32(255, 255, 255, 0));
                }
            }

        }
    }

    private void up_touch(Boolean oo)
    {
        foreach (GameObject key in keylist)
        {
            if (KeyColor == "white" || KeyColor == "transparent1")
            {
                key.GetComponent<key2>().takecolor(new Color32(255, 255, 255, 250));
            }
            // �������ɂȂ�
            else if (KeyColor == "transparent2")
            {
                key.GetComponent<key2>().takecolor(new Color32(255, 255, 255, 80));

            }
            // �����ɂȂ�
            else if (KeyColor == "transparent3")
            {
                key.GetComponent<key2>().takecolor(new Color32(255, 255, 255, 80));
            }

            //key.GetComponent<MeshRenderer>().material.color = new Color32(255, 255, 255, 0);
        }

        if (oo == true)
        {
            // �w�𗣂��Ƃ��C�ꏊ�̈�ɉ����ĕ������͂�����
            textobject.text = keep_word;

            int s_leng = keep_word.Length;
            if (s_leng >= 1)
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
}