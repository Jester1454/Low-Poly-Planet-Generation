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
        [SerializeField] private Color _atmoshpereColor;
        
        public ColorSettings(Material planetMaterial, Gradient oceanGradient, BiomeColourSettings biomeColourSettings, Color atmoshpereColor)
        {
            PlanetMaterial = planetMaterial;
            _oceanGradient = oceanGradient;
            _biomeColourSettings = biomeColourSettings;
            _atmoshpereColor = atmoshpereColor;
        }        
        
        public ColorSettings(Gradient oceanGradient, BiomeColourSettings biomeColourSettings, Color atmoshpereColor)
        {
            _oceanGradient = oceanGradient;
            _biomeColourSettings = biomeColourSettings;
            _atmoshpereColor = atmoshpereColor;
        }

        public BiomeColourSettings BiomeColorSettings
        {
            get { return _biomeColourSettings; }
        }

        public Gradient OceanGradient
        {
            get { return _oceanGradient; }
        }
        
        public Color AtmoshpereColor
        {
            get { return _atmoshpereColor; }
        }
    }
}
