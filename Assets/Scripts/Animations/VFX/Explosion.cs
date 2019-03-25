using UnityEngine;

namespace Animations.VFX
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _main;
        [SerializeField] private ParticleSystem[] _ring;
        [SerializeField] private SparkParticles _sparks;
        [SerializeField] private ParticleSystem _flash;
        [SerializeField] private ParticleSystem[] _debris;
        [SerializeField] private float _duration;
        
        public float Duration => _duration;

        public void Play()
        {
            if (_main != null)
            {
                _main.Play();
            }
            for (int i = 0; i < _ring.Length; i++)
            {
                if (_ring[i] != null)
                {
                    _ring[i].Play();
                }
            }

            if (_sparks != null)
            {
                _sparks.Play();
            }
            if (_flash != null)
            {
                _flash.Play();
            }
            
            for (int i = 0; i < _debris.Length; i++)
            {
                if (_debris[i] != null)
                {
                    _debris[i].Play();
                }
            }
        }
    }
}

