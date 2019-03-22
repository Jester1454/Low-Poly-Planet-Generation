using UnityEngine;

namespace GeneratePlanets.RandomGeneration
{
    [CreateAssetMenu()]
    public class PlanetSettings : ScriptableObject
    {
        [SerializeField, Range(2, 256)] private int _resolution;
        [SerializeField] private Material _baseMaterial;
        [SerializeField] private GameObject _facePrefab;
        [SerializeField] private GameObject _atmospherePrefab;
        [SerializeField] private float _atmosphereOffset = 0.4f;
        
        public int Resolution
        {
            get { return _resolution; }
        }      
        
        public float AtmosphereOffset
        {
            get { return _atmosphereOffset; }
        }        
        
        public Material Material
        {
            get { return _baseMaterial; }
        }
        
        public GameObject FacePrefab
        {
            get { return _facePrefab; }
        }        
        public GameObject AtmospherePrefab
        {
            get { return _atmospherePrefab; }
        }
    }
}
