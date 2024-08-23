#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;
using zSkull162.TycoonKit;

[CustomEditor(typeof(TriggerEnterRelay)), CanEditMultipleObjects]
public class TriggerEnterRelayEditor : Editor
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

        serializedObject.Update();

        InspectorUtils.TitleLabel(ThemeColor.Col3, "Trigger Enter Relay", true);
        EditorGUILayout.Space(1);

        EditorGUILayout.BeginVertical(helpBox);
        InspectorUtils.Description(ThemeColor.Col4, "Sends an event to the script when a player enters this trigger.");
        EditorGUILayout.Space(1);
        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.PropertyField(script);
        EditorGUILayout.PropertyField(eventName);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif