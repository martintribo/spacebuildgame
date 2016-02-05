using UnityEngine;
using System.Collections;

[System.Serializable]
public class Voxel {
	public int x;
	public int y;
	public int z;
	public Color color;

	public Voxel(int x = 0, int y = 0, int z = 0, Color color = default(Color)) {
		this.x = x;
		this.y = y;
		this.z = z;
		this.color = color;
	}
}
