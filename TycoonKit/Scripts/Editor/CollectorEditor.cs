#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;
using zSkull162.TycoonKit;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators;

[CustomEditor(typeof(Collector)), CanEditMultipleObjects]
public class CollectorEditor : Editor
{
    #region Get Serialized Properties
    SerializedProperty moneyManager;

    private void OnEnable()
    {
        moneyManager = serializedObject.FindProperty("moneyManager");
    }
    #endregion

    public override void OnInspectorGUI()
    {
        Collector _script = (Collector)target;
        if (_script == null) return;

        GUIStyle helpBox = new GUIStyle(EditorStyles.helpBox);
        GUIStyle buttonLabel = new GUIStyle(GUI.skin.button);
        GUIStyle richText = new GUIStyle(GUI.skin.label);
        GUIStyle richTextCentered = new GUIStyle(GUI.skin.label);
        buttonLabel.richText = true;
        richText.richText = true;
        richTextCentered.richText = true;
        richTextCentered.alignment = TextAnchor.UpperCenter;

        serializedObject.Update();

        EditorGUILayout.LabelField($"<size=14><b><color={InspectorUtils.Color(ThemeColor.Col2)}>----------------- Collector -----------------</color></b></size>", richTextCentered);
        EditorGUILayout.Space(1);

        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.Space(2);
        EditorGUILayout.PropertyField(moneyManager);
        EditorGUILayout.Space(2);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(2);
        EditorGUILayout.BeginVertical(helpBox);
        EditorGUILayout.LabelField($"<size=13><b><color={InspectorUtils.Color(ThemeColor.Col1)}>Editor</color></b></size>", richText);
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
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif