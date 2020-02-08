using UnityEngine;

namespace com.cobalt910.core.Runtime.Extension
{
    /// Extension methods (setters) for Material
    public static class MaterialSetterExtension
    {
        public static Material Property(this Material m, string name, float x)
        {
            m.SetFloat(name, x);
            return m;
        }

        public static Material Property(this Material m, string name, float x, float y)
        {
            m.SetVector(name, new Vector2(x, y));
            return m;
        }

        public static Material Property(this Material m, string name, float x, float y, float z)
        {
            m.SetVector(name, new Vector3(x, y, z));
            return m;
        }

        public static Material Property(this Material m, string name, float x, float y, float z, float w)
        {
            m.SetVector(name, new Vector4(x, y, z, w));
            return m;
        }

        public static Material Property(this Material m, string name, Vector2 v)
        {
            m.SetVector(name, v);
            return m;
        }

        public static Material Property(this Material m, string name, Vector3 v)
        {
            m.SetVector(name, v);
            return m;
        }

        public static Material Property(this Material m, string name, Vector4 v)
        {
            m.SetVector(name, v);
            return m;
        }

        public static Material Property(this Material m, string name, Color color)
        {
            m.SetColor(name, color);
            return m;
        }

        public static Material Property(this Material m, string name, Texture texture)
        {
            m.SetTexture(name, texture);
            return m;
        }
    }

    /// Extension methods (setters) for MaterialProperty
    public static class MaterialPropertySetterExtension
    {
        public static MaterialPropertyBlock Property(this MaterialPropertyBlock m, string name, float x)
        {
            m.SetFloat(name, x);
            return m;
        }

        public static MaterialPropertyBlock Property(this MaterialPropertyBlock m, string name, float x, float y)
        {
            m.SetVector(name, new Vector2(x, y));
            return m;
        }

        public static MaterialPropertyBlock Property(this MaterialPropertyBlock m, string name, float x, float y, float z)
        {
            m.SetVector(name, new Vector3(x, y, z));
            return m;
        }

        public static MaterialPropertyBlock Property(this MaterialPropertyBlock m, string name, float x, float y, float z, float w)
        {
            m.SetVector(name, new Vector4(x, y, z, w));
            return m;
        }

        public static MaterialPropertyBlock Property(this MaterialPropertyBlock m, string name, Vector2 v)
        {
            m.SetVector(name, v);
            return m;
        }

        public static MaterialPropertyBlock Property(this MaterialPropertyBlock m, string name, Vector3 v)
        {
            m.SetVector(name, v);
            return m;
        }

        public static MaterialPropertyBlock Property(this MaterialPropertyBlock m, string name, Vector4 v)
        {
            m.SetVector(name, v);
            return m;
        }

        public static MaterialPropertyBlock Property(this MaterialPropertyBlock m, string name, Color color)
        {
            m.SetColor(name, color);
            return m;
        }

        public static MaterialPropertyBlock Property(this MaterialPropertyBlock m, string name, Texture texture)
        {
            m.SetTexture(name, texture);
            return m;
        }
    }
}
