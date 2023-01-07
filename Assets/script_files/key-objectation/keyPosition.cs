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
    public float centerX = 0, centerY = 0;
    //public float OffsetX = 0, OffsetY = 0;
    public float keySizeX = (float)0.02, keySizeY = (float)0.015;
    public float keyGap = (float)0.005;

    public TextMeshProUGUI textx,texty,textratio;

    //private GameObject dummy = null;
    private float ratio = 1;

    // Start is called before the first frame update
    void Awake()
    {
        
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

                keyScale.x = keySizeX;
                keyScale.y = keySizeY;

                key.transform.localScale = keyScale;

                //if(i==0 && j == 0)
                //{
                //    keyPos.x = (float)-0.05;
                //    keyPos.y = (float)0.09;
                //    key.transform.position = keyPos;
                //}

                // i ‚ÍcCj ‚Í‰¡

                keyPos.x = (keyGap + keySizeX) * (float)(-1.5 + j) + centerX;
                keyPos.y = (keyGap + keySizeY) * (float)(1.5 + -i) + centerY;

                //if (i == 0)
                //{
                //    keyPos.y = (keyGap + keySizeY) * (float)1.5;
                //}
                //else if (i == 1)
                //{
                //    keyPos.y = keyGap/2 + keySizeY/2;
                //}
                //else if (i == 2)
                //{
                //    keyPos.y = -(keyGap / 2 + keySizeY / 2);
                //}
                //else
                //{
                //    keyPos.y = -((keyGap + keySizeY) * (float)1.5);
                //}

                //if(j == 0)
                //{

                //}
                //if (j == 3)
                //{
                //    keyPos.x = (keyGap + keySizeX) * (float)1.5;
                //}
                //else if (i == 2)
                //{
                //    keyPos.x = keyGap / 2 + keySizeX / 2;
                //}
                //else if (i == 1)
                //{
                //    keyPos.x = -(keyGap / 2 + keySizeX / 2);
                //}
                //else
                //{
                //    keyPos.x = -((keyGap + keySizeX) * (float)1.5);
                //}

                key.transform.localPosition = keyPos;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getRatio()
    {
        return ratio;
    }
}
