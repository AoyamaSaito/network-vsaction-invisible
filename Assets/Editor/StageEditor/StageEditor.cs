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
                GUILayout.Label("�X�e�[�W�쐬");
            }

            GUILayout.TextArea("Assets\\Editor\\StageEditor\\CreateScene\\NewScene.unity");

            GUI.backgroundColor = defaultColor;
            using (new GUILayout.HorizontalScope(GUI.skin.box))
            {
                GUI.backgroundColor = Color.gray;
                
                if (GUILayout.Button("�쐬�p�̃V�[�����J��"))
                {
                    Create();
                }
                if (GUILayout.Button("�X�e�[�W��ۑ�����"))
                {
                    Save();
                }
            }
        }
    }

    private GameObject _stage;
    private void Create()
    {
        //��Ɨp�̃V�[�����쐬
        var newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);
        newScene.name = "NewScene";
        EditorSceneManager.SaveScene(newScene, "Assets\\Editor\\StageEditor\\CreateScene\\NewScene.unity", true);
        //Stage���쐬
        _stage = AssetDatabase.LoadAssetAtPath<GameObject>("Assets\\Editor\\StageEditor\\StagePlane\\StagePlane.prefab");
        UnityEditor.PrefabUtility.InstantiatePrefab(_stage);
    }

    private void Save()
    {
        if (SceneManager.GetActiveScene().name != "NewScene") return;

        Debug.Log("�ۑ����܂���");
    }

    private void AddStage()
    {
        //StagePool�ɒǉ�����
    }
}
