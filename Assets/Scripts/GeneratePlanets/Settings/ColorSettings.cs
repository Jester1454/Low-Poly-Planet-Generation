using System;
using UnityEngine;

namespace GeneratePlanets.Settings
{
    [Serializable]
    public class ColorSettings 
    {
        [NonSerialized] public Material PlanetMaterial;
        [SerializeField] private Gradient _oceanGradient;
        [SerializeField] private BiomeColourSettings _biomeColourSettings;

        public ColorSettings(Material planetMaterial, Gradient oceanGradient, BiomeColourSettings biomeColourSettings)
        {
            PlanetMaterial = planetMaterial;
            _oceanGradient = oceanGradient;
            _biomeColourSettings = biomeColourSettings;
        }        
        
        public ColorSettings(Gradient oceanGradient, BiomeColourSettings biomeColourSettings)
        {
            _oceanGradient = oceanGradient;
            _biomeColourSettings = biomeColourSettings;
        }

        public BiomeColourSettings BiomeColorSettings
        {
            get { return _biomeColourSettings; }
        }

        public Gradient OceanGradient
        {
            get { return _oceanGradient; }
        }
    }
}
