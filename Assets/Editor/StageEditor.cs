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
        // ����
        StageEditor window = GetWindow<StageEditor>("StageEditor");
        // �ŏ��T�C�Y�ݒ�
        window.minSize = new Vector2(320, 320);
    }

    private void OnGUI()
    {
        using (new GUILayout.HorizontalScope(GUI.skin.box))
        {
            if (GUILayout.Button("�쐬"))
            {
                Create();
            }
            if (GUILayout.Button("�ۑ�"))
            {
                Save();
            }
        }
    }

    private void Create()
    {
        //�쐬�p�̃V�[�����쐬
        var newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);
        newScene.name = "NewScene";
        EditorSceneManager.SaveScene(newScene, "Assets\\Editor\\StageEditor\\CreateScene\\NewScene.unity", true);
    }

    private void Save()
    {
        if (SceneManager.GetActiveScene().name != "NewScene") return;

        Debug.Log("�ۑ����܂���");

    }
}
