using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class eval : MonoBehaviour
{
    public Text inputTextbox; // ���̓t�H�[��
    public Text phraseTextbox; // �t���[�Y��\������
    //public HandPosition handPosition;
    public string participantId; // �Q���҂̖��O
    public string methodName; // ��@�̖��O
    public int sessionNum; // �Z�b�V�����̔ԍ��i0~3�j
    public bool isPractice; // ���K�t���[�Y���ǂ���
    [System.NonSerialized] public bool isTest = false; // �e�X�g�����ǂ���

    string folderPath = "C:\\Users\\t-yamaguchi\\unity\\MyVRTextEntry\\Assets\\test_data\\";
    FileInfo file_keystroke;
    //FileInfo file_handmovement;
    Stopwatch stopwatch = new Stopwatch(); // �e�t���[�Y���\������Ă���̎��Ԃ��v������
    List<List<string>> phrases; // �����ŗp����t���[�Y
    IEnumerable<int> randomIndexList; // �������w�肷��C���f�b�N�X�̃��X�g
    int nowPhraseCount = 0; // ���݂̃t���[�Y�̃J�E���^
    class KeyStroke // �e�X�g���Ɏ��W����L�[�X�g���[�N�̃f�[�^�̃N���X
    {
        public KeyStroke(long timestamp, string key)
        {
            this.timestamp = timestamp;
            this.key = key;
        }
        public long timestamp; // �t���[�Y���\������Ă���L�[���͂܂ł̎���
        public string key; // ���͂��ꂽ�L�[�ioption�̏ꍇ��"option"�j
    }
    //class HandMovement // �e�X�g���Ɏ��W�����̍��W�f�[�^�̃N���X
    //{
    //    public HandMovement(long timestamp, Vector3 fingerTipPosition, Vector3 fingerRootPosition, Vector3 armPosition)
    //    {
    //        this.timestamp = timestamp;
    //        this.fingerTipPosition = fingerTipPosition;
    //        this.fingerRootPosition = fingerRootPosition;
    //        this.armPosition = armPosition;
    //    }
    //    public long timestamp; // �t���[�Y���\������Ă���̌o�ߎ���
    //    public Vector3 fingerTipPosition; // �w��̍��W
    //    public Vector3 fingerRootPosition; // �w�̕t�����̍��W
    //    public Vector3 armPosition; // ���̍��W
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

    // �e�X�g���J�n����
    public void StartTest()
    {
        inputTextbox.text = "";
        InitCSV();
        isTest = true;
        GenerateRandomIntList();
        nowPhraseCount = 0;
        StartTrial();
    }

    // �e�X�g���I������
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

    // 1�̃t���[�Y�̓��͂��J�n����
    void StartTrial()
    {
        stopwatch.Restart();
        keyStrokes = new List<KeyStroke>();
        //handMovement = new List<HandMovement>();

        phraseTextbox.text = phrases[randomIndexList.ElementAt(nowPhraseCount)][0] + "\n" + phrases[randomIndexList.ElementAt(nowPhraseCount)][1];
    }

    // ���̓t�H�[���ɓ����Ă��镶������m�肵�C�e�X�g�t���[�Y�Əƍ�
    // ���K�t���[�Y�ł͎��s���Ȃ��悤�ɂ���
    public void Enter()
    {
        if (inputTextbox.text == "") // ���͕������Ȃ���Ԃł̃G���^�[�͌���͂Ƃ݂Ȃ�
        {
            return;
        }

        inputTextbox.text = "";
        phraseTextbox.text = "";

        WriteCSV();

        nowPhraseCount++;

        if (nowPhraseCount == phrases.Count) // �t���[�Y��S�ē��͂��I�����ꍇ
        {
            EndTest();
        }
        else
        {
            StartTrial();
        }
    }

    // �����t���[�Y�����X�g�Ɋi�[����
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

        // CSV�̊e�s�̃f�[�^�g��List�Ɋi�[
        foreach (var line in phraseLines)
        {
            var items = line.Split(',');
            var pair = new List<string>();
            pair.Add(items[0]);
            pair.Add(items[1]);
            phrases.Add(pair);

        }
    }

    // �L�[�X�g���[�N�����X�g�ɒǉ�����
    public void AddKeyStroke(string key)
    {
        KeyStroke keyStroke = new KeyStroke(stopwatch.ElapsedMilliseconds, key);
        keyStrokes.Add(keyStroke);
        //UnityEngine.Debug.Log($"{key}: {stopwatch.ElapsedMilliseconds}");
    }

    // �������ʂ��L�^����CSV��������
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

    // �������ʂ�CSV�ɋL�^����
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

    // �����_���Ȑ����̃��X�g�𐶐�����
    void GenerateRandomIntList()
    {
        var random = new System.Random();
        var indexList = Enumerable.Range(0, phrases.Count);
        randomIndexList = indexList.OrderBy(x => random.Next()).ToArray();
    }

    // �������ʂ���CPM���v�Z����
    //double CalcCPM()
    //{
    //    // �����������Ԃ����v����
    //    double timeSumMillisec = 0;
    //    foreach (var timespanMillisec in timespans)
    //    {
    //        timeSumMillisec += timespanMillisec;
    //    }

    //    int charSum = 0;
    //    // ���͂��ꂽ�t���[�Y�̕����������v����
    //    foreach (var enteredPhrase in enteredPhrases)
    //    {
    //        charSum += enteredPhrase.Length;
    //    }

    //    var cpm = charSum / (timeSumMillisec * 1e-3 / 60);

    //    return cpm;
    //}
}
