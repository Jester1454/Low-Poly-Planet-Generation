using System.Collections;
using UnityEngine;

namespace Animations
{
    public class SubMeshesAnimation : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private float _speed;
        
        public IEnumerator PlayAnimation()
        {
            Mesh mesh = GetComponent<MeshFilter>().mesh;
            
            Vector3[] finishVertices = mesh.vertices;
            Vector3[] currentVerces = new Vector3[mesh.vertices.Length];
            bool[] vertextIsFinish = new bool[mesh.vertices.Length];
            
            for (int i = 0; i < currentVerces.Length; i++)
            {
                currentVerces[i] = transform.position;
                vertextIsFinish[i] = false;
            }

            float time = 0;
            while (true)
            {
                time += Time.deltaTime;
                for (int i = 0; i < currentVerces.Length; i++)
                {
                    if (currentVerces[i] != finishVertices[i])
                    {
                        currentVerces[i] = MovementTo(currentVerces[i], finishVertices[i], _speed, time);
                    }
                    else
                    {
                        vertextIsFinish[i] = true;
                    }
                }

                mesh.vertices = currentVerces;
                mesh.RecalculateNormals();
                
                mesh.subMeshCount = mesh.vertices.Length/2;

                int indexVertices1 = 0;
                int indexVertices2 = 1;
                int indexVertices3 = 2;
                for (int i = 0; i < mesh.vertices.Length; i++)
                {
                    int[] newId = new[] {indexVertices1, indexVertices2, indexVertices3};
                    mesh.SetTriangles(newId, i);
                }
                
                bool isEnd = false;
                
                for (int i = 0; i < vertextIsFinish.Length; i++)
                {
                    if (vertextIsFinish[i] == false)
                    {
                        isEnd = true;
                    }
                }

                if (isEnd)
                {
                    yield return null;
                }
                else
                {
                    yield break;
                }
            }
        }
        
        private Vector3 MovementTo(Vector3 current,Vector3 target, float speed, float time)
        {
            return Vector3.MoveTowards(current, target * _curve.Evaluate(time), Random.Range(speed/2, speed)  * Time.deltaTime);
        }

    }
}
