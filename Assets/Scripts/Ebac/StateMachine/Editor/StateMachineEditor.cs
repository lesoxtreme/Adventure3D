using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(FSMExample))]
public class StateMachineEditor : Editor
{
	public bool showFoldout;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		FSMExample fsm = (FSMExample)target;

		EditorGUILayout.Space(30);
		EditorGUILayout.LabelField("State Machine");
		if(fsm.stateMachine == null) return;

		if(fsm.stateMachine.CurrentState != null)
			EditorGUILayout.LabelField("Current State: ", fsm.stateMachine.CurrentState.ToString());

		showFoldout = EditorGUILayout.Foldout(showFoldout, "Available States");

		if(showFoldout)
		{
			if (fsm.stateMachine.dictionaryState != null)
			{
				var keys = fsm.stateMachine.dictionaryState.Keys.ToArray();
				var vals = fsm.stateMachine.dictionaryState.Values.ToArray();
			

				for(int i =0; i < keys.Length; i++)
				{
					EditorGUILayout.LabelField($"{keys[i]} :: {vals[i]}");
				}
			}
		}	

	}

}
