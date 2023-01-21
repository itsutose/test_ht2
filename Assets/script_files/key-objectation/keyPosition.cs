using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI; 

public class keyPosition : MonoBehaviour
{
    public GameObject a, k, s, t, n, h, m, y, r, w, hen, backspace, space, point, enter, dummy = null;
    private float centerX, centerY;

    //public float keySizeX = (float)0.02, keySizeY = (float)0.015;
    //public float keyGap = (float)0.005;


    private int mode;

    private float keySizeX, keySizeY;
    private float keyGap;
    private float keyGapX, keyGapY;

    public TextMeshProUGUI textx,texty,textratio;

    //private GameObject dummy = null;
    private float ratio = 1;

    // Start is called before the first frame update
    void Awake()
    {

        if(mode == 1)
        {
            keySizeX = (float)0.015;
            keySizeY = (float)0.01125;
            keyGapX = (float)0.00375;
            keyGapY = (float)0.00375;
            centerY = (float)(-0.01);
        }
        else if (mode == 2)
        {
            keySizeX = (float)0.015;
            keySizeY = (float)0.01125;
            keyGapX = (float)0.01;
            keyGapY = (float)0.01;
            centerY = (float)(-0.007);
        }
        else if (mode == 3)
        {
            keySizeX = (float)0.015;
            keySizeY = (float)0.01125;
            keyGapX = (float)0.02;
            keyGapY = (float)0.01;
            centerY = (float)(-0.007);
        }

        Debug.Log(string.Format("keyPosition mode:{0}", mode));

        GameObject[,] keylist = { { a, k, s, backspace}, { t, n, h, space}, { m, y, r, enter}, { hen, w, point, dummy}};

        textx.text += (keySizeX * 4 + keyGap * 3).ToString();
        texty.text += (keySizeY * 4 + keyGap * 3).ToString();

        ratio = (keySizeX * 4 + keyGap * 3) / (keySizeY * 4 + keyGap * 3);
        textratio.text += ratio.ToString();

        for (int i = 0; i< 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {

                if (i == 3 && j == 3 && dummy == null)
                {
                    continue;
                }

                GameObject key = keylist[i, j];
                Vector3 keyPos = key.transform.localPosition;
                Vector3 keyScale = key.transform.localScale;


                // Enter‚Ì‚±‚Æ
                if (i == 2 && j == 3)
                {
                    if (mode == 1)
                    {
                        keyScale.x = keySizeX;
                        keyScale.y = keySizeY * 2 + keyGapY;

                        key.transform.localScale = keyScale;

                        keyPos.x = (keyGapX + keySizeX) * (float)(-1.5 + j) + centerX;
                        keyPos.y = (keyGapY + keySizeY) * (float)(1.5 + -i) + centerY - keySizeY + keyGapY;

                        key.GetComponent<key2>().set_cx(keyPos.x);
                        key.GetComponent<key2>().set_cy(keyPos.y);

                        key.transform.localPosition = keyPos;
                    }
                    else
                    {
                        keyScale.x = keySizeX;
                        keyScale.y = keySizeY * 2 + keyGapY;

                        key.transform.localScale = keyScale;

                        keyPos.x = (keyGapX + keySizeX) * (float)(-1.5 + j) + centerX;
                        keyPos.y = (keyGapY + keySizeY) * (float)(1.5 + -i) + centerY - keySizeY;

                        key.GetComponent<key2>().set_cx(keyPos.x);
                        key.GetComponent<key2>().set_cy(keyPos.y);

                        key.transform.localPosition = keyPos;
                    }

                    continue;
                }
                else
                {

                    keyScale.x = keySizeX;
                    keyScale.y = keySizeY;


                    key.transform.localScale = keyScale;

                    keyPos.x = (keyGapX + keySizeX) * (float)(-1.5 + j) + centerX;
                    keyPos.y = (keyGapY + keySizeY) * (float)(1.5 + -i) + centerY;

                    key.GetComponent<key2>().set_cx(keyPos.x);
                    key.GetComponent<key2>().set_cy(keyPos.y);

                    key.transform.localPosition = keyPos;
                }
            }
        }
    }

    public void Refresh()
    {

        if (mode == 1)
        {
            keySizeX = (float)0.015;
            keySizeY = (float)0.01125;
            keyGapX = (float)0.00375;
            keyGapY = (float)0.00375;
            centerY = (float)(-0.01);
        }
        else if (mode == 2)
        {
            keySizeX = (float)0.015;
            keySizeY = (float)0.01125;
            keyGapX = (float)0.01;
            keyGapY = (float)0.01;
            centerY = (float)(-0.007);
        }
        else if (mode == 3)
        {
            keySizeX = (float)0.015;
            keySizeY = (float)0.01125;
            keyGapX = (float)0.02;
            keyGapY = (float)0.01;
            centerY = (float)(-0.007);
        }


        GameObject[,] keylist = { { a, k, s, backspace }, { t, n, h, space }, { m, y, r, enter }, { hen, w, point, dummy } };

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {

                if (i == 3 && j == 3 && dummy == null)
                {
                    continue;
                }

                GameObject key = keylist[i, j];
                Vector3 keyPos = key.transform.localPosition;
                Vector3 keyScale = key.transform.localScale;

                // Enter‚Ì‚±‚Æ
                if (i == 2 && j == 3)
                {
                    if (mode == 1)
                    {
                        keyScale.x = keySizeX;
                        keyScale.y = keySizeY * 2 + keyGapY;

                        key.transform.localScale = keyScale;

                        keyPos.x = (keyGapX + keySizeX) * (float)(-1.5 + j) + centerX;
                        keyPos.y = (keyGapY + keySizeY) * (float)(1.5 + -i) + centerY - keySizeY + keyGapY;

                        key.GetComponent<key2>().set_cx(keyPos.x);
                        key.GetComponent<key2>().set_cy(keyPos.y);

                        key.transform.localPosition = keyPos;
                    }
                    else
                    {
                        keyScale.x = keySizeX;
                        keyScale.y = keySizeY * 2 + keyGapY;

                        key.transform.localScale = keyScale;

                        keyPos.x = (keyGapX + keySizeX) * (float)(-1.5 + j) + centerX;
                        keyPos.y = (keyGapY + keySizeY) * (float)(1.5 + -i) + centerY - keySizeY;

                        key.GetComponent<key2>().set_cx(keyPos.x);
                        key.GetComponent<key2>().set_cy(keyPos.y);

                        key.transform.localPosition = keyPos;
                    }

                    continue;
                }
                else
                {

                    keyScale.x = keySizeX;
                    keyScale.y = keySizeY;

                    key.transform.localScale = keyScale;

                    keyPos.x = (keyGapX + keySizeX) * (float)(-1.5 + j) + centerX;
                    keyPos.y = (keyGapY + keySizeY) * (float)(1.5 + -i) + centerY;

                    key.GetComponent<key2>().set_cx(keyPos.x);
                    key.GetComponent<key2>().set_cy(keyPos.y);

                    key.transform.localPosition = keyPos;
                }
            }
        }

        
    }

    //private void dl(string s)
    //{
    //    Debug.Log(string.Format())
    //}
    

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public float getRatio()
    {
        return ratio;
    }

    public void setMode(int i)
    {
        mode = i;
    }
}
