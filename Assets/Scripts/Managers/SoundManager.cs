using System;
using System.Collections.Generic;
using UnityEngine;

namespace AldhaDev.Managers
{
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

    public class SoundManager : MonoSingleton<SoundManager>
    {
        [SerializeField] List<Sound> sounds = new List<Sound>();

        private readonly Dictionary<SoundType, Sound> _soundDictionary = new Dictionary<SoundType, Sound>();

        protected override void Awake()
        {
            base.Awake();
            foreach (var t in sounds)
            {
                _soundDictionary.Add(t.soundType, t);
            }
        }

        public void PlaySound(SoundType soundType)
        {
            if (_soundDictionary.TryGetValue(soundType, out Sound temp))
                temp.source.Play();
        }

        public void PlaySound_UI()
        {
            PlaySound(SoundType.ClickUI);
        }
    }
}