using com.cobalt910.core.Runtime.Extension;
using com.cobalt910.core.Runtime.Misc;
using com.cobalt910.core.Runtime.PoolManager;
using UnityEngine;

namespace com.cobalt910.core.Runtime.SoundManager
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : CachedBehaviour, IPoolObject
    {
        [SerializeField] private AudioSource _audioSource;
        private PoolObject _poolObject;

        private bool _isPlaying;
        private const float PlayingAdditionOffset = 0.2f;

        public void Play(SoundSettings settings)
        {
            if (_isPlaying) return;
            
            _audioSource.clip = settings.ClipsToPlay[Random.Range(0, settings.ClipsToPlay.Length - 1)];
            _audioSource.pitch = settings.Pitch.Random();
            _audioSource.volume = settings.Volume.Random();

            _audioSource.outputAudioMixerGroup = settings.AudioMixerGroup;

            _audioSource.spatialBlend = settings.Is3DSound ? 1f : 0f;
            _audioSource.spread = settings.Is3DSound ? -360f : 0f;
            _audioSource.minDistance = settings.Distance3D.x;
            _audioSource.maxDistance = settings.Distance3D.y;
            _audioSource.SetCustomCurve(AudioSourceCurveType.CustomRolloff, settings.VolumeChangeByDistance);
            
            _audioSource.Play();
            
            DisableOnSoundEnded();
            _isPlaying = true;
        }

        public void PlaySmooth(SoundSettings settings, float smoothTime)
        {
            if (_isPlaying) return;

            Play(settings);
            var targetVolume = _audioSource.volume;
            Timer.Timer.Register(smoothTime, delegate { }, x =>
            {
                var roundTime = x / smoothTime;
                roundTime = roundTime.Remap(0, 1, 0, targetVolume);
                _audioSource.volume = roundTime;
            });
        }

        public SoundPlayer AtPosition(Vector3 worldPosition)
        {
            if (_isPlaying) return null;
            Transform.Value.position = worldPosition;
            return this;
        }
        
        public SoundPlayer AtLocalPosition(Vector3 localPosition)
        {
            if (_isPlaying) return null;
            Transform.Value.localPosition = localPosition;
            return this;
        }
        
        public SoundPlayer FollowAt(Transform target, bool resetPosition = true)
        {
            if (_isPlaying) return null;
            
            Transform.Value.parent = target;
            if (resetPosition) AtLocalPosition(Vector3.zero);
            
            return this;
        }

        public SoundPlayer SetLoop()
        {
            if (_isPlaying) return null;
            _audioSource.loop = true;
            return this;
        }

        public void Terminate()
        {
            if (!_isPlaying) return;
            _poolObject.Destroy();
        }

        public void TerminateSmooth(float smoothTime)
        {
            if (!_isPlaying) return;
            var initialVolume = _audioSource.volume;
            Timer.Timer.Register(smoothTime, _poolObject.Destroy, x =>
            {
                var roundTime = 1 - (x / smoothTime);
                roundTime = roundTime.Remap(0, 1, 0, initialVolume);
                _audioSource.volume = roundTime;
            });
        }

        private void DisableOnSoundEnded()
        {
            if (_audioSource.loop) return;
            var playTime = _audioSource.clip.length + PlayingAdditionOffset;
            Timer.Timer.Register(playTime, () => _poolObject.Destroy());
        }

        void IPoolObject.PostAwake(PoolObject poolObject)
        {
            _poolObject = poolObject;
            _poolObject.ObjectLocator.Register(typeof(SoundPlayer), this);

            _audioSource.loop = false;
            _audioSource.playOnAwake = false;
            _audioSource.Stop();
        }

        void IPoolObject.OnReuseObject(PoolObject poolObject) { /* do nothing */ }

        void IPoolObject.OnDisposeObject(PoolObject poolObject)
        {
            if(_audioSource.isPlaying)
                _audioSource.Stop();
            
            _audioSource.loop = false;
            _isPlaying = false;
        }
    }
}