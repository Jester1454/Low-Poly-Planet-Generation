using GeneratePlanets.Settings;
using UnityEngine;

namespace GeneratePlanets.NoiseFilters
{
    public class RigidNoiseFilter : INoiseFilter
    {
        private RigidNoiseSettings _noiseSettings;
        private Noise _noise = new Noise();

        public RigidNoiseFilter(RigidNoiseSettings noiseSettings)
        {
            _noiseSettings = noiseSettings;
        }
        
        public float Evaluate(Vector3 point)
        {
            float noiseValue = 0;

            float frequency = _noiseSettings.BaseRoughness;
            float amplitude = 1;
            float weight = 1;
            
            for (int i = 0; i < _noiseSettings.NumLayers; i++)
            {
                float value = 1 - Mathf.Abs(_noise.Evaluate(point * frequency + _noiseSettings.Center));
                value *= value;
                value *= weight;
                weight = Mathf.Clamp01(value * _noiseSettings.WeightMultiplier);
                
                noiseValue += value * amplitude;
                frequency *= _noiseSettings.Roughness;
                amplitude *= _noiseSettings.Persisence;
            }

            noiseValue = Mathf.Max(0, noiseValue - _noiseSettings.MinValue);
            return noiseValue * _noiseSettings.Strength;
        }
    }
}
