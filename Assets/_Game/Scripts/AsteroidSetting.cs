using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Asteroid Setting")]
public class AsteroidSetting : ScriptableObject { // error handling is making sure min is lower than max

    [SerializeField] private GameSettings _settings; // dont forget to set this.
    
  /*  [SerializeField] private AsteroidVariables _forceVariables;

    [SerializeField] private AsteroidVariables _sizeVariables;

    [SerializeField] private AsteroidVariables _torqueVariables;

    [SerializeField] private AsteroidVariables _massVariables; // not yet used;*/

   /* private void OnValidate() { // this might not work. should make min to max and vice versa.
        _forceVariables.Sync();
        _sizeVariables.Sync();
        _torqueVariables.Sync();
        _massVariables.Sync();
    }*/
    
    public float GetForce() => _settings.ForceRandomize == RandomizedSetting.Fixed ? _settings.ForceSettingFixed : Random.Range(_settings.ForceSettingMin, _settings.ForceSettingMax);
    //public AsteroidVariables GetForceVars() => _forceVariables;
    public float GetSize() => _settings.SizeRandomize == RandomizedSetting.Fixed ? _settings.SizeSettingFixed : Random.Range(_settings.SizeSettingMin, _settings.SizeSettingMax);
    public float GetTorque() => _settings.TorqueRandomize == RandomizedSetting.Fixed ? _settings.TorqueSettingFixed : Random.Range(_settings.TorqueSettingMin, _settings.TorqueSettingMax);
    //ublic AsteroidVariables GetTorqueVars() => _torqueVariables;
    public float GetMass() => _settings.MassRandomize == RandomizedSetting.Fixed ? _settings.MassSettingFixed : Random.Range(_settings.MassSettingMin, _settings.MassSettingMax); // not yet used
   // public AsteroidVariables GetMassVars() => _massVariables; // not yet used

    private void OnEnable() {
        _settings ??= Resources.Load("GameSettingsFile") as GameSettings;

        EditorUtility.SetDirty(this);
        
     /*   _forceVariables.RandomizedSetting = _settings.ForceRandomize;
        _forceVariables.Fixed = _settings.ForceSettingFixed;
        _forceVariables.Min = _settings.ForceSettingMin;
        _forceVariables.Max = _settings.ForceSettingMax;
        
        _sizeVariables.RandomizedSetting = _settings.SizeRandomize;
        _sizeVariables.Fixed = _settings.SizeSettingFixed;
        _sizeVariables.Min = _settings.SizeSettingMin;
        _sizeVariables.Max = _settings.SizeSettingMax;
        
        _torqueVariables.RandomizedSetting = _settings.TorqueRandomize;
        _torqueVariables.Fixed = _settings.TorqueSettingFixed;
        _torqueVariables.Min = _settings.TorqueSettingMin;
        _torqueVariables.Max = _settings.TorqueSettingMax;
        
        _massVariables.RandomizedSetting = _settings.MassRandomize;
        _massVariables.Fixed = _settings.MassSettingFixed;
        _massVariables.Min = _settings.MassSettingMin;
        _massVariables.Max = _settings.MassSettingMax;*/
    }

}


