using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;

public class JudgeCross : MonoBehaviour
{

    //public GameObject Dpoint,Epoint;
    //public GameObject Wsll;
    public Boolean color_feedback = true;
    public GameObject Tip, First, Second, Third;
    public float feed_back_time = 0;
    public TextMeshProUGUI textobject;
    public GameObject a, k, s, t, n, h, m, y, r, w, hen, backspace, space, point, enter;



    private float cx, cy, cz; // key�̒��S�̈ʒu
    private float lx, ly; // key��x���Cy�c
    private float ux, uy;

    private int son = -1;
    private string keep_word = null;

    private GameObject[] keylist;
    private Boolean isTouched = false;
    private GameObject nowkey;

    Vector3 A, B, C, D1, E1, D2, E2, D3, E3, nowA, nowB, nowC;

    // Start is called before the first frame update
    void Start()
    {
        keylist = new GameObject[] { a, k, s, t, n, h, m, y, r, w, hen, backspace, space, point, enter };

        //Main();
    }

    // Update is called once per frame
    void Update()
    {
      
        D1 = Tip.transform.position;
        E1 = First.transform.position;

        D2 = First.transform.position;
        E2 = Second.transform.position;

        D3 = Second.transform.position;
        E3 = Third.transform.position;

        Vector3[] Ds = { D1, D2, D3 };
        Vector3[] Es = { E1, E2, E3 };

        Boolean isCrossed = false;

        if (isTouched == true)
        {
            foreach ((Vector3 D, Vector3 E) in (Ds, Es)) {

                if (IsCrossTriAndLine(nowA, nowB, nowC, D, E, false))
                {
                    Vector3 vCross = GetCrossPointTriAndLine(nowA, nowB, nowC, D, E);
                    touch_action(nowkey, vCross.x, vCross.y);
                    return;
                }
            }

            // �w�������
            up_touch();
            isTouched = false;
            nowkey = null;
        }
        else
        {
            // �S�ẴL�[�ɑ΂���
            foreach (var key in keylist)
            {
                var Apos = key.transform.position;
                cx = Apos.x;
                cy = Apos.y;
                cz = Apos.z;

                var Ascale = key.transform.localScale;
                lx = Ascale.x;
                ly = Ascale.y;

                A = new Vector3(cx - lx / 2, cy - ly / 2, cz);
                B = new Vector3(cx - lx / 2, cy + ly / 2, cz);
                C = new Vector3(cx + lx / 2, cy - ly / 2, cz);

                // index�̎w�悩�獪�{�̂ǂꂪ�G��Ă��邩
                foreach ((Vector3 D, Vector3 E) in (Ds, Es))
                {
                    // �G��Ă���ꍇ�́C
                    if (IsCrossTriAndLine(A, B, C, D, E))
                    {
                        // �������W�擾
                        Vector3 vCross = GetCrossPointTriAndLine(A, B, C, D, E);
                        Debug.Log(string.Format("�������܂��B\n�������W=({0}, {1}, {2})", vCross.x, vCross.y, vCross.z));
                        nowkey = key;
                        //Debug.Log(nowkey.GetComponent<key2>().takeword(0, textobject.text));

                        nowA = A;
                        nowB = B;
                        nowC = C;

                        isCrossed = true;
                        isTouched = true;
                        // ��������L�[�ȊO���ׂĔw�i�F�ɁC�������͂��ׂĔw�i�F��

                        nowkey.GetComponent<key2>().visible_key();

                        break;
                    }
                }

                // �G��Ă���Ƃ��͂����ɔ����o��
                if (isCrossed)
                {
                    //Debug.Log(string.Format("�������܂��B\n�������W=({0}, {1}, {2})", vCross.x, vCross.y, vCross.z));
                    break;
                }
            }
        }
    }

    /// <summary>
    /// �A�v���P�[�V�����̃��C�� �G���g�� �|�C���g�ł��B
    /// </summary>
    //[STAThread]
    static void Main()
    {
        // �����m�F�@
        {
            // �O�p�`�̒��_�ݒ�
            Vector3 vA = new Vector3(-13.01f, 14.29f, -6.99f);
            Vector3 vB = new Vector3(9.17f, 13.53f, 5.36f);
            Vector3 vC = new Vector3(-8.19f, 3.88f, -2.81f);
            // �����̒��_�ݒ�
            Vector3 vD = new Vector3(0.9f, 16.35f, 9.03f);
            Vector3 vE = new Vector3(-4.4f, 3.68f, -7.39f);
            // �������邩�m�F
            if (IsCrossTriAndLine(vA, vB, vC, vD, vE))
            {
                // �������W�擾
                Vector3 vCross = GetCrossPointTriAndLine(vA, vB, vC, vD, vE);
                Debug.Log(string.Format("�������܂��B\n�������W=({0}, {1}, {2})", vCross.x, vCross.y, vCross.z));
            }
            else
            {
                Debug.Log("�������܂���B");
            }
        }
    }
    /// <summary>
    /// �O�p�`�ƒ������������邩����
    /// </summary>
    /// <param name="vA"></param>
    /// <param name="vB"></param>
    /// <param name="vC"></param>
    /// <param name="vD"></param>
    /// <param name="vE"></param>
    /// <returns></returns>
    private static bool IsCrossTriAndLine(Vector3 vA, Vector3 vB, Vector3 vC, Vector3 vD, Vector3 vE, Boolean key_range = true)
    {
        bool bRet = false;

        float[] fAry = GetCrossJudgeParam(vA, vB, vC, vD, vE);
        if (fAry != null)
        {
            float k = fAry[0];
            float l = fAry[1];
            float d = fAry[2];

            if (key_range)
            {
                if (0 <= k && k <= 1 && 0 <= l && l <= 1 /* && k + l <= 1 */  && 0 <= d && d <= 1)
                {
                    bRet = true;
                }
            }
            else
            {
                if (/*0 <= k && k <= 1 && 0 <= l && l <= 1 && k + l <= 1 */ 0 <= d && d <= 1)
                {
                    bRet = true;
                }
            }
        }

        return bRet;
    }
    /// <summary>
    /// �O�p�`�ƒ����̌������W���擾
    /// </summary>
    /// <param name="vA"></param>
    /// <param name="vB"></param>
    /// <param name="vC"></param>
    /// <param name="vD"></param>
    /// <param name="vE"></param>
    /// <returns></returns>
    private static Vector3 GetCrossPointTriAndLine(Vector3 vA, Vector3 vB, Vector3 vC, Vector3 vD, Vector3 vE)
    {
        Vector3 vRet = new Vector3();

        float[] fAry = GetCrossJudgeParam(vA, vB, vC, vD, vE);
        float k = fAry[0];
        float l = fAry[1];
        float d = fAry[2];
        
        if (0 <= k && k <= 1 && 0 <= l && l <= 1 && /* k + l <= 1 && */ 0 <= d && d <= 1)
        {
            Vector3 vAB = vB - vA;
            Vector3 vAC = vC - vA;
            Vector3 vABk = vAB * k;
            Vector3 vACl = vAC * l;
            vRet = vA + vABk + vACl;
        }
        return vRet;
    }
    /// <summary>
    /// �O�p�`�Ɛ����̌�������� �p�����[�^(k,l,d)���擾
    /// </summary>
    /// <param name="vA"></param>
    /// <param name="vB"></param>
    /// <param name="vC"></param>
    /// <param name="vD"></param>
    /// <param name="vE"></param>
    /// <returns></returns>
    private static float[] GetCrossJudgeParam(Vector3 vA, Vector3 vB, Vector3 vC, Vector3 vD, Vector3 vE)
    {
        float[] fAryPram = null;
     
        // �x�N�g��
        Vector3 vAB = vB - vA;
        Vector3 vAC = vC - vA;
        Vector3 vDE = vE - vD;
        Vector3 vAE = vE - vA;
        // �A���������̍��ӂ̌W����2�����z��Ɋi�[
        float[,] fLeft = new float[3, 3];
        fLeft[0, 0] = vAB.x;
        fLeft[0, 1] = vAC.x;
        fLeft[0, 2] = vDE.x;
        fLeft[1, 0] = vAB.y;
        fLeft[1, 1] = vAC.y;
        fLeft[1, 2] = vDE.y;
        fLeft[2, 0] = vAB.z;
        fLeft[2, 1] = vAC.z;
        fLeft[2, 2] = vDE.z;
        // �A���������̉E�ӂ̒l��z��Ɋi�[
        float[] fRight = new float[3];
        fRight[0] = vAE.x;
        fRight[1] = vAE.y;
        fRight[2] = vAE.z;
        fAryPram = Cramer(fLeft, fRight);
     
        return fAryPram;
    }
    /// <summary>
    /// <para>�N�������̌��� 3x3</para>
    /// <para>�ȉ��̂悤�ȘA����������x,y,z�̒l���擾�ł���</para>
    /// <para>  2x - 2y + 3z = 7</para>
    /// <para>  3x + 2y - 4z = -5</para>
    /// <para>  4x - 3y + 2z = 4</para>
    /// <para>  ����: x=1, y=2, z=3</para>
    /// <para>  ���� a=���ӂ̌W���̓񎟌��z��, b=�E�ӂ̒l�̔z��</para>
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private static float[] Cramer(float[,] a, float[] b)
    {
        float[] x = null;
    
        float detA = det(a);
        if (detA == 0.0)
        {
            return null;
        }
        x = new float[3];
        for (int ii = 0; ii < 3; ii++)
        {
            float[,] a2 = new float[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (j == ii)
                    {
                        a2[i, j] = b[i];
                    }
                    else
                    {
                        a2[i, j] = a[i, j];
                    }
                }
            }
            x[ii] = det(a2) / detA;
        }
   
        return x;
    }
    /// <summary>
    /// �N�������̌���
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    private static float det(float[,] a)
    {
        float a1 = a[0, 0] * (a[1, 1] * a[2, 2] - a[1, 2] * a[2, 1]);
        float a2 = a[0, 1] * (a[1, 2] * a[2, 0] - a[1, 0] * a[2, 2]);
        float a3 = a[0, 2] * (a[1, 0] * a[2, 1] - a[1, 1] * a[2, 0]);
        return a1 + a2 + a3;
    }

    private void touch_action(GameObject nowkey, float ux, float uy)
    {
        var Apos = nowkey.transform.position;
        float cx = Apos.x;
        float cy = Apos.y;

        var Ascale = nowkey.transform.localScale;
        float lx = Ascale.x;
        float ly = Ascale.y;

        

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

        if (color_feedback == true)
        {
            nowkey.GetComponent<key2>().takecolor(Color.yellow, son);
        }
    }

    private void up_touch()
    {
        foreach (GameObject key in keylist)
        {
            key.GetComponent<MeshRenderer>().material.color = new Color32(255, 255, 255, 0);
        }

        //if (oo == true)
        //{
        //�w�𗣂��Ƃ��C�ꏊ�̈�ɉ����ĕ������͂�����
        textobject.text = keep_word;

        //int s_leng = keep_word.Length;
        //if (s_leng >= 1)
        //{
        //    char last_word = keep_word[s_leng - 1];
        //    nowkey.GetComponent<key2>().InputWordtoCSV(last_word);
        //}


        // �ꉹ�L�[�̔�\����
        nowkey.GetComponent<key2>().rmcolor(son);
        nowkey.GetComponent<key2>().in_visible_key();
        //}



        nowkey = null;

    }

    private void set_touch()
    {
        foreach (GameObject key in keylist)
        {
            Renderer mesh_obj = key.GetComponent<Renderer>();

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

                //if (priorkey != null)
                //{
                //    priorkey.GetComponent<key2>().rmcolor();
                //}


                continue;
            }
            else
            {
                //mesh_obj.material.color = new Color32(255, 255, 255, 80);
                key.GetComponent<MeshRenderer>().material.color = new Color32(150, 150, 150, 20);
            }

        }
    }
}

public static class TupleExtensions
{
    public static IEnumerator<(T1, T2)> GetEnumerator<T1, T2>(this (IEnumerable<T1> Source1, IEnumerable<T2> Source2) tuple)
    {
        // null�`�F�b�N���͏ȗ�

        using var e1 = tuple.Source1.GetEnumerator();
        using var e2 = tuple.Source2.GetEnumerator();

        // �����͒Z�����ɍ��킹��
        while (e1.MoveNext() && e2.MoveNext())
        {
            yield return (e1.Current, e2.Current);
        }
    }
}
