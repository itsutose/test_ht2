using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;

using UnityEngine.UI;  // �ǉ����܂��傤

public static class coordinates
{
    public static ServerManager server;
    public static string pr = "center";
    public static int out_range_times = 50;

    // server������W�Ƃ��������Ƃ�
    // ���android����̃f�[�^�̏����ɗp����
    static string andpos;
    static string now_time = "00:00:00.000", last_time = "00:00:00.000";
    static int same_times_count = 0;


    static float maxx = 1900, maxy = 1072; // ���ix�j�͗ǂ�����

    // ���̃X�N���v�g����̃A�N�Z�X�ɑ΂��Ă̕Ԃ��ϐ�
    static float ux, uy;
    static Boolean onoff = false;
    static Boolean onrunning = false;

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    static void Update()
    {
        if (server != null)
        {
            andpos = server.get_coordinates();
            if (andpos != null)
            {

                string[] result = Regex.Split(andpos, " ");

                if (result.Length != 5)
                {
                    return;
                }

                last_time = now_time;
                now_time = result[4];

                // �M������莞�ԑ����Ȃ������Ƃ�
                if (count_times(now_time, last_time) == true)
                {
                    //sphere.SetActive(false);

                    //if (nowkey != null)
                    //{
                    //    up_touch(true);
                    //}

                    onrunning = false;

                    return;

                }
                else
                {
                    //sphere.SetActive(true);
                    onrunning = true;
                }

                //Material mat1 = sphere.GetComponent<Renderer>().material;

                if (result[0] == "0")
                {
                    //mat1.color = Color.blue;
                    onoff = false;
                }
                else if (result[0] == "1")
                {
                    //mat1.color = Color.red;
                    onoff = true;
                }


                float xx = Convert.ToSingle(result[1]);
                float yy = Convert.ToSingle(result[2]);

                // x : �{��3.5�ł��傤�ǉ�ʂ����ς��ŃL�[�{�[�h��ԗ����� ((x / maxx) - (float)0.5) * (float)3.5;
                // y : �E���ł��傤�ǉ�ʂ����ς��ŃL�[�{�[�h��ԗ�����  (y / maxy - (float)0.5) * (float)5 - (float)0.5; 
                //ux = (xx / maxx - (float)0.3) * (float)6;

                if (pr == "center")
                {
                    ux = (xx / maxx - (float)0.5) * 2 * (float)2;
                    uy = (yy / maxy - (float)0.5) * (float)6 - (float)0.5;
                }
                else if (pr == "right")
                {
                    ux = (xx / maxx - (float)0.65) * (float)5.8;
                    uy = (yy / maxy - (float)0.5) * (float)6 - (float)0.5;
                }
                // ���[�J�����W����ɁA���W���擾
                //Vector3 localPos = sphere.transform.localPosition;

                //localPos.x = ux;
                //localPos.y = uy;

                //sphere.transform.localPosition = localPos;

            }

        }
    }

    static bool count_times(string nt, string lt)
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

    //public bool getOnoff()
    //{
    //    return onoff;
    //}

    //public bool getOnrunning()
    //{
    //    return onrunning;
    //}

    //public float getUX()
    //{
    //    return ux;
    //}

    //public float getUY()
    //{
    //    return uy;
    //}
}
