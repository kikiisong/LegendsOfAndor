using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ExtraShield : MonoBehaviour
{
    [SerializeField] public int numberOfShileds;
    public Transform textOfShileds;
    private Renderer shiledPic;


    public void Start()
    {
        //  numberOfFarmer = 0;
        textOfShileds = transform.Find("Text");
        textOfShileds.gameObject.SetActive(false);
        shiledPic = GetComponent<Renderer>();
        shiledPic.enabled = false;

    }

    public void Update()
    {
        displayNum();
    }

    public void displayNum()
    {
        if (numberOfShileds > 0)
        {
            shiledPic.enabled = true;
            textOfShileds.gameObject.SetActive(true);
            textOfShileds.gameObject.GetComponent<TextMesh>().text = numberOfShileds.ToString();
        }
        else
        {
            shiledPic.enabled = false;
            textOfShileds.gameObject.SetActive(false);
        }
    }

    [PunRPC]
    public void increaseShieldsNum()
    {
        this.numberOfShileds++;
    }

    [PunRPC]
    public void decreaseShieldsNum()
    {
        this.numberOfShileds--;
    }
}
