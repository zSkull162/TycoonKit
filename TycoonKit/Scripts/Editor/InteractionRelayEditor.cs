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
        EditorGUILayout.LabelField($"<size=11><color={InspectorUtils.Color(ThemeColor.Col4)}>Sends an event to the script when a player clicks this collider.</color></size>", description);
        EditorGUILayout.Space(1);
        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.PropertyField(script);
        EditorGUILayout.PropertyField(eventName);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(4);

        EditorGUILayout.BeginVertical(helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col1, "Udon Options");
        UdonSharpEditor.UdonSharpGUI.DrawInteractSettings(target);
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif