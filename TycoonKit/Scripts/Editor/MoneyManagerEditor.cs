#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;
using zSkull162.TycoonKit;

[CustomEditor(typeof(MoneyManager)), CanEditMultipleObjects]
public class MoneyManagerEditor : Editor
{
    #region Get Serialized Properties
    SerializedProperty displaytext;
    SerializedProperty startingMoney;

    private void OnEnable()
    {
        displaytext = serializedObject.FindProperty("displayText");
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

        EditorGUILayout.LabelField($"<size=14><b><color={InspectorUtils.Color(ThemeColor.Col3)}>----------------- Money Manager -----------------</color></b></size>", richTextCentered);
        EditorGUILayout.Space(1);

        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.Space(2);
        EditorGUILayout.PropertyField(displaytext);
        EditorGUILayout.Space(2);
        EditorGUILayout.PropertyField(startingMoney);
        EditorGUILayout.Space(2);
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif