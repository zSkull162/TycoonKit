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
        GUIStyle richText = new GUIStyle(GUI.skin.label);
        GUIStyle richTextCentered = new GUIStyle(GUI.skin.label);
        richText.richText = true;
        richTextCentered.richText = true;
        richTextCentered.alignment = TextAnchor.UpperCenter;

        serializedObject.Update();

        EditorGUILayout.LabelField($"<size=14><b><color={InspectorUtils.Color(ThemeColor.Col3)}>-------------- Interaction Relay --------------</color></b></size>", richTextCentered);
        EditorGUILayout.Space(1);

        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.LabelField($"<size=11><color={InspectorUtils.Color(ThemeColor.Col4)}>Sends an event to the script when a player clicks this collider.</color></size>", richText);
        EditorGUILayout.Space(1);
        EditorGUILayout.PropertyField(script);
        EditorGUILayout.PropertyField(eventName);
        EditorGUILayout.Space(2);
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(4);

        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.LabelField($"<size=13><b><color={InspectorUtils.Color(ThemeColor.Col1)}>Udon Options</color></b></size>", richText);
        UdonSharpEditor.UdonSharpGUI.DrawInteractSettings(target);
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif