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
        // ê∂ê¨
        StageEditor window = GetWindow<StageEditor>("StageEditor");
        // ç≈è¨ÉTÉCÉYê›íË
        window.minSize = new Vector2(320, 320);
    }

    private void Create()
    {

    }

    private void Save()
    {

    }
}
