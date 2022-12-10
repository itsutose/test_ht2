using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;  // ’Ç‰Á‚µ‚Ü‚µ‚å‚¤
//using Time.time;


public class Test_b : MonoBehaviour
{

    Material mat;
    
    // Start is called before the first frame update
    void Start()
    {
        mat = this.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void func_color(Color color)
    {

        mat.color = color;

        //while(Time.time <= 2)
        //{
        //    Debug.Log(Time.time);
        //}

        //return true;
    }

}
