using UnityEngine;
using System.Collections;

public class VoxelGrid : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Mesh mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;

		GenerateMesh();
	}

	// Update is called once per frame
	void Update () {

	}

	void GenerateMesh() {
		Vector3[] baseVertices = new Vector3[] {
			new Vector3(-0.5F, -0.5F, -0.5F),
			new Vector3(0.5F, -0.5F, -0.5F),
			new Vector3(-0.5F, 0.5F, -0.5F),
			new Vector3(0.5F, 0.5F, -0.5F),
			new Vector3(-0.5F, -0.5F, 0.5F),
			new Vector3(0.5F, -0.5F, 0.5F),
			new Vector3(-0.5F, 0.5F, 0.5F),
			new Vector3(0.5F, 0.5F, 0.5F),
		};
		int[] baseTriangles = new int[] {
			0, 2, 3, 0, 3, 1, //-z
			1, 3, 7, 1, 7, 5, //x
			5, 7, 6, 5, 6, 4, //z
			4, 6, 2, 4, 2, 0, //-x
			2, 6, 7, 2, 7, 3, //y
			1, 5, 4, 1, 4, 0 //-y
		};

		Mesh mesh = GetComponent<MeshFilter>().mesh;

		Vector3[] vertices = new Vector3[transform.childCount * 8];
		int[] triangles = new int[transform.childCount * 8 * 2 * 3];
		Color[] colors = new Color[vertices.Length];

		for (int c = 0; c < transform.childCount; c++) {
			Transform childTransform = transform.GetChild(c);
			Color color = childTransform.gameObject.GetComponent<Voxel>().color;
			for (int v = 0; v < 8; v++) {
				var vert = baseVertices[v];
				vertices[c * 8 + v] = vert + childTransform.position;
				colors[c * 8 + v] = color;
			}

			for (int t = 0; t < baseTriangles.Length; t++) {
				triangles[c * 8 * 2 * 3 + t] = baseTriangles[t] + c * 8;
			}
		}

		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.colors = colors;

		Vector2[] uvs = new Vector2[vertices.Length];

		for (int i=0; i < uvs.Length; i++) {
			uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
		}
		mesh.uv = uvs;
	}
}
