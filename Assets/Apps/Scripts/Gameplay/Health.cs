using UnityEngine;
using UnityEngine.Events;

namespace Apps.Scripts
{
    public class Health : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private int max;
        [SerializeField] private int current;
        private float Percentage => (float)current / (float)max;

        [Header("Events")]
        public UnityEvent OnHit;
        public UnityEvent OnRecover;
        public UnityEvent<float> OnHealthChanged;
    
        public void TakeDamage(int damage)
        {
            current -= damage;
            if (current <= 0)
                current = 0;
            OnHit?.Invoke();
            OnHealthChanged?.Invoke(Percentage);
        }

        public void Recover(int value)
        {
            current += value;
            if (current >= max)
                current = max;
            OnRecover?.Invoke();
            OnHealthChanged?.Invoke(current);
            OnHealthChanged?.Invoke(Percentage);
        }
    }
}