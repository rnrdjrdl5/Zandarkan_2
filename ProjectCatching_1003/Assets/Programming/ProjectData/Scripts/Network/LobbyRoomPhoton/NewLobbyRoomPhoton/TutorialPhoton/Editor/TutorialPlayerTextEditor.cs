using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TutorialPlayerText))]
public class TutorialPlayerTextEditor : Editor
{
    TutorialPlayerText tpt;

    private void OnEnable()
    {
        tpt = (TutorialPlayerText)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("고양이 튜토리얼 내용");
        tpt.CatText = EditorGUILayout.TextArea(tpt.CatText);

        EditorGUILayout.LabelField("쥐 튜토리얼 내용");
        tpt.MouseText = EditorGUILayout.TextArea(tpt.MouseText);
    }
}
