using UnityEngine;
using System.Collections;

public class VoxelGrid : MonoBehaviour {
	public int voxelSize = 2;
	public Vector3[] newVertices;
	public int[] newTriangles;

	// Use this for initialization
	void Start () {
		Mesh mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;

		Vector3[] vertices = mesh.vertices = new Vector3[] {
			new Vector3(0, 0, 0),
			new Vector3(0, 1, 0),
			new Vector3(1, 1, 0)
		};
		mesh.vertices = vertices;

		// Note: Use left hand rule to determine direction triangle face renders
		mesh.triangles = new int[] {0, 1, 2};

		Vector2[] uvs = new Vector2[vertices.Length];

		for (int i=0; i < uvs.Length; i++) {
			uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
		}
		mesh.uv = uvs;
	}

	// Update is called once per frame
	void Update () {

	}
}
