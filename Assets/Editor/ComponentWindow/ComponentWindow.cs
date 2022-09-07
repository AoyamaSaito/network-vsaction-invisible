using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 特定のコンポーネントを一覧表示するエディター拡張
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
    /// DataをWindowに表示する
    /// </summary>
    private void UpdateLayoutData()
    {
        using (GUILayout.ScrollViewScope scroll = new GUILayout.ScrollViewScope(_dataScrollPosition, EditorStyles.helpBox, GUILayout.Width(150)))
        {
            if (GUILayout.Button("更新", GUILayout.Width(80)))
            {
                SearchButton();
            }

            _dataScrollPosition = scroll.scrollPosition;
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
    /// ParamatorをWindowに表示する
    /// </summary>
    private void UpdateLayoutParameter()
    {
        using (GUILayout.ScrollViewScope scroll = new GUILayout.ScrollViewScope(_parameterScrollPosition, EditorStyles.helpBox))
        {
            _parameterScrollPosition = scroll.scrollPosition;

            if (_component)
            {
                Editor.CreateEditor(_component).DrawDefaultInspector();
            }
        }
    }

    private void SearchButton()
    {
        _components = MyGameObjectUtility.GetComponentsInActiveScene<T>();
    }

    private void OpenInspector(T button)
    {
        Selection.activeGameObject = button.gameObject;
    }
}
