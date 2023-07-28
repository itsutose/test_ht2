using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;

public class textSet : MonoBehaviour
{

    public Boolean random = false;
    public TextMeshProUGUI ExampleText;
    public TextMeshProUGUI textobject;
    public csv_output csvOP;
    //public int until = 26;

    private int i = 0;
    private Boolean isFirst = true;

    string[,] practice = {
        //{"Please input your name", "名前を入力してください" },
          {"0","思い切りも必要", "おもいきりもひつよう"},
          {"1","今日も徹夜", "きょうもてつや"},
          {"2","筑波山に登る", "つくばさんにのぼる"},
          {"3","つくばエクスプレス", "つくばえくすぷれす"},
          {"4","メールを送信", "めーるをそうしん"},
          {"5","これもお願い", "これもおねがい" },
          {"6","続きが気になる", "つづきがきになる" },
          {"7","みんなでミニゲーム","みんなでみにげーむ" },
          {"8","プログラミング", "ぷろぐらみんぐ" },
          {"9","発表会に臨む", "はっぴょうかいにのぞむ" },
          {"10","ページをめくる", "ぺーじをめくる"},
          {"11","仙台へ旅行", "せんだいへりょこう" },
          {"12","百円玉", "ひゃくえんだま" },
          {"13","やっぱりいいよ","やっぱりいいよ"},
          {"14","水を補充する", "みずをほじゅうする"},
          {"15","鍋奉行をする", "なべぶぎょうをする" },
          {"16","パソコンは高い", "ぱそこんはたかい"},
          {"17","ありがとうとざいます","ありがとうございます"},
          {"18","全国都道府県","ぜんこくとどうふけん"},
          {"19","記録を縮める","きろくをちぢめる"},
          {"20","僅かにとどまる","わずかにとどまる"},
          {"21","マット運動が得意", "まっとうんどうがとくい"},
          {"22","湯冷めした","ゆざめした"},
          {"23","足跡をたどる", "あしあとをたどる" },
          {"24","シャワーを浴びた","しゃわーをあびた"},
          {"25","お湯がぬるくなる","おゆがぬるくなる"},
          {"26","犬と散歩","いぬとさんぽ"}};

    //{"螺旋階段をのぼる","らせんかいだんをのぼる"}

    //Start is called before the first frame update
    void Start()
    {
        //Debug.Log(practice.Length); // 54と出てくる
        //for(int i = 0 ;i < practice.Length / 2; i++)
        //{
        //    Debug.Log(practice[i, 1]);
        //}
        //Debug.Log(practice.Length/2);

        //System.Random random = new System.Random();
        //words2 = .OrderBy(x => random.Next()).ToList();

        System.Random rnd = new System.Random();
        int rows = practice.GetLength(0);
        int cols = practice.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            int randomIndex = rnd.Next(rows);
            Debug.Log("randomIndex : "+randomIndex);
            for (int j = 0; j < cols; j++)
            {
                string temp = practice[i, j];
                practice[i, j] = practice[randomIndex, j];
                practice[randomIndex, j] = temp;
            }
        }

        //foreach(var line in practice)
        //{
        //    Debug.Log(string.Join(",", line));
        //}

        for (int i = 0; i < practice.GetLength(0); i++)
        {
            for (int j = 0; j < practice.GetLength(1); j++)
            {
                string element = practice[i, j];
                Debug.Log(element);
            }
        }

    }


    //// update is called once per frame
    //void update()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        NextText();
    //    }
    //}

    //public IEnumerable NextText()
    //{
    //    foreach(var phrases in practice)
    //    {
    //        ExampleText.text = phrases[0] + "\n" + phrases[1];
    //        yield return null;
    //    }
    //}

    public void InputWord(char word, float ux, float uy)
    {
        //if (i >= 1)
        //{
        //    string phrase = practice[i - 1, 1];
        //    Debug.Log("InputWord,  phrase:" + phrase + ", word: " + word + ", ux: " + ux + ", uy: " + uy);
        //    csvOP.KeyInputSave(phrase, word, ux, uy);
        //}
        if (isFirst == true)
        {

        }
        else
        {
            if (i < practice.GetLength(0))
            {
                string phrase = practice[i, 2];
                string original_num = practice[i, 0];
                Debug.Log("InputWord,  phrase:" + phrase + ", word: " + word + ", ux: " + ux + ", uy: " + uy);
                csvOP.KeyInputSave(phrase, word, ux, uy, original_num);
            }
            else
            {
                csvOP.KeyInputSave("dummy", 'd', 'd', 'd', "dummy");
            }
        }
    }

    public void NextText(char word, float ux, float uy)
    {
      
        Debug.Log(string.Format("textSet.NextText : {0}",Time.time));

        string phrase = "s";

        if (i < practice.GetLength(0))
        {
            phrase = practice[i, 2];
        }
        
        if (isFirst == true)
        {
            Debug.Log("NextText,  phrase " + phrase + ", word :" + word + ", ux :" + ux + ", uy :" + uy);
            csvOP.EnterSave(phrase, word, ux, uy);
            ExampleText.text = i.ToString() + ". " + practice[i, 1] + "\n" + practice[i, 2];
            textobject.text = "";

            isFirst = false;
        }
        else
        {

            if (i < practice.GetLength(0))
            {
                phrase = practice[i, 2];
                csvOP.EnterSave(phrase, word, ux, uy);
            }
            // ======================= ここまででi回目の作業 ========================

 

            //if (until == i)
            //{
            //    csvOP.csvClose();
            //}


            if (i >= practice.GetLength(0) && i <= practice.GetLength(0)+10)
            { 
                 ExampleText.text = "END";
                csvOP.EnterSave("dummy", 'd', 'd', 'd');

                return;
            }
            else if(i>= practice.GetLength(0)+5)
            { 
                csvOP.csvClose();
                return;
            }
            
            i += 1;
            
            if (i < practice.GetLength(0))
            {
                
                ExampleText.text = i.ToString() + ". " + practice[i, 1] + "\n" + practice[i, 2];
                textobject.text = "";
            }

        }
    }

}