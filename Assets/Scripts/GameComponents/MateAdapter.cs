using UnityEngine;
using System.Collections;

// MateAdapters exist at the points where modules can attach to each other,
// facing towards where the other adapter will attach
public class MateAdapter : MonoBehaviour {
	public MateAdapter connectedAdapter = null;

	// Use this for initialization
	void Start () {
		// TODO: Eventually this logic should be removed. This just removes a debug shape
		var renderers = GetComponentsInChildren<Renderer>();
		foreach (var r in renderers) {
			r.enabled = false;
		}
	}

	// Update is called once per frame
	void Update () {

	}

	public void ConnectToAdapter(MateAdapter adapter) {
		MateAdapter.ConnectAdapters(this, adapter);
		Transform ot = adapter.gameObject.GetComponent<Transform>();
		Transform moduleTransform = Module.GetModule(gameObject).transform;

		Vector3 forwardDir = ot.rotation * Vector3.back;
		Vector3 upDir = ot.rotation * Vector3.up;
		Quaternion relative = Quaternion.Inverse(transform.rotation) * moduleTransform.rotation;

		moduleTransform.rotation = Quaternion.LookRotation(forwardDir, upDir) * relative;

		moduleTransform.position -= transform.position - ot.position;
	}

	public static void ConnectAdapters(MateAdapter adapter1, MateAdapter adapter2) {
		if (adapter1 != null) {
			if (adapter1.connectedAdapter != null && adapter1.connectedAdapter != adapter2) {
				adapter1.connectedAdapter.connectedAdapter = null;
			}
			adapter1.connectedAdapter = adapter2;
		}
		if (adapter2 != null) {
			if (adapter2.connectedAdapter != null && adapter2.connectedAdapter != adapter1) {
				adapter2.connectedAdapter.connectedAdapter = null;
			}
			adapter2.connectedAdapter = adapter1;
		}
	}

	public static MateAdapter GetAdapter(GameObject obj) {
		GameObject currentGO = obj;
		MateAdapter adapter = null;

		while (currentGO != null && adapter == null) {
			adapter = currentGO.GetComponent<MateAdapter>();

			if (adapter == null) {
				currentGO = currentGO.transform.parent.gameObject;
			}
		}

		return adapter;
	}
}
