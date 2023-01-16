using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Variables;

public class GameCreatorEditor : EditorWindow { // todo: fix asteroidSetting
    [SerializeField] private VisualTreeAsset UXMLFile;

    private FloatVariable _throttlePower; // not set..
    private FloatVariable _rotationPower; // not set..
    private IntVariable _maxHealth; // 
    private FloatVariable _shipMass;
    private AsteroidSetting _asteroidSetting;


    [MenuItem("Tools/Game Editor")]
    public static void ShowWindow() {
        GameCreatorEditor window = GetWindow<GameCreatorEditor>();
        window.titleContent = new GUIContent("Game Editor");
    }

    private void OnEnable() {
        _throttlePower ??= Resources.Load("ThrottlePower") as FloatVariable;
        _rotationPower ??= Resources.Load("RotationPower") as FloatVariable;
        _maxHealth ??= Resources.Load("Health") as IntVariable;
        _shipMass ??= Resources.Load("ShipMass") as FloatVariable;
        
        var throttleObject = new SerializedObject(_throttlePower);
        var rotationObject = new SerializedObject(_rotationPower);
        var maxHealthObject = new SerializedObject(_maxHealth);
        var shipMassObject = new SerializedObject(_shipMass);
        rootVisualElement.Bind(throttleObject);
        rootVisualElement.Bind(rotationObject);
        rootVisualElement.Bind(maxHealthObject);
        rootVisualElement.Bind(shipMassObject);
        
        rootVisualElement.Query<ToolbarButton>("Save").First().clicked += SaveSettings;
        rootVisualElement.Query<ToolbarButton>("Load").First().clicked += LoadSettings;
        rootVisualElement.Query<ToolbarButton>("New").First().clicked += NewSettings;
        rootVisualElement.Query<ToolbarButton>("Generate").First().clicked += GenerateSettings;
    }

    private void CreateGUI() {
        UXMLFile.CloneTree(rootVisualElement); // bind everything here?
    }

    public void  SaveSettings() {
        
    }

    public void LoadSettings() {
       
    }

    public void NewSettings() {
        var asteroidSetting = ScriptableObject.CreateInstance<AsteroidSetting>();
        AssetDatabase.CreateAsset(asteroidSetting, $"Assets/Resources/");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asteroidSetting;
        rootVisualElement.Q<ObjectField>("AsteroidSettingField").value = asteroidSetting; //  why is this a asteroidVariable?
    }

    private void ResetAllVariables() {
        // call its emtpy or new has been clicked. 
    }

    public void GenerateSettings() {
        
    }
    
    

    private void OnSelectionChange() {
        //_asteroidSetting = rootVisualElement.Q<ObjectField>("");
    }
}