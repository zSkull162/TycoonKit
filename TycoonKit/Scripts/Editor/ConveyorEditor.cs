#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;
using zSkull162.TycoonKit;

[CustomEditor(typeof(Conveyor)), CanEditMultipleObjects]
public class ConveyorEditor : Editor
{
    #region Get Serialized Properties
    SerializedProperty objectName;
    SerializedProperty force;

    private void OnEnable()
    {
        objectName = serializedObject.FindProperty("objectName");
        force = serializedObject.FindProperty("force");
    }
    #endregion

    public override void OnInspectorGUI()
    {
        GUIStyle helpBox = new GUIStyle(EditorStyles.helpBox);

        serializedObject.Update();

        InspectorUtils.TitleLabel(ThemeColor.Col3, "Conveyor", true);
        EditorGUILayout.Space(1);

        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.Space(2);
        EditorGUILayout.PropertyField(objectName);
        EditorGUILayout.Space(2);
        EditorGUILayout.PropertyField(force);
        EditorGUILayout.Space(2);
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif