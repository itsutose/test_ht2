using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.UI;  // 追加しましょう


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

    // 母音キーを表示（押下時）
    public override void visible_key()
    {
        // これが呼び出されるとき，onoff == true && prior == null
        a0.SetActive(true);
        //a1.SetActive(true);
        //a2.SetActive(true);
        //a3.SetActive(true);
        //a4.SetActive(true);
    }

    // 母音キーを消す（解放時）
    public override void in_visible_key()
    {
        a0.SetActive(false);
        //a1.SetActive(false);
        //a2.SetActive(false);
        //a3.SetActive(false);
        //a4.SetActive(false);
    }


    public override void takecolor(Color32 color32, int aa)
    {
        Material mat;
        mat = a0.GetComponent<Renderer>().material;
        mat.color = color32;
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
