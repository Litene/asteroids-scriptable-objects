using UnityEngine;

namespace Asteroids {
    public class OnBecameInvisibleDestroyer : MonoBehaviour {
        [SerializeField] private GameObject _toDestroy;
        [SerializeField] private SharedPool _sharedPool;

        private void OnBecameInvisible() {
            _sharedPool.Release(transform);
        }
    }
}