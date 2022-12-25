using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.UI;  // 追加しましょう

public class keyUnder : key2
{
    //public TextMeshProUGUI textobject;
    public GameObject a0, a1, a2, a3;

    //private float cx, cy; // keyの中心の位置
    //private float lx, ly; // keyのx横，y縦

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
        //Debug.Log("到達している");
        a0.SetActive(true);
        a1.SetActive(true);
        a2.SetActive(true);
        a3.SetActive(true);
    }

    // 母音キーを消す（解放時）
    public override void in_visible_key()
    {
        a0.SetActive(false);
        a1.SetActive(false);
        a2.SetActive(false);
        a3.SetActive(false);
    }

    public override void takecolor(Color color, int aa)
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
        else
        {
            mat = a3.GetComponent<Renderer>().material;
        }

        mat.color = color;
    }

    public override void rmcolor(int aa)
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
        else
        {
            mat = a3.GetComponent<Renderer>().material;
        }

        mat.color = Color.white;
    }

    public override string takeword(int aa, string ss)
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
        else
        {
            word = a3.GetComponent<key_vowel>().thistext();
        }

        return ss + word;
    }

    public override void InputWordtoCSV(char word)
    {
        textset.InputWord(word, this.ux, this.uy);
    }
}
