using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;


public class ServerManager : MonoBehaviour
{
    private static string coordinates = null;
    public string debug = null;
    private string prior = null;

    WebSocketServer ws;
    void Start()
    {
        //�|�[�g�ԍ����w��
        //ws = new WebSocketServer(12361);  // ws_home
        ws = new WebSocketServer(12362); //ws_invb2
        //�N���C�A���g����̒ʐM���̋������`�����N���X�A�uExWebSocketBehavior�v��o�^
        ws.AddWebSocketService<ExWebSocketBehavior>("/");
        //�T�[�o�N��
        ws.Start();
        Debug.Log("�T�[�o�N��");
    }

    public string get_coordinates()
    {
        return ServerManager.coordinates;
    }


    private void OnApplicationQuit()
    {
        Debug.Log("�T�[�o��~");
        ws.Stop();
    }

    public class ExWebSocketBehavior : WebSocketBehavior
    {
        //�N�����ݐڑ����Ă���̂��Ǘ����郊�X�g�B
        public static List<ExWebSocketBehavior> clientList = new List<ExWebSocketBehavior>();
        //�ڑ��҂ɔԍ���U�邽�߂̕ϐ��B
        static int globalSeq = 0;
        //���g�̔ԍ�
        int seq;

        // Android���瑗����f�[�^���i�[����
        public string edata;

        //�N�������O�C�����Ă����Ƃ��ɌĂ΂�郁�\�b�h
        protected override void OnOpen()
        {
            //���O�C�����Ă����l�ɂ́A�ԍ������āA���X�g�ɓo�^�B
            globalSeq++;
            this.seq = globalSeq;
            clientList.Add(this);

            Debug.Log("Seq" + this.seq + " Login. (" + this.ID + ")");

            //�ڑ��ґS���Ƀ��b�Z�[�W�𑗂�
            foreach (var client in clientList)
            {
                client.Send("Seq:" + seq + " Login.");
            }
        }

        //�N�������b�Z�[�W�𑗐M���Ă����Ƃ��ɌĂ΂�郁�\�b�h
        protected override void OnMessage(MessageEventArgs e)
        {
            //Debug.Log(string.Format("ServerMessage OnMessage {0}, Type: {1}", e.Data, e.Data.GetType()));

            if (e.Data != null)
            {
                string dataString = e.Data.ToString();
                string[] splitData = dataString.Split(' ');

                if (splitData.Length > 0)
                {
                    string firstWord = splitData[0];

                    if (firstWord == "1" || firstWord == "0")
                    {
                        //Debug.Log(string.Format("ServerMessage OnMessage {0}, Type: {1}", e.Data, e.Data.GetType()));
                    }
                    //else if (firstWord == "TOUCH_DOWN")
                    //{
                    //    Debug.Log(string.Format("ServerMessage OnMessage {0}", e.Data));
                    //}
                    //else if (firstWord == "TOUCH_UP")
                    //{
                    //    Debug.Log(string.Format("ServerMessage OnMessage {0}", e.Data));
                    //}
                    //else
                    //{
                    //    Debug.Log(string.Format("ServerMessage OnMessage {0}", e.Data));
                    //}
                }
                //else
                //{
                //    //TOUCH_DOWN��TOUCH_UP�̂Ƃ�
                //    //Debug.Log(splitData);
                //    Debug.Log(string.Format("ServerMessage OnMessage e.Data {0}", e.Data));

                //}
            }
            else
            {
                Debug.Log("ServerMessage OnMessage: e.Data is null");
            }

            

            ServerManager.coordinates = e.Data;

            if(e.Data == "TOUCH_UP")
            {
                Debug.Log("TOUCH_UP");
            }
            else if(e.Data == "TOUCH_DOWN")
            {
                Debug.Log("TOUCH_DOWN");
            }

            //log();
        }

        public void log()
        {
            //Debug.Log("servermanager.coordinates :" + ServerManager.coordinates);
        }
            ////�ڑ��ґS���Ƀ��b�Z�[�W�𑗂�
            //foreach (var client in clientList)
            //{
            //    client.Send("Seq:" + seq + "..." + e.Data);
            //}
        //}

        //public void getMessage(MessageEventArgs e)
        //{
        //    //ServerManager server;
        //    //Debug.Log("Seq:" + seq + "..." + e.Data);
        //    edata = e.Data;
        //    //server.coordinates = edata;

        //    //changecoord(edata);
        //}

        //public string getedata()
        //{
        //    if(edata != null)
        //    {
        //        return edata;
        //    }
        //    else
        //    {
        //        return "xxxxx";
        //    }
        //}

        //�N�������O�A�E�g���Ă����Ƃ��ɌĂ΂�郁�\�b�h
        protected override void OnClose(CloseEventArgs e)
        {
            Debug.Log("Seq" + this.seq + " Logout. (" + this.ID + ")");

            //���O�A�E�g�����l���A���X�g����폜�B
            clientList.Remove(this);

            //�ڑ��ґS���Ƀ��b�Z�[�W�𑗂�
            foreach (var client in clientList)
            {
                client.Send("Seq:" + seq + " Logout.");
            }
        }
    }
}