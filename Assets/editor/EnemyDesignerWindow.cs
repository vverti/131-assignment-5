using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Types;

public class EnemyDesignerWindow : EditorWindow
{
    GUISkin skin;
    Texture2D headerSectionTexture;
    Texture2D mageSectionTexture; 
    Texture2D warriorSectionTexture; 
    Texture2D rogueSectionTexture;

    Color headerSectionColor = new Color(13f / 255f, 32f / 255f, 44f / 255f, 1f);
    Rect headerSection;
    Rect mageSection;
    Rect warriorSection;
    Rect rogueSection;

    static MageData mageData;
    static WarriorData warriorData;
    static RogueData rogueData;

    public static MageData MageInfo { get { return mageData; } }
    public static RogueData RogueInfo { get { return rogueData; } }
    public static WarriorData WarriorInfo { get { return warriorData; } }


    [MenuItem("Window/Enemy Designer")]   
   static void WindowOpen()
    {
        EnemyDesignerWindow window = (EnemyDesignerWindow)GetWindow(typeof(EnemyDesignerWindow));
        window.minSize = new Vector2(800, 400);
        window.Show();

    }
     void OnEnable()
    {
        InitTextures();
        InitData();
        skin = Resources.Load<GUISkin>("guiStyles/enemyDesignerSkin");
    }

    public static void InitData()
    {
        mageData = (MageData)ScriptableObject.CreateInstance(typeof(MageData));
        warriorData = (WarriorData)ScriptableObject.CreateInstance(typeof(WarriorData));
        rogueData = (RogueData)ScriptableObject.CreateInstance(typeof(RogueData));
    }
    void InitTextures()
    {
        headerSectionTexture = new Texture2D(1, 1);
        headerSectionTexture.SetPixel(0, 0, headerSectionColor);
        headerSectionTexture.Apply();
        warriorSectionTexture = Resources.Load<Texture2D>("icons/warrior");
        mageSectionTexture = Resources.Load<Texture2D>("icons/mage");
        rogueSectionTexture = Resources.Load<Texture2D>("icons/rogue");
    }
     void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawMageSettings();
        DrawRogueSettings();
        DrawWarriorSettings();
    }
    
    void DrawHeader()
    {
        GUILayout.BeginArea(headerSection);
        GUILayout.Label("Enemy designer", skin.GetStyle("header1"));
        GUILayout.EndArea();
    }
    void DrawLayouts()
    {
        headerSection.x = 0;
        headerSection.y = 0;
        headerSection.width = Screen.width;
        headerSection.height = 50;

        mageSection.x = 0;
        mageSection.y = 50;
        mageSection.width = Screen.width/3f;
        mageSection.height = Screen.height - 50;

        warriorSection.x = Screen.width / 3f;
        warriorSection.y = 50;
        warriorSection.width = Screen.width/3f;
        warriorSection.height = Screen.height - 50;

        rogueSection.x = Screen.width / 3f *2;
        rogueSection.y = 50;
        rogueSection.width = Screen.width/3f;
        rogueSection.height = Screen.height - 50;
        GUI.DrawTexture(headerSection, headerSectionTexture);
        GUI.DrawTexture(mageSection, mageSectionTexture);
        GUI.DrawTexture(warriorSection, warriorSectionTexture);
        GUI.DrawTexture(rogueSection, rogueSectionTexture);
    }
    void DrawWarriorSettings()
    {
        GUILayout.BeginArea(warriorSection);
        GUILayout.Label("Warrior", skin.GetStyle("header1"));
        GUILayout.BeginHorizontal();
        GUILayout.Label("Weapon", skin.GetStyle("header1"));
        warriorData.wpnType = (WarriorWpnType)EditorGUILayout.EnumPopup(warriorData.wpnType);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Class", skin.GetStyle("header1"));
        warriorData.classType = (WarriorClassType)EditorGUILayout.EnumPopup(warriorData.classType);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("Create", skin.GetStyle("header1")))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.WARRIOR);
        }
        GUILayout.EndArea();
    }
    void DrawRogueSettings()
    {
        GUILayout.BeginArea(rogueSection);
        GUILayout.Label("Rogue", skin.GetStyle("header1"));
        GUILayout.BeginHorizontal();
        GUILayout.Label("Strategy", skin.GetStyle("header1"));
        rogueData.strategyType = (RogueStrategyType)EditorGUILayout.EnumPopup(rogueData.strategyType);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Weapon", skin.GetStyle("header1"));
        rogueData.wpnType = (RogueWpnType)EditorGUILayout.EnumPopup(rogueData.wpnType);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("Create", skin.GetStyle("header1")))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.ROGUE);
        }
        GUILayout.EndArea();
    }
    void DrawMageSettings()
    {
        GUILayout.BeginArea(mageSection);
        GUILayout.Label("Mage", skin.GetStyle("header1"));
        GUILayout.BeginHorizontal();
        GUILayout.Label("Damage", skin.GetStyle("header1"));
        mageData.dmgType = (MageDmgType)EditorGUILayout.EnumPopup(mageData.dmgType);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Weapon", skin.GetStyle("header1"));
        mageData.wpnType = (MageWpnType)EditorGUILayout.EnumPopup(mageData.wpnType);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("Create", skin.GetStyle("header1")))
            {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.MAGE);
            }
        GUILayout.EndArea();
    }
}
public class GeneralSettings : EditorWindow
{
    GUISkin skin;

    public enum SettingsType
    {
        MAGE,
        WARRIOR,
        ROGUE
    }
    static SettingsType dataSetting;
    static GeneralSettings window;
    private void OnEnable()
    {
        skin = Resources.Load<GUISkin>("guiStyles/enemyDesignerSkin");
    }
    public static void OpenWindow(SettingsType setting)
    {
        dataSetting = setting;
        window = (GeneralSettings)GetWindow(typeof(GeneralSettings));
        window.minSize = new Vector2(800, 400);
        window.Show();

    }
    private void OnGUI()
    {
        switch(dataSetting)
        {
            case SettingsType.MAGE:
                DrawSettings((CharacterData)EnemyDesignerWindow.MageInfo);
                break;
            case SettingsType.ROGUE:
                DrawSettings((CharacterData)EnemyDesignerWindow.RogueInfo);
                break;
            case SettingsType.WARRIOR:
                DrawSettings((CharacterData)EnemyDesignerWindow.WarriorInfo);
                break;
        }
    }
    void DrawSettings(CharacterData charData)
    {

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Prefab", skin.GetStyle("header1"));
        charData.prefab = (GameObject)EditorGUILayout.ObjectField(charData.prefab,typeof(GameObject),false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Max Health", skin.GetStyle("header1"));       
        charData.maxHealth = EditorGUILayout.FloatField(charData.maxHealth);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Max Energy", skin.GetStyle("header1"));        
        charData.maxEnergy = EditorGUILayout.FloatField(charData.maxEnergy);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Power", skin.GetStyle("header1"));
        charData.power = EditorGUILayout.Slider(charData.power, 0, 100);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Crit Chance %", skin.GetStyle("header1"));
        charData.critChance = EditorGUILayout.Slider(charData.critChance, 0, charData.power);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name", skin.GetStyle("header1"));
        charData.name = EditorGUILayout.TextField(charData.name);
        EditorGUILayout.EndHorizontal();
        if (charData.prefab == null)
        {
            EditorGUILayout.HelpBox("This enemy needs a [Prefab] before it can be created", MessageType.Warning);
        }else
        if (charData.name == null ||  charData.name.Length < 4 || charData.name.Length > 15 )
        {
            EditorGUILayout.HelpBox("This enemy needs a valid [Name] before it can be created", MessageType.Warning);
        }
        else
        if (GUILayout.Button("Finish and save", skin.GetStyle("header1")))
        {
            SaveCharacterData();
            window.Close();
        }
    }
    void SaveCharacterData()
    {
        string prefabPath;
        string newPrefabPath = "Assets/prefabs/characters/";
        string datapath = "Assets/resources/characterData/data/";
        switch(dataSetting)
        {
            case SettingsType.MAGE:
                datapath += "mage/" + EnemyDesignerWindow.MageInfo.name + ".asset";
                AssetDatabase.CreateAsset(EnemyDesignerWindow.MageInfo, datapath);
                newPrefabPath += "mage/" + EnemyDesignerWindow.MageInfo.name + ".prefab";
                prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.MageInfo.prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                GameObject magePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                if (!magePrefab.GetComponent<Mage>())
                    magePrefab.AddComponent(typeof(Mage));
                magePrefab.GetComponent<Mage>().mageData = EnemyDesignerWindow.MageInfo;
                break;
            case SettingsType.ROGUE:
                datapath += "rogue/" + EnemyDesignerWindow.RogueInfo.name + ".asset";
                AssetDatabase.CreateAsset(EnemyDesignerWindow.RogueInfo, datapath);
                newPrefabPath += "rogue/" + EnemyDesignerWindow.RogueInfo.name + ".prefab";
                prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.RogueInfo.prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                GameObject roguePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                if (!roguePrefab.GetComponent<Rogue>())
                    roguePrefab.AddComponent(typeof(Rogue));
                roguePrefab.GetComponent<Rogue>().rogueData = EnemyDesignerWindow.RogueInfo;
                break;
            case SettingsType.WARRIOR:
                datapath += "warrior/" + EnemyDesignerWindow.WarriorInfo.name + ".asset";
                AssetDatabase.CreateAsset(EnemyDesignerWindow.WarriorInfo, datapath);
                newPrefabPath += "warrior/" + EnemyDesignerWindow.WarriorInfo.name + ".prefab";
                prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.WarriorInfo.prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                GameObject warriorPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                if (!warriorPrefab.GetComponent<Warrior>())
                    warriorPrefab.AddComponent(typeof(Warrior));
                warriorPrefab.GetComponent<Warrior>().warriorData = EnemyDesignerWindow.WarriorInfo;
                break;
        }
    }
}
