using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.UI;  // �ǉ����܂��傤


public class key2 : MonoBehaviour
{
    public TextMeshProUGUI textobject;
    public textSet textset;

    protected float cx, cy; // key�̒��S�̈ʒu
    protected float lx, ly; // key��x���Cy�c

    protected float ux, uy;

    // ux, uy ���L�[�͈͓̔��ɂ��邩
    public bool isin(float ux, float uy)
    {
        this.ux = ux;
        this.uy = uy;

        float minx = cx - lx / 2;
        float maxx = cx + lx / 2;
        float miny = cy - ly / 2;
        float maxy = cy + ly / 2;

        if (range(ux, minx, maxx) && range(uy, miny, maxy))
        {
            return true;
        }
        return false;
    }

    // range�֐�
    protected bool range(float a, float b, float c)
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

    // �L�[��textMP��Ԃ�
    public virtual string thistext()
    {
        string ss = "null";

        return ss;
    }

    // �ꉹ�L�[��\���i�������j
    public virtual void visible_key()
    {
    
    }

    // �ꉹ�L�[�������i������j
    public virtual void in_visible_key()
    {

    }

    // �L�[���g�̐F��ς���i�̈���Ɉړ������Ƃ��Ɏg�p�j
    public void takecolor()
    {
        Material mat = this.GetComponent<Renderer>().material;
        mat.color = Color.gray;
        mat.color = new Color(0.75f, 0.75f, 0.6f, 1.0f);
    }

    // �e�������Ă���q�̃L�[�̐F��ύX����
    public virtual void takecolor(Color color, int aa)
    {
 
    }

    // �L�[�̐F��߂��i�̈�O�Ɉړ������Ƃ��Ɏg�p�j
    public void rmcolor()
    {
        Material mat = this.GetComponent<Renderer>().material;
        mat.color = Color.white;
    }

    // �������̃t�B�[�h�o�b�N�̐F�߂��i�q�����֘A�j
    public virtual void rmcolor(int aa)
    {

    }

    // �w�𗣂����Ƃ��ɂɕ�����Ԃ��i�L�[�^�C�v�֘A�j
    public virtual string takeword(int aa, string ss)
    {
        return ss;
    }

    public virtual void touch_action(float cx, float cy)
    {

    }

    public virtual void InputWordtoCSV(char word)
    {

    }

    //protected void InputWordtoCSV(string word)
    //{
    //    //textset.NextText();
    //    textset.InputWord(word, this.ux, this.uy);
    //}

    public float get_cx()
    {
        return cx;
    }

    public float get_cy()
    {
        return cy;
    }

    public float get_lx()
    {
        return lx;
    }

    public float get_ly()
    {
        return ly;
    }
}
