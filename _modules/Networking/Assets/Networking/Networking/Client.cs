using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;


namespace Networking
{
    public abstract class Client : MonoBehaviour
    {
        public string clientName;
        public bool isHost;

        private bool socketReady;
        private TcpClient socket;
        private NetworkStream stream;
        private StreamWriter writer;
        private StreamReader reader;

        private List<GameClient> players = new List<GameClient>();

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
        private void Update()
        {
            if (socketReady)
            {
                if (stream.DataAvailable)
                {
                    string data = reader.ReadLine();
                    if (data != null)
                    {
                        OnIncomingData(data);
                    }
                }
            }
        }
        public bool ConnectToServer(string host, int port)
        {
            if (socketReady) return false;

            try
            {
                socket = new TcpClient(host, port);
                stream = socket.GetStream();
                writer = new StreamWriter(stream);
                reader = new StreamReader(stream);

                socketReady = true;
            }
            catch (Exception e)
            {
                Debug.Log("Socket error: " + e.StackTrace);
            }

            return socketReady;
        }

        //Sending messages to the server
        public void Send(string data)
        {
            if (!socketReady) return;

            writer.WriteLine(data);
            writer.Flush();
        }

        //Read messages from the server
        protected abstract void OnIncomingData(string data);

        private void UserConnected(string name)
        {
            GameClient c = new GameClient
            {
                name = name
            };

            players.Add(c);

            Debug.Log("Count: " + players.Count);
        }

        private void OnApplicationQuit()
        {
            CloseSocket();
        }

        private void OnDisable()
        {
            CloseSocket();
        }

        private void CloseSocket()
        {
            if (!socketReady)
            {
                return;
            }

            writer.Close();
            reader.Close();
            socket.Close();
            socketReady = false;
        }
    }

    public class GameClient
    {
        public string name;
        public bool isHost;
    }
}

