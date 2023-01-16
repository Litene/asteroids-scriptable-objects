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
        private void OnCollisionEnter2D(Collision2D other) {
            if (string.Equals(other.gameObject.tag, "Asteroid")) {
                // TODO can we bake this into one call?
                //_healthRef.ApplyChange(-1);
                //_onHealthChangedEvent.Raise(_healthRef);
                _healthObservable.ApplyChange(-1); // hardcoded, should be based on damage from astroids, can be based on mass
            }
        }

       
    }
}