using DefaultNamespace.ScriptableEvents;
using System;
using UnityEngine;
using UnityEngine.Pool;
using Variables;
using Random = UnityEngine.Random;

namespace Asteroids {
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : MonoBehaviour {
        [SerializeField] private ScriptableEventInt _onAsteroidDestroyed;

        [Header("Config:")] 
        [SerializeField] private AsteroidSetting _setting;
        [SerializeField] private SharedPool _pool;

        [Header("References:")] [SerializeField]
        private Transform _shape;

        private Rigidbody2D _rigidbody;
        private Vector3 _direction;
        private int _instanceId;
        private void Awake() {
            _rigidbody = GetComponent<Rigidbody2D>();
            _instanceId = GetInstanceID();
        }

        private void OnEnable() {
            SetDirection();
            AddForce();
            AddTorque();
            SetSize();
            SetMass();
        }


        private void OnTriggerEnter2D(Collider2D other) {
            if (string.Equals(other.tag, "Laser")) {
                HitByLaser();
            }
        }
        
        private void HitByLaser() {
            _onAsteroidDestroyed.Raise(_instanceId);
            Destroy(this.gameObject);
            //_pool.Release(this);
        }
        

        // TODO Can we move this to a single listener, something like an AsteroidDestroyer?
       /* public void OnHitByLaser(IntReference asteroidId) { 
            if (_instanceId == asteroidId.GetValue()) {
                Destroy(gameObject);
            }
        }*/

      /*  public void OnHitByLaserInt(int asteroidId) { // just overload the method above if needed. 
            if (_instanceId == asteroidId) {
                Destroy(gameObject);
            }
        }*/

        private void SetDirection() {
            var size = new Vector2(3f, 3f);
            var target = new Vector3
            (
                Random.Range(-size.x, size.x),
                Random.Range(-size.y, size.y)
            );

            _direction = (target - transform.position).normalized;
        }

        private void AddForce() {
            _rigidbody.AddForce(_direction * _setting.GetForce(), ForceMode2D.Impulse);
        }

        private void AddTorque() {
            var torque = _setting.GetTorque();
            var roll = Random.Range(0, 2);

            if (roll == 0)
                torque = -torque;

            _rigidbody.AddTorque(torque, ForceMode2D.Impulse);
        }

        private void SetSize() {
            var size = _setting.GetSize();
            _shape.localScale = new Vector3(size, size, 0f);
        }
        
        private void SetMass() {
            var mass = _setting.GetMass();
            _rigidbody.mass = mass;
        }
    }
}