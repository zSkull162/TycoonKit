#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;
using zSkull162.TycoonKit;

[CustomEditor(typeof(Dropper)), CanEditMultipleObjects]
public class DropperEditor : Editor
{
    #region Get Serialized Properties
    SerializedProperty objectInstance;
    SerializedProperty spawnDelay;

    private void OnEnable()
    {
        objectInstance = serializedObject.FindProperty("objectInstance");
        spawnDelay = serializedObject.FindProperty("spawnDelay");
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

        EditorGUILayout.LabelField($"<size=14><b><color={InspectorUtils.Color(ThemeColor.Col2)}>----------------- Dropper -----------------</color></b></size>", richTextCentered);
        EditorGUILayout.Space(1);

        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.Space(2);
        EditorGUILayout.PropertyField(objectInstance);
        EditorGUILayout.PropertyField(spawnDelay);
        EditorGUILayout.Space(2);
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif