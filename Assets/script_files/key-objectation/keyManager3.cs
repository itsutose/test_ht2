using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;

using UnityEngine.UI;  // �ǉ����܂��傤

public class keyManager3 : MonoBehaviour
{

    // sphere��unity�ɂ�������W���擾����D
    // ����� MovePointer �ł��s���Ă��邪�C
    // �ʂ̃X�N���v�g�ɗ���̂͏����S���ƂȂ��C
    public ServerManager server;
    public Boolean color_feedback = true;
    public Boolean sphere_feedback = true;

    public GameObject sphere;
    public TextMeshProUGUI textobject;
    public float feed_back_time = 0;
    public int out_range_times = 50;

    public GameObject a, k, s, t, n, h, m, y, r, w, hen, backspace, space, point, enter;
    key keyscript;

    

    private float ux, uy;
    private GameObject[] keylist;
    private GameObject
        nowkey = null, // ��������Ă���q���L�[
        priorkey = null; // ��O�̃L�[�i�F�̕ω����ɗp����j
    private bool onoff = false;
    private int son = 0;
    private string keep_word = null;
    private float maxx = 1900, maxy = 1072; // ���ix�j�͗ǂ�����
    private string andpos;

    private string now_time = "00:00:00.000", last_time = "00:00:00.000";
    private int same_times_count = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        ux = sphere.transform.position.x;
        uy = sphere.transform.position.y;

        keylist = new GameObject[] { a, k, s, t, n, h, m, y, r, w, hen, backspace, space, point, enter};

    }

    // Update is called once per frame
    void Update()
    {

        //if (server != null && andpos != null)
        //{
        //    andpos = server.get_coordinates();
        //    //if (andpos == "touch_off")
        //    //{
        //    //    Debug.Log("touch off if called aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

        //    //    sphere.SetActive(false);

        //    //    return;
        //    //}

        //    string[] result = Regex.Split(andpos, " ");
            
        //    if (result.Length != 5)
        //    {
        //        return;
        //    }


        //    Material mat1 = sphere.GetComponent<Renderer>().material;

        //    if (result[0] == "0")
        //    {
        //        mat1.color = Color.blue;
        //    }
        //    else if (result[0] == "1")
        //    {
        //        mat1.color = Color.red;
        //    }


        //    float x = Convert.ToSingle(result[1]);
        //    float y = Convert.ToSingle(result[2]);

        //    // ���[�J�����W����ɁA���W���擾
        //    Vector3 localPos = sphere.transform.localPosition;

        //    // x : �{��3.5�ł��傤�ǉ�ʂ����ς��ŃL�[�{�[�h��ԗ����� ((x / maxx) - (float)0.5) * (float)3.5;
        //    // y : �E���ł��傤�ǉ�ʂ����ς��ŃL�[�{�[�h��ԗ�����  (y / maxy - (float)0.5) * (float)5 - (float)0.5; 

        //    localPos.x = ((x / maxx) - (float)0.5) * (float)4.5;
        //    localPos.y = (y / maxy - (float)0.5) * (float)6 - (float)0.5;

        //    sphere.transform.localPosition = localPos;

        //}


        //if (Time.time <= 3)
        //{
        //    //Debug.Log("<=3");
        //    sphere.SetActive(true);
        //    //return;
        //}else if ((Time.time > 3) && (Time.time < 8))
        //{
        //    sphere.SetActive(false);
        //    return;
        //}
        //else if (Time.time > 8)
        //{
        //    sphere.SetActive(true);
        //    //return;
        //}


        if (Time.time <= 3)
        {
            Debug.Log("<=3");
            return;
        }

        if (sphere_feedback == true)
        {
            
            last_time = now_time;
            
            if (sphere != null) { 
                now_time = sphere.GetComponent<MovePointer>().get_now_time(); 
            }

            //if(sphere.GetComponent<MovePointer>().get_now_time() == "")
            //{
            //    Debug.Log(0);
            //}
            //else
            //{
            //    Debug.Log(sphere.GetComponent<MovePointer>().get_now_time());
            //}

  

            //Debug.Log(sphere.GetComponent<MovePointer>().get_now_time())

            if (count_times(now_time, last_time) == true)
            {
                sphere.SetActive(false);
                Debug.Log("");
                return;

            }
            else
            {
                sphere.SetActive(true);
                Debug.Log("�����");
            }

            //if (prior_time != last_time)
            //{
            //    //sphere.SetActive(true);
            //    Debug.Log(1 + " p_time : " + prior_time + " ,  last_time : " + last_time);
            //}
            //else
            //{
            //    //sphere.SetActive(false);
            //    if(count_same_time(prior_time) == true)
            //    {
            //        Debug.Log()
            //    }
            //    Debug.Log(00 + " p_time : " + prior_time + " ,  last_time : " + last_time);
            //}

            if (Time.time <= 3)
            {
                Debug.Log("<=3");
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


            if (onoff == true)
            {
                // �^�b�`��

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
                if (nowkey == null)
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


                            continue;
                        }

                    }
                }    // �w�𗣂����Ƃ�
                else
                {
                    foreach (GameObject key in keylist)
                    {
                            key.GetComponent<MeshRenderer>().material.color = new Color32(255,255, 255, 0);
                    }


                    // �w�𗣂��Ƃ��C�ꏊ�̈�ɉ����ĕ������͂�����
                    textobject.text = keep_word;

                    // �ꉹ�L�[�̔�\����
                    nowkey.GetComponent<key2>().rmcolor(son);
                    nowkey.GetComponent<key2>().in_visible_key();

                    //// �F�����Ƃɖ߂�
                    //priorkey.GetComponent<key>().rmcolor();
                    //nowkey.GetComponent<key>().rmcolor();

                    nowkey = null;

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
                nowkey.GetComponent<key2>().rmcolor(son);
            }
            son = 0;
        }
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
            Renderer mesh_obj = key.GetComponent<Renderer>();

            if (key.GetComponent<key2>().isin(ux, uy) == true)
            {

                nowkey = key;
                //nowkey.GetComponent<key>().takecolor();

                if (feed_back_time == 0)
                {
                    nowkey.GetComponent<key2>().visible_key();
                }
                else
                {
                    Invoke("invoke", feed_back_time);
                }

                if (priorkey != null)
                {
                    priorkey.GetComponent<key2>().rmcolor();
                }


                continue;
            }
            else
            {

                //mesh_obj.material.color = new Color32(255, 255, 255, 80);
                key.GetComponent<MeshRenderer>().material.color = new Color32(150, 150, 150, 20);
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

    bool count_times(string nt, string lt)
    {
        if (nt == lt)
        {
            same_times_count += 1;
            if (same_times_count >= out_range_times)
            {
                return true;
            }
        }
        else
        {

            same_times_count = 0;
        }

        return false;
    }
}


