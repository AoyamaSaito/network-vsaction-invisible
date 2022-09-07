using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ButtonWindow : ComponentWindow<Transform>
{
    [MenuItem("Editor/ButtonWindow")]
    private static void Create()
    {
        ButtonWindow window = GetWindow<ButtonWindow>("ButtonWindow");
    }
}
