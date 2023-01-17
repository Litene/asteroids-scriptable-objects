using UnityEngine;
[CreateAssetMenu(fileName = "new GameSettings", menuName = "ScriptableObjects/Game Settings")]
[System.Serializable]public class GameSettings : ScriptableObject { // fucking shitty solution... 

    public Color ShipColor; //add
    public float RotationPower;
    public float ThrottlePower;
    
    public int StartingHealth;
    public float ShipMass; //add

    public bool InvertControls; //add
    public bool CheatMode; //add

    private AsteroidSetting AsteroidSetting;
    
    public RandomizedSetting ForceRandomize;
    public float ForceSettingMin;
    public float ForceSettingMax;
    public float ForceSettingFixed;
    
    public RandomizedSetting SizeRandomize;
    public float SizeSettingMin;
    public float SizeSettingMax;
    public float SizeSettingFixed;
    
    public RandomizedSetting TorqueRandomize;
    public float TorqueSettingMin;
    public float TorqueSettingMax;
    public float TorqueSettingFixed;
    
    public RandomizedSetting MassRandomize;
    public float MassSettingMin;
    public float MassSettingMax;
    public float MassSettingFixed;
    
}