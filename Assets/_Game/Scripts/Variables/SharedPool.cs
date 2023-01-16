using System;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "Shared Pool")] public class SharedPool : ScriptableObject { // todo:cleanup
    private ObjectPool<MonoBehaviour> _value;
    [SerializeField] private MonoBehaviour _prefab;
    [SerializeField] private bool _collisionCheck;
    [SerializeField] private int _maxSize;
    [SerializeField] private int _defaultCapacity;
    private Vector3 _spawnPos;
    private Quaternion _spawnRot;

    public void OnEnable() {
        if (_maxSize == 0 || _defaultCapacity == 0) {
            _value = new ObjectPool<MonoBehaviour>(
                OnCreate,
                OnObjectGet,
                OnObjectRelease,
                OnObjectDestroy
            );
        }
        else {
            _value = new ObjectPool<MonoBehaviour>(
                OnCreate,
                OnObjectGet,
                OnObjectRelease,
                OnObjectDestroy,
                _collisionCheck,
                _defaultCapacity,
                _maxSize
            );
        }
    }

    public MonoBehaviour Get() {
        ResetRot();
        ResetPos();
        MonoBehaviour mono = _value.Get();
        Transform transform = mono.transform;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        return mono;
    }
    public MonoBehaviour Get(Vector3 position) {
        ResetRot();
        _spawnPos = position;
        MonoBehaviour mono = _value.Get(); // would like to know if it is a pooled object or a new object. 
        Transform transform = mono.transform;
        transform.position = position;
        transform.rotation = Quaternion.identity;
        return mono;
    }

    public MonoBehaviour Get(Vector3 position, Quaternion rotation) { // spawnrot only set for
        _spawnPos = position;
        _spawnRot = rotation;
        MonoBehaviour mono = _value.Get();
        Transform transform = mono.transform;
        transform.position = position;
        transform.rotation = rotation;
        return mono;
    }

    private void ResetPos() => _spawnPos = Vector3.zero;
    private void ResetRot() => _spawnRot = Quaternion.identity;
    
    //public void Release(MonoBehaviour behaviour) => _value.Release(behaviour);
    public void Release(Transform transform) {
        ResetPos(); // is this needed?
        ResetRot();
        try {
            var parentObj = transform.GetComponentInParent(_prefab.GetType()) as MonoBehaviour;
            if (parentObj) _value.Release(parentObj);
            else {
                var childObj = transform.GetComponentInChildren(_prefab.GetType()) as MonoBehaviour;
                if (childObj) _value.Release(childObj);
            }
        }
        catch (Exception e) {
            Debug.LogError($"lol");
        }
    }

    public void Release(MonoBehaviour behaviour) {
        ResetPos();
        ResetRot();
        _value.Release(behaviour);
    } 
    public void Dispose() => _value.Dispose();
    public void Clear() => _value.Clear();
    public int CountInactive() => _value.CountInactive;
    public int CountActive() => _value.CountActive;

    private MonoBehaviour OnCreate() {
        var tempObj = Instantiate(_prefab.gameObject, _spawnPos, _spawnRot);
        return tempObj.TryGetComponent(out MonoBehaviour monoBehavior) ? monoBehavior : null;
    }
    private void OnObjectDestroy(MonoBehaviour obj) => Destroy(obj.gameObject);
    private void OnObjectRelease(MonoBehaviour obj) => obj.gameObject.SetActive(false);

    private void OnObjectGet(MonoBehaviour obj) {
        obj.transform.position = _spawnPos;
        obj.gameObject.SetActive(true);
    }
}