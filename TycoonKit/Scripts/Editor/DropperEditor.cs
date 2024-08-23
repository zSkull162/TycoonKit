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
        richText.richText = true;

        serializedObject.Update();

        InspectorUtils.TitleLabel(ThemeColor.Col2, "Dropper", true);
        EditorGUILayout.Space(1);

        EditorGUILayout.BeginVertical(helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col3, "System");
        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.PropertyField(objectInstance);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(spawnDelay);
        if (spawnDelay.floatValue != 1.8f)
        {
            if (GUILayout.Button("Reset to default value", GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth / 3.25f)))
            {
                spawnDelay.floatValue = 1.8f;
            }
        }
        EditorGUILayout.EndHorizontal();
        if (spawnDelay.floatValue <= 0f)
        {
            EditorGUILayout.LabelField("<color=red><b>Disabled</b></color>", richText);
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif