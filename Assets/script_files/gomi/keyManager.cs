using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.UI;  // �ǉ����܂��傤

public class keyManager : MonoBehaviour
{

    // sphere��unity�ɂ�������W���擾����D
    // ����� MovePointer �ł��s���Ă��邪�C
    // �ʂ̃X�N���v�g�ɗ���̂͏����S���ƂȂ��C


    public GameObject sphere;
    public TextMeshProUGUI textobject;

    public GameObject a, k, s, t, n, h, m, y, r, w, hen;
    key keyscript;

    private float ux,uy;
    private GameObject[] keylist;
    private GameObject
        nowkey = null, // ���C�w������L�[�i�F�̕ω��ɗp����j
        consonant = null, // �q���̃L�[�i�������ɑ��̎q���L�[�������Ȃ��悤�Ɂj
        priorkey = null; // ��O�̃L�[�i�F�̕ω����ɗp����j
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
        // ux,uy�ō��W���擾
        ux = sphere.transform.localPosition.x;
        uy = sphere.transform.localPosition.y;

        // onoff�i�ڐG���Ă��邩�ǂ����j���擾
        Material mat = sphere.GetComponent<Renderer>().material;
        if(mat.color == Color.red)
        {
            onoff = true;
        }
        else if(mat.color == Color.blue)
        {
            onoff = false;
        }

        // MovePointer��ux,uy���󂯎��Ă��邩���ׂ�
        //Debug.Log("keyMannager :" + ux);

        Debug.Log(onoff);

        // update����keylist��for�Ō��݂�action�L�[����������
        foreach (GameObject key in keylist)
        {

            // key�͎w���̈���ɂ���L�[

            // �w���W���L�[�̈���ɂ��邩�ǂ����C�Ȃ���Ύ��̃L�[��T��
            if(key.GetComponent<key>().isin(ux, uy) != true)
            {
                continue;
            }

            // �w���ڐG���Ă���
            if (onoff == true)
            {

                // start touch
                if (nowkey == null)// consonant == null�ł��悳����
                {
                    // �q���L�[���擾
                    consonant = key;
                    nowkey = key;

                    // �ꉹ�L�[��\��
                    consonant.GetComponent<key>().visible_key();
                    
                    
                    // �����ŁCvowel���擾����



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
            // �z�o�[���Ă���
            else
            {
                // �z�o�[��
                if (nowkey == null)
                {
                    // priorkey��key�i���݂�key�j�ł͂Ȃ�
                    // && priorkey��null��������Ȃ���null�Q�Ƃ���
                    if(priorkey != key && priorkey != null)
                    {
                        priorkey.GetComponent<key>().rmcolor();
                    }
                    
                    priorkey = key;
                    priorkey.GetComponent<key>().takecolor();

                }
                // �w�𗣂����Ƃ�
                else
                {
                    // �w�𗣂��Ƃ��C�ꏊ�̈�ɉ����ĕ������͂�����
                    textobject.text += nowkey.GetComponent<key>().thistext();

                    // �ꉹ�L�[�̔�\����
                    consonant.GetComponent<key>().in_visible_key();

                    // �F�����Ƃɖ߂�
                    priorkey.GetComponent<key>().rmcolor();
                    nowkey.GetComponent<key>().rmcolor();

                    nowkey = null;
                    consonant = null;

                }
            }
        }

    }
}
