using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckInfo : MonoBehaviour
{

    public Texture2D info;
    private bool cursorVisible;
    public GameObject Panel;

    public TextMeshProUGUI field1;
    public TextMeshProUGUI field2;
    public TextMeshProUGUI field3;

    public Button next;
    public Button prev;

    public Image icon;


    private int current = 0;
    private int max = 0;

    private List<Monster> monsters;

    private List<HeroMoveController> heroMoveControllers;


    void Start()
    {
        cursorVisible = false;

    }

    void Update()
    {


        if (Input.GetMouseButtonDown(0) && cursorVisible)
        {
            checkInfo();
        }


    }

    public void changeCursorOnClick()
    {
        if (cursorVisible == false)
        {
            Cursor.SetCursor(info, new Vector2(25, 25), CursorMode.ForceSoftware);
            cursorVisible = true;
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
            cursorVisible = false;

            if (Panel != null && Panel.activeSelf == true)
            {

                Panel.SetActive(false);
            }

        }
    }




    public void checkInfo()
    {


        Vector3 position = GameGraph.Instance.CastRay(Input.mousePosition);
        Region clicked = GameGraph.Instance.FindNearest(position);

        int clickedRegionLabel = clicked.label;

        monsters = GameGraph.Instance.FindObjectsOnRegion<Monster>(clicked);

        heroMoveControllers = GameGraph.Instance.FindObjectsOnRegion<HeroMoveController>(clicked);

        max = monsters.Count + heroMoveControllers.Count;



        if (max != 0)
        {
            print("not 0");
            openInfoPanel();

            showInfo(current);

            if (current == 0 && max == 1)
            {
                next.gameObject.SetActive(false);
                prev.gameObject.SetActive(false);
            }
            else if (current == 0)
            {
                next.gameObject.SetActive(true);
                prev.gameObject.SetActive(false);
            }
            else if (current == max - 1)
            {
                next.gameObject.SetActive(false);
                prev.gameObject.SetActive(true);
            }
            else
            {
                next.gameObject.SetActive(true);
                prev.gameObject.SetActive(true);
            }
        }

    }

    private void openInfoPanel()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }

    }



    private void showInfo(int num)
    {
        //show moster info
        if (monsters != null && heroMoveControllers != null)
        {

            if (num <= monsters.Count - 1)
            {
                //TODO: change from currentWP to maxinum WP
                int monsterWP = monsters[num].maxWP;
                int monsterDamage = monsters[num].damage;
                field1.text = "WILLPOWER:  " + monsterWP.ToString();
                field2.text = "DAMAGE:  " + monsterDamage.ToString();

            }
            else if (num <= monsters.Count + heroMoveControllers.Count - 1)
            {
                Hero hero = (Hero)heroMoveControllers[num - monsters.Count].photonView.Owner.GetHero();
                icon.sprite = hero.ui.GetSprite();
                field1.text = "WILLPOWER:  " + hero.data.WP.ToString();
                field2.text = "STRENGTH:  " + hero.data.SP.ToString();
                field3.text = "GOLD: " + hero.data.gold.ToString();
            }

        }
    }



    public void showNext()
    {
        if (current + 1 < max)
        {
            current += 1;
            showInfo(current);
        }



    }

    public void showPrev()
    {
        if (current - 1 >= 0)
        {
            current -= 1;
            showInfo(current);
        }


    }
}