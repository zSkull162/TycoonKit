#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using zSkull162.TycoonKit;

[CustomEditor(typeof(UnlockButton)), CanEditMultipleObjects]
public class UnlockButtonEditor : Editor
{
    #region Get Serialized Properties
    SerializedProperty moneyManager;
    SerializedProperty soundEffect;
    SerializedProperty containerObject;
    SerializedProperty titleText;
    SerializedProperty costText;
    SerializedProperty unlockName;
    SerializedProperty cost;
    SerializedProperty globalSound;
    SerializedProperty unlocks;
    SerializedProperty previousObject;
    bool editorGroup, upgradeGroup;

    private void OnEnable()
    {
        moneyManager = serializedObject.FindProperty("moneyManager");
        soundEffect = serializedObject.FindProperty("soundEffect");
        containerObject = serializedObject.FindProperty("containerObject");
        titleText = serializedObject.FindProperty("titleText");
        costText = serializedObject.FindProperty("costText");
        unlockName = serializedObject.FindProperty("unlockName");
        cost = serializedObject.FindProperty("cost");
        globalSound = serializedObject.FindProperty("globalSound");
        unlocks = serializedObject.FindProperty("unlocks");
        previousObject = serializedObject.FindProperty("previousObject");
    }
    #endregion

    public override void OnInspectorGUI()
    {
        UnlockButton _script = (UnlockButton)target;
        if (_script == null) { return; }

        GUIStyle helpBox = new GUIStyle(EditorStyles.helpBox);
        GUIStyle buttonLabel = new GUIStyle(GUI.skin.button);
        GUIStyle richText = new GUIStyle(GUI.skin.label);
        GUIStyle richTextCentered = new GUIStyle(GUI.skin.label);

        GUIStyle foldoutStyle = EditorStyles.foldout;
        FontStyle previousStyle = foldoutStyle.fontStyle;
        foldoutStyle.fontStyle = FontStyle.Bold;
        GUIStyle buttonStyle = EditorStyles.radioButton;
        FontStyle previousStyle1 = buttonStyle.fontStyle;
        buttonStyle.fontStyle = FontStyle.Bold;

        richText.richText = true;
        richTextCentered.richText = true;
        buttonLabel.richText = true;
        buttonLabel.stretchWidth = true;
        richTextCentered.alignment = TextAnchor.UpperCenter;

        serializedObject.Update();

        EditorGUILayout.LabelField($"<size=14><b><color={InspectorUtils.Color(ThemeColor.Col1)}>-------------------- Editor --------------------</color></b></size>", richTextCentered);
        EditorGUILayout.Space(1);
        EditorGUILayout.BeginVertical(helpBox);
        editorGroup = GUILayout.Toggle(editorGroup, " Editor options", foldoutStyle);
        if (editorGroup)
        {
            EditorGUILayout.Space(2);
            EditorGUILayout.LabelField($"<size=13><b><color={InspectorUtils.Color(ThemeColor.Col4)}>Text</color></b></size>", richText);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Set Text"))
            {
                _script.TitleText.text = $"{_script.UnlockName}:";
                _script.CostText.text = _script.Cost.ToString("$#,###.#");
            }

            if (GUILayout.Button("Reset Text"))
            {
                _script.TitleText.text = "Unlock Name:";
                _script.CostText.text = "$Cost";
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(2);

            EditorGUILayout.LabelField($"<size=13><b><color={InspectorUtils.Color(ThemeColor.Col5)}>Objects</color></b></size>", richText);
            EditorGUILayout.LabelField($"<size=11>These buttons assume your money manager is named exactly \"MoneyManager\"</size>", richText);
            EditorGUILayout.LabelField($"<size=11>(capitalization and no space), and that the Container Object is the <i>first</i> child of</size>", richText);
            EditorGUILayout.LabelField($"<size=11>the object with this script.</size>", richText);
            EditorGUILayout.Space(1);
            EditorGUILayout.BeginHorizontal();
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

            if (_script.ContainerObject == null)
            {
                if (GUILayout.Button("Find Container Object"))
                {
                    Transform tsfm = _script.transform.GetChild(0);
                    if (tsfm == null) { Debug.Log($"<color=red>No child object found</color>"); return; }
                    else Debug.Log($"<color=lime><b>Returning {tsfm}</b></color>");

                    _script.ContainerObject = tsfm.gameObject;
                }
            }
            else
            {
                EditorGUILayout.LabelField("<color=grey>Find Container Object</color>", buttonLabel);
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(5);

        EditorGUILayout.LabelField($"<size=14><b><color={InspectorUtils.Color(ThemeColor.Col2)}>--------------- Unlock Button ---------------</color></b></size>", richTextCentered);
        EditorGUILayout.Space(1);
        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.LabelField($"<size=13><b><color={InspectorUtils.Color(ThemeColor.Col2)}>System</color></b></size>", richText);
        EditorGUILayout.PropertyField(moneyManager);
        EditorGUILayout.PropertyField(containerObject);
        EditorGUILayout.PropertyField(soundEffect);
        EditorGUILayout.PropertyField(titleText);
        EditorGUILayout.PropertyField(costText);
        EditorGUILayout.Space(2);
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(4);

        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.LabelField($"<size=13><b><color={InspectorUtils.Color(ThemeColor.Col3)}>Main</color></b></size>", richText);
        EditorGUILayout.PropertyField(unlockName);
        EditorGUILayout.PropertyField(cost);
        EditorGUILayout.PropertyField(globalSound);
        EditorGUILayout.Space(2);
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(4);

        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.LabelField($"<size=13><b><color={InspectorUtils.Color(ThemeColor.Col4)}>Unlocks</color></b></size>", richText);
        EditorGUILayout.PropertyField(unlocks);
        EditorGUILayout.Space(2);

        EditorGUILayout.BeginVertical(helpBox);
        upgradeGroup = GUILayout.Toggle(upgradeGroup, "  Is Upgrade", buttonStyle);
        if (_script.PreviousObject != null)
        {
            upgradeGroup = true;
            if (upgradeGroup)
            {
                EditorGUILayout.PropertyField(previousObject);
            }
        }
        else
        {
            if (upgradeGroup)
            {
                EditorGUILayout.PropertyField(previousObject);
            }
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(4);

        serializedObject.ApplyModifiedProperties();
    }
}
#endif