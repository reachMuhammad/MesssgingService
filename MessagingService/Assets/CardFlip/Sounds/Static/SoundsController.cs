using System.Collections;
using UnityEngine;

public partial class SoundsController : MonoBehaviour
{    
    private static SoundsController FindController => GameObject.FindObjectOfType<SoundsController>();
    private static SoundsController _instance;
    private static SoundsController _soundController { get { if(_instance == null) { _instance = FindController; } return _instance; } }

    public static void PlaySound(SoundType sound)
    {
        _soundController.PlaySoundEffect(sound);
    }

    public static void PlayDelayedSound(SoundType sound, float delay)
    {
        _soundController.StartCoroutine(DelayedSound());
        IEnumerator DelayedSound()
        {
            yield return new WaitForSeconds(delay);
            _soundController.PlaySoundEffect(sound);
        }
    }
}
