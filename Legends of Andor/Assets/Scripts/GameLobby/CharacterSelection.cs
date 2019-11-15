using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviourPun
{
    [Header("List of characters")]
    public List<Character> characters;

    [Header("UI")]
    public Image image;

    private int selectedCharacterIndex; 
    
    public Character CurrentCharacter {
        get
        {
            return characters[selectedCharacterIndex];
        }
    }

    //Careful with null
    public static List<Character> Characters;

    // Start is called before the first frame update
    void Start()
    {
        SetUp.Instance.Place(image.gameObject, photonView);
        Display();
        Characters = characters;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Next();
        }
    }


    public void Display()
    {
        image.sprite = characters[selectedCharacterIndex].GetSprite();
    }

    public void Switch()
    {
        characters[selectedCharacterIndex].ToggleGender();
        Display();
    }

    private void Next()
    {
        selectedCharacterIndex = (selectedCharacterIndex + 1) % characters.Count;
        Display();
    }
}


[System.Serializable]
public class Character
{
    public CharacterType type;
    public bool gender; // how to implement gender
    
    public Sprite female;
    public Sprite male;

    public Sprite GetSprite()
    {
        return gender ? male : female;
    }

    public void ToggleGender()
    {
        gender = !gender;
    }

}

public enum CharacterType
{
    ARCHER, WARRIOR, MAGE, DWARF
}
