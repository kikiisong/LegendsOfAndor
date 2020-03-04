using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerCreator : MonoBehaviour
{
    [SerializeField] Farmer farmerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 1; i++)
        {
            Farmer temp = Instantiate<Farmer>(farmerPrefab, gameObject.transform);
           // temp.region = i;
        }
    }
}