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
    //    //����A
    //    yield return StartCoroutine(Wait())
    ////����B
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

    //    //�ԐF�ɂ���
    //    gameObject.GetComponent<Renderer>().material.color = Color.red;

    //    //1�b��~
    //    yield return new WaitForSeconds(1);

    //    //������̃R���[�`�������s����
    //    yeild return StartCoroutine(ChangeColor2());



    //    yield return new WaitForSeconds(4);

    //    //�ĊJ�����Ɏw�肵���֐���true��Ԃ��ƍĊJ���܂��B

    //    //cube.GetComponent<Test_b>().func_color()

    //    gameObject.GetComponent<Renderer>().material.color = Color.white;

    //    //while (!ChangeColor2)
    //    //{
    //    //    // child��isComplete�ϐ���true�ɂȂ�܂őҋ@
    //    //    yield return new WaitForEndOfFrame();
    //    //}



    //}



    //IEnumerator ChangeColor2()
    //{
    //    yield return new WaitForSeconds(2);

    //    //�F�ɂ���
    //    gameObject.GetComponent<Renderer>().material.color = Color.blue;

    //    //1�b��~
    //    yield return new WaitForSeconds(1);

    //    //���F�ɂ���
    //    gameObject.GetComponent<Renderer>().material.color = Color.yellow;


    //}

}