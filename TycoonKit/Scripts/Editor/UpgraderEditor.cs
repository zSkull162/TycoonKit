#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;
using zSkull162.TycoonKit;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators;
using PlasticGui.WorkspaceWindow.Home.Workspaces;

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
        Upgrader _script = (Upgrader)target;
        if (_script == null) return;

        GUIStyle helpBox = new GUIStyle(EditorStyles.helpBox);
        GUIStyle foldoutStyle = EditorStyles.foldout;
        foldoutStyle.fontStyle = FontStyle.Bold;

        serializedObject.Update();

        InspectorUtils.TitleLabel(ThemeColor.Col1, "Editor", true);
        EditorGUILayout.Space(1);
        EditorGUILayout.BeginVertical(helpBox);
        _script.editorOptions = GUILayout.Toggle(_script.editorOptions, " Editor options", foldoutStyle);
        if (_script.editorOptions)
        {
            EditorGUILayout.BeginVertical(helpBox);
            InspectorUtils.SectionLabel(ThemeColor.Col4, "Text");
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Set Text"))
            {
                if (type.enumValueIndex == 0)
                {
                    _script.DisplayText.text = upgradeAmount.floatValue.ToString("+#,###.#");
                }
                else if (type.enumValueIndex == 1)
                {
                    _script.DisplayText.text = upgradeAmount.floatValue.ToString("x#,###.#");
                }
            }

            if (GUILayout.Button("Reset Text"))
            {
                _script.DisplayText.text = "$upgradeAmount";
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(4);

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