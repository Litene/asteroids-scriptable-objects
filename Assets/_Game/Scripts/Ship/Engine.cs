using System;
using UnityEditor.VersionControl;
using UnityEngine;
using Variables;

namespace Ship {
    [RequireComponent(typeof(Rigidbody2D))]
    public class Engine : MonoBehaviour {
        
        [SerializeField] private float _throttlePowerSimple; // not used?
        [SerializeField] private float _rotationPowerSimple; // not used?
        private bool _throttle;
        private bool _steerLeft;
        private bool _steerRight;
        private Rigidbody2D _rigidbody;
        [SerializeField] private GameSettings _settings;

        private void FixedUpdate() {
            if (_throttle) Throttle();
            if (_steerLeft) SteerLeft();
            else if (_steerRight) SteerRight();
        }
        private void Update() {
            _throttle = Input.GetKey(KeyCode.UpArrow);
            _steerLeft = Input.GetKey(KeyCode.LeftArrow);
            _steerRight = Input.GetKey(KeyCode.RightArrow);
        }

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody2D>();
        } 
        public void Throttle() => _rigidbody.AddForce(transform.up *  _settings.ThrottlePower, ForceMode2D.Force);
        public void SteerLeft() => _rigidbody.AddTorque(_settings.InvertControls ? -_settings.RotationPower : _settings.RotationPower, ForceMode2D.Force);
        public void SteerRight() => _rigidbody.AddTorque(_settings.InvertControls ? _settings.RotationPower : -_settings.RotationPower, ForceMode2D.Force);
    }
}