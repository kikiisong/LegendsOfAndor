using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavedGamesMenu : MonoBehaviourPunCallbacks
{

    public GameObject listingPrefab;
    public Transform content;

    [SceneName] public string nextScene;


    // Start is called before the first frame update
    void Start()
    {
        print("Saved folder: " + Saving.Helper.Path_Directory);
        foreach(var path in Directory.GetFiles(Saving.Helper.Path_Directory))
        {
            var go = Instantiate(listingPrefab, content);
            go.GetComponent<SavedGameListing>().Init(Path.GetFileNameWithoutExtension(path));
        }
    }

    public override void OnCreatedRoom()
    {
        if (!MainLobbyManager.IsSaved) return;
        print("Created saved room successfully.");
        PhotonNetwork.LoadLevel(nextScene);
    }
}
