using System;
using UnityEditor.VersionControl;
using UnityEngine;
using Variables;

namespace Ship {
    [RequireComponent(typeof(Rigidbody2D))]
    public class Engine : MonoBehaviour {
        [SerializeField] private FloatVariable _throttlePower;
        [SerializeField] private FloatVariable _rotationPower;
        [SerializeField] private FloatVariable _mass;
        
        [SerializeField] private float _throttlePowerSimple; // not used?
        [SerializeField] private float _rotationPowerSimple; // not used?

        private bool _throttle;
        private bool _steerLeft;
        private bool _steerRight;
        private Rigidbody2D _rigidbody;

        private void FixedUpdate() {
            if (_throttle) Throttle();
            if (_steerLeft) SteerLeft();
            else if (_steerRight) SteerRight();
        }
        private void Update() {
            _throttle = Input.GetKey(KeyCode.UpArrow);
            _steerLeft = Input.GetKey(KeyCode.LeftArrow);
            _steerRight = Input.GetKey(KeyCode.RightArrow);
            
            
            if (Input.GetKeyDown(KeyCode.H)) {
                _throttlePower.SetValue(10f);
            }

            if (Input.GetKeyDown(KeyCode.J)) {
                Debug.LogError(_throttlePower.Value);
            }
        }

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.mass = _mass.Value;
        } 
        public void Throttle() => _rigidbody.AddForce(transform.up * _throttlePower.Value, ForceMode2D.Force);
        public void SteerLeft() => _rigidbody.AddTorque(_rotationPower.Value, ForceMode2D.Force);
        public void SteerRight() => _rigidbody.AddTorque(-_rotationPower.Value, ForceMode2D.Force);
    }
}