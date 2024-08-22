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
        GUIStyle richTextCentered = new GUIStyle(GUI.skin.label);
        richText.richText = true;
        richTextCentered.richText = true;
        richTextCentered.alignment = TextAnchor.UpperCenter;

        serializedObject.Update();

        EditorGUILayout.LabelField($"<size=14><b><color={InspectorUtils.Color(ThemeColor.Col2)}>---------------- Dropper Button ----------------</color></b></size>", richTextCentered);
        EditorGUILayout.Space(1);

        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.LabelField($"<size=13><b><color={InspectorUtils.Color(ThemeColor.Col1)}>Udon</color></b></size>", richText);
        UdonSharpEditor.UdonSharpGUI.DrawInteractSettings(target);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(4);
        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.LabelField($"<size=13><b><color={InspectorUtils.Color(ThemeColor.Col3)}>System</color></b></size>", richText);
        EditorGUILayout.PropertyField(spawner);
        EditorGUILayout.PropertyField(sound);
        EditorGUILayout.Space(2);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(4);
        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.LabelField($"<size=13><b><color={InspectorUtils.Color(ThemeColor.Col4)}>Options</color></b></size>", richText);
        EditorGUILayout.PropertyField(cooldown);
        EditorGUILayout.Space(2);
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif