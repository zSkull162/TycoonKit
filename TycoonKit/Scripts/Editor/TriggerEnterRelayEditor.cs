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
        GUIStyle richText = new GUIStyle(GUI.skin.label);
        GUIStyle richTextCentered = new GUIStyle(GUI.skin.label);
        richText.richText = true;
        richTextCentered.richText = true;
        richTextCentered.alignment = TextAnchor.UpperCenter;

        serializedObject.Update();

        EditorGUILayout.LabelField($"<size=14><b><color={InspectorUtils.Color(ThemeColor.Col3)}>-------------- Trigger Enter Relay --------------</color></b></size>", richTextCentered);
        EditorGUILayout.Space(1);

        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.LabelField($"<size=11><color={InspectorUtils.Color(ThemeColor.Col4)}>Sends an event to the script when a player enters this trigger.</color></size>", richText);
        EditorGUILayout.Space(1);
        EditorGUILayout.PropertyField(script);
        EditorGUILayout.PropertyField(eventName);
        EditorGUILayout.Space(2);
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif