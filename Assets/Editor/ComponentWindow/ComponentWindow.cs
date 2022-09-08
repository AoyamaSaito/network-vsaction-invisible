using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����̃R���|�[�l���g���ꗗ�\������Window
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class ComponentWindow<T> : EditorWindow where T : Component
{
    private Vector2 _dataScrollPosition;
    private Vector2 _parameterScrollPosition;

    private T[] _components;
    private T _component;

    private void OnGUI()
    {
        using (new GUILayout.HorizontalScope())
        {
            UpdateLayoutData();
            UpdateLayoutParameter();
        }
    }

    /// <summary>
    /// Data��Window�ɕ\������
    /// </summary>
    private void UpdateLayoutData()
    {
        using (GUILayout.ScrollViewScope scroll = new GUILayout.ScrollViewScope(_dataScrollPosition, EditorStyles.helpBox, GUILayout.Width(150)))
        {
            //Window���X�V����
            if (GUILayout.Button("�X�V", GUILayout.Width(80)))
            {
                SearchButton();
            }

            _dataScrollPosition = scroll.scrollPosition;

            //���̃R���|�[�l���g���Ȃ��������͏������~�߂�
            if (_components == null) return;

            GenericMenu menu = new GenericMenu();
            for (int i = 0; i < _components.Length; i++)
            {
                GUI.backgroundColor = (_components[i] == _component ? Color.cyan : Color.white);
                if (GUILayout.Button(_components[i].gameObject.name.ToString()))
                {
                    _component = _components[i];
                    OpenInspector(_component);
                }
                GUI.backgroundColor = Color.white;
            }

            if (menu.GetItemCount() > 0)
            {
                menu.ShowAsContext();
                Event.current.Use();
            }

            GUI.backgroundColor = Color.gray;
        }
    }

    /// <summary>
    /// Paramator��Window�ɕ\������
    /// </summary>
    private void UpdateLayoutParameter()
    {
        using (GUILayout.ScrollViewScope scroll = new GUILayout.ScrollViewScope(_parameterScrollPosition, EditorStyles.helpBox))
        {
            using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                GUILayout.Label($"{typeof(T).Name}");
            }
            _parameterScrollPosition = scroll.scrollPosition;

            if (_component)
            {
                Editor.CreateEditor(_component).DrawDefaultInspector();
            }
        }

        
    }

    /// <summary>
    /// Hierarchy�ɂ���T��T��
    /// </summary>
    private void SearchButton()
    {
        _components = MyGameObjectUtility.GetComponentsInActiveScene<T>();
    }

    /// <summary>
    /// �I�������R���|�[�l���g��Inspector�ŊJ��
    /// </summary>
    /// <param name="button"></param>
    private void OpenInspector(T component)
    {
        Selection.activeGameObject = component.gameObject;
    }
}
