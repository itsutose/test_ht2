using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.UI;  // 追加しましょう


public class key2 : MonoBehaviour
{
    public TextMeshProUGUI textobject;
    public textSet textset;

    protected float cx, cy; // keyの中心の位置
    protected float lx, ly; // keyのx横，y縦

    protected float ux, uy;

    // ux, uy がキーの範囲内にあるか
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

    // range関数
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

    // キーのtextMPを返す
    public virtual string thistext()
    {
        string ss = "null";

        return ss;
    }

    // 母音キーを表示（押下時）
    public virtual void visible_key()
    {
    
    }

    // 母音キーを消す（解放時）
    public virtual void in_visible_key()
    {

    }

    // キー自身の色を変える（領域内に移動したときに使用）
    public void takecolor()
    {
        Material mat = this.GetComponent<Renderer>().material;
        mat.color = Color.gray;
        mat.color = new Color(0.75f, 0.75f, 0.6f, 1.0f);
    }

    // 親が持っている子のキーの色を変更する
    public virtual void takecolor(Color color, int aa)
    {
 
    }

    // キーの色を戻す（領域外に移動したときに使用）
    public void rmcolor()
    {
        Material mat = this.GetComponent<Renderer>().material;
        mat.color = Color.white;
    }

    // 押下時のフィードバックの色戻し（子供数関連）
    public virtual void rmcolor(int aa)
    {

    }

    // 指を離したときにに文字を返す（キータイプ関連）
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
