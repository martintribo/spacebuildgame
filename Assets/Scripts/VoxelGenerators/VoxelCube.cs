using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class VoxelCube : MonoBehaviour {
	public int width;
	public int length;
	public int height;
	public Color color;

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
		Voxel[] voxels = new Voxel[width * height * length];

		int v = 0;
		for (var x = 0; x < width; x++) {
			for (var z = 0; z < length; z++) {
				for (var y = 0; y < height; y++) {
					voxels[v++] = new Voxel(x, y, z, color);
				}
			}
		}

		grid.voxels = voxels;
		grid.GenerateMesh();
	}
}
