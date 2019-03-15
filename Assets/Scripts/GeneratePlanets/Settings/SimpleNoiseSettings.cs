using System;
using UnityEngine;

namespace GeneratePlanets.Settings
{
    [Serializable]
    public class SimpleNoiseSettings
    {
        public float Strength = 1;
        [Range(1, 8)] public int NumLayers = 1;
        public float BaseRoughness = 1;
        public float Persisence = .5f;
        public float Roughness = 2;
        public Vector3 Center;
        public float MinValue;
    }
}