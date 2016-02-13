using UnityEngine;
using System.Collections;

public class ZeroGravityCamera : MonoBehaviour {
	bool cameraTogglePressed = false;
	public bool firstPerson = false;
	// Use this for initialization
	void Start () {
		if (firstPerson) {
			SetFirstPerson();
		} else {
			SetThirdPerson();
		}
	}

	// Update is called once per frame
	void Update () {
		bool pressed = Input.GetButton("CameraToggle");
		if (!cameraTogglePressed && pressed) {
			if (firstPerson) {
				SetThirdPerson();
			} else {
				SetFirstPerson();
			}
		}

		cameraTogglePressed = pressed;
	}

	public void SetFirstPerson() {
		firstPerson = true;
		transform.localPosition = new Vector3(1, 2, 1);
		transform.parent.gameObject.GetComponent<Renderer>().enabled = false;
	}

	public void SetThirdPerson() {
		firstPerson = false;
		transform.localPosition = new Vector3(1, 2, -15);
		transform.parent.gameObject.GetComponent<Renderer>().enabled = true;
	}
}
