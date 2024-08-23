#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;
using zSkull162.TycoonKit;

[CustomEditor(typeof(InteractionRelay)), CanEditMultipleObjects]
public class InteractionRelayEditor : Editor
{
    #region Get Serialized Properties
    SerializedProperty script;
    SerializedProperty eventName;

    private void OnEnable()
    {
        script = serializedObject.FindProperty("script");
        eventName = serializedObject.FindProperty("eventName");
    }
    #endregion

    public override void OnInspectorGUI()
    {
        GUIStyle helpBox = new GUIStyle(EditorStyles.helpBox);
        GUIStyle description = new GUIStyle(GUI.skin.label);
        description.richText = true;
        description.wordWrap = true;

        serializedObject.Update();

        InspectorUtils.TitleLabel(ThemeColor.Col3, "Interaction Relay", true);
        EditorGUILayout.Space(1);

        EditorGUILayout.BeginVertical(helpBox);
        InspectorUtils.Description(ThemeColor.Col4, "Sends an event to the script when a player clicks this collider.");
        EditorGUILayout.Space(1);
        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.PropertyField(script);
        EditorGUILayout.PropertyField(eventName);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(2);

        EditorGUILayout.BeginVertical(helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col1, "Udon");
        EditorGUILayout.BeginVertical(helpBox);
        UdonSharpEditor.UdonSharpGUI.DrawInteractSettings(target);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif