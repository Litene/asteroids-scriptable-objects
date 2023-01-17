using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Variables {
    [CreateAssetMenu(fileName = "new FloatVariable", menuName = "ScriptableObjects/Variables/FloatVariable")]
    public class FloatVariable : ScriptableObject {
        public float Value;
        public void SetValue(float value) => Value = value;
    }
}