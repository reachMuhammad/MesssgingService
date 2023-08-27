using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GridViewConfigs", menuName = "ScriptableObjects/Grid/GridViewConfigs")]
public class GridViewConfigs : ScriptableObject
{
    public GridGameModeData[] GridGameModeData;
}

[Serializable]
public struct GridGameModeData
{
    public GameMode GameMode;
    public Vector2 GridSize;
}
