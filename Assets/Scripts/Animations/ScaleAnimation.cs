using System.Collections;
using UnityEngine;

namespace Animations
{
    public class ScaleAnimation : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private bool _autostart;
        [SerializeField] private Vector3 _from;
        [SerializeField] private Vector3 _to;
        [SerializeField] private float _duration;
            
        private void Awake()
        {
            if (_autostart)
            {
                StartCoroutine(PlayAnimation(_from, _to));
            }
        }
        
        public IEnumerator PlayAnimation(Vector3 from, Vector3 to)
        {
            _from = from;
            _to = to;
            
            float time = 0;
            transform.localScale = from;
            
            while (time < _duration)
            {
                time += Time.deltaTime;
                
                float delta = Time.deltaTime / (_duration );
                
                transform.localScale = Vector3.Lerp(transform.localScale, to, delta * _curve.Evaluate(time));
                yield return null;
            }
        }
    }
}
