using System.Collections;
using UnityEngine;

namespace Animations
{
    public class FaceAnimation : MonoBehaviour
    {
        [SerializeField] private float _centerOffset = 3f;
        [SerializeField] private AppearenceAnimation _appearenceAnimation;
        [SerializeField] private MovementAnimation _movementAnimation;
        [SerializeField] private DisolveAnimation _disolveAnimation;
        private Vector3 _offsetPosition;
        
        
        public IEnumerator PlayCreateAnimation(Vector3 planetCenter, Vector3 direction)
        {
            Vector3 currentCoordinate = transform.position;
            
            float distance = Vector3.Distance(planetCenter, direction);
            Vector3 newCoordinate = 1/distance * ((distance - _centerOffset)*planetCenter  + _centerOffset*direction);
            
            _offsetPosition = newCoordinate;
            transform.position = newCoordinate;
            if (_appearenceAnimation != null)
            {
                yield return StartCoroutine(_appearenceAnimation.PlayAnimation());
            }

            if (_movementAnimation != null)

            {
                yield return StartCoroutine(_movementAnimation.PlayAnimation(newCoordinate, currentCoordinate));
            }
        }        
        
        public IEnumerator PlayDestroyAnimation()
        {
            Vector3 newCoordinate = _offsetPosition;

            if (_movementAnimation != null)
            {
                yield return StartCoroutine(_movementAnimation.PlayAnimation(transform.position, newCoordinate));
            }

            if (_disolveAnimation != null)
            {
                yield return StartCoroutine(_disolveAnimation.PlayAnimation());
            }
        }
    }
}
