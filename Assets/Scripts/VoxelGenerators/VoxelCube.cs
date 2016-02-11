using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class VoxelCube : MonoBehaviour {
	public int width = 1;
	public int length = 1;
	public int height = 1;
	public Color color = Color.white;

	// Use this for initialization
	void Start () {
		updateVoxels();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnValidate() {
		updateVoxels();
	}

	void updateVoxels() {
		VoxelGrid grid = GetComponent<VoxelGrid>();
		List<Voxel> voxels = new List<Voxel>(width * height * length);

		for (var x = 0; x < width; x++) {
			for (var z = 0; z < length; z++) {
				for (var y = 0; y < height; y++) {
                    voxels.Add(new Voxel(x, y, z, color));
				}
			}
		}

		grid.voxels = voxels;
		grid.GenerateMesh();
	}
}
