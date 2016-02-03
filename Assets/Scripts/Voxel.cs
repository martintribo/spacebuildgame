using UnityEngine;
using System.Collections;

[System.Serializable]
public class Voxel {
	public int x;
	public int y;
	public int z;
	public Color color;

	public Voxel() {
		x = 0;
		y = 0;
		z = 0;
		color = Color.white;
	}
}
