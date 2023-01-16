using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(GameEditor))]public class GameEditorEditor : EditorWindow {
    [SerializeField] private VisualTreeAsset UXMLFile;

    [MenuItem("Tools/Game Editor")]public static void ShowWindow() {
        GameEditorEditor window = GetWindow<GameEditorEditor>();
        window.titleContent = new GUIContent("Game Editor");
    }

    private void CreateGUI() => UXMLFile.CloneTree(rootVisualElement);
    
    
    
}