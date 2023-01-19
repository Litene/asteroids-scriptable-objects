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
    public bool HoldToShoot;
    
    
}