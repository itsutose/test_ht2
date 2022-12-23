using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class eval : MonoBehaviour
{
    public Text inputTextbox; // 入力フォーム
    public Text phraseTextbox; // フレーズを表示する
    //public HandPosition handPosition;
    public string participantId; // 参加者の名前
    public string methodName; // 手法の名前
    public int sessionNum; // セッションの番号（0~3）
    public bool isPractice; // 練習フレーズかどうか
    [System.NonSerialized] public bool isTest = false; // テスト中かどうか

    string folderPath = "C:\\Users\\t-yamaguchi\\unity\\MyVRTextEntry\\Assets\\test_data\\";
    FileInfo file_keystroke;
    //FileInfo file_handmovement;
    Stopwatch stopwatch = new Stopwatch(); // 各フレーズが表示されてからの時間を計測する
    List<List<string>> phrases; // 実験で用いるフレーズ
    IEnumerable<int> randomIndexList; // 方向を指定するインデックスのリスト
    int nowPhraseCount = 0; // 現在のフレーズのカウンタ
    class KeyStroke // テスト中に収集するキーストロークのデータのクラス
    {
        public KeyStroke(long timestamp, string key)
        {
            this.timestamp = timestamp;
            this.key = key;
        }
        public long timestamp; // フレーズが表示されてからキー入力までの時間
        public string key; // 入力されたキー（optionの場合は"option"）
    }
    //class HandMovement // テスト中に収集する手の座標データのクラス
    //{
    //    public HandMovement(long timestamp, Vector3 fingerTipPosition, Vector3 fingerRootPosition, Vector3 armPosition)
    //    {
    //        this.timestamp = timestamp;
    //        this.fingerTipPosition = fingerTipPosition;
    //        this.fingerRootPosition = fingerRootPosition;
    //        this.armPosition = armPosition;
    //    }
    //    public long timestamp; // フレーズが表示されてからの経過時間
    //    public Vector3 fingerTipPosition; // 指先の座標
    //    public Vector3 fingerRootPosition; // 指の付け根の座標
    //    public Vector3 armPosition; // 手首の座標
    //}
    List<KeyStroke> keyStrokes = new List<KeyStroke>();
    //List<HandMovement> handMovement = new List<HandMovement>();


    // Start is called before the first frame update
    void Start()
    {
        LoadCSV();
        phraseTextbox.text = "Press T To Start.";
    }

    // Update is called once per frame
    void Update()
    {
        //if (handMovement != null)
        //{
        //    handMovement.Add(new HandMovement(stopwatch.ElapsedMilliseconds, handPosition.fingerTipPos, handPosition.fingerRootPos, handPosition.armPos));
        //}
    }

    // テストを開始する
    public void StartTest()
    {
        inputTextbox.text = "";
        InitCSV();
        isTest = true;
        GenerateRandomIntList();
        nowPhraseCount = 0;
        StartTrial();
    }

    // テストを終了する
    void EndTest()
    {
        isTest = false;
        //var cpm = CalcCPM();
        phraseTextbox.text = $"End"; // $"Test End\nCPM = {cpm}";
    }

    public void CancelTest()
    {
        isTest = false;
        phraseTextbox.text = "Canceled.";
    }

    // 1つのフレーズの入力を開始する
    void StartTrial()
    {
        stopwatch.Restart();
        keyStrokes = new List<KeyStroke>();
        //handMovement = new List<HandMovement>();

        phraseTextbox.text = phrases[randomIndexList.ElementAt(nowPhraseCount)][0] + "\n" + phrases[randomIndexList.ElementAt(nowPhraseCount)][1];
    }

    // 入力フォームに入っている文字列を確定し，テストフレーズと照合
    // 練習フレーズでは実行しないようにする
    public void Enter()
    {
        if (inputTextbox.text == "") // 入力文字がない状態でのエンターは誤入力とみなす
        {
            return;
        }

        inputTextbox.text = "";
        phraseTextbox.text = "";

        WriteCSV();

        nowPhraseCount++;

        if (nowPhraseCount == phrases.Count) // フレーズを全て入力し終えた場合
        {
            EndTest();
        }
        else
        {
            StartTrial();
        }
    }

    // 実験フレーズをリストに格納する
    void LoadCSV()
    {
        var folderPath = "C:\\Users\\t-yamaguchi\\unity\\MyVRTextEntry\\Assets\\phrase_set\\";
        string[] phraseLines;
        if (isPractice)
        {
            phraseLines = File.ReadAllLines(folderPath + "train.csv");
        }
        else
        {
            phraseLines = File.ReadAllLines(folderPath + "test.csv");
        }

        phrases = new List<List<string>>();

        // CSVの各行のデータ組をListに格納
        foreach (var line in phraseLines)
        {
            var items = line.Split(',');
            var pair = new List<string>();
            pair.Add(items[0]);
            pair.Add(items[1]);
            phrases.Add(pair);

        }
    }

    // キーストロークをリストに追加する
    public void AddKeyStroke(string key)
    {
        KeyStroke keyStroke = new KeyStroke(stopwatch.ElapsedMilliseconds, key);
        keyStrokes.Add(keyStroke);
        //UnityEngine.Debug.Log($"{key}: {stopwatch.ElapsedMilliseconds}");
    }

    // 実験結果を記録するCSVを初期化
    void InitCSV()
    {
        file_keystroke = new FileInfo(folderPath + $"data_keystroke_{participantId}_{methodName}_{sessionNum}.csv");
        file_keystroke.Directory.Create();
        File.WriteAllText(file_keystroke.FullName,
            "participant,method,session,isPractice,phrase,phraseNumber,phraseCount,"
            + "keystroke,timestamp\n");

        //file_handmovement = new FileInfo(folderPath + $"data_handmovement_{participantId}_{methodName}_{sessionNum}.csv");
        //file_handmovement.Directory.Create();
        //File.WriteAllText(file_handmovement.FullName,
        //    "participant,method,session,isPractice,phrase,phraseNumber,phraseCount,"
        //    + "finger tip x,finger tip y,finger tip z,"
        //    + "finger root x,finger root y,finger root z,"
        //    + "arm x,arm y,arm z,"
        //    + "timestamp\n");
    }

    // 実験結果をCSVに記録する
    void WriteCSV()
    {
        for (int i = 0; i < keyStrokes.Count; i++)
        {
            File.AppendAllText(file_keystroke.FullName, $"{participantId},{methodName},{sessionNum},{isPractice},{phrases[randomIndexList.ElementAt(nowPhraseCount)][1]},{randomIndexList.ElementAt(nowPhraseCount)},{nowPhraseCount},{keyStrokes[i].key},{keyStrokes[i].timestamp}\n");
        }

        //for (int i = 0; i < handMovement.Count; i++)
        //{
        //    File.AppendAllText(file_handmovement.FullName, $"{participantId},{methodName},{sessionNum},{isPractice},{phrases[randomIndexList.ElementAt(nowPhraseCount)][1]},{randomIndexList.ElementAt(nowPhraseCount)},{nowPhraseCount}," +
        //        $"{handMovement[i].fingerTipPosition.x},{handMovement[i].fingerTipPosition.y},{handMovement[i].fingerTipPosition.z}," +
        //        $"{handMovement[i].fingerRootPosition.x},{handMovement[i].fingerRootPosition.y},{handMovement[i].fingerRootPosition.z}," +
        //        $"{handMovement[i].armPosition.x},{handMovement[i].armPosition.y},{handMovement[i].armPosition.z}," +
        //        $"{handMovement[i].timestamp}\n");
        //}
    }

    // ランダムな整数のリストを生成する
    void GenerateRandomIntList()
    {
        var random = new System.Random();
        var indexList = Enumerable.Range(0, phrases.Count);
        randomIndexList = indexList.OrderBy(x => random.Next()).ToArray();
    }

    // 実験結果からCPMを計算する
    //double CalcCPM()
    //{
    //    // かかった時間を合計する
    //    double timeSumMillisec = 0;
    //    foreach (var timespanMillisec in timespans)
    //    {
    //        timeSumMillisec += timespanMillisec;
    //    }

    //    int charSum = 0;
    //    // 入力されたフレーズの文字数を合計する
    //    foreach (var enteredPhrase in enteredPhrases)
    //    {
    //        charSum += enteredPhrase.Length;
    //    }

    //    var cpm = charSum / (timeSumMillisec * 1e-3 / 60);

    //    return cpm;
    //}
}
