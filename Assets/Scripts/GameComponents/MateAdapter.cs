using UnityEngine;
using System.Collections;

// MateAdapters exist at the points where modules can attach to each other,
// facing towards where the other adapter will attach
public class MateAdapter : MonoBehaviour {
	public GameObject otherAdapter = null;

	// Use this for initialization
	void Start () {
		if (otherAdapter != null) {
			//Debug connect to other adapter
			Debug.Log("Connecting");
			ConnectToAdapter(otherAdapter);
		}
	}

	// Update is called once per frame
	void Update () {

	}

	void ConnectToAdapter(GameObject adapter) {
		Transform ot = adapter.GetComponent<Transform>();
		Transform moduleTransform = transform.parent.parent; // TODO: Use Module.GetModule(GameObject)

		Vector3 forwardDir = ot.rotation * Vector3.back;
		Vector3 upDir = ot.rotation * Vector3.up;
		Quaternion relative = Quaternion.Inverse(transform.rotation) * moduleTransform.rotation;

		moduleTransform.rotation = Quaternion.LookRotation(forwardDir, upDir) * relative;

		moduleTransform.position -= transform.position - ot.position;
	}
}
