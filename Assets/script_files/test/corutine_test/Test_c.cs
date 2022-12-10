using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_c: MonoBehaviour
{
    public GameObject sphere;
    public bool flag;
    public GameObject cube;

    private bool onoff;

    //IEnumerator Start()
    //{
    //    sphere.GetComponent<Renderer>().material.color = Color.red;

    //    Debug.Log(onoff);

    //    if (onoff ==true)
    //    {
    //        //return;
    //        Debug.Log("onoff :" + onoff);

    //        gameObject.GetComponent<Renderer>().material.color = Color.green;

    //        yield return new WaitForSeconds(2);

    //        yield return StartCoroutine(Wait());
    //        //èàóùB
    //        yield return new WaitForSeconds(2);
    //        gameObject.GetComponent<Renderer>().material.color = Color.white;
    //    }
    //}

    private void Start()
    {
        sphere.GetComponent<Renderer>().material.color = Color.red;

        Debug.Log(onoff);


    }

    private void Update()
    {
        Material mat = sphere.GetComponent<Renderer>().material;
        if (mat.color == Color.red)
        {
            onoff = true;
        }
        else if (mat.color == Color.blue)
        {
            onoff = false;
        } 
        
        if (onoff == true)
        {
            //return;
            Debug.Log("onoff :" + onoff);

            //gameObject.GetComponent<Renderer>().material.color = Color.green;


            StartCoroutine(Wait());
            
            //gameObject.GetComponent<Renderer>().material.color = Color.white;
        }



    }

    IEnumerator Wait()
    {
        //bool a = false;

        //while (flag == a)

        cube.GetComponent<Test_b>().func_color(Color.red);
        
        yield return new WaitForSeconds(2);

        cube.GetComponent<Test_b>().func_color(Color.blue);

        yield return new WaitForSeconds(2);

        //{
        //gameObject.GetComponent<Renderer>().material.color = Color.blue;

        //yield return new WaitForSeconds(2);

        //gameObject.GetComponent<Renderer>().material.color = Color.yellow;



        yield return new WaitForEndOfFrame();
        //}
        yield return null;
    }
}
