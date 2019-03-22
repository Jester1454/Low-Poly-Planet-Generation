using System.Collections;
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
        private bool _isAnimate;
        private Planet _currentPlanet;
        
        private void Awake()
        {
            GenerateRandomPlanet();
        }

        public void GenerateRandomPlanet()
        {
            if (_isAnimate)
            {
                return;;
            }
           StartCoroutine(AnimatedGenerateRandomPlanet());
        }

        private IEnumerator AnimatedGenerateRandomPlanet()
        {
            _isAnimate = true;
            yield return StartCoroutine(DestroyCurrentPlanetAnimation());
            
            GameObject planetGameObject = Instantiate(_planetPrefab);
            Planet planet = planetGameObject.AddComponent<Planet>();

            ColorSettings colorSettings = _colorSettings.GetRandomColorSettings();
            ShapeSettings shapeSettings = _randomSettings.GetRandomShapeSettings();
            
            yield return StartCoroutine(planet.GeneratePlanet(colorSettings, shapeSettings, _planetSettings));
            _currentPlanet = planet;
            _isAnimate = false;
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
            if (_isAnimate)
            {
                return;;
            }
            if (_currentPlanet!=null)
            {
                StartCoroutine(DestroyCurrentPlanetAnimation());
            }
        }

        private IEnumerator DestroyCurrentPlanetAnimation()
        {
            if (_currentPlanet != null)
            {
                _isAnimate = true;
                if (_currentPlanet != null)
                {
                    yield return StartCoroutine(_currentPlanet.DestroyAnimation());
                    Destroy(_currentPlanet.gameObject);
                }

                _isAnimate = false;
            }
        }
    }    
}
