using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chat : MonoBehaviour, IChatClientListener
{
    public ChatClient chatClient;

    public void Start()
    {
        Application.runInBackground = true;
        Connect();
    }

    public void Update()
    {
        chatClient.Service();
    }

    private void Connect()
    {
        chatClient = new ChatClient(this);
        string chatId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat;
        string version = PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion;
        chatClient.ChatRegion = "US";
        chatClient.Connect(chatId, version, new AuthenticationValues("name"));
        print("Chat : Connecting " + version + ", " + chatId);
    }

    public void DebugReturn(DebugLevel level, string message)
    {

    }

    public void OnChatStateChange(ChatState state)
    {

    }

    public void OnConnected()
    {
        print("Chat : Connected");
        chatClient.Subscribe(new string[] { "testChat" });
    }

    public void OnDisconnected()
    {
        throw new System.NotImplementedException();
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        print("Chat - Received");
        foreach(object message in messages)
        {
            print(message);
        }

        foreach(string sender in senders)
        {
            print(sender);
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        print("Chat : Subscribed");
        foreach(string c in channels)
        {
            print(c);
        }
        chatClient.PublishMessage("testChat", "hello there");
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

}
