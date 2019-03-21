using Animations;
using GeneratePlanets.Settings;
using UnityEngine;

namespace GeneratePlanets.RandomGeneration
{
    public class PlanetRandomGeneration : MonoBehaviour
    {
        [SerializeField] private ShapeRandomSettings _randomSettings;
        [SerializeField] private PlanetSettings _planetSettings;
        [SerializeField] private ColorRandomSettings _colorSettings;
        [SerializeField] private GameObject _planetPrefab;
        [SerializeField] private GameObject _atmospherePrefab;
        
        private Planet _currentPlanet;
        
        private void Awake()
        {
            GenerateRandomPlanet();
        }

        public void GenerateRandomPlanet()
        {
            DestroyCurrentPlanet();
            GameObject planetGameObject = Instantiate(_planetPrefab);
            Planet planet = planetGameObject.AddComponent<Planet>();

            ColorSettings colorSettings = _colorSettings.GetRandomColorSettings();
            ShapeSettings shapeSettings = _randomSettings.GetRandomShapeSettings();
            
            planet.GeneratePlanet(colorSettings, shapeSettings, _planetSettings);
            _currentPlanet = planet;
            CreateAtmoshere(shapeSettings, colorSettings, planet.transform);
            
//            return planet;
        }

        private void CreateAtmoshere(ShapeSettings shapeSettings, ColorSettings colorSettings, Transform planet)
        {
            GameObject atmosphere = Instantiate(_atmospherePrefab, planet);
            Material material = atmosphere.GetComponent<MeshRenderer>().material;
            material.SetVector("AtmoshereColor", colorSettings.AtmoshpereColor);
            atmosphere.transform.localScale *= (shapeSettings.PlanerRadius * 2 + 0.3f);
        }
        
        public void SavePlanet()
        {
            PlanetSaver saver = new PlanetSaver();
            saver.SavePlanet(_currentPlanet);
        }

        
        public void LoadPlanet()
        {
            PlanetSaver planetSaver = new PlanetSaver();
            _currentPlanet = planetSaver.LoadPlanet(_planetSettings, true);
        }

        public void DestroyCurrentPlanet()
        {
            if (_currentPlanet!=null)
            {
                Destroy(_currentPlanet.gameObject);    
            }
        }
    }    
}
