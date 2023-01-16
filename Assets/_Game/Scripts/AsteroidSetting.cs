using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Asteroid Setting")]
public class AsteroidSetting : ScriptableObject { // error handling is making sure min is lower than max
    [SerializeField] private RandomizedSetting _forceSetting;
    [SerializeField] private AsteroidVariables _forceVariables;

    [SerializeField] private RandomizedSetting _sizeSetting; 
    [SerializeField] private AsteroidVariables _sizeVariables;

    [SerializeField] private RandomizedSetting _torqueSetting; 
    [SerializeField] private AsteroidVariables _torqueVariables;

    [SerializeField] private RandomizedSetting _massSettings;
    [SerializeField] private AsteroidVariables _massVariables;

    private void OnValidate() { // this might not work. should make min to max and vice versa.
        _forceVariables.Sync();
        _sizeVariables.Sync();
        _torqueVariables.Sync();
        _massVariables.Sync();
    }
    
    public float GetForce() => _forceSetting == RandomizedSetting.Fixed ? _forceVariables.Fixed : Random.Range(_forceVariables.Min, _forceVariables.Max);
    public AsteroidVariables GetForceVars() => _forceVariables;
    public float GetSize() => _sizeSetting == RandomizedSetting.Fixed ? _sizeVariables.Fixed : Random.Range(_sizeVariables.Min, _sizeVariables.Max);
    public AsteroidVariables GetSizeVars() => _sizeVariables;
    public float GetTorque() => _torqueSetting == RandomizedSetting.Fixed ? _torqueVariables.Fixed : Random.Range(_torqueVariables.Min, _torqueVariables.Max);
    public AsteroidVariables GetTorqueVars() => _torqueVariables;
    
    public float GetMass() => _massSettings == RandomizedSetting.Fixed ? _massVariables.Fixed : Random.Range(_massVariables.Min, _massVariables.Max);
    public AsteroidVariables GetMassVars() => _massVariables;

}

[System.Serializable] public class AsteroidVariables {
    public float Fixed;
    public float Min;
    public float Max;

    public void Sync() {
        if (Min > Max) Min = Max;
        if (Max < Min) Max = Min;
    }
}

