using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(DBString))]
public class DBTypeStringButtons : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (DBString)target;

        if (GUILayout.Button("LOAD", GUILayout.Height(40)))
            script.Load();

        if (GUILayout.Button("SAVE", GUILayout.Height(40)))
            script.Save();
    }
}

