using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : MonoBehaviour, TurnManager.IOnSunrise
{
    public bool isFilled;
    public int region;
    private Renderer wellIcon;

    // Start is called before the first frame update
    void Start()
    {
        wellIcon = GetComponent<Renderer>();
        //wellIcon.enabled = false;
        GameGraph.Instance.PlaceAt(gameObject, region);
        TurnManager.Register(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnSunrise()
    {
        if (!isFilled)
        {
            Region currentRegion = GameGraph.Instance.FindNearest(transform.position);
            List<HeroMoveController> heroes = GameGraph.Instance.FindObjectsOnRegion<HeroMoveController>(currentRegion);

            if (heroes.Count == 0)
            {
                isFilled = true;
                //wellIcon.gameObject.enabled = true;
            }
        }
    }

    public void drunk()
    {
        isFilled = false;
        wellIcon.enabled = false;
    }


}
