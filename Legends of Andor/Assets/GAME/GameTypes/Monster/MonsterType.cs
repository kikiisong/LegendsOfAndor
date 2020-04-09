using System;
using System.Reflection;

namespace Monsters
{
    public enum MonsterType
    {
        [Monster(1, 1)] Gor,
        [Monster(1, 1)] Skral,
        [Monster(1, 1)] Wardrak
    }

    class MonsterAttribute: Attribute
    {
        public int WP { get; private set; }
        public int Damage { get; private set; }

        internal MonsterAttribute(int wp, int damage)
        {
            WP = wp;
            Damage = damage;
        }
    }

    public static class MonsterExtensions
    {
        public static int WP(this MonsterType m)
        {
            return GetAttribute(m).WP;
        }

        public static int Damage(this MonsterType m)
        {
            return GetAttribute(m).Damage;
        }

        private static MonsterAttribute GetAttribute(MonsterType m)
        {
            return (MonsterAttribute)Attribute.GetCustomAttribute(ForValue(m), typeof(MonsterAttribute));
        }
        private static MemberInfo ForValue(MonsterType m)
        {
            return typeof(MonsterType).GetField(Enum.GetName(typeof(MonsterType), m));
        }
    }
}

