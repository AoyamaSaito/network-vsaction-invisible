using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SOGenerator : EditorWindow
{
#if UNITY_EDITOR

    [MenuItem("Generator/CreateScriptableObject")]
    private static void Create()
    {
        // 生成
        SOGenerator window = GetWindow<SOGenerator>("WaveData");
        // 最小サイズ設定
        window.minSize = new Vector2(320, 320);
    }

    [SerializeField, Tooltip("Waveが入ったフォルダのパス")]
    private string _wavePath = "Assets\\Resorces\\Wave";
    [SerializeField, Tooltip("Waveが入ったDataを保存するフォルダのパス")]
    private string _savePath = "Assets\\Data";

    private void FindEventsAsset()
    {
        var obj = ScriptableObject.CreateInstance<WaveData>();
        obj.Waves = new List<WaveBase>();
        var fileNames = Directory.GetFiles(_wavePath, "*", SearchOption.AllDirectories);
        Debug.Log("開始");

        foreach (var fileName in fileNames)
        {
            Debug.Log(fileName);
            Debug.Log(fileName.Substring(fileName.Length - 6));
            var asset = AssetDatabase.LoadAssetAtPath<WaveBase>(fileName);
            if (asset != null && fileName.Substring(fileName.Length - 6) == "prefabs")
            {
                Debug.Log(asset);
                obj.Waves.Add(asset);
            }
        }

        if (!AssetDatabase.Contains(obj as UnityEngine.Object))
        {
            string directory = Path.GetDirectoryName(_wavePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            // アセット作成
            AssetDatabase.CreateAsset(obj, _wavePath);
        }

        // 保存
        AssetDatabase.SaveAssets();
        // エディタを最新の状態にする
        AssetDatabase.Refresh();
    }

    private void OnGUI()
    {
        using (new GUILayout.VerticalScope(GUI.skin.box))
        {
            GUILayout.Label("パス");
            _wavePath = EditorGUILayout.TextField("Waveが入ったフォルダのパス", _wavePath);
            _savePath = EditorGUILayout.TextField("保存するフォルダのパス", _savePath);
            // 読み込みボタン
            if (GUILayout.Button("作成＆上書き"))
            {
                FindEventsAsset();
            }
        }
    }
#endif
}
