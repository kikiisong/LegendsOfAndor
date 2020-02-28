using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Graph;
using System;

[CustomEditor(typeof(GameGraph))]
public class GraphEditor : Editor
{
    GameGraph graph;

    void OnEnable()
    {
        graph = target as GameGraph;
    }

    private void OnSceneGUI()
    {
        foreach (Region region in graph.vertices)
        {
            Vector3 worldPos;
            if (Application.isPlaying)
            {
                worldPos = region.position;
            }
            else
            {
                worldPos = graph.transform.TransformPoint(region.position);
            }
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.blue;
            Handles.Label(worldPos, new GUIContent(region.ToString()), style);
            Vector3 newWorld = Handles.PositionHandle(worldPos, Quaternion.identity);

            if (newWorld != worldPos)
            {
                Undo.RecordObject(region, region.ToString());
                region.position = graph.transform.InverseTransformPoint(newWorld); //why does it work
            }
        }

        foreach (Border border in graph.edges)
        {
            Vector3 v1 = graph.transform.TransformPoint(border.from.position);
            Vector3 v2 = graph.transform.TransformPoint(border.to.position);
            float length = (v1 - v2).magnitude;
            Handles.color = border.isDirected? Color.magenta: Color.green;
            if (border.isDirected)
            {
                GUIStyle style = new GUIStyle();
                style.normal.textColor = Color.red;
                Handles.Label(v1 - (v1 - v2) / 2, new GUIContent(border.ToString()), style);
            }
            Handles.DrawLine(v1, v2);
        }
    }

    public static bool show;
    public static bool show2;
	public override void OnInspectorGUI()
	{
        base.OnInspectorGUI();
		EditorGUILayout.BeginHorizontal();
		AddRegion();
		AddBorder();
		EditorGUILayout.EndHorizontal();
        MoveAll();

        show = EditorGUILayout.Foldout(show, "Regions");
		if (show)
		{
			foreach (Region region in graph.vertices)
			{
				EditorGUILayout.BeginHorizontal();

				EditorGUILayout.BeginVertical(GUILayout.Width(30));
				EditorGUILayout.LabelField(region.ToString());
				EditorGUILayout.EndVertical();

				EditorGUILayout.BeginVertical();
				region.position = EditorGUILayout.Vector3Field("", region.position);
				EditorGUILayout.EndVertical();

				EditorGUILayout.BeginVertical(GUILayout.Width(10));
				EditorGUILayout.BeginHorizontal();
				region.label = EditorGUILayout.IntField(region.label, GUILayout.Width(20));
				if (GUILayout.Button("R"))
				{
					graph.Remove(region);
				}
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.EndVertical();

				EditorGUILayout.EndHorizontal();
			}
		}

		show2 = EditorGUILayout.Foldout(show2, "Borders");
		if (show2)
		{
			foreach (Border border in graph.edges)
			{
				EditorGUILayout.BeginHorizontal();

				EditorGUILayout.BeginVertical(GUILayout.Width(30));
				EditorGUILayout.LabelField(border.ToString());
				EditorGUILayout.EndVertical();

				EditorGUILayout.BeginVertical();
				EditorGUILayout.BeginHorizontal();
				int from = border.from.label;
				int to = border.to.label;
				from = EditorGUILayout.IntField(from, GUILayout.Width(20));
				to = EditorGUILayout.IntField(to, GUILayout.Width(20));
				if (from != border.from.label)
				{

				}
				if (to != border.to.label)
				{

				}
				border.isDirected = EditorGUILayout.Toggle(border.isDirected, GUILayout.Width(20));
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.EndVertical();

				EditorGUILayout.BeginVertical();
				if (GUILayout.Button("R", GUILayout.Width(20)))
				{
					graph.Remove(border);
				}
				EditorGUILayout.EndVertical();

				EditorGUILayout.EndHorizontal();
			}
		}
	}

    static float moveX;
    static float moveY;
    private void MoveAll()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        var clicked = GUILayout.Button("Move all");
        moveX = EditorGUILayout.FloatField(moveX, GUILayout.ExpandWidth(false));
        moveY = EditorGUILayout.FloatField(moveY, GUILayout.ExpandWidth(false));
        if (clicked)
        {
            foreach(Region region in graph.vertices)
            {
                region.position += new Vector3(moveX, moveY, 0);
            }
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    static int value = 1;
    private void AddRegion()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        bool button = GUILayout.Button("Add region");
        EditorGUIUtility.labelWidth = 20;
        value = EditorGUILayout.IntField("", value, GUILayout.ExpandWidth(false));
        if (button)
        {
            Undo.RecordObject(graph, "Graph");
            graph.Add(Region.Create(value, Vector3.zero));
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    static int first = 1;
    static int second = 1;
    private void AddBorder()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        bool button = GUILayout.Button("Add border");
        first = EditorGUILayout.IntField(first, GUILayout.ExpandWidth(false));
        second = EditorGUILayout.IntField(second, GUILayout.ExpandWidth(false));

        if (button)
        {
            Undo.RecordObject(graph, "Graph");
            graph.Add(new Border(graph.Find(first), graph.Find(second)));
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }
}

