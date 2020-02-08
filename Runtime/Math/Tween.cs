using com.cobalt910.core.Runtime.Extension;
using UnityEngine;

namespace com.cobalt910.core.Runtime.Math
{
    /// Exponential interpolation
    public static class ETween
    {
        #region Static Functions

        public static float Step(
            float current, float target, float omega)
        {
            var exp = Mathf.Exp(-omega * Time.deltaTime);
            return Mathf.Lerp(target, current, exp);
        }

        public static float StepAngle(
            float current, float target, float omega)
        {
            var exp = Mathf.Exp(-omega * Time.deltaTime);
            var delta = Mathf.DeltaAngle(current, target);
            return target - delta * exp;
        }

        public static Vector3 Step(
            Vector3 current, Vector3 target, float omega)
        {
            var exp = Mathf.Exp(-omega * Time.deltaTime);
            return Vector3.Lerp(target, current, exp);
        }

        public static Quaternion Step(
            Quaternion current, Quaternion target, float omega)
        {
            if (current == target) return target;
            var exp = Mathf.Exp(-omega * Time.deltaTime);
            return Quaternion.Lerp(target, current, exp);

        }

        #endregion
    }

    /// Interpolation with critically damped spring model
    public struct DTween
    {
        #region Static Functions

        public static float Step(
            float current, float target, ref float velocity, float omega)
        {
            var dt = Time.deltaTime;
            var n1 = velocity - (current - target) * (omega * omega * dt);
            var n2 = 1 + omega * dt;
            velocity = n1 / (n2 * n2);
            return current + velocity * dt;
        }

        public static Vector2 Step(
            Vector2 current, Vector2 target, ref Vector2 velocity, float omega)
        {
            var dt = Time.deltaTime;
            var n1 = velocity - (current - target) * (omega * omega * dt);
            var n2 = 1 + omega * dt;
            velocity = n1 / (n2 * n2);
            return current + velocity * dt;
        }

        public static Vector3 Step(
            Vector3 current, Vector3 target, ref Vector3 velocity, float omega)
        {
            var dt = Time.deltaTime;
            var n1 = velocity - (current - target) * (omega * omega * dt);
            var n2 = 1 + omega * dt;
            velocity = n1 / (n2 * n2);
            return current + velocity * dt;
        }

        public static Vector4 Step(
            Vector4 current, Vector4 target, ref Vector4 velocity, float omega)
        {
            var dt = Time.deltaTime;
            var n1 = velocity - (current - target) * (omega * omega * dt);
            var n2 = 1 + omega * dt;
            velocity = n1 / (n2 * n2);
            return current + velocity * dt;
        }

        public static Quaternion Step(
            Quaternion current, Quaternion target,
            ref Vector4 velocity, float omega)
        {
            var vcurrent = current.ToVector4();
            var vtarget = target.ToVector4();
            // We can use either of vtarget/-vtarget. Use closer one.
            if (Vector4.Dot(vcurrent, vtarget) < 0) vtarget = -vtarget;
            var dt = Time.deltaTime;
            var n1 = velocity - (vcurrent - vtarget) * (omega * omega * dt);
            var n2 = 1 + omega * dt;
            velocity = n1 / (n2 * n2);
            return (vcurrent + velocity * dt).ToNormalizedQuaternion();
        }

        #endregion

        #region Struct Implementation

        public float position;
        public float velocity;
        public float omega;

        public DTween(float position, float omega)
        {
            this.position = position;
            this.velocity = 0;
            this.omega = omega;
        }

        public void Step(float target)
        {
            position = Step(position, target, ref velocity, omega);
        }

        public static implicit operator float(DTween m)
        {
            return m.position;
        }

        #endregion
    }

    /// Interpolation with critically damped spring model
    public struct DTweenVector2
    {
        public Vector2 position;
        public Vector2 velocity;
        public float omega;

        public DTweenVector2(Vector2 position, float omega)
        {
            this.position = position;
            this.velocity = Vector2.zero;
            this.omega = omega;
        }

        public void Step(Vector2 target)
        {
            position = DTween.Step(position, target, ref velocity, omega);
        }

        public static implicit operator Vector2(DTweenVector2 m)
        {
            return m.position;
        }
    }

    /// Interpolation with critically damped spring model
    struct DTweenVector3
    {
        public Vector3 position;
        public Vector3 velocity;
        public float omega;

        public DTweenVector3(Vector3 position, float omega)
        {
            this.position = position;
            this.velocity = Vector3.zero;
            this.omega = omega;
        }

        public void Step(Vector3 target)
        {
            position = DTween.Step(position, target, ref velocity, omega);
        }

        public static implicit operator Vector3(DTweenVector3 m)
        {
            return m.position;
        }
    }

    /// Interpolation with critically damped spring model
    struct DTweenQuaternion
    {
        public Quaternion rotation;
        public Vector4 velocity;
        public float omega;

        public DTweenQuaternion(Quaternion rotation, float omega)
        {
            this.rotation = rotation;
            this.velocity = Vector4.zero;
            this.omega = omega;
        }

        public void Step(Quaternion target)
        {
            rotation = DTween.Step(rotation, target, ref velocity, omega);
        }

        public static implicit operator Quaternion(DTweenQuaternion m)
        {
            return m.rotation;
        }
    }
}
