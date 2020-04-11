using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskBot : MonoBehaviour
{
    public GameObject taskPanel;
    public GameObject newTaskIcon;

    public void displayTaskPanel()
    {
        taskPanel.SetActive(true);
        newTaskIcon.SetActive(false);
    }
}
