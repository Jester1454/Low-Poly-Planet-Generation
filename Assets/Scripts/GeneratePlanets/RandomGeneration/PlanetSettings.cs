using UnityEngine;

namespace GeneratePlanets.RandomGeneration
{
    [CreateAssetMenu()]
    public class PlanetSettings : ScriptableObject
    {
        [SerializeField, Range(2, 256)] private int _resolution;
        [SerializeField] private Material _baseMaterial;
        
        public int Resolution
        {
            get { return _resolution; }
        }        
        
        public Material Material
        {
            get { return _baseMaterial; }
        }
    }
}
