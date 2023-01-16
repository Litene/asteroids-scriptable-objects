using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(AsteroidVariables))]
public class AsteroidVariableEditor : PropertyDrawer {
    public override VisualElement CreatePropertyGUI(SerializedProperty property) {
        return new PropertyField(property);
        
        
    }
    
    
}