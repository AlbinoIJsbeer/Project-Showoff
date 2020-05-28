using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TrashSpawner))]
public class TrashSpawnerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		TrashSpawner trashSpawn = (TrashSpawner)target;

		//DrawDefaultInspector();
		if (DrawDefaultInspector())
		{
			if (trashSpawn.autoUpdate)
			{
				trashSpawn.ClearTrash();
				trashSpawn.GenerateTrash();
			}
		}

		if (GUILayout.Button("Generate"))
		{
			trashSpawn.ClearTrash();
			trashSpawn.GenerateTrash();	
		}
	}
}
