using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.UI;  // �ǉ����܂��傤

public class keyManager2 : MonoBehaviour
{

    // sphere��unity�ɂ�������W���擾����D
    // ����� MovePointer �ł��s���Ă��邪�C
    // �ʂ̃X�N���v�g�ɗ���̂͏����S���ƂȂ��C


    public GameObject sphere;
    public TextMeshProUGUI textobject;
    public float feed_back_time = 0;
    public bool debug = true;

    public GameObject a, k, s, t, n, h, m, y, r, w, hen;
    key keyscript;

    private float ux, uy;
    private GameObject[] keylist;
    private GameObject
        nowkey = null, // ��������Ă���q���L�[
        //consonant = null, // �q���̃L�[�i�������ɑ��̎q���L�[�������Ȃ��悤�Ɂj
        priorkey = null; // ��O�̃L�[�i�F�̕ω����ɗp����j
    private bool onoff = false;
    private int son = 0;
    private string keep_word = null;

    // Start is called before the first frame update
    void Start()
    {
        ux = sphere.transform.position.x;
        uy = sphere.transform.position.y;

        keylist = new GameObject[] { a, k, s, t, n, h, m, y, r, w, hen };

    }

    // Update is called once per frame
    void Update()
    {

        if(Time.time <= 3)
        {
            if(debug == true)
            {
            Debug.Log("<=3");
            }

            
            return;
        }

        // ux,uy�ō��W���擾
        ux = sphere.transform.localPosition.x;
        uy = sphere.transform.localPosition.y;

        // onoff�i�ڐG���Ă��邩�ǂ����j���擾
        Material mat = sphere.GetComponent<Renderer>().material;
        if (mat.color == Color.red)
        {
            onoff = true;
        }
        else if (mat.color == Color.blue)
        {
            onoff = false;
        }

        if(onoff == true)
        {
            // �^�b�`��

            if(nowkey == null)
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
            if (nowkey == null)
            {

                foreach (GameObject key in keylist)
                {
                    // �w���W���L�[�̈���ɂ��邩�ǂ����C�Ȃ���Ύ��̃L�[��T��
                    if (key.GetComponent<key>().isin(ux, uy) == true)
                    {
                        // priorkey��key�i���݂�key�j�ł͂Ȃ�
                        // && priorkey��null��������Ȃ���null�Q�Ƃ���
                        if (priorkey != key && priorkey != null)
                        {
                            priorkey.GetComponent<key>().rmcolor();
                        }

                        priorkey = key;
                        priorkey.GetComponent<key>().takecolor();
                        
                        continue;
                    }

                }
            }    // �w�𗣂����Ƃ�
            else
            {
                // �w�𗣂��Ƃ��C�ꏊ�̈�ɉ����ĕ������͂�����
                textobject.text += keep_word;

                // �ꉹ�L�[�̔�\����
                nowkey.GetComponent<key>().rmcolor(son);
                nowkey.GetComponent<key>().in_visible_key();

                //// �F�����Ƃɖ߂�
                //priorkey.GetComponent<key>().rmcolor();
                //nowkey.GetComponent<key>().rmcolor();

                nowkey = null;

            }
        }

    }

    private void touch_action()
    {
        float cx = nowkey.GetComponent<key>().get_cx();
        float cy = nowkey.GetComponent<key>().get_cy();

        if (nowkey.GetComponent<key>().isin(ux, uy) == true)
        {
            if (son != 0)
            {
                nowkey.GetComponent<key>().rmcolor(son);
            }
            son = 0;
        }
        else if (ux <= cx && uy <= cy) // ��O�ی�
        {
            if (cx - ux < cy - uy) // ��쐼
            {
                if(son != 4)
                {
                    nowkey.GetComponent<key>().rmcolor(son);
                }
                son = 4;
            }
            else // ���쐼
            {
                if (son != 1)
                {
                    nowkey.GetComponent<key>().rmcolor(son);
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
                    nowkey.GetComponent<key>().rmcolor(son);
                }
                son = 4;
            }
            else // ���쓌
            {
                if (son != 3)
                {
                    nowkey.GetComponent<key>().rmcolor(son);
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
                    nowkey.GetComponent<key>().rmcolor(son);
                }
                son = 2;
            }
            else // ���k��
            {
                if (son != 3)
                {
                    nowkey.GetComponent<key>().rmcolor(son);
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
                    nowkey.GetComponent<key>().rmcolor(son);
                }
                son = 2;
            }
            else // ���k��
            {
                if (son != 1)
                {
                    nowkey.GetComponent<key>().rmcolor(son);
                }
                son = 1;
                //nowkey.GetComponent<key>().takecolor(Color.red, 1);
            }
        }

        keep_word = nowkey.GetComponent<key>().takeword(son);

        // if time.time�ł��������^�C�~���O��x�点�邱�Ƃ��\
        nowkey.GetComponent<key>().takecolor(Color.red, son);

        if (debug == true)
        {
        Debug.Log("keep_word ;" + keep_word);
        }

    }

    private void invoke()
    {
        nowkey.GetComponent<key>().visible_key();
    }

    private void set_touch()
    {
        foreach (GameObject key in keylist)
        {
            if (key.GetComponent<key>().isin(ux, uy) == true)
            {

                nowkey = key;
                //nowkey.GetComponent<key>().takecolor();

                if (feed_back_time == 0)
                {
                    nowkey.GetComponent<key>().visible_key();
                }else
                {
                    Invoke("invoke", feed_back_time);
                }

                //if (priorkey != key && priorkey != null)
                //{
                //    priorkey.GetComponent<key>().rmcolor();
                //}
                if (priorkey != null)
                {
                    priorkey.GetComponent<key>().rmcolor();
                }
                
                
                continue;
            }

        }
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


