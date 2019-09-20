using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int a = 1;
    public Fruit fruit = new Fruit("fruit");
    public int b = 2;

    public Container container = new Container(); 
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("" + a + " " + fruit.name + " " + b);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Fruit
{
    public string name = "a";
    public Fruit(string name)
    {
        this.name = name;
    }
}

public class Container
{
    public Fruit reference;
}

public class PermanentFruit : ScriptableObject
{
    string fruitName;

    public static PermanentFruit Create(string name)
    {
        PermanentFruit permanentFruit = ScriptableObject.CreateInstance<PermanentFruit>();
        permanentFruit.fruitName = name;
        return permanentFruit;
    }

    
}