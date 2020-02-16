using UnityEngine;
using System.Collections;


public class FightManager : MonoBehaviour
{
    FightState state;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public FightState getFightState() {
        return this.state;

    }
}
