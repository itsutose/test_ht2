using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;

//[DefaultExecutionOrder(-5)]

public class AllManager : MonoBehaviour
{

    public FKB fkb;
    public PreTest_output pretest;
    public coordinates coords;


    public Boolean PreTest = false;
    public Boolean HoverColorFeedback = true;
    public Boolean KeyBoardFeedback = true;
    public Boolean Pointer = true;
    public String KeyColor = "TP0";
    public int HowTransparent = 80;
    public int CloverTransparent = 80;


    public string folder_path = ".\\Assets\\CSV_output\\PreTest";
    public string file_name = "test";
    public string _ID;
    //public string _InputType;
    public float _Distance =  (float)0.15;
    public int TestTimes = 10;


    public int out_range_times = 50;
    public float magnification = 1;

    //private string inputType;

    // Start is called before the first frame update
    void Start()
    {
        //inputType = string.Format("KeyColor:{0}, Pointer:{1}, KBF:{2}, HCF:{3}",KeyColor, Pointer, KeyBoardFeedback, HoverColorFeedback);

        file_name = file_name;

        fkb.GetComponent<keyManager4>().setPreTest(PreTest);
        fkb.GetComponent<keyManager4>().setHoverColorFeedback(HoverColorFeedback);
        fkb.GetComponent<keyManager4>().setKeyBoardFeedback(KeyBoardFeedback);
        fkb.GetComponent<keyManager4>().setKeyColor(KeyColor);
        fkb.GetComponent<keyManager4>().setHowTransparent(HowTransparent);
        fkb.GetComponent<keyManager4>().setCloverTransparent(CloverTransparent);

        fkb.GetComponent<FKB>().setDistance(_Distance);

        pretest.folder_path = folder_path;
        pretest.file_name = file_name;
        pretest._ID = _ID;
        pretest.KC = KeyColor;
        pretest.P = Pointer;
        pretest.KBF = KeyBoardFeedback;
        pretest.HCF = HoverColorFeedback;
        pretest._Distance = _Distance;
        pretest.TestTimes = TestTimes;


        coords.Pointer = Pointer;
        coords.out_range_times = out_range_times;
        coords.magnification = magnification;

        fkb.GetComponent<keyManager4>().SStart();
        pretest.SStart();

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time <= 0.05)
        {
            fkb.GetComponent<keyManager4>().setPreTest(PreTest);
            fkb.GetComponent<keyManager4>().setHoverColorFeedback(HoverColorFeedback);
            fkb.GetComponent<keyManager4>().setKeyBoardFeedback(KeyBoardFeedback);
            fkb.GetComponent<keyManager4>().setKeyColor(KeyColor);
            fkb.GetComponent<keyManager4>().setHowTransparent(HowTransparent);
            fkb.GetComponent<keyManager4>().setCloverTransparent(CloverTransparent);

            fkb.GetComponent<FKB>().setDistance(_Distance);

            pretest.folder_path = folder_path;
            pretest.file_name = file_name;
            pretest._ID = _ID;
            pretest.KC = KeyColor;
            pretest.P = Pointer;
            pretest.KBF = KeyBoardFeedback;
            pretest.HCF = HoverColorFeedback;
            pretest._Distance = _Distance;
            pretest.TestTimes = TestTimes;

            coords.Pointer = Pointer;
            coords.out_range_times = out_range_times;
            coords.magnification = magnification;

            fkb.GetComponent<keyManager4>().refresh();
            return;
        }

        if (Input.GetKey(KeyCode.Return))
        {
            fkb.GetComponent<keyManager4>().setPreTest(PreTest);
            fkb.GetComponent<keyManager4>().setHoverColorFeedback(HoverColorFeedback);
            fkb.GetComponent<keyManager4>().setKeyBoardFeedback(KeyBoardFeedback);
            fkb.GetComponent<keyManager4>().setKeyColor(KeyColor);
            fkb.GetComponent<keyManager4>().setHowTransparent(HowTransparent);
            fkb.GetComponent<keyManager4>().setCloverTransparent(CloverTransparent);

            fkb.GetComponent<FKB>().setDistance(_Distance);

            pretest.folder_path = folder_path;
            pretest.file_name = file_name;
            pretest._ID = _ID;
            pretest.KC = KeyColor;
            pretest.P = Pointer;
            pretest.KBF = KeyBoardFeedback;
            pretest.HCF = HoverColorFeedback;
            pretest._Distance = _Distance;
            pretest.TestTimes = TestTimes;

            coords.Pointer = Pointer;
            coords.out_range_times = out_range_times;
            coords.magnification = magnification;

            fkb.GetComponent<keyManager4>().refresh();
        }

        if (Input.GetKey(KeyCode.R))
        {

        }
    }
}
