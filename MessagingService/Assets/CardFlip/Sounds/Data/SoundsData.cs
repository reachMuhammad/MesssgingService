using UnityEngine;

[CreateAssetMenu(menuName = "Data/Sounds", fileName = "Sounds")]
public class SoundsData : ScriptableObject
{
    [NonReorderable] public SoundEffect[] m_SoundEffectArray;
}

[System.Serializable]
public struct SoundEffect
{
    public SoundType type;
    public AudioClip clip;
    public float Volume;
}

public enum SoundType
{
    ButtonClick,
    CardSelect,
    CorrectSelection,
    WrongSelection,
    GameOver
}