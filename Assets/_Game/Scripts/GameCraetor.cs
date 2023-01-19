using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Variables;

public class GameCraetor : MonoBehaviour {
    [Header("Engine")] [SerializeField] private FloatVariable _throttlePowerSO;
    [SerializeField, Range(0.1f, 10f)] private float _throttlePower;
    [SerializeField] private FloatVariable _rotationPowerSO;
    [SerializeField, Range(0.1f, 10f)] private float _rotationPower;

    [Header("Hull")] [SerializeField] private IntVariable _maxHealthSO;
    [SerializeField] private int _maxHealth;
    [SerializeField] private FloatVariable _shipMassSO;
    [SerializeField, Range(0.1f, 10f)] private float _shipMass;

    [Header("Asteroids")] // maybe an scriptableObject for astroid setting containing all of them. 
    [SerializeField]
    private AsteroidSetting _asteroidSetting;

    
    
   /* [SerializeField] private RandomizedSetting _MassSetting;
    [SerializeField] private float _asteroidMassFixed;
    [SerializeField] private float _asteroidMassLower;
    [SerializeField] private float _asteroidMassUpper;*/
    
    // [SerializeField] private RandomizedSetting _forceSetting;
    // [SerializeField] private float _fixedForce;
    // [SerializeField] private float _minForce;
    // [SerializeField] private float _maxForce;
    
    // [SerializeField] private RandomizedSetting _sizeSetting;
    // [SerializeField] private float _fixedSize;
    // [SerializeField] private float _minSize;
    // [SerializeField] private float _maxSize;
    
    // [SerializeField] private RandomizedSetting _torqueSetting;
    // [SerializeField] private float _fixedTorque;
    // [SerializeField] private float _minTorque;
    // [SerializeField] private float _maxTorque;

    [SerializeField] private string asteroidSettingName;
    

    [ContextMenu("Save")] public string SaveSettings() {

        return "";
    }

    [ContextMenu("Load")] public string LoadSettings() {
        if (_asteroidSetting == null) {
            return "Nothing to load";
        }
        else {
            return "";
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.H)) {
            EditorUtility.SetDirty(_throttlePowerSO);
            _throttlePowerSO.SetValue(10);
        }
    }

    public void CreateScriptableObject() {
        var asteroidSetting = ScriptableObject.CreateInstance<AsteroidSetting>();
        AssetDatabase.CreateAsset(asteroidSetting, $"Assets/Resources/{asteroidSettingName}.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asteroidSetting;
        
    }
    
    [ContextMenu("SetVariables")] public void SetVariables() {
        _throttlePowerSO ??= Resources.Load<FloatVariable>("ThrottlePower");
        _rotationPowerSO ??= Resources.Load<FloatVariable>("RotationPower");
        _shipMassSO ??= Resources.Load<FloatVariable>("ShipMass");
        _maxHealthSO ??= Resources.Load<IntVariable>("Health");
        EditorUtility.SetDirty(_throttlePowerSO);
        EditorUtility.SetDirty(_rotationPowerSO);
        EditorUtility.SetDirty(_maxHealthSO);
        EditorUtility.SetDirty(_shipMassSO);
        _throttlePowerSO.SetValue(5);
        _rotationPowerSO.SetValue(_rotationPower);
        _maxHealthSO.SetValue(_maxHealth);
        _shipMassSO.SetValue(_shipMass);
        
    }

}

public enum RandomizedSetting {
    Fixed,
    BetweenTwoConstants
}