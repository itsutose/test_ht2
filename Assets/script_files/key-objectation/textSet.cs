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
    private string practice_or_test = "test";
    //public int until = 26;

    private int i = 0;
    private Boolean isFirst = true;
    private Boolean Tpressed = false;

    string[,] phrases;

    //string[,] practice = {
    //    //{"Please input your name", "名前を入力してください" },
    //      {"0","思い切りも必要", "おもいきりもひつよう"}, // 10, 10
    //      {"1","今日も徹夜", "きょうもてつや"}, // 7, 8 
    //      {"2","筑波山に登る", "つくばさんにのぼる"}, // 9, 11
    //      {"3","つくばエクスプレス", "つくばえくすぷれす"}, // 9, 12
    //      {"4","メールを送信", "めーるをそうしん"}, // 8. 8
    //      {"5","これもお願い", "これもおねがい" }, // 7, 8
    //      {"6","続きが気になる", "つづきがきになる" },
    //      {"7","みんなでミニゲーム","みんなでみにげーむ" },
    //      {"8","プログラミング", "ぷろぐらみんぐ" },
    //      {"9","発表会に臨む", "はっぴょうかいにのぞむ" },
    //      {"10","ページをめくる", "ぺーじをめくる"},
    //      {"11","仙台へ旅行", "せんだいへりょこう" },
    //      {"12","百円玉", "ひゃくえんだま" },
    //      {"13","やっぱりいいよ","やっぱりいいよ"},
    //      {"14","水を補充する", "みずをほじゅうする"},
    //      {"15","鍋奉行をする", "なべぶぎょうをする" },
    //      {"16","パソコンは高い", "ぱそこんはたかい"},
    //      {"17","ありがとうとざいます","ありがとうございます"},
    //      {"18","全国都道府県","ぜんこくとどうふけん"},
    //      {"19","記録を縮める","きろくをちぢめる"},
    //      {"20","僅かにとどまる","わずかにとどまる"},
    //      {"21","マット運動が得意", "まっとうんどうがとくい"},
    //      {"22","湯冷めした","ゆざめした"},
    //      {"23","足跡をたどる", "あしあとをたどる" },
    //      {"24","シャワーを浴びた","しゃわーをあびた"},
    //      {"25","お湯がぬるくなる","おゆがぬるくなる"},
    //      {"26","犬と散歩","いぬとさんぽ"}};


    string[,] practice = {
            {"0", "面目ない" ,"めんぼくない" },
            {"1", "やっぱりいいよ" ,"やっぱりいいよ" },
            {"2", "一人減らした" ,"ひとりへらした" },
            {"3", "足音がする" ,"あしおとがする" },
            {"4", "つくばエクスプレス" ,"つくばえくすぷれす" },
            {"5", "会議中" ,"かいぎちゅう" },
            {"6", "思い切りも必要" ,"おもいきりもひつよう" },
            {"7", "ありがとうとざいます" ,"ありがとうございます" },
            {"8", "百円玉" ,"ひゃくえんだま" },
            {"9", "わかりました" ,"わかりました" },
            {"10", "Uber Eats" ,"うーばーいーつ" },
            {"11", "筑波山に登る" ,"つくばさんにのぼる" },
            {"12", "これもお願い" ,"これもおねがい" },
            {"13", "レポート締め切り" ,"れぽーとしめきり" },
            {"14", "弾がない" ,"たまがない" },
            {"15", "すぐに行きます" ,"すぐにいきます" },
            {"16", "仙台へ旅行" ,"せんだいへりょこう" },
            {"17", "プログラミング" ,"ぷろぐらみんぐ" },
            {"18", "少し遅れます" ,"すこしおくれます" },
            {"19", "流石にヤバい" ,"さすがにやばい" },
            {"20", "あれ撃って" ,"あれうって" },
            {"21", "メールを送信" ,"めーるをそうしん" },
            {"22", "午後休講" ,"ごごきゅうこう" },
            {"23", "続きが気になる" ,"つづきがきになる" },
            {"24", "インタラクション" ,"いんたらくしょん" },
            {"25", "ガバエイムじゃん" ,"がばえいむじゃん" },
            {"26", "筑波にいます" ,"つくばにいます" },
            {"27", "みんなでミニゲーム" ,"みんなでみにげーむ" },
            {"28", "放っとけ" ,"ほっとけ" },
            {"29", "了解です" ,"りょうかいです" },
            {"30", "発表会" ,"はっぴょうかい" },
            {"31", "ビデオ返却" ,"びでおへんきゃく" },
            {"32", "論文を読む" ,"ろんぶんをよむ" },
            {"33", "お風呂はいいや" ,"おふろはいいや" },
            {"34", "土曜飲み会" ,"どようのみかい"}};
        
    string[,] test = {
            {"0","高台取ろう", "たかだいとろう"},
            {"1","シャワーを浴びた", "しゃわーをあびた"},
            {"2","飯抜けします", "めしぬけします"},
            {"3","犬と散歩", "いぬとさんぽ"},
            {"4","今出先です", "いまでさきです"},
            {"5","回復してる", "かいふくしてる"},
            {"6","眠過ぎ", "ねむすぎ"},
            {"7","スマブラに勝った", "すまぶらにかった"},
            {"8","おやすみなさい", "おやすみなさい"},
            {"9","明日は休み", "あしたはやすみ"},
            {"10","敵がいる", "てきがいる"},
            {"11","お湯がぬるくなる", "おゆがぬるくなる"},
            {"12","ちょっと待って", "ちょっとまって"},
            {"13","記録を縮める", "きろくをちぢめる"},
            {"14","足跡をたどる", "あしあとをたどる"},
            {"15","ごめんなさい", "ごめんなさい"},
            {"16","リコイル制御", "りこいるせいぎょ"},
            {"17","先に食べて", "さきにたべて"},
            {"18","湯冷めした", "ゆざめした"},
            {"19","マット運動が得意", "まっとうんどうがとくい"},
            {"20","ページをめくる", "ぺーじをめくる"},
            {"21","投げ物使い切った", "なげものつかいきった"},
            {"22","水道代", "すいどうだい"},
            {"23","練習してきた", "れんしゅうしてきた"},
            {"24","銃口曲がってる", "じゅうこうまがってる"}};

    public void setIsPractice(Boolean io)
    {
        // practice_or_test の値に基づいて適切な配列を選択
        if (io == true)
        {
            phrases = practice;
        }
        else
        {
            phrases = test;
        }
    }

    //void SomeFunction() // 適切な関数名に変更してください
    //{


    //    // practice_or_test の値に基づいて適切な配列を選択
    //    if (practice_or_test == "practice")
    //    {
    //        phrases = practice;
    //    }
    //    else if (practice_or_test == "test")
    //    {
    //        phrases = test;
    //    }
    //    else
    //    {
    //        throw new Exception("Invalid value for practice_or_test");
    //    }

    //}

    //Start is called before the first frame update
    void Start()
    {
        //SomeFunction();


        System.Random rnd = new System.Random();
        int rows = phrases.GetLength(0);
        int cols = phrases.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            int randomIndex = rnd.Next(rows);
            Debug.Log("randomIndex : "+randomIndex);
            for (int j = 0; j < cols; j++)
            {
                string temp = phrases[i, j];
                phrases[i, j] = phrases[randomIndex, j];
                phrases[randomIndex, j] = temp;
            }
        }

        for (int i = 0; i < phrases.GetLength(0); i++)
        {
            for (int j = 0; j < phrases.GetLength(1); j++)
            {
                string element = phrases[i, j];
                Debug.Log(element);
            }
        }

    }


    public void InputWord(char word, float ux, float uy)
    {
        if (isFirst == true)
        {

        }
        else
        {
            if (i < phrases.GetLength(0))
            {
                string phrase = phrases[i, 2];
                string original_num = phrases[i, 0];
                Debug.Log("InputWord,  phrase:" + phrase + ", word: " + word + ", ux: " + ux + ", uy: " + uy);
                csvOP.KeyInputSave(phrase, word, ux, uy, original_num);
            }
            else
            {
                csvOP.KeyInputSave("dummy", 'd', 'd', 'd', "dummy");
            }
        }
    }

    // UnityのUpdateメソッド内でTキーが押されたかチェック
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // Tキーが押されたかチェック
        {
            Tpressed = true;
        }
    }

    public void NextText(char word, float ux, float uy)
    {
      
        Debug.Log(string.Format("textSet.NextText : {0}",Time.time));

        string phrase = "s";

        if (i < phrases.GetLength(0))
        {
            phrase = phrases[i, 2];
        }
        
        if (isFirst == true)
        {
            if (Tpressed == true)
            {
                Debug.Log("NextText,  phrase " + phrase + ", word :" + word + ", ux :" + ux + ", uy :" + uy);
                csvOP.EnterSave(phrase, word, ux, uy);
                ExampleText.text = i.ToString() + ". " + phrases[i, 1] + "\n" + phrases[i, 2];
                textobject.text = "";

                isFirst = false;

            }
        }
        else
        {

            if (i < phrases.GetLength(0))
            {
                phrase = phrases[i, 2];
                csvOP.EnterSave(phrase, word, ux, uy);
            }
            // ======================= ここまででi回目の作業 ========================

            if (i >= phrases.GetLength(0) && i <= phrases.GetLength(0)+10)
            { 
                 ExampleText.text = "END";
                csvOP.EnterSave("dummy", 'd', 'd', 'd');

                return;
            }
            else if(i>= phrases.GetLength(0)+5)
            { 
                csvOP.csvClose();
                return;
            }
            
            i += 1;
            
            if (i < phrases.GetLength(0))
            {
                
                ExampleText.text = i.ToString() + ". " + phrases[i, 1] + "\n" + phrases[i, 2];
                textobject.text = "";
            }

        }
    }

}