using System;
using UnityEngine;
using UnityEngine.Audio;

namespace ProjectCore.SoundManager
{
    [Serializable]
    public class SoundSettings
    {
        public AudioClip[] ClipsToPlay;
        
        [Space]
        public Vector2 Pitch = new Vector2(0.95f, 1.05f);
        public Vector2 Volume = new Vector2(0.9f, 1f);
        
        [Space]
        public AudioMixerGroup AudioMixerGroup;

        [Space]
        public bool Is3DSound;
        public Vector2 Distance3D = new Vector2(1.5f, 10f);
        public AnimationCurve VolumeChangeByDistance;
    }
}