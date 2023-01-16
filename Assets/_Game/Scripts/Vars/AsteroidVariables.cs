using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidVariables {
    public RandomizedSetting RandomizedSetting;
    public float Fixed;
    public float Min;
    public float Max;
    public void Sync() {
        if (Min > Max) Min = Max;
        if (Max < Min) Max = Min;
    }
}

