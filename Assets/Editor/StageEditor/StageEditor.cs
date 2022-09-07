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
        window.minSize = new Vector2(640, 640);
    }

    private void OnGUI()
    {
        Color defaultColor = GUI.backgroundColor;
        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            GUI.backgroundColor = Color.gray;
            using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                GUILayout.Label("ステージ作成");
            }

            GUILayout.TextArea("Assets\\Editor\\StageEditor\\CreateScene\\NewScene.unity");

            GUI.backgroundColor = defaultColor;
            using (new GUILayout.HorizontalScope(GUI.skin.box))
            {
                GUI.backgroundColor = Color.gray;
                
                if (GUILayout.Button("作成用のシーンを開く"))
                {
                    Create();
                }
                if (GUILayout.Button("ステージを保存する"))
                {
                    Save();
                }
            }
        }
    }

    private GameObject _stage;
    private void Create()
    {
        //作業用のシーンを作成
        var newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);
        newScene.name = "NewScene";
        EditorSceneManager.SaveScene(newScene, "Assets\\Editor\\StageEditor\\CreateScene\\NewScene.unity", true);
        //Stageを作成
        _stage = AssetDatabase.LoadAssetAtPath<GameObject>("Assets\\Editor\\StageEditor\\StagePlane\\StagePlane.prefab");
        UnityEditor.PrefabUtility.InstantiatePrefab(_stage);
    }

    private void Save()
    {
        if (SceneManager.GetActiveScene().name != "NewScene") return;

        Debug.Log("保存しました");
    }

    private void AddStage()
    {
        //StagePoolに追加する
    }
}
