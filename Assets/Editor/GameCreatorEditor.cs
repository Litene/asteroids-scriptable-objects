using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Variables;

public class GameCreatorEditor : EditorWindow {
    // todo: fix asteroidSetting
    [SerializeField] private VisualTreeAsset UXMLFile;

    private GameSettings _gameSettings;
    private AsteroidSetting _asteroidSetting;
    private VisualElement _root;

    private EnumField _forceRandom;
    private FloatField _forceFixed;
    private FloatField _forceMin;
    private FloatField _forceMax;
    private EnumField _sizeRandom;
    private FloatField _sizeFixed;
    private FloatField _sizeMin;
    private FloatField _sizeMax;
    private EnumField _torqueRandom;
    private FloatField _torqueFixed;
    private FloatField _torqueMin;
    private FloatField _torqueMax;
    private EnumField _massRandom;
    private FloatField _massFixed;
    private FloatField _massMin;
    private FloatField _massMax;

    [MenuItem("Tools/Game Editor")]
    public static void ShowWindow() {
        GameCreatorEditor window = GetWindow<GameCreatorEditor>();
        window.titleContent = new GUIContent("Game Editor");
    }

    private void OnEnable() {
        

        _gameSettings ??= Resources.Load("GameSettingsFile") as GameSettings;
        var settingsObject = new SerializedObject(_gameSettings);
        rootVisualElement.Bind(settingsObject);

        /* var maxHealthObject = new SerializedObject(_maxHealth);
         var shipMassObject = new SerializedObject(_shipMass);*/


        //rootVisualElement.(x => x.viewDataKey == "ButtonVisualElements").Q<Button>("Save").clicked += SaveSettings;

        // rootVisualElement.Query<Button>("Load").First().clicked += LoadSettings;
        // rootVisualElement.Query<Button>("New").First().clicked += NewSettings;
        // rootVisualElement.Query<Button>("Generate").First().clicked += GenerateSettings;
    }


    private void CreateGUI() {
        _root = new VisualElement();
        rootVisualElement.Add(_root);
        UXMLFile.CloneTree(_root); // bind everything here?

        _root.Query<Button>("Save").First().clicked += SaveSettings;
        _root.Query<Button>("Load").First().clicked += LoadSettings;
        _root.Query<Button>("New").First().clicked += NewSettings;
        _root.Query<Button>("Generate").First().clicked += GenerateSettings;
        
        _forceRandom = rootVisualElement.Q<EnumField>("ForceRandom");
        _sizeRandom = rootVisualElement.Q<EnumField>("SizeRandom");
        _torqueRandom = rootVisualElement.Q<EnumField>("TorqueRandom");
        _massRandom = rootVisualElement.Q<EnumField>("MassRandom");
        _forceFixed = rootVisualElement.Q<FloatField>("ForceFixed");
        _forceMin = rootVisualElement.Q<FloatField>("ForceMin");
        _forceMax = rootVisualElement.Q<FloatField>("ForceMax");
        _sizeFixed = rootVisualElement.Q<FloatField>("SizeFixed");
        _sizeMin = rootVisualElement.Q<FloatField>("SizeMin");
        _sizeMax = rootVisualElement.Q<FloatField>("SizeMax");
        _torqueFixed = rootVisualElement.Q<FloatField>("TorqueFixed");
        _torqueMin = rootVisualElement.Q<FloatField>("TorqueMin");
        _torqueMax = rootVisualElement.Q<FloatField>("TorqueMax");
        _massFixed = rootVisualElement.Q<FloatField>("MassFixed");
        _massMin = rootVisualElement.Q<FloatField>("MassMin");
        _massMax = rootVisualElement.Q<FloatField>("MassMax");
    }

    private void InitializeFields() {
        //rootVisualElement.Q<Slider>("ThrottlePower").value = _throttlePower.Value;
    }

    private void SetValues() {
        //_throttlePower.SetValue(rootVisualElement.Q<Slider>("ThrottlePower").value);
    }


    public void SaveSettings() {
        _asteroidSetting =
            rootVisualElement.Query<ObjectField>("AsteroidSettingField").First().value as AsteroidSetting;
        try {
            Debug.Log(_asteroidSetting.readValue);
        }
        catch (Exception e) {
            throw;
        }
    }

    public void LoadSettings() {
        try {
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public void NewSettings() {
        var asteroidSetting = UnityEngine.ScriptableObject.CreateInstance<AsteroidSetting>();
        var path = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/AsteroidSetting.asset");
        AssetDatabase.CreateAsset(asteroidSetting, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.ImportAsset(path);
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asteroidSetting;
        rootVisualElement.Q<ObjectField>("AsteroidSettingField").value = asteroidSetting;
        ResetAllVariables(); //  why is this a asteroidVariable?
    }

    private void ResetAllVariables() {
        _forceRandom.value = RandomizedSetting.Fixed;
        _forceFixed.value = 0;
        _forceMin.value = 0;
        _forceMax.value = 0;

        _sizeRandom.value = RandomizedSetting.Fixed;
        _sizeFixed.value = 0;
        _sizeMin.value = 0;
        _sizeMax.value = 0;

        _torqueRandom.value = RandomizedSetting.Fixed;
        _torqueFixed.value = 0;
        _torqueMin.value = 0;
        _torqueMax.value = 0;

        _massRandom.value = RandomizedSetting.Fixed;
        _massFixed.value = 0;
        _massMin.value = 0;
        _massMax.value = 0;
    }


    public void GenerateSettings() {
    }


    private void OnSelectionChange() {
        //_asteroidSetting = rootVisualElement.Q<ObjectField>("");
    }
}