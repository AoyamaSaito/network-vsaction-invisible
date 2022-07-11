//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityEditor;
//using UnityEngine;

//public class SOGenerator : EditorWindow
//{
//#if UNITY_EDITOR

//    [MenuItem("Generator/CreateScriptableObject")]
//    private static void Create()
//    {
//        // ����
//        SOGenerator window = GetWindow<SOGenerator>("WaveData");
//        // �ŏ��T�C�Y�ݒ�
//        window.minSize = new Vector2(320, 320);
//    }

//    [SerializeField, Tooltip("Wave���������t�H���_�̃p�X")]
//    private string _wavePath = "Assets\\Prefabs\\Wave";
//    [SerializeField, Tooltip("Wave��������Data��ۑ�����t�H���_�̃p�X")]
//    private string _savePath = "Assets\\Data";

//    private WaveData _obj;

//    private void FindEventsAsset()
//    {
//        var obj = ScriptableObject.CreateInstance<WaveData>();
//        obj.Waves = new List<WaveBase>();
//        var fileNames = Directory.GetFiles(_wavePath, "*", SearchOption.AllDirectories);
//        Debug.Log("�J�n");

//        foreach (var fileName in fileNames)
//        {
//            Debug.Log(fileName);
//            Debug.Log(fileName.Substring(fileName.Length - 6));
//            var asset = AssetDatabase.LoadAssetAtPath<WaveBase>(fileName);
//            if (asset != null && !fileName.Contains("meta"))
//            {
//                Debug.Log(asset);
//                obj.Waves.Add(asset);
//            }
//        }

//        if (AssetDatabase.LoadAssetAtPath<WaveData>(_savePath) == null)
//        {
//            string directory = Path.GetDirectoryName(_savePath);
//            if (!Directory.Exists(directory))
//            {
//                Directory.CreateDirectory(directory);
//            }
//            // �A�Z�b�g�쐬
//            AssetDatabase.CreateAsset(obj, _savePath);
//        }

//        // �R�s�[
//        //sample.Copy(_sample);
//        EditorUtility.CopySerialized(_obj, obj);
//        // ���ڕҏW�ł��Ȃ��悤�ɂ���
//        obj.hideFlags = HideFlags.NotEditable;
//        // �X�V�ʒm
//        EditorUtility.SetDirty(obj);
//        // �ۑ�
//        AssetDatabase.SaveAssets();
//        // �G�f�B�^���ŐV�̏�Ԃɂ���
//        AssetDatabase.Refresh();
//    }

//    private void OnGUI()
//    {
//        using (new GUILayout.VerticalScope(GUI.skin.box))
//        {
//            GUILayout.Label("�p�X");
//            _wavePath = EditorGUILayout.TextField("Wave���������t�H���_�̃p�X", _wavePath);
//            _savePath = EditorGUILayout.TextField("�ۑ�����t�H���_�̃p�X", _savePath);
//            // �ǂݍ��݃{�^��
//            if (GUILayout.Button("�쐬���㏑��"))
//            {
//                FindEventsAsset();
//            }
//        }
//    }
//#endif
//}
