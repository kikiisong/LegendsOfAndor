using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistributionManager : MonoBehaviour
{
    public static DistributionManager Instance;

    [Header("Next")]
    [SceneName]
    public string nextScene;
    public Button next;

    [Header("Instantiate")]
    public GameObject parent;
    public GameObject resourcePrefab;

    public Resource gold;
    public Resource wineskin;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject r = PhotonNetwork.Instantiate(resourcePrefab);
        r.GetComponent<ResourcesManager>().SetParentRPC(parent);
        next.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.IsMasterClient && gold.amount == 0 && wineskin.amount == 0)
        {
            next.gameObject.SetActive(true);
        }
    }

    public void Click_Continue()
    {
        PhotonNetwork.LoadLevel(nextScene);
    }
}
