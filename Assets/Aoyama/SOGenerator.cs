using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SOGenerator : MonoBehaviour
{
    [MenuItem("MyGenerator/CreateScriptableObject")]
    private static void CreateScriptableObject()
    {
        var obj = ScriptableObject.CreateInstance<MyScriptableObject>();
        obj.hogeValue = "hogehoge";
        var path = "Assets/MyFiles";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        AssetDatabase.CreateAsset(obj, Path.Combine(path, fileName));
    }
}
