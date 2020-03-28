using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class Farmer : MonoBehaviour
{
    [SerializeField] public int numberOfFarmer;
    public Transform textOfFarmer;
    private Renderer farmerPic;
    public int region;
    [SerializeField] private GameObject Graph;
    public GameGraph gameGraph;
    public GameObject pickUpButton;

    public void Start()
    {
      //  numberOfFarmer = 0;
        textOfFarmer = transform.Find("Text");
        textOfFarmer.gameObject.SetActive(false);
        farmerPic = GetComponent<Renderer>();
        farmerPic.enabled = false;
        Graph = GameObject.Find("Graph");
        gameGraph = Graph.GetComponent<GameGraph>();
        gameGraph.PlaceAt(gameObject, region);
        //
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        
      // displayNum();
        
    }

    public void Update()
    {
        displayNum();
        ifthereisAMonster();
    }

    // detect if there is a monster on this region
    private void ifthereisAMonster()
    {
        Region currentRegion = findCurrentRegion();

        List<MonsterMoveController> MonsterOnRegion = GameGraph.Instance.FindObjectsOnRegion<MonsterMoveController>(currentRegion);

        if (MonsterOnRegion.Count == 0)
        {
            return;
        }
        else
        {
          //  print("there is a monster on this region and the region number is " + region);
          //  print("the number of monster is " + MonsterOnRegion.Count);
            numberOfFarmer = 0;
        }
    }

    public void displayNum()
    {
        if(numberOfFarmer > 0)
        {
            farmerPic.enabled = true;
            textOfFarmer.gameObject.SetActive(true);
            textOfFarmer.gameObject.GetComponent<TextMesh>().text = numberOfFarmer.ToString();
        }
        else
        {
            farmerPic.enabled = false;
            textOfFarmer.gameObject.SetActive(false);
        }
    }

    public void setNumOfFarmer(int num)
    {
        numberOfFarmer = num;

    }

    public void decrease()
    {
        if (numberOfFarmer > 0)
        {
            numberOfFarmer = numberOfFarmer - 1;
        }
    }

    public void increase()
    {
        if(numberOfFarmer < 2)
        {
            numberOfFarmer = numberOfFarmer + 1;
        }
    }

    public int getNumOfFarmer()
    {
        return numberOfFarmer;
    }
    
    public void farmersDied()
    {
        numberOfFarmer = 0;
    }

    private Region findCurrentRegion()
    {
        return GameGraph.Instance.FindNearest(transform.position);

    }
}
