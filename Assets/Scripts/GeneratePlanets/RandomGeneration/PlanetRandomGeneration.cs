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
                return;
            }
            _isAnimate = true;
           StartCoroutine(AnimatedGenerateRandomPlanet());
        }

        private IEnumerator AnimatedGenerateRandomPlanet()
        {
            yield return StartCoroutine(DestroyCurrentPlanetAnimation());
            _isAnimate = true;

            GameObject planetGameObject = Instantiate(_planetPrefab);
            Planet planet = planetGameObject.AddComponent<Planet>();

            ColorSettings colorSettings = _colorSettings.GetRandomColorSettings();
            ShapeSettings shapeSettings = _randomSettings.GetRandomShapeSettings();
            
            yield return StartCoroutine(planet.GeneratePlanet(colorSettings, shapeSettings, _planetSettings, false, true));
            _currentPlanet = planet;
          
            yield return new WaitForEndOfFrame();
            _isAnimate = false;
        }

        public void SavePlanet()
        {
            if (_currentPlanet != null)
            {
                PlanetSaver saver = new PlanetSaver();
                saver.SavePlanet(_currentPlanet);
            }
        }

        public void LoadPlanet()
        {
            if (_isAnimate)
            {
                return;
            }
            StartCoroutine(AnimatedLoadPlanet());
        }

        private IEnumerator AnimatedLoadPlanet()
        {
            _isAnimate = true;
            PlanetSaver planetSaver = new PlanetSaver();

            yield return StartCoroutine(DestroyCurrentPlanetAnimation());
            _isAnimate = true;
            _currentPlanet = planetSaver.LoadPlanet(_planetSettings, true);
            yield return StartCoroutine(_currentPlanet.CreateAnimation());
            yield return new WaitForEndOfFrame();

            _isAnimate = false;
        }

        public void DestroyCurrentPlanet()
        {
            if (_isAnimate)
            {
                return;
            }
            if (_currentPlanet!=null)
            {
                StartCoroutine(DestroyCurrentPlanetAnimation());
            }
        }

        private IEnumerator DestroyCurrentPlanetAnimation()
        {
            _isAnimate = true;

            if (_currentPlanet != null)
            {
                if (_currentPlanet != null)
                {
                    yield return StartCoroutine(_currentPlanet.DestroyAnimation());
                    Destroy(_currentPlanet.gameObject);
                }
            }
            yield return new WaitForEndOfFrame();
            _isAnimate = false;
        }
    }    
}
