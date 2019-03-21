using System.Collections;
using UnityEngine;

namespace MovementScripts
{
    public class RotateAround : MonoBehaviour
    {
        [SerializeField] private bool _autoStart;
        [SerializeField] private float _speed;
        [SerializeField] private Vector3 _rotationAxis;
        [SerializeField] private Vector3 _pointRotation;
        
        private void Awake()
        {
            if (_autoStart)
            {
                StartCoroutine(Rotatate());
            }
        }

        private IEnumerator Rotatate()
        {
            while (true)
            {
                transform.RotateAround(_pointRotation, _rotationAxis, _speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
