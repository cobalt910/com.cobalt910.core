using com.cobalt910.core.Runtime.Math;
using NaughtyAttributes;
using UnityEngine;

namespace com.cobalt910.core.Runtime.Misc
{
    [AddComponentMenu("Klak/Motion/Brownian Motion")]
    public class BrownianMotion : MonoBehaviour
    {
        #region Editable Properties

        [BoxGroup("Position Noise"), SerializeField] bool _enablePositionNoise = true;
        [ShowIf("_enablePositionNoise"), Label("Frequency"), BoxGroup("Position Noise"), SerializeField] float _positionFrequency = 0.2f;
        [ShowIf("_enablePositionNoise"), Label("Amplitude"), BoxGroup("Position Noise"), SerializeField] float _positionAmplitude = 0.5f;
        [ShowIf("_enablePositionNoise"), Label("Scale"), BoxGroup("Position Noise"), SerializeField] Vector3 _positionScale = Vector3.one;
        [ShowIf("_enablePositionNoise"), Label("Fractal Level"), BoxGroup("Position Noise"), SerializeField, Range(0, 8)] int _positionFractalLevel = 3;
        
        [BoxGroup("Rotation Noise"), SerializeField] bool _enableRotationNoise = true;
        [ShowIf("_enableRotationNoise"), Label("Frequency"), BoxGroup("Rotation Noise"), SerializeField] float _rotationFrequency = 0.2f;
        [ShowIf("_enableRotationNoise"), Label("Amplitude"), BoxGroup("Rotation Noise"), SerializeField] float _rotationAmplitude = 10.0f;
        [ShowIf("_enableRotationNoise"), Label("Scale"), BoxGroup("Rotation Noise"), SerializeField] Vector3 _rotationScale = new Vector3(1, 1, 0);
        [ShowIf("_enableRotationNoise"), Label("Fractal Level"), BoxGroup("Rotation Noise"), SerializeField, Range(0, 8)] int _rotationFractalLevel = 3;

        #endregion

        #region Public Properties And Methods

        public bool enablePositionNoise {
            get { return _enablePositionNoise; }
            set { _enablePositionNoise = value; }
        }

        public bool enableRotationNoise {
            get { return _enableRotationNoise; }
            set { _enableRotationNoise = value; }
        }

        public float positionFrequency {
            get { return _positionFrequency; }
            set { _positionFrequency = value; }
        }

        public float rotationFrequency {
            get { return _rotationFrequency; }
            set { _rotationFrequency = value; }
        }

        public float positionAmplitude {
            get { return _positionAmplitude; }
            set { _positionAmplitude = value; }
        }

        public float rotationAmplitude {
            get { return _rotationAmplitude; }
            set { _rotationAmplitude = value; }
        }

        public Vector3 positionScale {
            get { return _positionScale; }
            set { _positionScale = value; }
        }

        public Vector3 rotationScale {
            get { return _rotationScale; }
            set { _rotationScale = value; }
        }

        public int positionFractalLevel {
            get { return _positionFractalLevel; }
            set { _positionFractalLevel = value; }
        }

        public int rotationFractalLevel {
            get { return _rotationFractalLevel; }
            set { _rotationFractalLevel = value; }
        }

        public void Rehash()
        {
            for (var i = 0; i < 6; i++)
                _time[i] = Random.Range(-10000.0f, 0.0f);
        }

        #endregion

        #region Private Members

        const float _fbmNorm = 1 / 0.75f;

        Vector3 _initialPosition;
        Quaternion _initialRotation;
        float[] _time;

        #endregion

        #region MonoBehaviour Functions

        void Start()
        {
            _time = new float[6];
            Rehash();
        }

        void OnEnable()
        {
            _initialPosition = transform.localPosition;
            _initialRotation = transform.localRotation;
        }

        void Update()
        {
            var dt = Time.deltaTime;

            if (_enablePositionNoise)
            {
                for (var i = 0; i < 3; i++)
                    _time[i] += _positionFrequency * dt;

                var n = new Vector3(
                    Perlin.Fbm(_time[0], _positionFractalLevel),
                    Perlin.Fbm(_time[1], _positionFractalLevel),
                    Perlin.Fbm(_time[2], _positionFractalLevel));

                n = Vector3.Scale(n, _positionScale);
                n *= _positionAmplitude * _fbmNorm;

                transform.localPosition = _initialPosition + n;
            }

            if (_enableRotationNoise)
            {
                for (var i = 0; i < 3; i++)
                    _time[i + 3] += _rotationFrequency * dt;

                var n = new Vector3(
                    Perlin.Fbm(_time[3], _rotationFractalLevel),
                    Perlin.Fbm(_time[4], _rotationFractalLevel),
                    Perlin.Fbm(_time[5], _rotationFractalLevel));

                n = Vector3.Scale(n, _rotationScale);
                n *= _rotationAmplitude * _fbmNorm;

                transform.localRotation =
                    Quaternion.Euler(n) * _initialRotation;
            }
        }

        #endregion
    }
}
