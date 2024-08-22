#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;
using zSkull162.TycoonKit;

[CustomEditor(typeof(Upgrader)), CanEditMultipleObjects]
public class UpgraderEditor : Editor
{
    #region Get Serialized Properties
    SerializedProperty type;
    SerializedProperty upgradeAmount;
    SerializedProperty displayText;
    SerializedProperty upgradeParticles;

    private void OnEnable()
    {
        type = serializedObject.FindProperty("type");
        upgradeAmount = serializedObject.FindProperty("upgradeAmount");
        displayText = serializedObject.FindProperty("displayText");
        upgradeParticles = serializedObject.FindProperty("upgradeParticles");
    }
    #endregion

    public override void OnInspectorGUI()
    {
        GUIStyle helpBox = new GUIStyle(EditorStyles.helpBox);

        serializedObject.Update();

        InspectorUtils.TitleLabel(ThemeColor.Col2, "Upgrader", true);
        EditorGUILayout.Space(1);

        EditorGUILayout.BeginVertical(helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col3, "Options");
        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.PropertyField(type);
        EditorGUILayout.PropertyField(upgradeAmount);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(4);
        EditorGUILayout.BeginVertical(helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col4, "Other");
        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.PropertyField(displayText);
        EditorGUILayout.PropertyField(upgradeParticles);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif