#if UNITY_EDITOR && !COMPILER_UDONSHARP
using System.Linq;
using UnityEditor;
using UnityEngine;

public enum ThemeColor
{
    Col1,
    Col2,
    Col3,
    Col4,
    Col5
}

public static class InspectorUtils
{
    // public static string[] colorList = { "teal", "#38c194", "#31bdb6", "#2c99bc", "#3775bf" };    (old code from before the theme picker)
    private static ThemeScriptableObject themeContainer;

    public static ThemeScriptableObject GetThemeSO()
    {
        // Thanks to LiveDimension for providing this Scriptable Object loading code,
        // and for some help getting it all to work correctly.

        if (themeContainer == null)
        {
            var guid = AssetDatabase.FindAssets($"ThemeContainer t:{nameof(ThemeScriptableObject)}").First();
            ThemeScriptableObject themeSO = AssetDatabase.LoadAssetAtPath<ThemeScriptableObject>(AssetDatabase.GUIDToAssetPath(guid));

            themeContainer = themeSO;
            return themeSO;
        }
        else
        {
            return themeContainer;
        }
    }

    public static string Color(ThemeColor color)
    {
        if (themeContainer == null) themeContainer = GetThemeSO();
        string[] colorList = themeContainer.storedTheme;
        string target = "";

        if (colorList == null || colorList.Length == 0) colorList = new string[] { "teal", "#38c194", "#31bdb6", "#2c99bc", "#3775bf" };

        switch (color)
        {
            case ThemeColor.Col1:
                target = colorList[0];
                break;
            case ThemeColor.Col2:
                target = colorList[1];
                break;
            case ThemeColor.Col3:
                target = colorList[2];
                break;
            case ThemeColor.Col4:
                target = colorList[3];
                break;
            case ThemeColor.Col5:
                target = colorList[4];
                break;
        }

        if (target == "")
        {
            Debug.Log($"<color=red>Target was empty.</color>");
            return null;
        }
        return target;
    }

    public static void TitleLabel(ThemeColor color, string title, bool useBorder)
    {
        GUIStyle richTextCentered = new GUIStyle(GUI.skin.label);
        richTextCentered.richText = true;
        richTextCentered.alignment = TextAnchor.UpperCenter;
        if (useBorder)
        {
            EditorGUILayout.LabelField($"<size=14><b><color={Color(color)}>----------------- {title} -----------------</color></b></size>", richTextCentered);
            EditorGUILayout.Space(1);
        }
        else
        {
            EditorGUILayout.LabelField($"<size=14><b><color={Color(color)}>{title}</color></b></size>", richTextCentered);
            EditorGUILayout.Space(1);
        }
    }

    public static void SectionLabel(ThemeColor color, string title)
    {
        GUIStyle richText = new GUIStyle(GUI.skin.label);
        richText.richText = true;
        EditorGUILayout.LabelField($"<size=13><b><color={Color(color)}>{title}</color></b></size>", richText);
    }

    public static void Description(string body)
    {
        GUIStyle description = new GUIStyle(GUI.skin.label);
        description.richText = true;
        description.wordWrap = true;

        EditorGUILayout.LabelField($"<size=11>{body}</size>", description);
    }
    public static void Description(string color, string body)
    {
        GUIStyle description = new GUIStyle(GUI.skin.label);
        description.richText = true;
        description.wordWrap = true;

        EditorGUILayout.LabelField($"<size=11><color={color}>{body}</color></size>", description);
    }
    public static void Description(ThemeColor color, string body)
    {
        GUIStyle description = new GUIStyle(GUI.skin.label);
        description.richText = true;
        description.wordWrap = true;

        EditorGUILayout.LabelField($"<size=11><color={Color(color)}>{body}</color></size>", description);
    }

    public static GameObject FindObjectByName(string name)
    {
        GameObject _target;

        GameObject[] objs = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in objs)
        {
            if (obj.name == name)
            {
                _target = obj;
                Debug.Log($"<color=lime><b>Returning {obj}</b></color>");
                return _target;
            }
        }
        Debug.Log($"<color=red>obj was null</color>");
        return null;
    }
}
#endif