using System;
using UnityEngine;

namespace Ship {
    public class Gun : MonoBehaviour {
        [SerializeField] private Laser _laserPrefab;
        [SerializeField] private SharedPool _laserPool;

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space))
                Shoot();
        }

        private void Shoot() {
            _laserPool.Get(transform.position, transform.rotation);
        }
    }
}