using DefaultNamespace.ScriptableEvents;
using System;
using UnityEngine;
using Variables;

namespace Ship {
    public class Hull : MonoBehaviour {
        //[SerializeField] private IntVariable _health;
        [SerializeField] private ScriptableEventIntReference _onHealthChangedEvent;
        [SerializeField] private IntReference _healthRef;
        [SerializeField] private IntObservable _healthObservable;
        [SerializeField] private GameSettings _gameSettings;
        private SpriteRenderer _renderer;
        private Rigidbody2D _rb;
        private void Awake() {
            _renderer = GetComponentInChildren<SpriteRenderer>();
            _rb = GetComponent<Rigidbody2D>();
            _renderer.color = _gameSettings.ShipColor;
            _rb.mass = _gameSettings.ShipMass;
            _healthRef.SetValue(_gameSettings.StartingHealth);
            _healthObservable.SetValue(_gameSettings.StartingHealth);
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (_gameSettings.CheatMode) return;    
            if (string.Equals(other.gameObject.tag, "Asteroid")) {
                // TODO can we bake this into one call?
                //_healthRef.ApplyChange(-1);
                //_onHealthChangedEvent.Raise(_healthRef);
                _healthObservable.ApplyChange(-1); 
            }
        }

       
    }
}