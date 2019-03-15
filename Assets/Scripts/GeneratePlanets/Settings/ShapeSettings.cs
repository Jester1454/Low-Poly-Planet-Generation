using System;
using UnityEngine;

namespace GeneratePlanets.Settings
{
    [Serializable]
    public class ShapeSettings
    {
        [SerializeField] private float _planerRadius = 1;
        [SerializeField] private NoiseLayer[] _noiseSettings;

        public ShapeSettings(NoiseLayer[] noiseSettings)
        {
            _noiseSettings = noiseSettings;
        }
        public ShapeSettings(NoiseLayer[] noiseSettings, float planerRadius)
        {
            _noiseSettings = noiseSettings;
            _planerRadius = planerRadius;
        }

        public float PlanerRadius
        {
            get { return _planerRadius; }
        }

        public NoiseLayer[] NoiseSettings
        {
            get { return _noiseSettings; }
        }
        
        [Serializable]
        public class NoiseLayer
        {
            public bool Enabled = true;
            public bool UseFirstLayerAsMask;
            public NoiseSettings NoiseSettings;
        }
    }
}
