using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class VoxelCylinder : MonoBehaviour {
	public int diameter = 4;
	public int length = 1;
	public Color color = Color.white;
	private List<Vector2I> octantPoints = new List<Vector2I>();
	// Use this for initialization
	void Start () {
		GenerateOctantPoints();
		GenerateShape();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnValidate() {
		GenerateOctantPoints();
		GenerateShape();
	}

	void GenerateOctantPoints() {
		int radius = diameter / 2;
		int x = radius;
		int y = 0;
		int decisionOver2 = 1 - x;

		octantPoints.Clear();

		while (y <= x) {
			if (diameter % 2 != 0  || y != 0) {
				octantPoints.Add(new Vector2I(x, y));
			}

			y++;
			if (decisionOver2 <= 0) {
				decisionOver2 += 2 * y + 1;
			} else {
				x--;
				decisionOver2 += 2 * (y - x) + 1;
			}
		}
	}

	void GenerateShape() {
		VoxelGrid grid = GetComponent<VoxelGrid>();
        var voxels = new List<Voxel>(octantPoints.Count * 8 * length);

		int x0 = diameter / 2;
		int y0 = diameter / 2;
		bool even = diameter % 2 == 0;
		if (even) {
			x0--;
			y0--;
		}
		for (var l = 0; l < length; l++) {
			for (int p = 0; p < octantPoints.Count; p++) {
				int x = octantPoints[p].x;
				int y = octantPoints[p].y;

				voxels.Add(new Voxel(x + x0, y + y0, l, color));
                voxels.Add(new Voxel(y + x0, x + y0, l, color));
				if (even) {
					x0 += 1;
				}
                voxels.Add(new Voxel(-x + x0, y + y0, l, color));
                voxels.Add(new Voxel(-y + x0, x + y0, l, color));
				if (even) {
					y0++;
				}
                voxels.Add(new Voxel(-x + x0, -y + y0, l, color));
                voxels.Add(new Voxel(-y + x0, -x + y0, l, color));
				if (even) {
					x0--;
				}
                voxels.Add(new Voxel(x + x0, -y + y0, l, color));
                voxels.Add(new Voxel(y + x0, -x + y0, l, color));
				if (even) {
					y0--;
				}
			}
		}

		grid.voxels = voxels;
		grid.GenerateMesh();
	}
}
