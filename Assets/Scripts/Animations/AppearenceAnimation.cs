using System.Collections;
using UnityEngine;

namespace Animations
{
    public class AppearenceAnimation : MonoBehaviour
    {
        [SerializeField] private Material _disolveMaterial;
        [SerializeField] private float _duration;
        [SerializeField] private bool _autostart;
        
        private void Awake()
        {
            if (_autostart)
            {
                StartCoroutine(PlayAnimation());
            }
        }
        
        public IEnumerator PlayAnimation()
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            Material oldMaterial = meshRenderer.material;
            meshRenderer.material = _disolveMaterial;

            float currentDisolve = 1;
            
            while (currentDisolve > 0)
            {
                currentDisolve -= Time.deltaTime/_duration;
                
                meshRenderer.material.SetFloat("Vector1_C86947F1", currentDisolve);
                
                yield return null;
            }

            meshRenderer.material = oldMaterial;
        }
    }
}
