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
        window.minSize = new Vector2(320, 320);
    }

    private void OnGUI()
    {
        using (new GUILayout.HorizontalScope(GUI.skin.box))
        {
            if (GUILayout.Button("作成"))
            {
                Create();
            }
            if (GUILayout.Button("保存"))
            {
                Save();
            }
        }
    }

    private void Create()
    {
        //作成用のシーンを作成
        var newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);
        newScene.name = "NewScene";
        EditorSceneManager.SaveScene(newScene, "Assets\\Editor\\StageEditor\\CreateScene\\NewScene.unity", true);
    }

    private void Save()
    {
        if (SceneManager.GetActiveScene().name != "NewScene") return;

        Debug.Log("保存しました");

    }
}
