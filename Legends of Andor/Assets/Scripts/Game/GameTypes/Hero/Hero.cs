using Photon.Pun;
using Routines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : MonoBehaviourPun, IPunObservable
{
    public float radius = 3;
    HeroType charactertype;

    int redDice;
    int blackDice;

    void Start()
    {
        HeroUIData character = (HeroUIData) photonView.Owner.CustomProperties["character"];
        SetUp(character);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && photonView.IsMine)
        {
            MoveToClick();
        }
    }

    public void SetUp(HeroUIData character)
    {
        GetComponent<SpriteRenderer>().sprite = character.GetSprite();
        switch (character.type)
        {
            case HeroType.ARCHER:
                GameGraph.Instance.PlaceAt(gameObject, 53);
                break;
            case HeroType.WARRIOR:
                GameGraph.Instance.PlaceAt(gameObject, 25);
                break;
            case HeroType.DWARF:
                GameGraph.Instance.PlaceAt(gameObject, 43);
                break;
            case HeroType.WIZARD:
                GameGraph.Instance.PlaceAt(gameObject, 9);
                break;
        }

        //save as attribute-chelly
        charactertype = character.type;
    }

    public void MoveToClick()
    {
        Region current = GameGraph.Instance.FindNearest(transform.position);
        Vector3 position = GameGraph.Instance.CastRay(Input.mousePosition);
        Region clicked = GameGraph.Instance.FindNearest(position);
        bool contained = GameGraph.Instance.AdjacentVertices(current).Contains(clicked);
        if (contained && (clicked.position - position).magnitude <= radius)
        {
            StopAllCoroutines();
            StartCoroutine(CommonRoutines.MoveTo(gameObject.transform, clicked.position, 2f));
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
       
    }

    public HeroType getcharacterType() {
        return this.charactertype;
    }


    bool Magic;
    public void initializeMagic() {
        if (this.charactertype == HeroType.WIZARD)
        {
            Magic = true;
        }
        else {
            Magic = false;
        }
    }
    public bool useMagic()
    {
        //check for prefession
        if (Magic) {
            Magic = false;
            return true;
        }
        return false;
    }

    bool HerbS = false;
    public void initializeHerbS()
    {
        //TODO: if have herbs then true
        /*
            if (numHerb > 0)
        {
           HerbS = true;

        }
        else{
            HerbS = false;
        }
         */

    }
    public int useHerbStrength()
    {
        //TODO: return number of increase strength
        //0 means not able to use herb
        if (HerbS)
        {
            HerbS = false;
            int herb = 0;//get number of herbs
            //set number of herbs to 0
            return herb ;
        }

        return 0;

        
    }

    bool Brew = false;
    public void initializeBrew()
    {
        //TODO: same logic
    }

    public void useBrew()
    {
        if (Helm != true)
        {
            Brew = true;
        }
        else
        {
            //Maybe pop some warning message
        }

    }

    bool Helm = false;
    public void useHelm()
    {
        if (Brew != true)
        {
            Helm = true;
        }
        else
        {
            // maybe pop some warning
        }

    }

    private void initial()
    {
        //after each round initialize everything
        Magic = false;
        Brew = false;
        Helm = false;
        HerbS = false;

    }
}
