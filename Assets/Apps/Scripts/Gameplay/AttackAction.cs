using UnityEngine;

namespace Apps.Scripts
{
    public class AttackAction : MonoBehaviour, ICharacterAction<int>
    {
        [Header("Attributes")]
        [SerializeField] private int attackPower;
    
        private ObjectDetector  _objectDetector;
        private Animator _animator;

        private void Awake()
        {
            _objectDetector = GetComponentInChildren<ObjectDetector>();
            _animator = GetComponent<Animator>();
        }

        public void Execute(int info)
        {
            _animator.SetTrigger("attack");
            foreach (Health obj in _objectDetector.detectedObjects)
            {
                obj.TakeDamage(attackPower);
            }
        }
    }
}
