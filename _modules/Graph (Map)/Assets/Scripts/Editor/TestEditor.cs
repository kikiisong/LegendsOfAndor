using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Test))]
public class TestEditor : Editor
{
    public Test test;

    private void OnEnable()
    {
        test = target as Test;
        //test.container.reference = test.fruit;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //test.container.reference = test.fruit;
    }
}