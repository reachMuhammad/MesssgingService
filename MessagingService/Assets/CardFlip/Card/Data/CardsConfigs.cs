using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CardsConfigs", menuName = "ScriptableObjects/Card/CardsConfigs")]
public class CardsConfigs : ScriptableObject
{
    public float CardPreferedSize;
    public GameObject CardObject;
    public CardData[] CardsData; 
}

[Serializable]
public struct CardData
{
    public int CardId;
    public Sprite CardSprite;
}
