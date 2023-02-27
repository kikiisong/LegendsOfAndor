using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Saving
{
    public static class J 
    {
        public static JObject FromHero(Hero hero, int region=-1)
        {
            return new JObject
            {
                {"type", new JValue(hero.type) },
                {"data", JObject.FromObject(hero.data)},
                {"gender", hero.ui.gender },
                {"region", region }
            };
        }

        public static Hero ToHero(JToken jToken)
        {
            var hero = Hero.FindInResources(jToken["type"].ToObject<Hero.Type>());
            hero.data = jToken["data"].ToObject<Hero.Data>();
            hero.ui.gender = jToken["gender"].ToObject<bool>();
            return hero;
        }
    }
}
