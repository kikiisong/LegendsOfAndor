using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace Networking
{
    public abstract class Server : MonoBehaviour
    {
        public int port = 6321; //will it always work?

        private List<ServerClient> clients;
        private List<ServerClient> disconnectList;

        private TcpListener server;
        private bool serverStarted;

        private void Update()
        {
            if (!serverStarted) return;

            foreach (ServerClient c in clients)
            {
                //Is client connected?
                if (!IsConnected(c.tcp))
                {
                    c.tcp.Close();
                    disconnectList.Add(c);
                    continue;
                }
                else
                {
                    //Get data
                    NetworkStream s = c.tcp.GetStream();
                    if (s.DataAvailable)
                    {
                        StreamReader reader = new StreamReader(s, true);
                        string data = reader.ReadLine(); //Can there be two lines?

                        if (data != null)
                        {
                            OnIncomingData(c, data);
                        }
                    }
                }

            }

            for (int i = 0; i < disconnectList.Count - 1; i++)
            {
                //Tell our player somebody has disconnected
                clients.Remove(disconnectList[i]);
                disconnectList.RemoveAt(i);
            }
        }

        //Server Send
        private void BroadCast(string data, List<ServerClient> cl)
        {
            foreach (ServerClient sc in cl)
            {
                try
                {
                    StreamWriter writer = new StreamWriter(sc.tcp.GetStream());
                    writer.WriteLine(data);
                    writer.Flush();
                }
                catch (Exception e)
                {
                    Debug.Log("Write error: " + e.Message);
                }
            }
        }

        private void BroadCast(string data, ServerClient serverClient)
        {
            List<ServerClient> client = new List<ServerClient>() { serverClient };
            BroadCast(data, client);
        }

        //Server Read
        private void OnIncomingData(ServerClient c, string data)
        {
            Debug.Log(c.clientName + ": " + data);
            
        }

        public void Init()
        {
            DontDestroyOnLoad(gameObject);
            clients = new List<ServerClient>();
            disconnectList = new List<ServerClient>();

            try
            {
                server = new TcpListener(IPAddress.Any, port);
                server.Start();

                StartListening();
                serverStarted = true;
            }
            catch (Exception e)
            {
                Debug.Log("Socket error: " + e.Message);
            }

        }

        private void StartListening()
        {
            server.BeginAcceptTcpClient(AcceptTcpClient, server);
        }

        private void AcceptTcpClient(IAsyncResult ar)
        {
            TcpListener listener = (TcpListener) ar.AsyncState;

            string allUsers = "";
            foreach (ServerClient s in clients)
            {
                allUsers += s.clientName + "|";
            }

            ServerClient sc = new ServerClient(listener.EndAcceptTcpClient(ar));
            clients.Add(sc);

            StartListening();
            Debug.Log("Somebody has connected.");

            BroadCast("SWHO|" + allUsers, clients[clients.Count - 1]);
        }

        private bool IsConnected(TcpClient c)
        {
            try
            {
                if (c != null && c.Client != null && c.Client.Connected)
                {
                    if (c.Client.Poll(0, SelectMode.SelectRead))
                    {
                        return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                    }
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

    }

    public class ServerClient
    {
        public string clientName;
        public TcpClient tcp;

        public bool isHost;

        public ServerClient(TcpClient tcp)
        {
            this.tcp = tcp;
        }
    }
}