using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonWindow : ComponentWindow<Button>
{
    [MenuItem("Editor/ButtonWindow")]
    private static void Create()
    {
        ButtonWindow window = GetWindow<ButtonWindow>("ComponentWindow");
    }
}
