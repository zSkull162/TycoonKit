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

        serializedObject.Update();

        InspectorUtils.TitleLabel(ThemeColor.Col2, "Dropper", true);
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