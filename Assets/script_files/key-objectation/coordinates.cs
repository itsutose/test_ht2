using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;

public class coordinates : MonoBehaviour
{
    public ServerManager server;
    public keyPosition keypos;
    public GameObject sphere;
    public string pr = "center";
    public Boolean Pointer = true;
    public int out_range_times = 50;
    public float magnification = 1;

    // server������W�Ƃ��������Ƃ�
    // ���android����̃f�[�^�̏����ɗp����
    string andpos;
    string now_time = "00:00:00.000", last_time = "00:00:00.000";
    int same_times_count = 0;

    float sizex = 1900, sizey = 1072; // ���ix�j�͗ǂ�����

    // ���̃X�N���v�g����̃A�N�Z�X�ɑ΂��Ă̕Ԃ��ϐ�
    float ux = 0, uy = 0;
    Boolean onoff = false;
    Boolean onrunning = false;
    private float ratio = 1;

    private float maxx = -100, maxy = -100, minx = 100, miny = 100;

    // Start is called before the first frame update
    void Start()
    {
        ratio = keypos.getRatio();
    }

    // Update is called once per frame
    void Update()
    {

        if (ratio == 1)
        {
            ratio = keypos.getRatio();
        }

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

                if (result[0] == "0")
                {
                    onoff = false;
                }
                else if (result[0] == "1")
                {
                    onoff = true;
                }


        ////////////////////////  ���������pointer��\�����邩�ǂ���

                if (Pointer == true)
                {

                    // �M������莞�ԑ����Ȃ������Ƃ�
                    if (count_times(now_time, last_time) == true)
                    {
                        sphere.SetActive(false);

                        onrunning = false;

                        return;

                    }
                    else
                    {
                        sphere.SetActive(true);
                        onrunning = true;
                    }

                    Material mat1 = sphere.GetComponent<Renderer>().material;

                    if (result[0] == "0")
                    {
                        mat1.color = Color.blue;
                    }
                    else if (result[0] == "1")
                    {
                        mat1.color = Color.red;
                    }
                }
                else
                {
                    sphere.SetActive(false);
                    onrunning = true;
                }
        ////////////////////////////// �����܂�

                //Debug.Log("coordinates has onoff : "+onoff);


                float xx = Convert.ToSingle(result[1]);
                float yy = Convert.ToSingle(result[2]);


                /////// �傫���𑪂�悤

                //if (xx > maxx)
                //{
                //    maxx = xx;
                //}

                //if (xx < minx)
                //{
                //    minx = xx;
                //}

                //if (yy > maxy)
                //{
                //    maxy = yy;
                //}

                //if (yy < miny)
                //{
                //    miny = yy;
                //}

                //Debug.Log("minx : " + minx + ", maxx :" + maxx);

                ///////

                // x : �{��3.5�ł��傤�ǉ�ʂ����ς��ŃL�[�{�[�h��ԗ����� ((x / maxx) - (float)0.5) * (float)3.5;
                // y : �E���ł��傤�ǉ�ʂ����ς��ŃL�[�{�[�h��ԗ�����  (y / maxy - (float)0.5) * (float)5 - (float)0.5; 
                //ux = (xx / maxx - (float)0.3) * (float)6;

                if (pr == "center")
                {
                    ux = (xx / sizex - (float)0.5) * 2 * (float)0.0375 * (sizex/sizey)*magnification;
                    uy = (yy / sizey - (float)0.5) * 2 * (float)0.0375 * magnification;
                }
                else if (pr == "right")
                {
                    ux = (xx / maxx - (float)0.65) * (float)5.8;
                    uy = (yy / maxy - (float)0.5) * (float)6 - (float)0.5;
                }

                //���[�J�����W����ɁA���W���擾
               Vector3 localPos = sphere.transform.localPosition;

                localPos.x = ux;
                localPos.y = uy;

                sphere.transform.localPosition = localPos;

            }

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

    public bool getOnoff()
    {
        return onoff;
    }

    public bool getOnrunning()
    {
        return onrunning;
    }

    public float getUX()
    {
        return ux;
    }

    public float getUY()
    {
        return uy;
    }
}
