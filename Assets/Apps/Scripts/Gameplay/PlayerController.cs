using UnityEngine;
using UnityEngine.InputSystem;

namespace Apps.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        // attributes
        private MovementAction _movementAction;
        private JumpAction _jumpAction;
        private AttackAction _attackAction;

        private void Awake()
        {
            _movementAction = GetComponent<MovementAction>();
            _jumpAction = GetComponent<JumpAction>();
            _attackAction = GetComponent<AttackAction>();
        }

        public void OnMove(InputAction.CallbackContext ctx)
        {
            if (_movementAction is null)
            {
                Debug.LogWarning("Component MovementAction is not attached to the object", this);
                return;
            }
        
            if (ctx.performed || ctx.canceled)
            {
                _movementAction.Execute(ctx.ReadValue<Vector2>());
            }
        }

        public void OnJump(InputAction.CallbackContext ctx)
        {
            if (_jumpAction is null)
            {
                Debug.LogWarning("Component JumpAction is not attached to the object", this);
                return;
            }

            if (ctx.performed)
            {
                _jumpAction.Execute(20);
            }
        }

        public void OnAttack(InputAction.CallbackContext ctx)
        {
            if (_attackAction is null)
            {
                Debug.LogWarning("Component AttackAction is not attached to the object", this);
                return;
            }

            if (ctx.performed)
            {
                _attackAction.Execute(5);
            }
        }
    }
}
