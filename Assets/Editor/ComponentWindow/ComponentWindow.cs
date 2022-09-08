using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 特定のコンポーネントを一覧表示するWindow
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
            //Windowを更新する
            if (GUILayout.Button("更新", GUILayout.Width(80)))
            {
                SearchButton();
            }

            _dataScrollPosition = scroll.scrollPosition;

            //そのコンポーネントがなかった時は処理を止める
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
    /// HierarchyにあるTを探す
    /// </summary>
    private void SearchButton()
    {
        _components = MyGameObjectUtility.GetComponentsInActiveScene<T>();
    }

    /// <summary>
    /// 選択したコンポーネントをInspectorで開く
    /// </summary>
    /// <param name="button"></param>
    private void OpenInspector(T component)
    {
        Selection.activeGameObject = component.gameObject;
    }
}
