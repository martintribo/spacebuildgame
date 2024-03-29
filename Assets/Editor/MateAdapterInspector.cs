﻿using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MateAdapter))]
public class MateAdapterInspector : Editor {

	// Use this for initialization
	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		MateAdapter thisAdapter = (MateAdapter) target;

		if (GUILayout.Button("Attach New Module")) {
			if (thisAdapter.connectedAdapter == null) {
				GameObject module = PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/Samples/StraightModule") as GameObject) as GameObject;
				module.GetComponent<Module>().mateAdapters[0].ConnectToAdapter(thisAdapter);
			} else {
				Debug.Log("Adapter is already connected to another adapter!");
			}
		}

		if (GUILayout.Button("Attach New Connector")) {
			if (thisAdapter.connectedAdapter == null) {
				GameObject module = PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/Samples/ConnectorModule") as GameObject) as GameObject;
				module.GetComponent<Module>().mateAdapters[0].ConnectToAdapter(thisAdapter);
			} else {
				Debug.Log("Adapter is already connected to another adapter!");
			}
		}
	}
}
