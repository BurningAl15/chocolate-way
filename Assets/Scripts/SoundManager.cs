using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    ClickUI,
    Player_Error,
    Player_Move,

}

[Serializable]
public class Sound
{
    public SoundType soundType;
    public AudioSource source;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance;

    [SerializeField] List<Sound> sounds = new List<Sound>();

    Dictionary<SoundType, Sound> soundDictionary = new Dictionary<SoundType, Sound>();

    private void Awake()
    {
        _instance = this;
        for (int i = 0; i < sounds.Count; i++)
        {
            soundDictionary.Add(sounds[i].soundType, sounds[i]);
        }
    }

    public void PlaySound(SoundType soundType)
    {
        Sound temp;
        if (soundDictionary.TryGetValue(soundType, out temp))
        {
            temp.source.Play();
        }
    }

    public void PlaySound_UI()
    {
        PlaySound(SoundType.ClickUI);
    }
}
