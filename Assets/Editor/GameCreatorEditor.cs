using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Variables;

public class GameCreatorEditor : EditorWindow {
    // todo: fix asteroidSetting
    [SerializeField] private VisualTreeAsset UXMLFile;
    private string[] typeArray = new string[]{ "Force", "Size", "Torque", "Mass" }; 
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
    private FloatField _forceFixedFiller;
    private FloatField _sizeFixedFiller;
    private FloatField _torqueFixedFiller;
    private FloatField _massFixedFiller;

    [MenuItem("Tools/Game Editor")] public static void ShowWindow() {
        GameCreatorEditor window = GetWindow<GameCreatorEditor>();
        window.titleContent = new GUIContent("Game Editor");
        window.minSize = new Vector2(835, 400);
        window.maxSize = new Vector2(835, 400);
    }

    private void OnEnable() {
        _gameSettings ??= Resources.Load("GameSettingsFile") as GameSettings;
        var settingsObject = new SerializedObject(_gameSettings);
        rootVisualElement.Bind(settingsObject);

        
    }

    private void CreateGUI() {
        _root = new VisualElement();
        rootVisualElement.Add(_root);
        UXMLFile.CloneTree(_root); // bind everything here?

        _root.Query<Button>("Save").First().clicked += SaveSettings;
        _root.Query<Button>("Load").First().clicked += LoadSettings;
        _root.Query<Button>("New").First().clicked += NewSettings;

        _forceRandom = rootVisualElement.Q<EnumField>("ForceRandom"); //doesn't work
        _forceRandom.Init(RandomizedSetting.Fixed);
        _sizeRandom = rootVisualElement.Q<EnumField>("SizeRandom"); //doesn't work
        _forceRandom.Init(RandomizedSetting.Fixed);
        _torqueRandom = rootVisualElement.Q<EnumField>("TorqueRandom"); //doesn't work
        _forceRandom.Init(RandomizedSetting.Fixed);
        _massRandom = rootVisualElement.Q<EnumField>("MassRandom"); //doesn't work
        _forceRandom.Init(RandomizedSetting.Fixed);
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
        _forceFixedFiller = rootVisualElement.Q<FloatField>("ForceFixedFiller");
        _sizeFixedFiller = rootVisualElement.Q<FloatField>("SizeFixedFiller");
        _torqueFixedFiller = rootVisualElement.Q<FloatField>("TorqueFixedFiller");
        _massFixedFiller = rootVisualElement.Q<FloatField>("MassFixedFiller");
        
        _forceRandom.RegisterCallback<ChangeEvent<Enum>>((evt) => { CorrectFields(); } );
        _sizeRandom.RegisterCallback<ChangeEvent<Enum>>((evt) => { CorrectFields(); } );
        _torqueRandom.RegisterCallback<ChangeEvent<Enum>>((evt) => { CorrectFields(); } );
        _massRandom.RegisterCallback<ChangeEvent<Enum>>((evt) => { CorrectFields(); } );

        ResetAllVariables();
    }

    public void SaveSettings() {
        _asteroidSetting = rootVisualElement.Query<ObjectField>("AsteroidSettingField").First().value as AsteroidSetting;
        if (_asteroidSetting == null) return;
        
        EditorUtility.SetDirty(_asteroidSetting);
        
        CorrectFields();

        _asteroidSetting.SetVariables(AsteroidSettingType.Force, _forceFixed.value, _forceMin.value, _forceMax.value,_forceRandom.value.ToString());
        _asteroidSetting.SetVariables(AsteroidSettingType.Size, _sizeFixed.value, _sizeMin.value, _sizeMax.value, _sizeRandom.value.ToString());
        _asteroidSetting.SetVariables(AsteroidSettingType.Torque, _torqueFixed.value, _torqueMin.value, _torqueMax.value, _torqueRandom.value.ToString());
        _asteroidSetting.SetVariables(AsteroidSettingType.Mass, _massFixed.value, _massMin.value, _massMax.value, _massRandom.value.ToString());
       
    }

    private void CorrectFields() {
        if (_forceRandom == null) return;
        
        ToggleFields("Force", GetEnumFromString(_forceRandom.value.ToString()));
        ToggleFields("Size", GetEnumFromString(_sizeRandom.value.ToString()));
        ToggleFields("Torque", GetEnumFromString(_torqueRandom.value.ToString()));
        ToggleFields("Mass", GetEnumFromString(_massRandom.value.ToString()));
    }

    private RandomizedSetting GetEnumFromString(string enumName) {
        if (enumName == "BetweenTwoConstants") return RandomizedSetting.BetweenTwoConstants;
        else return RandomizedSetting.Fixed;
    }
    
    public void LoadSettings() { // todo: break out into methods
        _asteroidSetting = rootVisualElement.Query<ObjectField>("AsteroidSettingField").First().value as AsteroidSetting;
        if (_asteroidSetting == null) return;

        _forceRandom.value = _asteroidSetting.ForceVariables.RandomizedSetting;
        _forceFixed.value = _asteroidSetting.ForceVariables.Fixed;
        _forceMin.value = _asteroidSetting.ForceVariables.Min;
        _forceMax.value = _asteroidSetting.ForceVariables.Max;
        
        _sizeRandom.value = _asteroidSetting.SizeVariables.RandomizedSetting;
        _sizeFixed.value = _asteroidSetting.SizeVariables.Fixed;
        _sizeMin.value = _asteroidSetting.SizeVariables.Min;
        _sizeMax.value = _asteroidSetting.SizeVariables.Max;
        
        _torqueRandom.value = _asteroidSetting.TorqueVariables.RandomizedSetting;
        _torqueFixed.value = _asteroidSetting.TorqueVariables.Fixed;
        _torqueMin.value = _asteroidSetting.TorqueVariables.Min;
        _torqueMax.value = _asteroidSetting.TorqueVariables.Max;
        
        _massRandom.value = _asteroidSetting.MassVariables.RandomizedSetting;
        _massFixed.value = _asteroidSetting.MassVariables.Fixed;
        _massMin.value = _asteroidSetting.MassVariables.Min;
        _massMax.value = _asteroidSetting.MassVariables.Max;
        
        CorrectFields();
    }

    private void ToggleFields(string field, RandomizedSetting setting) {
        switch (field) {
            case "Force":
                if (setting == RandomizedSetting.Fixed) {
                    _forceMax.style.display = DisplayStyle.None;
                    _forceMin.style.display = DisplayStyle.None;
                    _forceFixed.style.display = DisplayStyle.Flex;
                    _forceFixedFiller.style.display = DisplayStyle.Flex;
                }
                else {
                    _forceMax.style.display = DisplayStyle.Flex;
                    _forceMin.style.display = DisplayStyle.Flex;
                    _forceFixed.style.display = DisplayStyle.None;
                    _forceFixedFiller.style.display = DisplayStyle.None;
                }
                break;
            case "Size":
                if (setting == RandomizedSetting.Fixed) {
                    _sizeMax.style.display = DisplayStyle.None;
                    _sizeMin.style.display = DisplayStyle.None;
                    _sizeFixed.style.display = DisplayStyle.Flex;
                    _sizeFixedFiller.style.display = DisplayStyle.Flex;
                }
                else {
                    _sizeMax.style.display = DisplayStyle.Flex;
                    _sizeMin.style.display = DisplayStyle.Flex;
                    _sizeFixed.style.display = DisplayStyle.None;
                    _sizeFixedFiller.style.display = DisplayStyle.None;
                }
                break;
            case "Torque":
                if (setting == RandomizedSetting.Fixed) {
                    _torqueMax.style.display = DisplayStyle.None;
                    _torqueMin.style.display = DisplayStyle.None;
                    _torqueFixed.style.display = DisplayStyle.Flex;
                    _torqueFixedFiller.style.display = DisplayStyle.Flex;
                }
                else {
                    _torqueMax.style.display = DisplayStyle.Flex;
                    _torqueMin.style.display = DisplayStyle.Flex;
                    _torqueFixed.style.display = DisplayStyle.None;
                    _torqueFixedFiller.style.display = DisplayStyle.None;
                }
                break;
            case "Mass":
                if (setting == RandomizedSetting.Fixed) {
                    _massMax.style.display = DisplayStyle.None;
                    _massMin.style.display = DisplayStyle.None;
                    _massFixed.style.display = DisplayStyle.Flex;
                    _massFixedFiller.style.display = DisplayStyle.Flex;
                }
                else {
                    _massMax.style.display = DisplayStyle.Flex;
                    _massMin.style.display = DisplayStyle.Flex;
                    _massFixed.style.display = DisplayStyle.None;
                    _massFixedFiller.style.display = DisplayStyle.None;
                }
                break;
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

    private void ResetAllVariables() { // break out into methods with overload 
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
        
        CorrectFields();
    }

     // private void OnSelectionChange() {
     //     Debug.Log();
     //     CorrectFields();
     // }

    // private void OnValidate() {
    //     CorrectFields();
    // }

}