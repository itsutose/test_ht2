using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.UI;  // 追加しましょう

public class keyManager : MonoBehaviour
{

    // sphereのunityにおける座標を取得する．
    // これは MovePointer でも行われているが，
    // 別のスクリプトに頼るのは少し心もとない，


    public GameObject sphere;
    public TextMeshProUGUI textobject;

    public GameObject a, k, s, t, n, h, m, y, r, w, hen;
    key keyscript;

    private float ux,uy;
    private GameObject[] keylist;
    private GameObject
        nowkey = null, // 今，指があるキー（色の変化に用いる）
        consonant = null, // 子音のキー（押下時に他の子音キーを押さないように）
        priorkey = null; // 一つ前のキー（色の変化時に用いる）
    private bool onoff = false;
    //private Text text;


    // Start is called before the first frame update
    void Start()
    {
        ux = sphere.transform.position.x;
        uy = sphere.transform.position.y;

        keylist = new GameObject[] { a, k, s, t, n, h, m, y, r, w, hen };

        //text = textobject.GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        // ux,uyで座標を取得
        ux = sphere.transform.localPosition.x;
        uy = sphere.transform.localPosition.y;

        // onoff（接触しているかどうか）を取得
        Material mat = sphere.GetComponent<Renderer>().material;
        if(mat.color == Color.red)
        {
            onoff = true;
        }
        else if(mat.color == Color.blue)
        {
            onoff = false;
        }

        // MovePointerのux,uyが受け取れているか調べる
        //Debug.Log("keyMannager :" + ux);

        Debug.Log(onoff);

        // update中はkeylistをforで現在のactionキーを検索する
        foreach (GameObject key in keylist)
        {

            // keyは指が領域内にあるキー

            // 指座標がキー領域内にあるかどうか，なければ次のキーを探す
            if(key.GetComponent<key>().isin(ux, uy) != true)
            {
                continue;
            }

            // 指が接触している
            if (onoff == true)
            {

                // start touch
                if (nowkey == null)// consonant == nullでもよさそう
                {
                    // 子音キーを取得
                    consonant = key;
                    nowkey = key;

                    // 母音キーを表示
                    consonant.GetComponent<key>().visible_key();
                    
                    
                    // ここで，vowelを取得する



                }
                else // being touch
                {

                    if (priorkey != key ) // && priorkey != null)
                    {
                        priorkey.GetComponent<key>().rmcolor();
                    }
                    priorkey = key;
                    nowkey = key;
                    nowkey.GetComponent<key>().takecolor();
                }
            }
            // ホバーしている
            else
            {
                // ホバー中
                if (nowkey == null)
                {
                    // priorkeyがkey（現在のkey）ではない
                    // && priorkeyのnull判定をしないとnull参照する
                    if(priorkey != key && priorkey != null)
                    {
                        priorkey.GetComponent<key>().rmcolor();
                    }
                    
                    priorkey = key;
                    priorkey.GetComponent<key>().takecolor();

                }
                // 指を離したとき
                else
                {
                    // 指を離すとき，場所領域に応じて文字入力を完了
                    textobject.text += nowkey.GetComponent<key>().thistext();

                    // 母音キーの非表示化
                    consonant.GetComponent<key>().in_visible_key();

                    // 色をもとに戻す
                    priorkey.GetComponent<key>().rmcolor();
                    nowkey.GetComponent<key>().rmcolor();

                    nowkey = null;
                    consonant = null;

                }
            }
        }

    }
}
