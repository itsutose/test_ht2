using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.UI;  // ’Ç‰Á‚µ‚Ü‚µ‚å‚¤


public class keySpace : key2
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
        return ss + " ";
    }

    public override void InputWordtoCSV(char word)
    {
        textset.InputWord('S', this.ux, this.uy);
    }
}
