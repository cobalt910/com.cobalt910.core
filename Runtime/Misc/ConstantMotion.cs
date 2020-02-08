using NaughtyAttributes;
using UnityEngine;

namespace com.cobalt910.core.Runtime.Misc
{
    [AddComponentMenu("Klak/Motion/Constant Motion")]
    public class ConstantMotion : MonoBehaviour
    {
        #region Nested Classes

        public enum TranslationMode {
            Off, XAxis, YAxis, ZAxis, Vector, Random
        };

        public enum RotationMode {
            Off, XAxis, YAxis, ZAxis, Vector, Random
        }

        #endregion

        #region Editable Properties
        [BoxGroup, SerializeField] bool _useLocalCoordinate = true;

        private bool _translationOn => _translationMode != TranslationMode.Off;
        private bool _translationIsVector => _translationMode == TranslationMode.Vector && _translationOn;

        [BoxGroup("Translation"), Label("Mode"), SerializeField] TranslationMode _translationMode = TranslationMode.Off;
        [BoxGroup("Translation"), Label("Vector"), ShowIf("_translationIsVector"), SerializeField] Vector3 _translationVector = Vector3.forward;
        [BoxGroup("Translation"), Label("Speed"), ShowIf("_translationOn"), SerializeField] float _translationSpeed = 1.0f;

        private bool _rotationOn => _rotationMode != RotationMode.Off;
        private bool _rotationIsVector => _rotationMode == RotationMode.Vector && _rotationOn;
        
        [BoxGroup("Rotation"), Label("Mode"), SerializeField] RotationMode _rotationMode = RotationMode.Off;
        [BoxGroup("Rotation"), Label("Vector"), ShowIf("_rotationIsVector"), SerializeField] Vector3 _rotationAxis = Vector3.up;
        [BoxGroup("Rotation"), Label("Speed"), ShowIf("_rotationOn"), SerializeField] float _rotationSpeed = 30.0f;

        #endregion

        #region Public Properties

        public TranslationMode translationMode {
            get { return _translationMode; }
            set { _translationMode = value; }
        }

        public Vector3 translationVector {
            get { return _translationVector; }
            set { _translationVector = value; }
        }

        public float translationSpeed {
            get { return _translationSpeed; }
            set { _translationSpeed = value; }
        }

        public RotationMode rotationMode {
            get { return _rotationMode; }
            set { _rotationMode = value; }
        }

        public Vector3 rotationAxis {
            get { return _rotationAxis; }
            set { _rotationAxis = value; }
        }

        public float rotationSpeed {
            get { return _rotationSpeed; }
            set { _rotationSpeed = value; }
        }

        public bool useLocalCoordinate {
            get { return _useLocalCoordinate; }
            set { _useLocalCoordinate = value; }
        }

        #endregion

        #region Private Variables

        Vector3 _randomVectorT;
        Vector3 _randomVectorR;

        Vector3 TranslationVector {
            get {
                switch (_translationMode)
                {
                    case TranslationMode.XAxis:  return Vector3.right;
                    case TranslationMode.YAxis:  return Vector3.up;
                    case TranslationMode.ZAxis:  return Vector3.forward;
                    case TranslationMode.Vector: return _translationVector;
                }
                // TranslationMode.Random
                return _randomVectorT;
            }
        }

        Vector3 RotationVector {
            get {
                switch (_rotationMode)
                {
                    case RotationMode.XAxis:  return Vector3.right;
                    case RotationMode.YAxis:  return Vector3.up;
                    case RotationMode.ZAxis:  return Vector3.forward;
                    case RotationMode.Vector: return _rotationAxis;
                }
                // RotationMode.Random
                return _randomVectorR;
            }
        }

        #endregion

        #region MonoBehaviour Functions

        void Start()
        {
            _randomVectorT = Random.onUnitSphere;
            _randomVectorR = Random.onUnitSphere;
        }

        void Update()
        {
            var dt = Time.deltaTime;

            if (_translationMode != TranslationMode.Off)
            {
                var dp = TranslationVector * _translationSpeed * dt;

                if (_useLocalCoordinate)
                    transform.localPosition += dp;
                else
                    transform.position += dp;
            }

            if (_rotationMode != RotationMode.Off)
            {
                var dr = Quaternion.AngleAxis(
                    _rotationSpeed * dt, RotationVector);

                if (_useLocalCoordinate)
                    transform.localRotation = dr * transform.localRotation;
                else
                    transform.rotation = dr * transform.rotation;
            }
        }

        #endregion
    }
}
