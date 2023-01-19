using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]public class AsteroidVariables {
    public RandomizedSetting RandomizedSetting;
    public float Fixed;
    public float Min;
    public float Max;
    public override string ToString() => $"RandomSetting: {RandomizedSetting}, Fixed: {Fixed}, Min: {Min}, Max: {Max}";
}

