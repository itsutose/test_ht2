using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.UI;  // �ǉ����܂��傤


public class keyDummy : key2
{

    public GameObject a0;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);

        Transform obj = this.transform;

        cx = obj.localPosition.x;
        cy = obj.localPosition.y;

        lx = obj.localScale.x;
        ly = obj.localScale.y;
    }

    // �ꉹ�L�[��\���i�������j
    public override void visible_key()
    {
        // ���ꂪ�Ăяo�����Ƃ��Conoff == true && prior == null
        a0.SetActive(true);
        //a1.SetActive(true);
        //a2.SetActive(true);
        //a3.SetActive(true);
        //a4.SetActive(true);
    }

    // �ꉹ�L�[�������i������j
    public override void in_visible_key()
    {
        a0.SetActive(false);
        //a1.SetActive(false);
        //a2.SetActive(false);
        //a3.SetActive(false);
        //a4.SetActive(false);
    }


    public override void takecolor(Color color, int aa)
    {
        Material mat;
        mat = a0.GetComponent<Renderer>().material;
        mat.color = color;
    }

    public override void rmcolor(int aa)
    {
        Material mat;

        mat = a0.GetComponent<Renderer>().material;
        mat.color = Color.white;
    }

    public override string takeword(int aa, string ss)
    {
        return ss + "D";
    }

    public override void InputWordtoCSV(char word)
    {
        textset.InputWord('D', this.ux, this.uy);
    }

    //public override float get_cx()
    //{
    //    return cx;
    //}

    //public override float get_cy()
    //{
    //    return cy;
    //}

    //public override float get_lx()
    //{
    //    return lx;
    //}

    //public override float get_ly()
    //{
    //    return ly;
    //}
}