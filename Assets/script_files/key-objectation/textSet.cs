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
    public int until = 26;

    private int i = 0;
    private Boolean isFirst = true;

    string[,] practice = {
        //{"Please input your name", "名前を入力してください" },
        {"思い切りも必要", "おもいきりもひつよう"},
          { "今日も徹夜", "きょうもてつや" },
          { "筑波山に登る", "つくばさんにのぼる" },
          { "つくばエクスプレス", "つくばえくすぷれす" },
          { "メールを送信", "めーるをそうしん"},
          { "これもお願い", "これもおねがい" },
          { "続きが気になる", "つづきがきになる" },
          { "みんなでミニゲーム","みんなでみにげーむ" },
          { "プログラミング", "ぷろぐらみんぐ" },
          { "発表会に臨む", "はっぴょうかいにのぞむ" },
          { "ページをめくる", "ぺーじをめくる"},
          { "仙台へ旅行", "せんだいへりょこう" },
          { "百円玉", "ひゃくえんだま" },
          { "やっぱりいいよ","やっぱりいいよ"},
          { "水を補充する", "みずをほじゅうする"},
          { "鍋奉行をする", "なべぶぎょうをする" },
          { "パソコンは高い", "ぱそこんはたかい"},
          { "ありがとうとざいます","ありがとうございます"},
          { "全国都道府県","ぜんこくとどうふけん"},
          { "記録を縮める","きろくをちぢめる"},
          { "僅かにとどまる","わずかにとどまる"},
          { "マット運動が得意", "まっとうんどうがとくい"},
          { "湯冷めした","ゆざめした"},
          { "足跡をたどる", "あしあとをたどる" },
          { "シャワーを浴びた","しゃわーをあびた"},
          { "お湯がぬるくなる","おゆがぬるくなる"},
          { "犬と散歩","いぬとさんぽ"}};

    //{"螺旋階段をのぼる","らせんかいだんをのぼる"}

    //Start is called before the first frame update
    void Start()
    {
        //Debug.Log(practice.Length); // 54と出てくる
        //for(int i = 0 ;i < practice.Length / 2; i++)
        //{
        //    Debug.Log(practice[i, 1]);
        //}
        
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
        if(isFirst == true)
        {

        }
        else
        {
            string phrase = practice[i, 1];
            Debug.Log("InputWord,  phrase:" + phrase + ", word: " + word + ", ux: " + ux + ", uy: " + uy);
            csvOP.KeyInputSave(phrase, word, ux, uy);
        }
    }

    public void NextText(char word, float ux, float uy)
    {
        //if (i >= practice.Length / 2)
        //{
        //    ExampleText.text = "END";
        //    textobject.text = "";

        //    csvOP.csvClose();
        //    return;
        //}

        //string phrase = practice[i - 1, 1];
        //csvOP.EnterSave(phrase, word, ux, uy);

        //if(i == 3)
        //{
        //    csvOP.csvClose();
        //}


        //phrase = practice[i, 1];
        //Debug.Log("NextText,  phrase " + phrase + ", word :" + word + ", ux :" + ux + ", uy :" + uy);

        //textobject.text = "";

        //ExampleText.text = i.ToString() +  ". "+ practice[i,0] + "\n" + practice[i,1];

        //i += 1;
        string phrase = practice[i, 1];

        if (isFirst == true)
        {
            //phrase = practice[i, 1];
            Debug.Log("NextText,  phrase " + phrase + ", word :" + word + ", ux :" + ux + ", uy :" + uy);

            ExampleText.text = i.ToString() + ". " + practice[i, 0] + "\n" + practice[i, 1];
            textobject.text = "";

            isFirst = false;
        }
        else
        {
            //string phrase = practice[i, 1];
            csvOP.EnterSave(phrase, word, ux, uy);
            // ======================= ここまででi回目の作業 ========================

            i += 1;
            ExampleText.text = i.ToString() + ". " + practice[i, 0] + "\n" + practice[i, 1];
            textobject.text = "";

            if (until == i)
            {
                csvOP.csvClose();
            }


            if (i >= practice.Length / 2)
            {
                ExampleText.text = "END";
                //textobject.text = "";

                csvOP.csvClose();
                return;
            }
        }
    }

}