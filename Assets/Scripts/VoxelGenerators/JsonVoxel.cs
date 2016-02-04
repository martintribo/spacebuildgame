using UnityEngine;
using System.Collections;

public class JsonVoxel : MonoBehaviour {
	public TextAsset initialFile = null;

	// Use this for initialization
	void Start () {
		VoxelGrid grid = GetComponent<VoxelGrid>();
		if (initialFile != null) {
			JsonUtility.FromJsonOverwrite(initialFile.text, grid);
		}

		GetComponent<VoxelGrid>().GenerateMesh();
	}

	// Update is called once per frame
	void Update () {

	}
}
