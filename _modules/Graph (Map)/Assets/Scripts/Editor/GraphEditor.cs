using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Graph;

[CustomEditor(typeof(GameGraph))]
public class GraphEditor : Editor
{
    GameGraph graph;

    void OnEnable()
    {
        graph = target as GameGraph;
    }

    //public void ShowArrayProperty(SerializedProperty list)
    //{
    //    EditorGUILayout.PropertyField(list);
    //    EditorGUI.indentLevel++;
    //    for(int i = 0; i < list.arraySize; i++)
    //    {
    //        EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), new GUIContent("hey"));
    //    }

    //    EditorGUI.indentLevel--;
    //}

    //private void OnSceneGUI()
    //{
    //    foreach(Region location in graph.vertices)
    //    {
    //        Vector3 worldPos;
    //        if (Application.isPlaying)
    //        {
    //            worldPos = location.position;
    //        }
    //        else
    //        {
    //            worldPos = graph.transform.TransformPoint(location.position);
    //        }
    //        GUIStyle style = new GUIStyle();
    //        //style.normal.textColor = Color.white;
    //        Handles.Label(worldPos, new GUIContent(location.ToString()), style);
    //        Vector3 newWorld = Handles.PositionHandle(worldPos, Quaternion.identity);

    //        if(newWorld != worldPos)
    //        {
    //            location.position = graph.transform.InverseTransformPoint(newWorld); //why does it work
    //        }
    //    }

    //    foreach(Border border in graph.edges)
    //    {
    //        Vector3 v1 = graph.transform.TransformPoint(border.v1.position);
    //        Vector3 v2 = graph.transform.TransformPoint(border.v2.position);
    //        float length = (v1 - v2).magnitude;
    //        Handles.DrawLine(v1, v2);
    //    }
    //}

    //static int value = 1;
    //private void AddLocation()
    //{
    //    EditorGUILayout.BeginHorizontal();
    //    bool button = GUILayout.Button("Add region");
    //    EditorGUIUtility.labelWidth = 50;
    //    value = EditorGUILayout.IntField("Label: ", value, GUILayout.ExpandWidth(false));
    //    if (button)
    //    {
    //        graph.Add(Region.Create(value, Vector3.zero));
    //    }
    //    EditorGUILayout.EndHorizontal();
    //}

    //static int first = 1;
    //static int second = 1;
    //private void AddEdge()
    //{
    //    EditorGUILayout.BeginHorizontal();
    //    bool button = GUILayout.Button("Add edge");
    //    first = EditorGUILayout.IntField(first, GUILayout.ExpandWidth(false));
    //    second = EditorGUILayout.IntField(second, GUILayout.ExpandWidth(false));

    //    if (button)
    //    {
    //        //graph.Add(new Border(graph.Find(first), graph.Find(second)));
    //        Border border = Border.Create(graph.Find(first), graph.Find(second));
    //        graph.Add(border);
    //    }
    //    EditorGUILayout.EndHorizontal();
    //}

    //public static bool show;
    //public static bool show2;
    public override void OnInspectorGUI()
    {
        ///ShowArrayProperty(serializedObject.FindProperty("vertices"));
        //base.OnInspectorGUI();        

        //AddLocation();
        //ArrayGUI(nameof(graph.vertices), "Locations", ref show, graph.vertices);
        //AddEdge();
        //ArrayGUI(nameof(graph.edges), "Borders", ref show2, graph.edges);



        if (true)
        {
            foreach (Region region in graph.vertices)
            { 
                EditorGUILayout.BeginHorizontal();

                int size = 64;
                EditorGUILayout.BeginVertical(GUILayout.Width(size));
                EditorGUILayout.LabelField("Region " + region.label);
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical();
                region.position = EditorGUILayout.Vector3Field("position", region.position);
                EditorGUILayout.EndVertical();

                EditorGUILayout.EndHorizontal();
            }
        }


        //    //Edges

        //}

        //private void ArrayGUI(string arrayName, string displayName, ref bool show, object[] objects)
        //{
        //    show = EditorGUILayout.Foldout(show, new GUIContent(displayName));
        //    if (!show) return;
        //    SerializedProperty array = serializedObject.FindProperty(arrayName);

        //    for (int i = 0; i < array.arraySize; i++)
        //    {
        //        string name = objects[i].ToString();
        //        EditorGUILayout.PropertyField(array.GetArrayElementAtIndex(i), new GUIContent(name), true);
        //    }
        //    serializedObject.ApplyModifiedProperties();
        }
    }

