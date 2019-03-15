using System.Collections.Generic;
using GeneratePlanets.NoiseFilters;
using GeneratePlanets.Settings;
using UnityEngine;

namespace GeneratePlanets.RandomGeneration
{
    [CreateAssetMenu()]
    public class ColorRandomSettings : ScriptableObject
    {
        [SerializeField] private Material _material;
        [SerializeField] private Vector2Int _biomCount;
        [SerializeField] private List<Gradient> _oceanGradients;
        [SerializeField] private List<Gradient> _bionGradients;
        [SerializeField] private List<Color> _tintColors;
        [SerializeField] private SimpleNoiseSettings _minNoiseFilter;
        [SerializeField] private SimpleNoiseSettings _maxNoiseFilter;
        
        public ColorSettings GetRandomColorSettings()
        {
            int biomCount = Random.Range(_biomCount.x, _biomCount.y);
            BiomeColourSettings.Biome[] biomes = new BiomeColourSettings.Biome[biomCount];

            float starHeight = 0;
            
            for (int i = 0; i < biomCount; i++)
            {
                starHeight = Random.Range(starHeight, Random.Range(0f, 1f));

                biomes[i] = new BiomeColourSettings.Biome
                {
                    Gradient = _bionGradients[Random.Range(0, _bionGradients.Count - 1)],
                    StartHeight = starHeight,
                    Tint = _tintColors[Random.Range(0, _tintColors.Count - 1)],
                    TintPercent = Random.Range(0, 0.3f)
                };
            }

            NoiseSettings noiseSettings = new NoiseSettings
            {
                NoiseFilterType = NoiseFilterType.Simple,
                RigidNoiseSettings = null,
                SimpleNoiseSettings = new SimpleNoiseSettings
                {
                    BaseRoughness = Random.Range(_minNoiseFilter.BaseRoughness, _maxNoiseFilter.BaseRoughness),
                    Center = new Vector3
                    {
                        x = Random.Range(_minNoiseFilter.Center.x, _maxNoiseFilter.Center.x),
                        y = Random.Range(_minNoiseFilter.Center.y, _maxNoiseFilter.Center.y),
                        z = Random.Range(_minNoiseFilter.Center.z, _maxNoiseFilter.Center.z)
                    },
                    MinValue = Random.Range(_minNoiseFilter.MinValue, _maxNoiseFilter.MinValue),
                    NumLayers = Random.Range(_minNoiseFilter.NumLayers, _maxNoiseFilter.NumLayers),
                    Persisence = Random.Range(_minNoiseFilter.Persisence, _maxNoiseFilter.Persisence),
                    Roughness = Random.Range(_minNoiseFilter.Roughness, _maxNoiseFilter.Roughness),
                    Strength = Random.Range(_minNoiseFilter.Strength, _maxNoiseFilter.Strength)
                }
            };
            
            BiomeColourSettings biomeColourSettings = new BiomeColourSettings
            {
               Biomes = biomes,
               BlendAmount = Random.Range(0f,1f),
               Noise = noiseSettings,
               NoiseOffset = Random.Range(0f,1f),
               NoiseStrength = Random.Range(0f,1f),
            };
            Material planetMaterila = new Material(_material);
            
            return new ColorSettings(planetMaterila, _oceanGradients[Random.Range(0, _oceanGradients.Count-1)], biomeColourSettings);
        }
    }
}