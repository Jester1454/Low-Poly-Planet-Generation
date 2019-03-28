using System.Collections;
using Animations.VFX;
using UnityEngine;

namespace Animations
{
    public class AtmosphereAnimation : MonoBehaviour
    {
        [SerializeField] private SimpleGrowingVertexAnimation _growingVertexAnimation;
        [SerializeField] private ScaleAnimation _scaleAnimation;
        [SerializeField] private Explosion[] _explosion;
        [SerializeField] private Explosion _explosionCreate;
        
        public IEnumerator PlayCreateAnimation()
        {
            Explosion explosion = null;
            if (_explosionCreate != null)
            {
                explosion = Instantiate(_explosionCreate);
                explosion.Play();
            }
            
            if (_growingVertexAnimation != null)
            {
                yield return _growingVertexAnimation.PlayAnimation();
            }

            if (explosion != null)
            {
                Destroy(explosion.gameObject);
            }
        }        
        
        public IEnumerator PlayDestroyAnimation()
        {
            Explosion explosion = Instantiate(_explosion[Random.Range(0, _explosion.Length)]);
            explosion.Play();

            if (_scaleAnimation != null)
            {
                yield return _scaleAnimation.PlayAnimation();
                yield return new WaitForSeconds(explosion.Duration - _scaleAnimation.Duration);
            }
            else
            {
                yield return new WaitForSeconds(explosion.Duration);
            }
            
            Destroy(explosion.gameObject);
        }
    }
}
