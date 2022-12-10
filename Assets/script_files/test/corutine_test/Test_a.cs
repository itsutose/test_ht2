using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_a: MonoBehaviour
{

    public GameObject cube;


    private void Start()
    {
        //StartCoroutine(ChangeColor1());

        //b = cube.GetComponent<Test_b>();



    }

    //private void Update()
    //{

    //}

    //public bool flag;
    //IEnumerator Start()
    //{
    //    //処理A
    //    yield return StartCoroutine(Wait())
    ////処理B
    //}

    //IEnumerator Wait()
    //{
    //    while (flag == false)
    //    {
    //        yield return new WaitForEndOfFrame();
    //    }
    //    yield return null;
    //}


    //IEnumerator ChangeColor1()
    //{

    //    //赤色にする
    //    gameObject.GetComponent<Renderer>().material.color = Color.red;

    //    //1秒停止
    //    yield return new WaitForSeconds(1);

    //    //もう一つのコルーチンを実行する
    //    yeild return StartCoroutine(ChangeColor2());



    //    yield return new WaitForSeconds(4);

    //    //再開条件に指定した関数がtrueを返すと再開します。

    //    //cube.GetComponent<Test_b>().func_color()

    //    gameObject.GetComponent<Renderer>().material.color = Color.white;

    //    //while (!ChangeColor2)
    //    //{
    //    //    // childのisComplete変数がtrueになるまで待機
    //    //    yield return new WaitForEndOfFrame();
    //    //}



    //}



    //IEnumerator ChangeColor2()
    //{
    //    yield return new WaitForSeconds(2);

    //    //青色にする
    //    gameObject.GetComponent<Renderer>().material.color = Color.blue;

    //    //1秒停止
    //    yield return new WaitForSeconds(1);

    //    //黄色にする
    //    gameObject.GetComponent<Renderer>().material.color = Color.yellow;


    //}

}