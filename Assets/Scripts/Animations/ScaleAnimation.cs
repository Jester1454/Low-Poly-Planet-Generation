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
        [SerializeField] private bool _fromCurrentScale;
        [SerializeField] private float _speed;
        
        public float Duration => _duration;

        private void Awake()
        {
            if (_autostart)
            {
                StartCoroutine(PlayAnimation());
            }
        }
        
        public IEnumerator PlayAnimation(Vector3 from, Vector3 to)
        {
            if (_fromCurrentScale)
            {
                _from = transform.localScale;
            }
            else
            {
                _from = from;
            }
            
            _to = to;
            
            float time = 0;
            transform.localScale = _from;
            
            while (time < _duration)
            {
                time += Time.deltaTime;

                transform.localScale = Vector3.Lerp(transform.localScale, to * _curve.Evaluate(time),
                    _speed * Time.deltaTime);
                yield return null;
            }
        }
        
        public IEnumerator PlayAnimation()
        {
            yield return StartCoroutine(PlayAnimation(_from, _to));
        }
    }
}
