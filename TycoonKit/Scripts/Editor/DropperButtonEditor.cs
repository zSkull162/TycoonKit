#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;
using zSkull162.TycoonKit;

[CustomEditor(typeof(DropperButton)), CanEditMultipleObjects]
public class DropperButtonEditor : Editor
{
    #region Get Serialized Properties
    SerializedProperty spawner;
    SerializedProperty cooldown;
    SerializedProperty sound;

    private void OnEnable()
    {
        spawner = serializedObject.FindProperty("spawner");
        cooldown = serializedObject.FindProperty("cooldown");
        sound = serializedObject.FindProperty("sound");
    }
    #endregion

    public override void OnInspectorGUI()
    {
        GUIStyle helpBox = new GUIStyle(EditorStyles.helpBox);
        GUIStyle richText = new GUIStyle(GUI.skin.label);
        richText.richText = true;

        serializedObject.Update();

        InspectorUtils.TitleLabel(ThemeColor.Col2, "Dropper Button", true);

        EditorGUILayout.BeginVertical(helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col1, "Udon");
        EditorGUILayout.BeginVertical(helpBox);
        UdonSharpEditor.UdonSharpGUI.DrawInteractSettings(target);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(2);
        EditorGUILayout.BeginVertical(helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col3, "System");
        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.PropertyField(spawner);
        EditorGUILayout.PropertyField(sound);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(2);
        EditorGUILayout.BeginVertical(helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col2, "Settings");
        EditorGUILayout.BeginVertical(helpBox);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(cooldown);
        if (cooldown.floatValue != 0.3f)
        {
            if (GUILayout.Button("Reset to default value", GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth / 3.25f)))
            {
                cooldown.floatValue = 0.3f;
            }
        }
        EditorGUILayout.EndHorizontal();
        if (cooldown.floatValue <= 0f)
        {
            EditorGUILayout.LabelField("<color=red><b>Disabled</b></color>", richText);
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif