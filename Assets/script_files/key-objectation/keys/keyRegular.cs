using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

using UnityEngine.UI;  // 追加しましょう

public class keyRegular : key2
{
    public TextMeshProUGUI _text = null;
    public GameObject a0, a1, a2, a3, a4;

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
        a1.SetActive(true);
        a2.SetActive(true);
        a3.SetActive(true);
        a4.SetActive(true);
    }

    // 母音キーを消す（解放時）
    public override void in_visible_key()
    {
        a0.SetActive(false);
        a1.SetActive(false);
        a2.SetActive(false);
        a3.SetActive(false);
        a4.SetActive(false);
    }

    public override void takecolor(Color32 color32)
    {
        Material mat = this.GetComponent<Renderer>().material;
        mat.color = color32;

        //_text.outlineColor = color32;
        _text.color = color32;
    }

    public override void takecolor(Color32 color32, int aa)
    {
        Material mat;

        if (aa == -1)
        {
            GameObject[] AllSon = new GameObject[] { a1, a2, a3, a4 };

            foreach (var key in AllSon)
            {
                mat = key.GetComponent<Renderer>().material;
                mat.color = color32;

                key.GetComponent<key_vowel>().textobject.color = color32;


                //Debug.Log(string.Format("takecolor(color32, aa) : key {0}, color32 {1}",key, color32));
            }
            
        }
        else
        {

            if (aa == 0)
            {
                mat = a0.GetComponent<Renderer>().material;
                a0.GetComponent<key_vowel>().textobject.color = color32;
            }
            else if (aa == 1)
            {
                mat = a1.GetComponent<Renderer>().material;
                a1.GetComponent<key_vowel>().textobject.color = color32;
            }
            else if (aa == 2)
            {
                mat = a2.GetComponent<Renderer>().material;
                a2.GetComponent<key_vowel>().textobject.color = color32;
            }
            else if (aa == 3)
            {
                mat = a3.GetComponent<Renderer>().material;
                a3.GetComponent<key_vowel>().textobject.color = color32;
            }
            else
            {
                mat = a4.GetComponent<Renderer>().material;
                a4.GetComponent<key_vowel>().textobject.color = color32;
            }

            mat.color = color32;
        }

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

    public override string takeword(int aa, string ss)
    {
        string word = "nanimonasi";

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


        return ss + word;
    }

    public override void InputWordtoCSV(char word)
    {
        textset.InputWord(word, this.ux, this.uy);
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
