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
    bool editorGroup;

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
        GUIStyle richText = new GUIStyle(GUI.skin.label);
        GUIStyle richTextCentered = new GUIStyle(GUI.skin.label);

        GUIStyle foldoutStyle = EditorStyles.foldout;
        FontStyle previousStyle = foldoutStyle.fontStyle;
        foldoutStyle.fontStyle = FontStyle.Bold;

        richText.richText = true;
        richTextCentered.richText = true;
        buttonLabel.richText = true;
        richTextCentered.alignment = TextAnchor.UpperCenter;

        serializedObject.Update();

        EditorGUILayout.LabelField($"<size=14><b><color={InspectorUtils.Color(ThemeColor.Col1)}>-------------------- Editor --------------------</color></b></size>", richTextCentered);
        EditorGUILayout.Space(1);
        EditorGUILayout.BeginVertical(helpBox);
        editorGroup = GUILayout.Toggle(editorGroup, " Editor options", foldoutStyle);
        if (editorGroup)
        {
            EditorGUILayout.LabelField($"<size=13><b><color={InspectorUtils.Color(ThemeColor.Col4)}>Text</color></b></size>", richText);
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
            EditorGUILayout.Space(2);

            EditorGUILayout.LabelField($"<size=13><b><color={InspectorUtils.Color(ThemeColor.Col5)}>Objects</color></b></size>", richText);
            EditorGUILayout.LabelField($"<size=11>This button assumes your money manager is named exactly \"MoneyManager\"</size>", richText);
            EditorGUILayout.LabelField($"<size=11>(capitalization and no space)</size>", richText);
            EditorGUILayout.Space(1);
            if (_script.MoneyManager == null)
            {
                if (GUILayout.Button("Find Money Manager"))
                {
                    GameObject obj = InspectorUtils.FindObjectByName("MoneyManager");
                    if (obj == null) { Debug.Log($"<color=red>No object found</color>"); return; }

                    _script.MoneyManager = obj.GetComponent<MoneyManager>();
                }
            }
            else
            {
                EditorGUILayout.LabelField("<color=grey>Find Money Manager</color>", buttonLabel);
            }
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(5);

        EditorGUILayout.LabelField($"<size=14><b><color={InspectorUtils.Color(ThemeColor.Col2)}>----------------- Money Giver -----------------</color></b></size>", richTextCentered);
        EditorGUILayout.Space(1);
        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.LabelField($"<size=13><b><color={InspectorUtils.Color(ThemeColor.Col2)}>System</color></b></size>", richText);
        EditorGUILayout.PropertyField(moneyManager);
        EditorGUILayout.PropertyField(soundEffect);
        EditorGUILayout.PropertyField(displayText);
        EditorGUILayout.Space(2);
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(4);

        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.LabelField($"<size=13><b><color={InspectorUtils.Color(ThemeColor.Col3)}>Main</color></b></size>", richText);
        EditorGUILayout.PropertyField(teleportPos);
        EditorGUILayout.PropertyField(winAmount);
        EditorGUILayout.PropertyField(globalSound);
        EditorGUILayout.Space(2);
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif