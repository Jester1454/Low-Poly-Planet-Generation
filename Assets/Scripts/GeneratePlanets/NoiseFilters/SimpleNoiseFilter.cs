using GeneratePlanets.Settings;
using UnityEngine;

namespace GeneratePlanets.NoiseFilters
{
    public class SimpleNoiseFilter : INoiseFilter
    {
        private SimpleNoiseSettings _noiseSettings;
        private Noise _noise = new Noise();

        public SimpleNoiseFilter(SimpleNoiseSettings noiseSettings)
        {
            _noiseSettings = noiseSettings;
        }
        
        public float Evaluate(Vector3 point)
        {
            float noiseValue = 0;

            float frequency = _noiseSettings.BaseRoughness;
            float amplitude = 1;

            for (int i = 0; i < _noiseSettings.NumLayers; i++)
            {
                float value = _noise.Evaluate(point * frequency + _noiseSettings.Center);
                noiseValue += (value + 1) * .5f * amplitude;
                frequency *= _noiseSettings.Roughness;
                amplitude *= _noiseSettings.Persisence;
            }

            noiseValue = noiseValue - _noiseSettings.MinValue;
            return noiseValue * _noiseSettings.Strength;
        }
    }
}
