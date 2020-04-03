using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class ResourcesManager : MonoBehaviourPun
{
    public TextMeshProUGUI playerName;
    public ResourceDistributeUI gold;
    public ResourceDistributeUI wineSkin;

    // Start is called before the first frame update
    void Start()
    {
        playerName.text = photonView.Owner.NickName;
        gold.resource = DistributionManager.Instance.gold;
        wineSkin.resource = DistributionManager.Instance.wineskin;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
