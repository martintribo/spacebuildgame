using UnityEngine;
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
				GameObject module = Instantiate(Resources.Load("Prefabs/Samples/StraightModule") as GameObject);
				module.GetComponent<Module>().mateAdapters[0].ConnectToAdapter(thisAdapter);
			} else {
				Debug.Log("Adapter is already connected to another adapter!");
			}
		}
	}
}
