using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
using System.Net.Sockets;

namespace Networking
{
    public static class NetworkUtils
    {
        /// <summary>
        /// Returns a serialized version of the message as a string
        /// </summary>
        public static string ConvertToString<M>(M networkMessage)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            formatter.Serialize(ms, networkMessage);

            return Encoding.ASCII.GetString(ms.GetBuffer());
        }

        public static O ConvertTo<O>(string serializedMessage)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream receive = new MemoryStream(Encoding.ASCII.GetBytes(serializedMessage));
            O message = (O)formatter.Deserialize(receive);
            return message;
        }

        public static NetworkMessage ConvertToMessage(string serializedMessage)
        {
            return ConvertTo<NetworkMessage>(serializedMessage);
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            Debug.Log(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "didnt work";
        }
    }
}
