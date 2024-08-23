#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;
using zSkull162.TycoonKit;

[CustomEditor(typeof(MoneyManager)), CanEditMultipleObjects]
public class MoneyManagerEditor : Editor
{
    #region Get Serialized Properties
    SerializedProperty displaytext;
    SerializedProperty hudText;
    SerializedProperty startingMoney;

    private void OnEnable()
    {
        displaytext = serializedObject.FindProperty("displayText");
        hudText = serializedObject.FindProperty("hudText");
        startingMoney = serializedObject.FindProperty("startingMoney");
    }
    #endregion

    public override void OnInspectorGUI()
    {
        GUIStyle helpBox = new GUIStyle(EditorStyles.helpBox);
        GUIStyle richTextCentered = new GUIStyle(GUI.skin.label);
        richTextCentered.richText = true;
        richTextCentered.alignment = TextAnchor.UpperCenter;

        serializedObject.Update();

        InspectorUtils.TitleLabel(ThemeColor.Col3, "Money Manager", true);
        EditorGUILayout.Space(1);

        EditorGUILayout.BeginVertical(helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col4, "Text");
        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.PropertyField(displaytext);
        EditorGUILayout.PropertyField(hudText);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col5, "Options");
        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.PropertyField(startingMoney);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif