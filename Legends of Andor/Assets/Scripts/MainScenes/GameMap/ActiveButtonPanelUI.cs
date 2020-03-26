using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActiveButtonPanelUI : MonoBehaviour
{
    public GameObject Panel;
    public TextMeshProUGUI buttonDisplay;



    public void HidePanel()
    {
        if (Panel != null)
        {
            Animator animator = Panel.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("open");
                animator.SetBool("open", !isOpen);
                if (isOpen) buttonDisplay.text = ">";
                else buttonDisplay.text = "< ";
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
