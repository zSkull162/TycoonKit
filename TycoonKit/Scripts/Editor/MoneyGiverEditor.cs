#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;
using zSkull162.TycoonKit;

[CustomEditor(typeof(MoneyGiver)), CanEditMultipleObjects]
public class MoneyGiverEditor : Editor
{
    #region Get Serialized Properties
    SerializedProperty moneyManager;
    SerializedProperty soundEffect;
    SerializedProperty teleportPos;
    SerializedProperty displayText;
    SerializedProperty winAmount;
    SerializedProperty globalSound;

    private void OnEnable()
    {
        moneyManager = serializedObject.FindProperty("moneyManager");
        soundEffect = serializedObject.FindProperty("soundEffect");
        teleportPos = serializedObject.FindProperty("teleportPosition");
        displayText = serializedObject.FindProperty("displayText");
        winAmount = serializedObject.FindProperty("winAmount");
        globalSound = serializedObject.FindProperty("globalSound");
    }
    #endregion

    public override void OnInspectorGUI()
    {
        MoneyGiver _script = (MoneyGiver)target;
        if (_script == null) return;

        GUIStyle helpBox = new GUIStyle(EditorStyles.helpBox);
        GUIStyle buttonLabel = new GUIStyle(GUI.skin.button);

        GUIStyle foldoutStyle = EditorStyles.foldout;
        FontStyle previousStyle = foldoutStyle.fontStyle;
        foldoutStyle.fontStyle = FontStyle.Bold;
        buttonLabel.richText = true;

        serializedObject.Update();

        InspectorUtils.TitleLabel(ThemeColor.Col1, "Editor", true);
        EditorGUILayout.Space(1);
        EditorGUILayout.BeginVertical(helpBox);
        _script.editorOptions = GUILayout.Toggle(_script.editorOptions, " Editor options", foldoutStyle);
        if (_script.editorOptions)
        {
            EditorGUILayout.BeginVertical(helpBox);
            InspectorUtils.SectionLabel(ThemeColor.Col4, "Text");
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Set Text"))
            {
                _script.DisplayText.text = _script.WinAmount.ToString("Win: $#,###.#");
            }

            if (GUILayout.Button("Reset Text"))
            {
                _script.DisplayText.text = "Win: $winAmount";
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(2);

            EditorGUILayout.BeginVertical(helpBox);
            InspectorUtils.SectionLabel(ThemeColor.Col5, "Objects");
            InspectorUtils.Description("This button assumes your money manager is named exactly \"MoneyManager\" (capitalization and no space)");
            EditorGUILayout.Space(1);
            if (_script.MoneyManager == null)
            {
                if (GUILayout.Button("Find Money Manager"))
                {
                    GameObject obj = InspectorUtils.FindObjectByName("MoneyManager");
                    if (obj == null) { Debug.Log($"<color=red>No object found</color>"); return; }

                    moneyManager.objectReferenceValue = obj.GetComponent<MoneyManager>();
                }
            }
            else
            {
                EditorGUILayout.LabelField("<color=grey>Find Money Manager</color>", buttonLabel);
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(4);

        InspectorUtils.TitleLabel(ThemeColor.Col2, "Money Giver", true);
        EditorGUILayout.Space(1);
        EditorGUILayout.BeginVertical(helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col2, "System");
        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.PropertyField(moneyManager);
        EditorGUILayout.PropertyField(soundEffect);
        EditorGUILayout.PropertyField(displayText);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(2);

        EditorGUILayout.BeginVertical(helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col3, "Main");
        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.PropertyField(teleportPos);
        EditorGUILayout.PropertyField(winAmount);
        EditorGUILayout.PropertyField(globalSound);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif