using System.Collections.Generic;
using UnityEngine;

namespace A3C.Combat
{
    public static class RuntimeVisuals
    {
        private static readonly Dictionary<Color, Material> materials = new Dictionary<Color, Material>();
        public static Material Material(Color color)
        {
            if (materials.TryGetValue(color, out var material) && material != null) return material;
            var shader = Shader.Find("Universal Render Pipeline/Unlit");
            if (shader == null) shader = Shader.Find("Unlit/Color");
            material = new Material(shader) { color = color };
            materials[color] = material;
            return material;
        }
        public static GameObject Primitive(string name, PrimitiveType type, Vector3 position, Vector3 scale, Color color, bool solid = true)
        {
            var go = GameObject.CreatePrimitive(type);
            go.name = name;
            go.transform.position = position;
            go.transform.localScale = scale;
            go.GetComponent<Renderer>().sharedMaterial = Material(color);
            if (!solid)
            {
                var collider = go.GetComponent<Collider>();
                collider.enabled = false;
                Object.Destroy(collider);
            }
            return go;
        }
    }
}
