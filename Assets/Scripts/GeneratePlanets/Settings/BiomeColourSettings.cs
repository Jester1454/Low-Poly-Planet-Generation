using UnityEngine;

namespace GeneratePlanets.Settings
{
    [System.Serializable]
    public class BiomeColourSettings
    {
        public Biome[] Biomes;
        public NoiseSettings Noise;
        public float NoiseOffset;
        public float NoiseStrength;
        [Range(0,1)]
        public float BlendAmount;

        [System.Serializable]
        public class Biome
        {
            public Gradient Gradient;
            public Color Tint;
            [Range(0, 1)]
            public float StartHeight;
            [Range(0, 1)]
            public float TintPercent;
        }
    }
}