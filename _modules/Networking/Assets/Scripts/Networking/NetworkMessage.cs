using UnityEngine;
using System.Collections;

namespace Networking
{
    [System.Serializable]
    public abstract class NetworkMessage
    {
        //Operation code
        public byte OP { set; get; }

        public NetworkMessage()
        {
            OP = NetOP.None;
        }
    }

    public static class NetOP
    {
        public const int None = 0;

    }
}

