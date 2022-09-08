using System;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

class WeekdaysDropdown : AdvancedDropdown
{
    public event Action<AdvancedDropdownItem> OnSelect;

    public WeekdaysDropdown(AdvancedDropdownState state) : base(state)
    {
        Unsupported.GetSubmenus("Component");
    }

    protected override AdvancedDropdownItem BuildRoot()
    {
        var root = new AdvancedDropdownItem("Weekdays");

        var firstHalf = new AdvancedDropdownItem("First half");
        var secondHalf = new AdvancedDropdownItem("Second half");
        var weekend = new AdvancedDropdownItem("Weekend");

        firstHalf.AddChild(new AdvancedDropdownItem("Monday"));
        firstHalf.AddChild(new AdvancedDropdownItem("Tuesday"));
        secondHalf.AddChild(new AdvancedDropdownItem("Wednesday"));
        secondHalf.AddChild(new AdvancedDropdownItem("Thursday"));
        weekend.AddChild(new AdvancedDropdownItem("Friday"));
        weekend.AddChild(new AdvancedDropdownItem("Saturday"));
        weekend.AddChild(new AdvancedDropdownItem("Sunday"));

        root.AddChild(firstHalf);
        root.AddChild(secondHalf);
        root.AddChild(weekend);

        return root;
    }

    protected override void ItemSelected(AdvancedDropdownItem item)
    {
        OnSelect?.Invoke(item);
    }
}

public class AdvancedDropdownTestWindow : EditorWindow
{
    [MenuItem("Tools/Hoge")]
    private static void Open()
    {
        GetWindow<AdvancedDropdownTestWindow>();
    }

    void OnGUI()
    {
        var rect = GUILayoutUtility.GetRect(new GUIContent("Show"), EditorStyles.toolbarButton);
        if (GUI.Button(rect, new GUIContent("Show"), EditorStyles.toolbarButton))
        {
            var dropdown = new WeekdaysDropdown(new AdvancedDropdownState());
            dropdown.OnSelect += item => Debug.Log(item.name);
            dropdown.Show(rect);
        }
    }
}