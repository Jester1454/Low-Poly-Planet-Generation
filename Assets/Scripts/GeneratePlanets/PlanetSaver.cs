using System.IO;
using GeneratePlanets.RandomGeneration;
using GeneratePlanets.Settings;
using UnityEngine;
using Utilities;

namespace GeneratePlanets
{
    public class PlanetSaver
    {
        private string _path;

        public PlanetSaver()
        {
            _path = Application.persistentDataPath;
        }
        
        public Planet LoadPlanet(PlanetSettings planetSettings, bool autoCreatePlanet = false)
        {
            string	colorSavePath = Path.Combine(_path, "ColorPlanet.json");
            string	shapeSavePath = Path.Combine(_path, "ShapePlanet.json");
            
            ColorSettings colorSettings = LoadObjectFromJSON.Load<ColorSettings>(colorSavePath);
            ShapeSettings shapeSettings = LoadObjectFromJSON.Load<ShapeSettings>(shapeSavePath);
           
            if (autoCreatePlanet)
            {
                GameObject planetGameObject = new GameObject("planet");
                Planet planet = planetGameObject.AddComponent<Planet>();
                planet.GeneratePlanet(colorSettings, shapeSettings, planetSettings, true);

                return planet;
            }

            return null;
        }

        public void SavePlanet(Planet _planet)
        {
            string	colorSavePath = Path.Combine(_path, "ColorPlanet.json");
            string	shapeSavePath = Path.Combine(_path, "ShapePlanet.json");
            
            SaveObjectInJSON.Save(shapeSavePath, _planet.ShapeSettings);
            SaveObjectInJSON.Save(colorSavePath, _planet.ColorSettings);
        }
    }
}
