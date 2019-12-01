using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Heroes")]
public class Heroes : SingletonScriptableObject<Heroes>
{
    
    public List<Character> characters;
}
