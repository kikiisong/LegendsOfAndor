using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : MonoBehaviour, TurnManager.IOnSunrise
{
    public int region;

    private Renderer wellIcon;
    public bool IsFilled
    {
        get;
        private set;
    } = true;

    // Start is called before the first frame update
    void Start()
    {
        wellIcon = GetComponent<Renderer>();
        //wellIcon.enabled = false;
        Region r = GameGraph.Instance.FindNearest(transform.position);
        region = r.label;
        TurnManager.Register(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSunrise()
    {
        if (!IsFilled)
        {
            Region currentRegion = GameGraph.Instance.FindNearest(transform.position);
            List<HeroMoveController> heroes = GameGraph.Instance.FindObjectsOnRegion<HeroMoveController>(currentRegion);

            if (heroes.Count == 0)
            {
                IsFilled = true;
                wellIcon.enabled = true;
            }
        }
    }

    public void Drunk()
    {
        IsFilled = false;
        wellIcon.enabled = false;
    }

    public void emptied()
    {
        IsFilled = false;
        wellIcon = GetComponent<Renderer>();
        wellIcon.enabled = false;
    }

}
