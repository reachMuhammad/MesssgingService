using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SoundsController : MonoBehaviour
{
    private SoundsData _soundsData;
    private Stack<AudioSource> _audioSourcesSfx = new Stack<AudioSource>();

    void Awake()
    {
        _instance = this;
        LoadData();
    }
   
    private void LoadData()
    {
        _soundsData = Resources.Load<SoundsData>("Sounds");
    }

    private void PlaySoundEffect(SoundType sound)
    {
        var data = GetSoundData(sound);
        PlaySoundEffect(data.clip, data.Volume);
    }
    
    private void PlaySoundEffect(AudioClip clip, float volume)
    {
        if (clip == null)
        {
            Debug.LogError("AudioClip is null");
            return;
        }

        AudioSource audioSource = GetAudioSource();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        StartCoroutine(CacheAudioSource(audioSource, audioSource.clip.length));
    }


    IEnumerator CacheAudioSource(AudioSource source, float time)
    {
        var wait = new WaitForSeconds(time);

        yield return wait;

        _audioSourcesSfx.Push(source);
    }

    private AudioSource GetAudioSource()
    {
        if (_audioSourcesSfx.Count == 0)
            _audioSourcesSfx.Push(CreateAudioSource("OnShotAudio"));

        var audioSource = _audioSourcesSfx.Pop();
        audioSource.volume = 0.2f;
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        return audioSource;
    }

    private AudioSource CreateAudioSource(string name)
    {
        GameObject audioSourceGameObject = new GameObject(name);
        audioSourceGameObject.transform.parent = transform;
       return audioSourceGameObject.AddComponent<AudioSource>();
    }

    private SoundEffect GetSoundData(SoundType sound)
    {
        foreach (var soundEffect in _soundsData.m_SoundEffectArray)
        {
            if (soundEffect.type == sound)
            {
                return soundEffect;
            }
        }
        return new SoundEffect();
    }
}
