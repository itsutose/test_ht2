using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;

//[DefaultExecutionOrder(-5)]

public class NormalManager : MonoBehaviour
{

    public FKB fkb;
    //public PreTest_output pretest;
    public coord_model coords;
    public keyPosition kp;
    public csv_output csvop;

    //public Boolean PreTest = false;
    public Boolean HoverColorFeedback = true;
    public Boolean KeyBoardFeedback = true;
    public Boolean Pointer = true;
    public Boolean HoverPointer = true;
    public String KeyColor = "TP0";
    public int HowTransparent = 80;
    public int CloverTransparent = 80;


    public string folder_path = ".\\Assets\\CSV_output\\Normal";
    public string file_name = "test";
    public string _ID;
    public string _InputType;
    public float _Distance = (float)0.15;
    public int _Mode;
    public int TestTimes = 10;


    public string ModelType = "class";
    public int out_range_times = 50;
    public float magnification = (float)1.2;

    //private string inputType;

    // Start is called before the first frame update
    void Start()
    {

        file_name = file_name;

        csvop.setFolderPath(folder_path);
        csvop.setFileName(file_name);
        csvop.setID(_ID);
        csvop.setInputType(_InputType);
        csvop.setDistance(_Distance.ToString());


        fkb.GetComponent<keyManager6>().setModelType(ModelType);
        fkb.GetComponent<keyManager6>().setHoverColorFeedback(HoverColorFeedback);
        fkb.GetComponent<keyManager6>().setKeyBoardFeedback(KeyBoardFeedback);
        fkb.GetComponent<keyManager6>().setKeyColor(KeyColor);
        fkb.GetComponent<keyManager6>().setHowTransparent(HowTransparent);
        fkb.GetComponent<keyManager6>().setCloverTransparent(CloverTransparent);

        fkb.GetComponent<FKB>().setDistance(_Distance);


        kp.GetComponent<keyPosition>().setMode(_Mode);
        kp.GetComponent<keyPosition>().Refresh();


        coords.setPointer(Pointer);
        coords.setHoverPointer(HoverPointer);
        coords.setORT(out_range_times);
        coords.setMagnification(magnification);


        fkb.GetComponent<keyManager6>().SStart();
        csvop.SStart();

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time <= 0.05)
        {

            fkb.GetComponent<keyManager6>().setHoverColorFeedback(HoverColorFeedback);
            fkb.GetComponent<keyManager6>().setKeyBoardFeedback(KeyBoardFeedback);
            fkb.GetComponent<keyManager6>().setKeyColor(KeyColor);
            fkb.GetComponent<keyManager6>().setHowTransparent(HowTransparent);
            fkb.GetComponent<keyManager6>().setCloverTransparent(CloverTransparent);

            fkb.GetComponent<FKB>().setDistance(_Distance);

            coords.setPointer(Pointer); 
            coords.setHoverPointer(HoverPointer);
            coords.setORT(out_range_times);
            coords.setMagnification(magnification);

            //kp.GetComponent<keyPosition>().Refresh();
            fkb.GetComponent<keyManager6>().refresh();
            return;
        }

        if (Input.GetKey(KeyCode.Return))
        {
   
            fkb.GetComponent<keyManager6>().setHoverColorFeedback(HoverColorFeedback);
            fkb.GetComponent<keyManager6>().setKeyBoardFeedback(KeyBoardFeedback);
            fkb.GetComponent<keyManager6>().setKeyColor(KeyColor);
            fkb.GetComponent<keyManager6>().setHowTransparent(HowTransparent);
            fkb.GetComponent<keyManager6>().setCloverTransparent(CloverTransparent);
                                       
            fkb.GetComponent<FKB>().setDistance(_Distance);


            coords.setPointer(Pointer);
            coords.setHoverPointer(HoverPointer);
            coords.setORT(out_range_times);
            coords.setMagnification(magnification);

            kp.GetComponent<keyPosition>().Refresh();
            fkb.GetComponent<keyManager6>().refresh();
        }

        if (Input.GetKey(KeyCode.R))
        {

        }
    }
}
