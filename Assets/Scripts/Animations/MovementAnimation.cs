using System.Collections;
using UnityEngine;

namespace Animations
{
    public class MovementAnimation : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private float _speed;
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
            while (time< _duration)
            {
                time += Time.deltaTime;
                transform.position = MovementTo(transform.position, to, _speed, time);
                yield return null;
            }
        }
        
        private Vector3 MovementTo(Vector3 current,Vector3 target, float speed, float time)
        {
            return Vector3.MoveTowards(current, target * _curve.Evaluate(time), speed  * Time.deltaTime);
        }
    }
}
