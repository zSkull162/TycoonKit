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
        GUIStyle richText = new GUIStyle(GUI.skin.label);
        GUIStyle richTextCentered = new GUIStyle(GUI.skin.label);
        richText.richText = true;
        richTextCentered.richText = true;
        richTextCentered.alignment = TextAnchor.UpperCenter;

        serializedObject.Update();

        EditorGUILayout.LabelField($"<size=14><b><color={InspectorUtils.Color(ThemeColor.Col2)}>----------------- Upgrader -----------------</color></b></size>", richTextCentered);
        EditorGUILayout.Space(1);

        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.LabelField($"<size=13><b><color={InspectorUtils.Color(ThemeColor.Col3)}>Options</color></b></size>", richText);
        EditorGUILayout.PropertyField(type);
        EditorGUILayout.PropertyField(upgradeAmount);
        EditorGUILayout.Space(2);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(4);
        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.LabelField($"<size=13><b><color={InspectorUtils.Color(ThemeColor.Col4)}>Other</color></b></size>", richText);
        EditorGUILayout.PropertyField(displayText);
        EditorGUILayout.PropertyField(upgradeParticles);
        EditorGUILayout.Space(2);
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif