using UnityEngine;

namespace Ship {
    
    //deprecated?
    public class Health : MonoBehaviour {
        [SerializeField] private int _health = 20;
        private const int MinHealth = 0;

        public void TakeDamage(int damage) {
            _health = Mathf.Max(MinHealth, _health - damage);
        }
    }
}