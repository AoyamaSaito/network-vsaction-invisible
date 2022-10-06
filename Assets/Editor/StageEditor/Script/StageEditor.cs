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

            if (GUILayout.Button("�쐬����"))
            {
                Create();
            }

            using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                GUI.backgroundColor = Color.gray;
                _name = GUILayout.TextField(_name);
            }
            if (GUILayout.Button("�X�e�[�W��ۑ�����"))
            {
                Save(_name);
            }
        }
    }

    GameObject _currentstage;
    /// <summary>
    /// ��Ɨp�̃V�[���ƁA�X�e�[�W�̃v���[�������
    /// </summary>
    private void Create()
    {
        //��Ɨp�̃V�[�����쐬
        var newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);
        newScene.name = "NewScene";
        EditorSceneManager.SaveScene(newScene, "Assets\\Editor\\StageEditor\\CreateScene\\NewScene.unity", true);
        //Stage���쐬
        GameObject stage = AssetDatabase.LoadAssetAtPath<GameObject>("Assets\\Editor\\StageEditor\\StagePlane\\StagePlane.prefab");
        _currentstage = (GameObject)PrefabUtility.InstantiatePrefab(stage);
    }

    /// <summary>
    /// ���ݍ�ƒ��̃X�e�[�W���v���n�u������
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
        //StagePool�ɒǉ�����
    }
}
