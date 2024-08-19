#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;
using zSkull162.TycoonKit;

[CustomEditor(typeof(ObjectCleanup)), CanEditMultipleObjects]
public class ObjectCleanerEditor : Editor
{
    #region Get Serialized Properties
    SerializedProperty objName;

    private void OnEnable()
    {
        objName = serializedObject.FindProperty("objName");
    }
    #endregion

    public override void OnInspectorGUI()
    {
        GUIStyle helpBox = new GUIStyle(EditorStyles.helpBox);

        serializedObject.Update();

        InspectorUtils.TitleLabel(ThemeColor.Col2, "Object Cleanup", true);
        EditorGUILayout.Space(1);

        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.Space(2);
        EditorGUILayout.PropertyField(objName);
        EditorGUILayout.Space(2);
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif