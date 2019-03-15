using UnityEngine;

namespace GeneratePlanets.RandomGeneration
{
    public class PlanetRandomGeneration : MonoBehaviour
    {
        [SerializeField] private ShapeRandomSettings _randomSettings;
        [SerializeField] private PlanetSettings _planetSettings;
        [SerializeField] private ColorRandomSettings _colorSettings;
        private Planet _currentPlanet;
        
        private void Awake()
        {
            GenerateRandomPlanet();
        }

        public void GenerateRandomPlanet()
        {
            DestroyCurrentPlanet();
            GameObject planetGameObject = new GameObject("planet");
            Planet planet = planetGameObject.AddComponent<Planet>();
            planet.GeneratePlanet(_colorSettings.GetRandomColorSettings(), _randomSettings.GetRandomShapeSettings(), _planetSettings);
            _currentPlanet = planet;
//            return planet;
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
