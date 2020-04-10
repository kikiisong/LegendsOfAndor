using System;
using System.Reflection;

namespace Monsters
{
    public enum MonsterType
    {
        Gor,
        Skral,
        Wardrak
    }
    
    [Serializable]
    public class MonsterData
    {
        public int wp;
        public int sp;
    }
}

