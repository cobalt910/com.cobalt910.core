using System;
using UnityEngine;

namespace com.cobalt910.core.Runtime.Math
{
    public struct FloatInterpolator
    {
        #region Nested Public Classes

        [Serializable]
        public class Config
        {
            public enum InterpolationType {
                Direct, Exponential, DampedSpring
            }

            [SerializeField]
            InterpolationType _interpolationType
                = InterpolationType.DampedSpring;

            public InterpolationType interpolationType {
                get { return _interpolationType; }
                set { _interpolationType = value; }
            }

            public bool enabled {
                get { return interpolationType != InterpolationType.Direct; }
            }

            [SerializeField, Range(0.1f, 100)]
            float _interpolationSpeed = 10;

            public float interpolationSpeed {
                get { return _interpolationSpeed; }
                set { _interpolationSpeed = value; }
            }

            public Config() {}

            public Config(InterpolationType type, float speed)
            {
                _interpolationType = type;
                _interpolationSpeed = speed;
            }

            public static Config Direct {
                get { return new Config(InterpolationType.Direct, 10); }
            }

            public static Config Quick {
                get { return new Config(InterpolationType.DampedSpring, 50); }
            }
        }

        #endregion

        #region Private Members

        float _velocity;

        #endregion

        #region Public Properties And Methods

        public Config config { get; set; }
        public float currentValue { get; set; }
        public float targetValue { get; set; }

        public FloatInterpolator(float initialValue, Config config)
        {
            this.config = config;
            currentValue = targetValue = initialValue;
            _velocity = 0;
        }

        public float Step(float targetValue)
        {
            this.targetValue = targetValue;
            return Step();
        }

        public float Step()
        {
            if (config.interpolationType == Config.InterpolationType.Exponential)
            {
                currentValue = ETween.Step(
                    currentValue, targetValue, config.interpolationSpeed);
            }
            else if (config.interpolationType == Config.InterpolationType.DampedSpring)
            {
                currentValue = DTween.Step(
                    currentValue, targetValue, ref _velocity, config.interpolationSpeed);
            }
            else
            {
                currentValue = targetValue;
            }
            return currentValue;
        }

        #endregion
    }
}
