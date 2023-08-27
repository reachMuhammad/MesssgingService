using UnityEditor;
using UnityEngine;

public class ClearPrefsEditor
{
    [MenuItem("BattleField/Clear Prefs")]
    private static void ClearPrefs() => PlayerPrefs.DeleteAll();
}
