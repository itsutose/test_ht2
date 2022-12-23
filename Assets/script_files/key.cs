using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;  // 追加しましょう

public class key : MonoBehaviour
{
    public TextMeshProUGUI textobject;
    public GameObject a0, a1, a2, a3, a4;

    private float cx, cy; // keyの中心の位置
    private float lx, ly; // keyのx横，y縦

    void Start()
    {
        gameObject.SetActive(true);
        Transform obj = this.transform;

        cx = obj.localPosition.x;
        cy = obj.localPosition.y;
        
        lx = obj.localScale.x;
        ly = obj.localScale.y;
    }
    // 引数がキーの範囲内にあるか判定
    public bool isin(float ux, float uy)
    {
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

    bool range(float a, float b, float c)
    {
        if(b <= a && a <= c)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public string thistext()
    {
        return textobject.text;
    }
    // 母音キーを表示（押下時）
    public void visible_key()
    {
        // これが呼び出されるとき，onoff == true && prior == null
        a0.SetActive(true);
        a1.SetActive(true);
        a2.SetActive(true);
        a3.SetActive(true);
        a4.SetActive(true);
    }
    // 母音キーを消す（解放時）
    public void in_visible_key()
    {
        a0.SetActive(false);
        a1.SetActive(false);
        a2.SetActive(false);
        a3.SetActive(false);
        a4.SetActive(false);
    }
    // キーの色を変える（領域内に移動したときに使用）
    public void takecolor()
    {
        Material mat = this.GetComponent<Renderer>().material;
        mat.color = Color.gray;
        mat.color = new Color(0.75f, 0.75f, 0.6f, 1.0f);
    }

    public void takecolor(Color color, int aa)
    {
        Material mat;

        if(aa == 0)
        {
            mat = a0.GetComponent<Renderer>().material;
        }else if(aa == 1)
        {
            mat = a1.GetComponent<Renderer>().material;
        }
        else if (aa == 2)
        {
            mat = a2.GetComponent<Renderer>().material;
        }
        else if (aa == 3)
        {
            mat = a3.GetComponent<Renderer>().material;
        }
        else
        {
            mat = a4.GetComponent<Renderer>().material;
        }

        mat.color = color;
    }
    // キーの色を戻す（領域外に移動したときに使用）
    public void rmcolor()
    {
        Material mat = this.GetComponent<Renderer>().material;
        mat.color = Color.white;
    }

    public void rmcolor(int aa)
    {
        Material mat;

        if (aa == 0)
        {
            mat = a0.GetComponent<Renderer>().material;
        }
        else if (aa == 1)
        {
            mat = a1.GetComponent<Renderer>().material;
        }
        else if (aa == 2)
        {
            mat = a2.GetComponent<Renderer>().material;
        }
        else if (aa == 3)
        {
            mat = a3.GetComponent<Renderer>().material;
        }
        else
        {
            mat = a4.GetComponent<Renderer>().material;
        }

        mat.color = Color.white;
    }

    public string takeword(int aa)
    {
        string word;

        if (aa == 0)
        {
            word = a0.GetComponent<key_vowel>().thistext();
        }
        else if (aa == 1)
        {
            word = a1.GetComponent<key_vowel>().thistext();
        }
        else if (aa == 2)
        {
            word = a2.GetComponent<key_vowel>().thistext();
        }
        else if (aa == 3)
        {
            word = a3.GetComponent<key_vowel>().thistext();
        }
        else
        {
            word = a4.GetComponent<key_vowel>().thistext();
        }

        return word;
    }

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