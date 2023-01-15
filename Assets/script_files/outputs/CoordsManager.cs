using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;

//[DefaultExecutionOrder(-5)]

public class CoordsManager : MonoBehaviour
{

    public FKB fkb;
    public PreCoords precoords;
    public coordinates coords;



    public float _Distance = (float)0.15;
    public string folder_path = ".\\Assets\\CSV_output\\PreCoords";
    public string file_name = "test";
    public string _ID;
    public int TestTimes = 10;


    public float magnification = 1;


    // Start is called before the first frame update
    void Start()
    {
        
        fkb.GetComponent<FKB>().setDistance(_Distance);
        fkb.GetComponent<keyManager5>().setTestTimes(TestTimes);


        precoords.GetComponent<PreCoords>().setfolder_path(folder_path);
        precoords.GetComponent<PreCoords>().setfile_name(file_name);
        precoords.GetComponent<PreCoords>().setID(_ID);
        precoords.GetComponent<PreCoords>().setTestTimes(TestTimes);



        //coords.Pointer = Pointer;
        //coords.out_range_times = out_range_times;
        //coords.magnification = magnification;


        //kp.GetComponent<keyPosition>().Refresh();
        fkb.GetComponent<keyManager5>().SStart();
        precoords.SStart();

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time <= 0.05)
        {
            fkb.GetComponent<FKB>().setDistance(_Distance);

            precoords.GetComponent<PreCoords>().setfolder_path(folder_path);
            precoords.GetComponent<PreCoords>().setfile_name(file_name);
            precoords.GetComponent<PreCoords>().setID(_ID);
            precoords.GetComponent<PreCoords>().setTestTimes(TestTimes);

            //fkb.GetComponent<keyManager5>().SStart();
            //precoords.SStart();
            return;
        }

        if (Input.GetKey(KeyCode.Return))
        {
            fkb.GetComponent<FKB>().setDistance(_Distance);

            precoords.GetComponent<PreCoords>().setfolder_path(folder_path);
            precoords.GetComponent<PreCoords>().setfile_name(file_name);
            precoords.GetComponent<PreCoords>().setID(_ID);
            precoords.GetComponent<PreCoords>().setTestTimes(TestTimes);

            //precoords.SStart();
        }

        if (Input.GetKey(KeyCode.R))
        {

        }
    }
}
