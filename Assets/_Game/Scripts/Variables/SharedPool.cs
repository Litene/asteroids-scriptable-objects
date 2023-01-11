using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "yo")]
public class SharedPool : ScriptableObject {
    private ObjectPool<MonoBehaviour> _value;
    [SerializeField] private MonoBehaviour _prefab;
    [SerializeField] private bool _collisionCheck;
    [SerializeField] private int _maxSize;
    [SerializeField] private int _defaultCapacity;
    private Vector3 _spawnPos;
    public void OnEnable() {
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

    public void Get(Vector3 position) {
        _spawnPos = position;
        _value.Get();
    }
    
    public void Release(MonoBehaviour behaviour) => _value.Release(behaviour);
    public void Dispose() => _value.Dispose();
    public void Clear() => _value.Clear();
    public int CountInactive() => _value.CountInactive;
    public int CountActive() => _value.CountActive;
    
    private MonoBehaviour OnCreate() {
        var tempObj = Instantiate(_prefab.gameObject, _spawnPos, Quaternion.identity);
        return tempObj.TryGetComponent(out MonoBehaviour monoBehavior) ? monoBehavior : null;
    }
    private void OnObjectDestroy(MonoBehaviour obj) => Destroy(obj.gameObject);
    private void OnObjectRelease(MonoBehaviour obj) => obj.gameObject.SetActive(false);
    private void OnObjectGet(MonoBehaviour obj) {
        obj.transform.position = _spawnPos;    
        obj.gameObject.SetActive(true);
    } 
}