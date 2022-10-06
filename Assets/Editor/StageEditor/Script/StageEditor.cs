using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

public class StageEditor : EditorWindow
{
    [MenuItem("Editor/StageEditor")]
    private static void WindowCreate()
    {
        // 生成
        StageEditor window = GetWindow<StageEditor>("StageEditor");
        // 最小サイズ設定
        window.minSize = new Vector2(360, 80);
        window.maxSize = new Vector2(600, 80);
    }

    string _name = "Stage";
    private void OnGUI()
    {
        Color defaultColor = GUI.backgroundColor;
        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            GUI.backgroundColor = Color.white;

            GUILayout.TextArea("Assets\\Editor\\StageEditor\\CreateScene\\NewScene.unity");

            GUI.backgroundColor = defaultColor;

            GUI.backgroundColor = Color.gray;

            if (GUILayout.Button("作成する"))
            {
                Create();
            }

            using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                GUI.backgroundColor = Color.gray;
                _name = GUILayout.TextField(_name);
            }
            if (GUILayout.Button("ステージを保存する"))
            {
                Save(_name);
            }
        }
    }

    GameObject _currentstage;
    /// <summary>
    /// 作業用のシーンと、ステージのプレーンを作る
    /// </summary>
    private void Create()
    {
        //作業用のシーンを作成
        var newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);
        newScene.name = "NewScene";
        EditorSceneManager.SaveScene(newScene, "Assets\\Editor\\StageEditor\\CreateScene\\NewScene.unity", true);
        //Stageを作成
        GameObject stage = AssetDatabase.LoadAssetAtPath<GameObject>("Assets\\Editor\\StageEditor\\StagePlane\\StagePlane.prefab");
        _currentstage = (GameObject)PrefabUtility.InstantiatePrefab(stage);
    }

    /// <summary>
    /// 現在作業中のステージをプレハブ化する
    /// </summary>
    /// <param name="name"></param>
    private void Save(string name)
    {
        _currentstage.gameObject.name = name;
        PrefabUtility.SaveAsPrefabAsset(_currentstage, $"Assets\\Prefab\\Stage\\{name}.prefab");
        PrefabUtility.UnloadPrefabContents(_currentstage);
    }

    private void AddStage()
    {
        //StagePoolに追加する
    }
}
