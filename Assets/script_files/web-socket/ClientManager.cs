using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Net;
using UnityEngine.UI;
using TMPro;

public class ClientManager : MonoBehaviour
{
    public WebSocket ws;
    public Text chatText;
    // public TextMeshProUGUI chatText;
    public Button sendButton ;
    public InputField messageInput;

    //�T�[�o�ցA���b�Z�[�W�𑗐M����
    public void SendText()
    {
        ws.Send(messageInput.text);
    }

    //�T�[�o����󂯎�������b�Z�[�W���AChatText�ɕ\������
    public void RecvText(string text)
    {
        chatText.text += (text + "\n");
    }
    //�T�[�o�̐ڑ����؂ꂽ�Ƃ��̃��b�Z�[�W���AChatText�ɕ\������
    public void RecvClose()
    {
        chatText.text = ("Close.");
    }

    void Start()
    {
        //�ڑ������B�ڑ���T�[�o�ƁA�|�[�g�ԍ����w�肷��
        ws = new WebSocket("ws://localhost:12345/");
        ws.Connect();

        //���M�{�^���������ꂽ�Ƃ��Ɏ��s���鏈���uSendText�v��o�^����
        sendButton.onClick.AddListener(SendText);
        //�T�[�o���烁�b�Z�[�W����M�����Ƃ��Ɏ��s���鏈���uRecvText�v��o�^����
        ws.OnMessage += (sender, e) => RecvText(e.Data);
        //�T�[�o�Ƃ̐ڑ����؂ꂽ�Ƃ��Ɏ��s���鏈���uRecvClose�v��o�^����
        ws.OnClose += (sender, e) => RecvClose();
    }
}