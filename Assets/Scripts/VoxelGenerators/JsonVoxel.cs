using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class JsonVoxel : MonoBehaviour {
	public TextAsset initialFile = null;

	// Use this for initialization
	void Start () {
		SetVoxelGrid();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnValidate() {
		SetVoxelGrid();
	}

	void SetVoxelGrid() {
		VoxelGrid grid = GetComponent<VoxelGrid>();
		grid.voxels = null;
		if (initialFile != null) {
			JsonUtility.FromJsonOverwrite(initialFile.text, grid);
		} else {
            grid.voxels = new List<Voxel>();
		}

		grid.GenerateMesh();
	}
}
