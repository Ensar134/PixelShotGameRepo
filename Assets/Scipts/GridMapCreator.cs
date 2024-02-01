#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridMapCreator : EditorWindow
{
    Vector2 offset;
    Vector2 drag;
    List<List<Node>> nodes;
    List<List<PartScripts>> Parts;

    GUIStyle empty;
    Vector2 nodePos;
    StyleManager styleman;
    bool isErasing;
    Rect MenuBar;
    private GUIStyle currentStyle;
    GameObject TheMap;

    private int numberRows = 20;
    private int numberCol = 22;
    private int interval = 30;

    [MenuItem("Window/Grid Map Creator")]
    private static void OpenWindow()
    {
        GridMapCreator window = GetWindow<GridMapCreator>();
        window.titleContent = new GUIContent("Grid Map Creator");        
    }

    private void OnEnable()
    {
        SetupStyles();
        SetUpMap();
        SetUpNodesAndParts();
    }

    private void SetUpMap()
    {
        try
        {
            TheMap = GameObject.FindGameObjectWithTag("Map");
            RestoreTheMap(TheMap);
        }
        catch (Exception e) { }
        if (TheMap == null)
        {
            TheMap = new GameObject("Map");
            TheMap.tag = "Map";
        }
    }

    private void RestoreTheMap(GameObject theMap)
    {
        if (TheMap.transform.childCount > 0)
        {
            for (int i = 0; i < theMap.transform.childCount; i++)
            {
                int ii = theMap.transform.GetChild(i).GetComponent<PartScripts>().Row;
                int jj = theMap.transform.GetChild(i).GetComponent<PartScripts>().Column;
                GUIStyle TheStyle = theMap.transform.GetChild(i).GetComponent<PartScripts>().style;
                nodes[ii][jj].SetStyle(TheStyle);
                Parts[ii][jj] = theMap.transform.GetChild(i).GetComponent<PartScripts>();
                Parts[ii][jj].Part = theMap.transform.GetChild(i).gameObject;
                Parts[ii][jj].name = theMap.transform.GetChild(i).name;
                Parts[ii][jj].Row = ii;
                Parts[ii][jj].Column = jj;
            }
        }
    }

    private void SetupStyles()
    {
        try
        {
            styleman = GameObject.FindGameObjectWithTag("StyleManager").GetComponent<StyleManager>();
            for (int i = 0; i < styleman.buttonStyles.Length; i++)
            {
                styleman.buttonStyles[i].NodeStyle = new GUIStyle();
                styleman.buttonStyles[i].NodeStyle.normal.background = styleman.buttonStyles[i].icon;
            }
        }
        catch (Exception){ }
        empty = styleman.buttonStyles[0].NodeStyle;
        currentStyle = styleman.buttonStyles[1].NodeStyle;
    }

    private void SetUpNodesAndParts()
    {
        nodes = new List<List<Node>>();
        Parts = new List<List<PartScripts>>();
        for (int i=0; i< numberRows; i++)
        {
            nodes.Add(new List<Node>());
            Parts.Add(new List<PartScripts>());
            for(int j = 0; j < numberCol; j++)
            {
                nodePos.Set(i * interval, j * interval);
                nodes[i].Add(new Node(nodePos, interval, interval, empty));
                Parts[i].Add(null);
            }
        }
    }

    private void OnGUI()
    {
        DrawGrid();
        DrawNodes();
        DrawMenuBar();

        ProcessNodes(Event.current);
        ProcessGrid(Event.current);

        if (GUI.changed)
        {
            Repaint();
        }
    }

    private void DrawMenuBar()
    {
        MenuBar = new Rect(0, 0, position.width, 20);
        GUILayout.BeginArea(MenuBar, EditorStyles.toolbar);
        GUILayout.BeginHorizontal();

        for(int i = 0; i<styleman.buttonStyles.Length; i++)
        {
            if(GUILayout.Toggle((currentStyle == styleman.buttonStyles[i].NodeStyle), new GUIContent(styleman.buttonStyles[i].ButtonTex),
                EditorStyles.toolbarButton, GUILayout.Width(80)))
            {
                currentStyle = styleman.buttonStyles[i].NodeStyle;
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    private void ProcessNodes(Event e)
    {
        int Row = (int)((e.mousePosition.x - offset.x) / interval);
        int Col = (int)((e.mousePosition.y - offset.y) / interval);
        if ((e.mousePosition.x - offset.x) < 0 || (e.mousePosition.x - offset.x) > interval * numberRows || (e.mousePosition.y - offset.y) < 0 || (e.mousePosition.y - offset.y) > interval * numberCol)
        {

        }
        else
        {
            if (e.type == EventType.MouseDown) 
            {
                if (nodes[Row][Col].style.normal.background.name == "Empty")
                {
                    isErasing = false;
                }
                else
                {
                    isErasing = true;
                }
                PaintNodes(Row, Col);
            }
        
            if (e.type == EventType.MouseDrag)
            {
                PaintNodes(Row, Col);
                e.Use();
            }
        }
    }

    private void PaintNodes(int Row, int Col)
    {
        if (isErasing)
        {
            if (Parts[Row][Col] != null)
            {
                nodes[Row][Col].SetStyle(empty);
                DestroyImmediate(Parts[Row][Col].gameObject);
                GUI.changed = true;
            }
            Parts[Row][Col] = null;
        }
        else
        {
            if(Parts[Row][Col] == null)
            {
                nodes[Row][Col].SetStyle(currentStyle);
                GameObject G = Instantiate(Resources.Load("MapParts/" + currentStyle.normal.background.name)) as GameObject;
                G.name = currentStyle.normal.background.name;
                G.transform.position = new Vector3(Col * 0.53f, 0, Row * 0.53f);
                G.transform.parent = TheMap.transform;
                Parts[Row][Col] = G.GetComponent<PartScripts>();
                Parts[Row][Col].Part = G;
                Parts[Row][Col].name = G.name;
                Parts[Row][Col].Row = Row;
                Parts[Row][Col].Column = Col;
                Parts[Row][Col].style = currentStyle;
                GUI.changed = true;
            }
        }
    }

    private void DrawNodes()
    {
        for (int i = 0; i < numberRows; i++)
        {
            for (int j = 0; j < numberCol; j++)
            {
                nodes[i][j].Draw();
            }
        }
    }

    private void ProcessGrid(Event e)
    {
        drag = Vector2.zero;
        switch (e.type)
        {
            case EventType.MouseDrag:
                if (e.button == 0)
                {
                    OnMouseDrag(e.delta);
                }
                break;
        }
    }

    private void OnMouseDrag(Vector2 delta)
    {
        drag = delta;

        for (int i = 0; i < numberRows; i++)
        {
            for (int j = 0; j < numberCol; j++)
            {
                nodes[i][j].Drag(delta);
            }
        }

        GUI.changed = true;
    }

    private void DrawGrid()
    {
        int widthDivider = Mathf.CeilToInt(position.width / 20);
        int HightDivider = Mathf.CeilToInt(position.height / 20);
        Handles.BeginGUI();
        Handles.color = new Color(0.5f, 0.5f, 0.5f, 0.2f);
        offset += drag;
        Vector3 newoffset = new Vector3(offset.x % 20, offset.y % 20, 0);
        for (int i = 0; i < widthDivider; i++)
        {
            Handles.DrawLine(new Vector3(20 * i, -20, 0) + newoffset, new Vector3(20 * i, position.height, 0) + newoffset);
        }
        for (int i = 0; i < HightDivider; i++)
        {
            Handles.DrawLine(new Vector3 (-20, 20 * i ,0)+newoffset, new Vector3(position.width,20*i,0) + newoffset);
        }

        Handles.color = Color.white;
        Handles.EndGUI();
    }
}
#endif