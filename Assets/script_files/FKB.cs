using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FKB : MonoBehaviour
{

    //public GameObject kb; // keyboard

    // publicとすることでinspectorからobjectを取得することができる
    public GameObject center;
    public float distance = 3;
    public Boolean debug = true;

    private Vector3 obj;

    private Vector3 rot;
    private Vector3 pos;


    private float phi;
    private float theta;

    // Start is called before the first frame update
    void Start()
    {
 
        obj = transform.position;

        pos = center.transform.position;

        rot = center.transform.eulerAngles;

    }

    // Update is called once per frame
    void Update()
    {


        if (debug == true)
        {
            rot = center.transform.eulerAngles;
            pos = center.transform.position;


            // 垂直向きの角度：phi
            if (rot.x > 180)
            {
                phi = (float)(Math.PI / 180) * (rot.x - 360) * -1;
            }
            else
            {
                phi = (float)(Math.PI / 180) * (rot.x * -1);
            }

            // 水平方向の角度：theta
            theta = (float)(Math.PI / 180) * rot.y;


            obj.x = distance * (float)Math.Cos(phi) * (float)Math.Sin(theta) + pos.x;
            obj.y = distance * (float)Math.Sin(phi) + pos.y;
            obj.z = distance * (float)Math.Cos(phi) * (float)Math.Cos(theta) + pos.z;

            //objrot.x = objrot.x;
            //objrot.y = objrot.y + 90;
            //objrot.z = objrot.z;

            transform.position = obj;


            float nowtime = Time.time;
            if (nowtime <= 8f)
            {
                //Vector3 centerpos = center.transform.position;
                //pos.y = centerpos.y + distance;

                //transform.position = pos;

            }
            else if (range(Time.time, 8f, 11f))
            {
                //pos.x += 0.005f;
                //pos.y += 0.005f;
                //pos.z += 0.005f;

                //transform.position = pos;
            }
        }
    }


    //bool range<T>(T a, T b, T c) where T : IComparable
    bool range (float a, float b, float c)
    {
        //if(a.CompareTo(b) > 0 && c.CompareTo(a) > 0)
        if(b <= a && a <= c)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}


class fixedmove3D{

    private float x;
    private float y;
    private float z;
    private Vector3 pos;
    private Vector3 rot;

    private float theta;
    private float omega;

    public fixedmove3D(float x, float y, float z, Vector3 pos, Vector3 rot)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        //this.center = center;
    }






}