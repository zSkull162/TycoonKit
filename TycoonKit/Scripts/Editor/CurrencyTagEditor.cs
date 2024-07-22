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
        GUIStyle richTextCentered = new GUIStyle(GUI.skin.label);
        richTextCentered.richText = true;
        richTextCentered.alignment = TextAnchor.UpperCenter;

        serializedObject.Update();

        EditorGUILayout.LabelField($"<size=14><b><color={InspectorUtils.Color(ThemeColor.Col4)}>----------------- Currency Tag -----------------</color></b></size>", richTextCentered);
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