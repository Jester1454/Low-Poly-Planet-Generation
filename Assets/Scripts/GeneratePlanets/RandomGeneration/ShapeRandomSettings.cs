using GeneratePlanets.Settings;
using UnityEngine;

namespace GeneratePlanets.RandomGeneration
{
    [CreateAssetMenu()]
    public class ShapeRandomSettings : ScriptableObject
    {
       public Vector2 PlanerRadius = Vector2.zero;
       public ShapeSettings.NoiseLayer[] MinimalValueNoiseLayers;
       public ShapeSettings.NoiseLayer[] MaximalValueNoiseLayers;
        
        public ShapeSettings GetRandomShapeSettings()
        {
            ShapeSettings.NoiseLayer[] noiseLayers = new ShapeSettings.NoiseLayer[MinimalValueNoiseLayers.Length];
            for (int i = 0; i < MinimalValueNoiseLayers.Length; i++)
            {
                NoiseSettings minNoiseSetings = MinimalValueNoiseLayers[i].NoiseSettings;
                NoiseSettings maxNoiseSetings = MaximalValueNoiseLayers[i].NoiseSettings;
                
                noiseLayers[i] = new ShapeSettings.NoiseLayer
                {
                    Enabled =  MinimalValueNoiseLayers[i].Enabled,
                    NoiseSettings = new NoiseSettings
                    {
                        NoiseFilterType =  minNoiseSetings.NoiseFilterType,
                        RigidNoiseSettings = new RigidNoiseSettings
                        {
                            BaseRoughness = Random.Range(minNoiseSetings.RigidNoiseSettings.BaseRoughness, maxNoiseSetings.RigidNoiseSettings.BaseRoughness),
                            Center = new Vector3
                            {
                                x = Random.Range(minNoiseSetings.RigidNoiseSettings.Center.x, maxNoiseSetings.RigidNoiseSettings.Center.x),
                                y = Random.Range(minNoiseSetings.RigidNoiseSettings.Center.y, maxNoiseSetings.RigidNoiseSettings.Center.y),
                                z = Random.Range(minNoiseSetings.RigidNoiseSettings.Center.z, maxNoiseSetings.RigidNoiseSettings.Center.z)
                            },
                            MinValue = Random.Range(minNoiseSetings.RigidNoiseSettings.MinValue, maxNoiseSetings.RigidNoiseSettings.MinValue),
                            NumLayers = Random.Range(minNoiseSetings.RigidNoiseSettings.NumLayers, maxNoiseSetings.RigidNoiseSettings.NumLayers),
                            Persisence = Random.Range(minNoiseSetings.RigidNoiseSettings.Persisence, maxNoiseSetings.RigidNoiseSettings.Persisence),
                            Roughness = Random.Range(minNoiseSetings.RigidNoiseSettings.Roughness, maxNoiseSetings.RigidNoiseSettings.Roughness),
                            Strength = Random.Range(minNoiseSetings.RigidNoiseSettings.Strength, maxNoiseSetings.RigidNoiseSettings.Strength),
                            WeightMultiplier = Random.Range(minNoiseSetings.RigidNoiseSettings.WeightMultiplier, maxNoiseSetings.RigidNoiseSettings.WeightMultiplier)
                        },
                        SimpleNoiseSettings =  new SimpleNoiseSettings
                        {
                            BaseRoughness = Random.Range(minNoiseSetings.SimpleNoiseSettings.BaseRoughness, maxNoiseSetings.SimpleNoiseSettings.BaseRoughness),
                            Center = new Vector3
                            {
                                x = Random.Range(minNoiseSetings.SimpleNoiseSettings.Center.x, maxNoiseSetings.SimpleNoiseSettings.Center.x),
                                y = Random.Range(minNoiseSetings.SimpleNoiseSettings.Center.y, maxNoiseSetings.SimpleNoiseSettings.Center.y),
                                z = Random.Range(minNoiseSetings.SimpleNoiseSettings.Center.z, maxNoiseSetings.SimpleNoiseSettings.Center.z)
                            },
                            MinValue = Random.Range(minNoiseSetings.SimpleNoiseSettings.MinValue, maxNoiseSetings.SimpleNoiseSettings.MinValue),
                            NumLayers = Random.Range(minNoiseSetings.SimpleNoiseSettings.NumLayers, maxNoiseSetings.SimpleNoiseSettings.NumLayers),
                            Persisence = Random.Range(minNoiseSetings.SimpleNoiseSettings.Persisence, maxNoiseSetings.SimpleNoiseSettings.Persisence),
                            Roughness = Random.Range(minNoiseSetings.SimpleNoiseSettings.Roughness, maxNoiseSetings.SimpleNoiseSettings.Roughness),
                            Strength = Random.Range(minNoiseSetings.SimpleNoiseSettings.Strength, maxNoiseSetings.SimpleNoiseSettings.Strength),
                        }
                    },
                    UseFirstLayerAsMask = MinimalValueNoiseLayers[i].UseFirstLayerAsMask
                };
            }

            ShapeSettings shapeSettings = new ShapeSettings(noiseLayers, Random.Range(PlanerRadius.x, PlanerRadius.y));
            return shapeSettings;
        }
    }
}
