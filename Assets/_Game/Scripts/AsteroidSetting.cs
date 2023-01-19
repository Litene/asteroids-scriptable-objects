using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Asteroid Setting")]
public class AsteroidSetting : ScriptableObject {

    [SerializeField] public AsteroidVariables ForceVariables = new();

    [SerializeField] public AsteroidVariables SizeVariables = new();

    [SerializeField] public AsteroidVariables TorqueVariables = new();

    [SerializeField] public AsteroidVariables MassVariables = new();
    
#if UNITY_EDITOR
    public List<AsteroidVariables> settings;

    private void OnEnable() {
        settings = new List<AsteroidVariables>() { ForceVariables, SizeVariables, TorqueVariables, MassVariables };
    }
#endif


    public void SetVariables(AsteroidSettingType type, float fixedVar, float minVar, float maxVar, string enumString) {
        var setting = GetEnumFromString(enumString);
        switch (type) {
            case AsteroidSettingType.Force:
                ForceVariables.RandomizedSetting = setting;
                ForceVariables.Fixed = fixedVar;
                ForceVariables.Min = minVar;
                ForceVariables.Max = maxVar;
                break;
            case AsteroidSettingType.Size:
                SizeVariables.RandomizedSetting = setting;
                SizeVariables.Fixed = fixedVar;
                SizeVariables.Min = minVar;
                SizeVariables.Max = maxVar;
                break;
            case AsteroidSettingType.Torque:
                TorqueVariables.RandomizedSetting = setting;
                TorqueVariables.Fixed = fixedVar;
                TorqueVariables.Min = minVar;
                TorqueVariables.Max = maxVar;
                break;
            default:
                MassVariables.RandomizedSetting = setting;
                MassVariables.Fixed = fixedVar;
                MassVariables.Min = minVar;
                MassVariables.Max = maxVar;
                break;
        }
    }


    private RandomizedSetting GetEnumFromString(string enumName) {
        if (enumName == "BetweenTwoConstants") return RandomizedSetting.BetweenTwoConstants;
        else return RandomizedSetting.Fixed;
    }

    public float GetForce() {
        if (ForceVariables.RandomizedSetting == RandomizedSetting.Fixed) {
            return ForceVariables.Fixed;
        }
        else {
            return ForceVariables.Min > ForceVariables.Max || ForceVariables.Max < ForceVariables.Min
                ? ForceVariables.Min
                : Random.Range(ForceVariables.Min, ForceVariables.Max);
        }
    }

    public AsteroidVariables GetForceVars() => ForceVariables;

    public float GetSize() {
        if (SizeVariables.RandomizedSetting == RandomizedSetting.Fixed) {
            return SizeVariables.Fixed;
        }
        else {
            return SizeVariables.Min > SizeVariables.Max || SizeVariables.Max < SizeVariables.Min
                ? SizeVariables.Min
                : Random.Range(SizeVariables.Min, SizeVariables.Max);
        }
    }

    public AsteroidVariables GetSizeVars() => SizeVariables;

    public float GetTorque() {
        if (TorqueVariables.RandomizedSetting == RandomizedSetting.Fixed) {
            return TorqueVariables.Fixed;
        }
        else {
            return TorqueVariables.Min > TorqueVariables.Max || TorqueVariables.Max < TorqueVariables.Min
                ? TorqueVariables.Min
                : Random.Range(TorqueVariables.Min, TorqueVariables.Max);
        }
    }

    public AsteroidVariables GetTorqueVars() => TorqueVariables;


    public float GetMass() {
        if (MassVariables.RandomizedSetting == RandomizedSetting.Fixed) {
            return MassVariables.Fixed;
        }
        else {
            return MassVariables.Min > MassVariables.Max || MassVariables.Max < MassVariables.Min
                ? MassVariables.Min
                : Random.Range(MassVariables.Min, MassVariables.Max);
        }
    }


    public AsteroidVariables GetMassVars() => MassVariables;
}

public enum AsteroidSettingType {
    Force,
    Size,
    Torque,
    Mass
}