using System;
using System.Collections;
using Animations;
using GeneratePlanets.RandomGeneration;
using GeneratePlanets.Settings;
using UnityEngine;

namespace GeneratePlanets
{
    [Serializable]
    public class Planet : MonoBehaviour
    {
        [SerializeField] private PlanetSettings _planetSettings;
        [SerializeField] private ShapeSettings _shapeSettings;
        [SerializeField] private ColorSettings _colorSettings;
        
        private MeshFilter[] _meshFilters;
        private TerrainFace[] _terrainFaces;
        private ShapeGenerator _shapeGenerator = new ShapeGenerator();
        private ColorGenerator _colorGenerator = new ColorGenerator();
        private GameObject _atmosphere;
        
        public ShapeSettings ShapeSettings
        {
            get { return _shapeSettings; }
        }
                
        public PlanetSettings PlanetSettings
        {
            get { return _planetSettings; }
        }
        
        public ColorSettings ColorSettings
        {
            get { return _colorSettings; }
        }
        
        private void Initialize()
        {
            _shapeGenerator.UpdateSettings(_shapeSettings);
            _colorGenerator.UpdateSettings(_colorSettings);
            
            if (_meshFilters == null || _meshFilters.Length == 0)
            {
                _meshFilters = new MeshFilter[6];
            }
            
            _terrainFaces = new TerrainFace[6];
            Vector3[] directions =
                {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};

            for (int i = 0; i < 6; i++)
            {
                if (_meshFilters[i] == null)
                {
                    GameObject meshObj = Instantiate(_planetSettings.FacePrefab, transform);
                    
                    _meshFilters[i] = meshObj.GetComponent<MeshFilter>();
                    _meshFilters[i].sharedMesh = new Mesh();
                }
                _meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = _colorSettings.PlanetMaterial;
                
                _terrainFaces[i] = new TerrainFace(_shapeGenerator, _meshFilters[i].sharedMesh, _planetSettings.Resolution, directions[i]);
            }
        }

        public void GeneratePlanet()
        {
            Initialize();
            GenerateMesh();
            GenerateColors();
        }      
                
        private IEnumerator CreateAtmoshere(ShapeSettings shapeSettings, ColorSettings colorSettings, PlanetSettings planetSettings, Transform planet)
        {
            _atmosphere = Instantiate(planetSettings.AtmospherePrefab, planet);
            Material material = _atmosphere.GetComponent<MeshRenderer>().material;
            material.SetColor("Color_8BBDCD19", colorSettings.AtmoshpereColor);
            _atmosphere.transform.localScale *= (shapeSettings.PlanerRadius * 2 + planetSettings.AtmosphereOffset);

            yield return StartCoroutine(_atmosphere.GetComponent<SimpleGrowingVertexAnimation>().PlayAnimation());
        }
        
        public IEnumerator GeneratePlanet(ColorSettings colorSettings, ShapeSettings shapeSettings, 
            PlanetSettings planetSettings, bool materialFromPlanet = false, bool startCreateAnimation = true)
        {
            _colorSettings = colorSettings;
            if (materialFromPlanet)
            {
                _colorSettings.PlanetMaterial = planetSettings.Material;
            }
            _shapeSettings = shapeSettings;
            _planetSettings = planetSettings;
            
            Initialize();
            GenerateMesh();
            GenerateColors();
            if (startCreateAnimation)
            {
               yield return StartCoroutine(CreateAnimation());
            }

           yield return StartCoroutine(CreateAtmoshere(_shapeSettings, _colorSettings, _planetSettings, transform));
        }

        public IEnumerator CreateAnimation()
        {
            Vector3[] directions =
                {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};
            for (int i = 0; i < _meshFilters.Length-1; i++)
            {
                StartCoroutine(_meshFilters[i].gameObject.GetComponent<FaceAnimation>().PlayCreateAnimation(transform.position, directions[i]));
            }

            yield return StartCoroutine(_meshFilters[_meshFilters.Length - 1].gameObject.GetComponent<FaceAnimation>()
                .PlayCreateAnimation(transform.position, directions[directions.Length -1]));

        }        
        
        public IEnumerator DestroyAnimation()
        {
            for (int i = 0; i < _meshFilters.Length-1; i++)
            {
                StartCoroutine(_meshFilters[i].gameObject.GetComponent<FaceAnimation>().PlayDestroyAnimation());
            }

            yield return StartCoroutine(_meshFilters[_meshFilters.Length - 1].gameObject.GetComponent<FaceAnimation>()
                .PlayDestroyAnimation());
            
            yield return StartCoroutine(_atmosphere.GetComponent<DisolveAnimation>().PlayAnimation());
        }
        
        private void GenerateMesh()
        {
            foreach (TerrainFace face in _terrainFaces)
            {
                face.ConstructMesh();
            }
            
            _colorGenerator.UpdateElevation(_shapeGenerator.ElevationMinMax);
        }

        private void GenerateColors()
        {
            _colorGenerator.UpdateColours();
            foreach (TerrainFace face in _terrainFaces)
            {
                face.UpdateUVs(_colorGenerator);
            }
        }

        private void CombineMesh(MeshFilter[] meshFilters)
        {
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];

            int i = 0;
            while (i < meshFilters.Length)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                Destroy(meshFilters[i].gameObject);
                i++;
            }
            
            MeshFilter unionFilter= gameObject.AddComponent<MeshFilter>();
            unionFilter.sharedMesh = new Mesh();
            unionFilter.sharedMesh.CombineMeshes(combine);
            
            MeshRenderer unionMeshRenderer = gameObject.AddComponent<MeshRenderer>();
            unionMeshRenderer.sharedMaterial = _colorSettings.PlanetMaterial;
        }
    }
}