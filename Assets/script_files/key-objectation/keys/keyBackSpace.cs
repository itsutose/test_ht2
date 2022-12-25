using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.UI;  // ’Ç‰Á‚µ‚Ü‚µ‚å‚¤


public class keyBackSpace : key2
{

    //public GameObject a0;

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

    public override void takecolor(Color color, int aa)
    {
        Material mat = this.GetComponent<Renderer>().material;
        mat.color = Color.gray;
        mat.color = new Color(0.75f, 0.75f, 0.6f, 1.0f);
    }

    public override void rmcolor(int aa)
    {
        Material mat = this.GetComponent<Renderer>().material;
        mat.color = Color.white;
    }

    public override string takeword(int aa, string ss)
    {

        string word;

        if (aa == 1)
        {
            word = "";
        }
        else
        {
            int s_leng = ss.Length;

            if (s_leng == 0)
            {
                return "";
            }

            word = ss.Substring(0, s_leng - 1);
        }

        return word;
    }

    public override void InputWordtoCSV(char word)
    {
        textset.InputWord('D', this.ux, this.uy);
    }
}
