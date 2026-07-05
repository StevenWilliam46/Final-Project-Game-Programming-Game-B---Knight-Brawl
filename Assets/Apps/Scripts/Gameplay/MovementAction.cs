using System;
using UnityEngine;

namespace Apps.Scripts
{
    public class MovementAction : MonoBehaviour, ICharacterAction<Vector2>
    {
        // attributes
        [SerializeField, Range(0, 30)] private float movementSpeed;
        
        // component references
        private Rigidbody _rb;
        private Animator _animator;
        private Vector3 _direction;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            if (_rb is null) Debug.LogWarning("No Rigidbody attached to the object", this);
            
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            _direction.y = _rb.linearVelocity.y;
            _rb.linearVelocity = _direction;
        }

        public void Execute(Vector2 info)
        {
            _direction.x = info.x * movementSpeed;
            _direction.z = info.y * movementSpeed;

            SetOrientation(info);
            SetAnimationParameter(info);
        }

        private void SetOrientation(Vector2 info)
        {
            transform.LookAt(new Vector3(transform.position.x + info.x
            , transform.position.y
            , transform.position.z + info.y));
        }

        private void SetAnimationParameter(Vector2 info)
        {
            _animator.SetFloat("horizontal velocity", info.magnitude);
        }
    }
}