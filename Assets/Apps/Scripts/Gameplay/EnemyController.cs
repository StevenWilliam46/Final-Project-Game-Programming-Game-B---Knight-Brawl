using UnityEngine;

namespace Apps.Scripts
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }

    public class EnemyController : MonoBehaviour
    {
        [Header("Target")]
        [SerializeField] private string playerTag = "Player";
        [SerializeField] private Transform target; // bisa diisi manual, atau auto-cari lewat tag

        [Header("Ranges")]
        [SerializeField, Range(0, 30)] private float chaseRange = 8f;
        [SerializeField, Range(0, 10)] private float attackRange = 1.5f;

        [Header("Attack")]
        [SerializeField] private float attackCooldown = 1.5f;
        private float _attackTimer;

        [Header("Debug")]
        [SerializeField] private EnemyState currentState = EnemyState.Idle;

        // component references
        private MovementAction _movementAction;
        private AttackAction _attackAction;

        private void Awake()
        {
            _movementAction = GetComponent<MovementAction>();
            _attackAction = GetComponent<AttackAction>();

            if (_movementAction is null) Debug.LogWarning("Component MovementAction is not attached to the object", this);
            if (_attackAction is null) Debug.LogWarning("Component AttackAction is not attached to the object", this);

            if (target is null)
            {
                GameObject player = GameObject.FindGameObjectWithTag(playerTag);
                if (player is not null) target = player.transform;
                else Debug.LogWarning($"No object with tag '{playerTag}' found in scene", this);
            }
        }

        private void Update()
        {
            if (target is null) return;

            _attackTimer -= Time.deltaTime;
            float distance = Vector3.Distance(transform.position, target.position);

            switch (currentState)
            {
                case EnemyState.Idle:
                    TickIdle(distance);
                    break;
                case EnemyState.Chase:
                    TickChase(distance);
                    break;
                case EnemyState.Attack:
                    TickAttack(distance);
                    break;
            }
        }

        private void TickIdle(float distance)
        {
            if (distance <= chaseRange)
                currentState = EnemyState.Chase;
        }

        private void TickChase(float distance)
        {
            if (distance > chaseRange)
            {
                currentState = EnemyState.Idle;
                _movementAction?.Execute(Vector2.zero);
                return;
            }

            if (distance <= attackRange)
            {
                currentState = EnemyState.Attack;
                _movementAction?.Execute(Vector2.zero);
                return;
            }

            MoveTowardsTarget();
        }

        private void TickAttack(float distance)
        {
            if (distance > attackRange)
            {
                currentState = EnemyState.Chase;
                return;
            }

            FaceTarget();

            if (_attackTimer <= 0f)
            {
                _attackAction?.Execute(0); // info tidak dipakai isinya di AttackAction, aman diisi 0
                _attackTimer = attackCooldown;
            }
        }

        private void MoveTowardsTarget()
        {
            Vector3 delta = target.position - transform.position;
            Vector2 input = new Vector2(delta.x, delta.z).normalized;
            _movementAction?.Execute(input);
        }

        private void FaceTarget()
        {
            Vector3 lookPos = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.LookAt(lookPos);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
