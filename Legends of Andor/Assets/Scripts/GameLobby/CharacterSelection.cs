using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviourPun
{
    [Header("List of characters")]
    public List<Character> characters;

    [Header("UI")]
    public Image image;

    private int selectedCharacterIndex; 

    // Start is called before the first frame update
    void Start()
    {
        //SetUp.Instance.Place(image.gameObject, photonView);
        int i = PhotonNetwork.CurrentRoom.PlayerCount;
        print(i);
        if (photonView.IsMine)
        {
            print("t");
            image.gameObject.transform.position = SetUp.Instance.spawnPoints[i - 1].position;
            print(gameObject.transform.position.x);
        }
        Display();
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
    public CharacterType characterType;
    public bool gender; // how to implement gender
    public Sprite male;
    public Sprite female;

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
