using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Create()
    {

    }

    private void Save()
    {

    }
}
