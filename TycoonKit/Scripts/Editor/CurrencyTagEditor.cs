#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;
using zSkull162.TycoonKit;

[CustomEditor(typeof(CurrencyTag)), CanEditMultipleObjects]
public class CurrencyTagEditor : Editor
{
    #region Get Serialized Properties
    SerializedProperty objectValue;

    private void OnEnable()
    {
        objectValue = serializedObject.FindProperty("objectValue");
    }
    #endregion

    public override void OnInspectorGUI()
    {
        GUIStyle helpBox = new GUIStyle(EditorStyles.helpBox);

        serializedObject.Update();

        InspectorUtils.TitleLabel(ThemeColor.Col3, "Currency Tag", true);
        EditorGUILayout.Space(1);

        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.Space(2);
        EditorGUILayout.PropertyField(objectValue);
        EditorGUILayout.Space(2);
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif