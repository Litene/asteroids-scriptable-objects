using System;
using UnityEngine;
using UnityEngine.Animations;
using Random = UnityEngine.Random;
using UnityEngine.Pool;


namespace Asteroids {
    public class AsteroidSpawner : MonoBehaviour { // pool all the astroids, and set max astroids on scene, set spawntimer, 
        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private float _minSpawnTime; // needs to be scriptableObject
        [SerializeField] private float _maxSpawnTime; // needs to be scriptableObject
        [SerializeField] private int _minAmount; // needs to be scripableObject
        [SerializeField] private int _maxAmount; // needs to be scripableObject

        private float _timer;
        private float _nextSpawnTime;
        private Camera _camera;
        private ObjectPool<Asteroid> _asteroidPool;

        private enum SpawnLocation {
            Top,
            Bottom,
            Left,
            Right
        }

        private void Awake() {
            _camera = Camera.main;
            _asteroidPool = new ObjectPool<Asteroid>(
                SpawnNew,
                OnAstroidGet,
                OnAstroidRelease,
                OnAstroidDestroy,
                true, 
                4, 
                10);
        }

        private void OnAstroidDestroy(Asteroid obj) => Destroy(obj.gameObject);
        private void OnAstroidRelease(Asteroid obj) => obj.gameObject.SetActive(false);
        private void OnAstroidGet(Asteroid obj) => obj.gameObject.SetActive(true);
        
        

        private void Start() {
            SpawnNew();
            UpdateNextSpawnTime();
        }

        private void Update() {
            UpdateTimer();

            if (!ShouldSpawn())
                return;

            SpawnNew();
            UpdateNextSpawnTime();
            _timer = 0f;
        }

        private void UpdateNextSpawnTime() {
            _nextSpawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
        }

        private void UpdateTimer() {
            _timer += Time.deltaTime;
        }

        private bool ShouldSpawn() {
            return _timer >= _nextSpawnTime;
        }

        private Asteroid SpawnNew() {
            var amount = Random.Range(_minAmount, _maxAmount + 1);
            Asteroid asteroid = null;
            for (var i = 0; i < amount; i++) {
                var location = GetSpawnLocation();
                var position = GetStartPosition(location);
                asteroid = Instantiate(_asteroidPrefab, position, Quaternion.identity);
                asteroid.SetPool(_asteroidPool);
            }

            return asteroid;
        }

        private void Spawn() {
            var amount = Random.Range(_minAmount, _maxAmount + 1);

            for (var i = 0; i < amount; i++) {
                var location = GetSpawnLocation();
                var position = GetStartPosition(location);
                var asteroid = _asteroidPool.Get();
            }
        }
        

        private static SpawnLocation GetSpawnLocation() {
            var roll = Random.Range(0, 4);

            return roll switch {
                1 => SpawnLocation.Bottom,
                2 => SpawnLocation.Left,
                3 => SpawnLocation.Right,
                _ => SpawnLocation.Top
            };
        }

        private Vector3 GetStartPosition(SpawnLocation spawnLocation) {
            var pos = new Vector3 { z = Mathf.Abs(_camera.transform.position.z) };

            const float padding = 5f;
            switch (spawnLocation) {
                case SpawnLocation.Top:
                    pos.x = Random.Range(0f, Screen.width);
                    pos.y = Screen.height + padding;
                    break;
                case SpawnLocation.Bottom:
                    pos.x = Random.Range(0f, Screen.width);
                    pos.y = 0f - padding;
                    break;
                case SpawnLocation.Left:
                    pos.x = 0f - padding;
                    pos.y = Random.Range(0f, Screen.height);
                    break;
                case SpawnLocation.Right:
                    pos.x = Screen.width - padding;
                    pos.y = Random.Range(0f, Screen.height);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(spawnLocation), spawnLocation, null);
            }

            return _camera.ScreenToWorldPoint(pos);
        }
    }
}