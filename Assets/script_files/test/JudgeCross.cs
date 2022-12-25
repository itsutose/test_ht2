using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeCross : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        // �����m�F�A
        {
            // �O�p�`�̒��_�ݒ�
            Vector3 vA = new Vector3(0.9f, 16.35f, 9.03f);
            Vector3 vB = new Vector3(-4.4f, 3.68f, -7.39f);
            Vector3 vC = new Vector3(10.17f, -5.0f, 0.0f);
            // �����̒��_�ݒ�
            Vector3 vD = new Vector3(-11.45f, 11.69f, -5.6f);
            Vector3 vE = new Vector3(8.8f, 31.03f, -0.42f);
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
    private static bool IsCrossTriAndLine(Vector3 vA, Vector3 vB, Vector3 vC, Vector3 vD, Vector3 vE)
    {
        bool bRet = false;
        //try
        //{
            float[] fAry = GetCrossJudgeParam(vA, vB, vC, vD, vE);
            if (fAry != null)
            {
                float k = fAry[0];
                float l = fAry[1];
                float d = fAry[2];
                if (0 <= k && k <= 1 && 0 <= l && l <= 1 && k + l <= 1 && 0 <= d && d <= 1)
                {
                    bRet = true;
                }
            }
        //}
        //catch (Exception ex)
        //{
        //    Debug.Log(ex.ToString(), "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    bRet = false;
        //}
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
        //try
        //{
            float[] fAry = GetCrossJudgeParam(vA, vB, vC, vD, vE);
            float k = fAry[0];
            float l = fAry[1];
            float d = fAry[2];
            if (0 <= k && k <= 1 && 0 <= l && l <= 1 && k + l <= 1 && 0 <= d && d <= 1)
            {
                Vector3 vAB = vB - vA;
                Vector3 vAC = vC - vA;
                Vector3 vABk = vAB * k;
                Vector3 vACl = vAC * l;
                vRet = vA + vABk + vACl;
            }
        //}
        //catch (Exception ex)
        //{
        //    Debug.Log(ex.ToString(), "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //}
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
        //try
        //{
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
        //}
        //catch (Exception ex)
        //{
        //    Debug.Log(ex.ToString(), "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    fAryPram = null;
        //}
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
        //try
        //{
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
        //}
        //catch (Exception ex)
        //{
        //    Debug.Log(ex.ToString(), "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    x = null;
        //}
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
}

