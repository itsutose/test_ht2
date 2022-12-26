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
        //ポート番号を指定
        //ws = new WebSocketServer(12361);  // ws_home
        ws = new WebSocketServer(12356); //ws_invb2
        //クライアントからの通信時の挙動を定義したクラス、「ExWebSocketBehavior」を登録
        ws.AddWebSocketService<ExWebSocketBehavior>("/");
        //サーバ起動
        ws.Start();
        Debug.Log("サーバ起動");
    }

    public string get_coordinates()
    {
        return ServerManager.coordinates;
    }


    private void OnApplicationQuit()
    {
        Debug.Log("サーバ停止");
        ws.Stop();
    }

    public class ExWebSocketBehavior : WebSocketBehavior
    {
        //誰が現在接続しているのか管理するリスト。
        public static List<ExWebSocketBehavior> clientList = new List<ExWebSocketBehavior>();
        //接続者に番号を振るための変数。
        static int globalSeq = 0;
        //自身の番号
        int seq;

        // Androidから送られるデータを格納する
        public string edata;

        //誰かがログインしてきたときに呼ばれるメソッド
        protected override void OnOpen()
        {
            //ログインしてきた人には、番号をつけて、リストに登録。
            globalSeq++;
            this.seq = globalSeq;
            clientList.Add(this);

            Debug.Log("Seq" + this.seq + " Login. (" + this.ID + ")");

            //接続者全員にメッセージを送る
            foreach (var client in clientList)
            {
                client.Send("Seq:" + seq + " Login.");
            }
        }

        //誰かがメッセージを送信してきたときに呼ばれるメソッド
        protected override void OnMessage(MessageEventArgs e)
        {
            //if (debug.Contains("1"))
            //{
            //    Debug.Log(e.Data);
            //}
            //if(prior != e.Data)
            //{
            //    prior = e.Data;
                
            //}
            //else
            //{

            //}

            ServerManager.coordinates = e.Data;

            //if()

            //log();
        }

        public void log()
        {
            //Debug.Log("servermanager.coordinates :" + ServerManager.coordinates);
        }
            ////接続者全員にメッセージを送る
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

        //誰かがログアウトしてきたときに呼ばれるメソッド
        protected override void OnClose(CloseEventArgs e)
        {
            Debug.Log("Seq" + this.seq + " Logout. (" + this.ID + ")");

            //ログアウトした人を、リストから削除。
            clientList.Remove(this);

            //接続者全員にメッセージを送る
            foreach (var client in clientList)
            {
                client.Send("Seq:" + seq + " Logout.");
            }
        }
    }
}