using System;
using UnityEngine;

namespace Ship {
    public class Gun : MonoBehaviour {
        [SerializeField] private Laser _laserPrefab;
        [SerializeField] private SharedPool _laserPool;
        [SerializeField] private GameSettings _settings;
        private float shootCooldown = 0.2f;
        private float shootTimer;
        private void Update() {
            shootTimer += Time.deltaTime;
            if (!_settings.HoldToShoot) {
                if (Input.GetKeyDown(KeyCode.Space))
                    Shoot();
            }
            else {
                if (Input.GetKey(KeyCode.Space)) {
                    if (shootTimer > shootCooldown) {
                        Shoot();
                        shootTimer = 0;
                    }
                }
            }
        }

        private void Shoot() {
            _laserPool.Get(transform.position, transform.rotation);
        }
    }
}