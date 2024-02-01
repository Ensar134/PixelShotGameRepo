#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Level))]
public class LevelEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (Level)target;

        if (GUILayout.Button("Save Level", GUILayout.Height(40)))
        {
            script.SaveLevel();
        }

        if (GUILayout.Button("Load Level", GUILayout.Height(40)))
        {
            script.LoadLevel();
        }
    }
}
#endif