using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.UI;  // 追加しましょう

public class keyHenkan : key2
{
    public TextMeshProUGUI _text = null;
    public GameObject a0;

    //private float cx, cy; // keyの中心の位置
    //private float lx, ly; // keyのx横，y縦

    private char output_word;

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
        //Debug.Log("到達している");
        a0.SetActive(true);
        //a1.SetActive(true);
        //a3.SetActive(true);
    }

    // 母音キーを消す（解放時）
    public override void in_visible_key()
    {
        a0.SetActive(false);
        //a1.SetActive(false);
        //a3.SetActive(false);
    }

    public override void takecolor(Color32 color32, int aa)
    {
        Material mat;

        mat = a0.GetComponent<Renderer>().material;

        //if (aa == 0)
        //{
        //    mat = a0.GetComponent<Renderer>().material;
        //}
        //else if (aa == 1)
        //{
        //    mat = a1.GetComponent<Renderer>().material;
        //}
        //else
        //{
        //    mat = a3.GetComponent<Renderer>().material;
        //}
        mat.color = color32;
    }

    public override void takecolor(Color32 color32)
    {
        Material mat = this.GetComponent<Renderer>().material;
        mat.color = color32;
        _text.color = color32;
    }

    public override void takecolor(Color32 color32, string word)
    {
        Material mat = this.GetComponent<Renderer>().material;
        mat.color = color32;

        if (word == " ")
        {
            _text.color = color32;
        }
        else
        {
            _text.color = new Color32(255, 255, 255, 80);
        }
        _text.text = word;
    }

    public override void rmcolor(int aa)
    {
        Material mat;

        mat = a0.GetComponent<Renderer>().material;

        //if (aa == 0)
        //{
        //    mat = a0.GetComponent<Renderer>().material;
        //}
        //else if (aa == 1)
        //{
        //    mat = a1.GetComponent<Renderer>().material;
        //}
        //else
        //{
        //    mat = a3.GetComponent<Renderer>().material;
        //}

        mat.color = Color.white;
    }

    public override string takeword(int aa, string ss)
    {

        int s_leng = ss.Length;

        if (s_leng == 0)
        {
            return "";
        }

        char last_word = ss[s_leng -1];
        string s = ss.Substring(0, s_leng-1);

        if (aa == 0)
        {
            //word = a0.GetComponent<key_vowel>().thistext();
            last_word = henkan(last_word, 0);
        }
        //else if (aa == 1)
        //{
        //    last_word = '壱';
        //    //a1.GetComponent<key_vowel>().thistext();
        //}
        //else
        //{
        //    last_word = '弐';
        //}

        return s + last_word;
    }

    private char henkan(char c, int x)
    {
        output_word = c;

        char[,] g1 =  { { 'か', 'が' }, { 'き', 'ぎ' }, { 'く', 'ぐ' }, { 'け', 'げ' }, { 'こ', 'ご' },
                        { 'さ', 'ざ' }, { 'し', 'じ' }, { 'す', 'ず' }, { 'せ', 'ぜ' }, { 'そ', 'ぞ' },
                        { 'た', 'だ' }, { 'ち', 'ぢ' }, { 'て', 'で' }, { 'と', 'ど' },
                        { 'や', 'ゃ' }, { 'ゆ', 'ゅ' }, { 'よ', 'ょ' }, 
                        { 'あ', 'ぁ' }, { 'い', 'ぃ' }, { 'え', 'ぇ' }, { 'お', 'ぉ' } };

        char[,] g2 = { { 'は', 'ば', 'ぱ' }, { 'ひ', 'び', 'ぴ' }, { 'ふ', 'ぶ', 'ぷ' }, { 'へ', 'べ', 'ぺ' }, { 'ほ', 'ぼ', 'ぽ' }, { 'つ', 'っ', 'づ' }, { 'う', 'ぅ', 'ヴ' } };

        if (x == 0)
        {

            //Debug.Log(g1[0, 0]);
            //output_word = g1[1, 1];
            //foreach (char[] elem in g1)
            //{
            //    output_word = elem{0};

            for(int i = 0; i < 21; i++) 
            {
                if (g1[i, 0] == c)
                {
                    output_word = g1[i, 1];
                }
                else if (g1[i, 1] == c)
                {
                    output_word = g1[i, 0];
                }
            }
            for (int i = 0; i < 7; i++)
            {
                if (g2[i, 0] == c)
                {
                    output_word = g2[i, 1];
                }
                else if (g2[i, 1] == c)
                {
                    output_word = g2[i, 2];
                }
                else if (g2[i, 2] == c)
                {
                    output_word = g2[i, 0];
                }
            }

            return output_word;

        }
        else if(x == 1)
        {

        }
        else if(x == 2)
        {

        }
        else
        {
            Debug.Log("KeyHenkan.henkan()でミス");
            output_word = '×';
        }
        return output_word;
    }

    public override void InputWordtoCSV(char word)
    {
        textset.InputWord(output_word, this.ux, this.uy);
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