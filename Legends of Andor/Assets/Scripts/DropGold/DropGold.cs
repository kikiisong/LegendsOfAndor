using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class DropGold : MonoBehaviour
{
    Button myBtn = GameObject.Find("DropButton").GetComponent<Button>();
    Region region; 
    // Start is called before the first frame update
    void Start()
    {
        Region region = GameGraph.Instance.FindNearest(transform.position);
        myBtn.onClick.AddListener(popUp);
    }

    // Update is called once per frame
    void Update()
    {
        Region old = GameGraph.Instance.FindNearest(transform.position);
        if (region != old) {
            string regionNum = Regex.Replace(old.ToString(), "[^0-9]", "");
            myBtn.GetComponentInChildren<Text>().text = regionNum;
            Debug.Log("this is from aosk" + regionNum + " button name is " + myBtn.ToString());
        }
    }
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(1)) // if rigth clicked 
        {
            popUp();
        }
    }
    
    //window to display how many gold to drop 
    void popUp()
    {

    }
}
