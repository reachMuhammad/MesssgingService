using UnityEditor;
using UnityEngine;

public class ClearPrefsEditor
{
    [MenuItem("CardFlip/Clear Prefs")]
    private static void ClearPrefs() => PlayerPrefs.DeleteAll();
}
