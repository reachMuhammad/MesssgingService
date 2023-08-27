using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DBInt))]
public class DBTypeIntButtons : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (DBInt)target;

        if (GUILayout.Button("LOAD", GUILayout.Height(40)))
            script.Load();                        

        if (GUILayout.Button("SAVE", GUILayout.Height(40)))
            script.Save();
    }
}

