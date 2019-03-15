using UnityEngine;

namespace GeneratePlanets
{
    public class TerrainFace
    {
        private Mesh _mesh;
        private ShapeGenerator _shapeGenerator;
        private int _resolution;
        private Vector3 _localUp;
        private Vector3 _axisA;
        private Vector3 _axisB;

        public TerrainFace(ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localUp)
        {
            _shapeGenerator = shapeGenerator;
            _mesh = mesh;
            _resolution = resolution;
            _localUp = localUp;

            _axisA = new Vector3(localUp.y, localUp.z, localUp.x);
            _axisB = Vector3.Cross(localUp, _axisA);
        }

        public void ConstructMesh()
        {
            Vector3[] verticles = new Vector3[_resolution * _resolution];
            int[] triangles = new int[(_resolution-1) * (_resolution-1) * 6];
            int triangleIndex = 0;
            Vector2[] uv = (_mesh.uv.Length == verticles.Length)?_mesh.uv:new Vector2[verticles.Length];;

            for (int y = 0; y < _resolution; y++)
            {
                for (int x = 0; x < _resolution; x++)
                {
                    int i = x + y * _resolution;
                    Vector2 percent = new Vector2(x, y)/(_resolution-1);
                    Vector3 pointOnUnitCube =
                        _localUp + (percent.x - .5f) * 2 * _axisA + (percent.y - .5f) * 2 * _axisB;
                    Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                    float unscaledElevation = _shapeGenerator.CalculateUnscaledElevation(pointOnUnitSphere);
                    verticles[i] = pointOnUnitSphere * _shapeGenerator.GetScaledElevation(unscaledElevation);
                    uv[i].y = unscaledElevation;
                    
                    if (x != _resolution - 1 && y != _resolution - 1)
                    {
                        triangles[triangleIndex] = i;
                        triangles[triangleIndex+1] = i + _resolution +1;
                        triangles[triangleIndex+2] = i + _resolution;                        
                        
                        triangles[triangleIndex+3] = i;
                        triangles[triangleIndex+4] = i + 1;
                        triangles[triangleIndex+5] = i + _resolution + 1;

                        triangleIndex += 6;
                    }
                }
            }
            _mesh.Clear();
            _mesh.vertices = verticles;
            _mesh.triangles = triangles;
            _mesh.RecalculateNormals();
            _mesh.uv = uv;
        }
            
        public void UpdateUVs(ColorGenerator colourGenerator)
        {
            Vector2[] uv = _mesh.uv;

            for (int y = 0; y < _resolution; y++)
            {
                for (int x = 0; x < _resolution; x++)
                {
                    int i = x + y * _resolution;
                    Vector2 percent = new Vector2(x, y) / (_resolution - 1);
                    Vector3 pointOnUnitCube = _localUp + (percent.x - .5f) * 2 * _axisA + (percent.y - .5f) * 2 * _axisB;
                    Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;

                    uv[i].x = colourGenerator.BiomPercentFromPoint(pointOnUnitSphere);
                }
            }
            _mesh.uv = uv;
        }
    }

}