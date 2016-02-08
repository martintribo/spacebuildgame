using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Module : MonoBehaviour {
	public List<MateAdapter> mateAdapters = new List<MateAdapter>();

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public static Module GetModule(GameObject obj) {
		GameObject currentGO = obj;
		Module module = null;

		while (currentGO != null && module == null) {
			module = currentGO.GetComponent<Module>();

			if (module == null) {
				currentGO = currentGO.transform.parent.gameObject;
			}
		}

		return module;
	}
}
