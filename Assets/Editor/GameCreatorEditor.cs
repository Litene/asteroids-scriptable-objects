using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Variables;

public class GameCreatorEditor : EditorWindow { // todo: fix asteroidSetting
    [SerializeField] private VisualTreeAsset UXMLFile;

    private GameSettings _gameSettings;
    private AsteroidSetting _asteroidSetting;


    [MenuItem("Tools/Game Editor")]
    public static void ShowWindow() {
        GameCreatorEditor window = GetWindow<GameCreatorEditor>();
        window.titleContent = new GUIContent("Game Editor");
    }

    private void OnEnable() {

        /*_maxHealth ??= Resources.Load("Health") as IntVariable;
        */
        
        _gameSettings ??= Resources.Load("GameSettingsFile") as GameSettings;
        var settingsObject = new SerializedObject(_gameSettings);
        rootVisualElement.Bind(settingsObject);

       /* var maxHealthObject = new SerializedObject(_maxHealth);
        var shipMassObject = new SerializedObject(_shipMass);*/
        // rootVisualElement.Query<Button>("Save").First().clicked += SaveSettings;
        

        //rootVisualElement.(x => x.viewDataKey == "ButtonVisualElements").Q<Button>("Save").clicked += SaveSettings;
        
        // rootVisualElement.Query<Button>("Load").First().clicked += LoadSettings;
        // rootVisualElement.Query<Button>("New").First().clicked += NewSettings;
        // rootVisualElement.Query<Button>("Generate").First().clicked += GenerateSettings;
    }

    private void CreateGUI() {
        UXMLFile.CloneTree(rootVisualElement); // bind everything here?
     //   InitializeFields();
    }

    private void InitializeFields() {
        //rootVisualElement.Q<Slider>("ThrottlePower").value = _throttlePower.Value;
    }

    private void SetValues() {
        //_throttlePower.SetValue(rootVisualElement.Q<Slider>("ThrottlePower").value);
    }
    
    
    
    public void  SaveSettings() {
        Debug.Log("Yo");
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
       // rootVisualElement.Q<ObjectField>("AsteroidSettingField").value = asteroidSetting; //  why is this a asteroidVariable?
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