using GeneratePlanets.NoiseFilters;
using GeneratePlanets.Settings;
using GeneratePlanets.Utillity;
using UnityEngine;

namespace GeneratePlanets
{
    public class ColorGenerator
    {
        private ColorSettings _colorSettings;
        private Texture2D _texture2D;
        private const int TextureResolution = 50;
        private INoiseFilter _biomeNoiseFilter;

        public void UpdateSettings(ColorSettings settings)
        {
            this._colorSettings = settings;
            if (_texture2D == null || settings.BiomeColorSettings.Biomes.Length != _texture2D.height)
            {
                _texture2D = new Texture2D(TextureResolution * 2, settings.BiomeColorSettings.Biomes.Length,
                    TextureFormat.RGBA32, false);
                _texture2D.filterMode = FilterMode.Point;
            }
            _biomeNoiseFilter = NoiseFilterFactory.CreateNoiseFilter(settings.BiomeColorSettings.Noise);
        }

        public void UpdateElevation(MinMax elevation)
        {
           _colorSettings.PlanetMaterial.SetVector("_elevationMinMax", new Vector4(elevation.Min, elevation.Max));
        }

        public float BiomPercentFromPoint(Vector3 pointOnShpere)
        {
            float heightPercent = (pointOnShpere.y + 1) / 2f;
            heightPercent +=
                (_biomeNoiseFilter.Evaluate(pointOnShpere) - _colorSettings.BiomeColorSettings.NoiseOffset) *
                _colorSettings.BiomeColorSettings.NoiseStrength;
            float biomeIndex = 0;
            int numBiomes = _colorSettings.BiomeColorSettings.Biomes.Length;

            float blendRange = _colorSettings.BiomeColorSettings.BlendAmount / 2f + .001f;

            for (int i = 0; i < numBiomes; i++)
            {
                float dst = heightPercent - _colorSettings.BiomeColorSettings.Biomes[i].StartHeight;
                float weight = Mathf.InverseLerp(-blendRange, blendRange, dst);
                biomeIndex *= (1 - weight);
                biomeIndex += i * weight;
            }

            return biomeIndex / Mathf.Max(1, numBiomes - 1);
        }
        
        public void UpdateColours()
        {
            Color[] colours = new Color[_texture2D.width * _texture2D.height];
            int colorIndex = 0;
            foreach (var biome in _colorSettings.BiomeColorSettings.Biomes)
            {
                for (int i = 0; i < TextureResolution * 2; i++)
                {
                    Color gradientCol;
                    if (i < TextureResolution)
                    {
                        gradientCol = _colorSettings.OceanGradient.Evaluate(i / (TextureResolution - 1f));
                    }
                    else
                    {
                        gradientCol = biome.Gradient.Evaluate((i - TextureResolution) / (TextureResolution - 1f));
                    }

                    Color tintColor = biome.Tint;
                    colours[colorIndex] = gradientCol * (1 - biome.TintPercent) + tintColor * biome.TintPercent;
                    colorIndex++;
                }
            }

            _texture2D.SetPixels(colours);
            _texture2D.Apply();
            _colorSettings.PlanetMaterial.SetTexture("_planetTexture", _texture2D);
        }
    }
}
