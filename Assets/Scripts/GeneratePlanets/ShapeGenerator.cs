using GeneratePlanets.NoiseFilters;
using GeneratePlanets.Settings;
using GeneratePlanets.Utillity;
using UnityEngine;

namespace GeneratePlanets
{
    public class ShapeGenerator
    {
        private ShapeSettings _shapeSettings;
        private INoiseFilter[] _simpleNoiseFilters;
        public MinMax ElevationMinMax;
        
        public void UpdateSettings(ShapeSettings settings)
        {
            _shapeSettings = settings;
            _simpleNoiseFilters = new INoiseFilter[_shapeSettings.NoiseSettings.Length];

            for (int i = 0; i < _simpleNoiseFilters.Length; i++)
            {
                _simpleNoiseFilters[i] = NoiseFilterFactory.CreateNoiseFilter(settings.NoiseSettings[i].NoiseSettings);
            }
            ElevationMinMax = new MinMax();
        }

        public float CalculateUnscaledElevation(Vector3 pointOnSphere)
        {
            float elevation = 0;
            float firstLayerValue = 0;
            
            if (_simpleNoiseFilters.Length > 0)
            {
                firstLayerValue = _simpleNoiseFilters[0].Evaluate(pointOnSphere);
                if (_shapeSettings.NoiseSettings[0].Enabled)
                {
                    elevation = firstLayerValue;
                }
            }

            for (int i = 1; i < _simpleNoiseFilters.Length; i++)
            {
                if (_shapeSettings.NoiseSettings[i].Enabled)
                {
                    float mask = (_shapeSettings.NoiseSettings[i].UseFirstLayerAsMask) ? firstLayerValue : 1;
                    elevation += _simpleNoiseFilters[i].Evaluate(pointOnSphere) * mask;
                }
            }

            ElevationMinMax.Addvalue(elevation);
            return elevation;
        }

        public float GetScaledElevation(float unscaledElevation)
        {
            float elevation = Mathf.Max(0, unscaledElevation);
            elevation = _shapeSettings.PlanerRadius * (1 + elevation);
            return elevation;
        }
    }
}
