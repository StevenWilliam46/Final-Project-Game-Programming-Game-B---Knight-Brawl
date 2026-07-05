using System;
using UnityEngine;

namespace Apps.Scripts
{
    public enum JumpState
    {
        Grounded,
        Jumping
    }
    
    public class JumpAction : MonoBehaviour, ICharacterAction<float>
    {
        // attributes
        [SerializeField, Range(0, 30)] private float jumpPower;
        private JumpState _jumpState = JumpState.Grounded;
        
        // component references
        private Rigidbody _rb;
        private Animator _animator;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            if (_rb is null) Debug.LogWarning("No Rigidbody attached to the object", this);
            
            _animator = GetComponent<Animator>();
            
            Physics.gravity = new Vector3(0, -41f, 0);
        }

        private void FixedUpdate()
        {
            if (_jumpState == JumpState.Grounded) return;
            
            _animator.SetFloat("vertical velocity", _rb.linearVelocity.y);
        }

        public void Execute(float info)
        {
            Vector3 jumpVector = _rb.linearVelocity;
            jumpVector.y = jumpPower;
            _rb.linearVelocity = jumpVector;

            SetAnimationParameter(info);
            _jumpState = JumpState.Jumping;
        }

        private void SetAnimationParameter(float info)
        {
            _animator.SetTrigger("jump");
        }
    }
}