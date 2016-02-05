using UnityEngine;
using System.Collections;

[System.Serializable]
[ExecuteInEditMode]
public class VoxelGrid : MonoBehaviour {
	[HideInInspector] public Voxel[] voxels;

	void Awake() {
		GetComponent<MeshFilter>().sharedMesh = new Mesh();;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void GenerateMesh() {
		Vector3[] baseVertices = new Vector3[] {
			new Vector3(0, 0, 0),
			new Vector3(1, 0, 0),
			new Vector3(0, 1, 0),
			new Vector3(1, 1, 0),
			new Vector3(0, 0, 1),
			new Vector3(1, 0, 1),
			new Vector3(0, 1, 1),
			new Vector3(1, 1, 1),
		};
		int[] baseTriangles = new int[] {
			0, 2, 3, 0, 3, 1, //-z
			1, 3, 7, 1, 7, 5, //x
			5, 7, 6, 5, 6, 4, //z
			4, 6, 2, 4, 2, 0, //-x
			2, 6, 7, 2, 7, 3, //y
			1, 5, 4, 1, 4, 0 //-y
		};

		Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
		if (mesh == null) {
			return;
		}
		mesh.Clear();

		int numVoxels = voxels.Length;
		int verticesPerCube = 8;
		int trianglesPerCube = 6 * 2;

		Vector3[] vertices = new Vector3[numVoxels * verticesPerCube];
		int[] triangles = new int[numVoxels * trianglesPerCube * 3];
		Color[] colors = new Color[vertices.Length];

		for (int c = 0; c < numVoxels; c++) {
			Voxel voxel = voxels[c];
			Color color = voxel.color;
			Vector3 position = new Vector3(voxel.x, voxel.y, voxel.z);
			int cubeVertexStartIndex = c * verticesPerCube;

			for (int v = 0; v < verticesPerCube; v++) {
				Vector3 vert = baseVertices[v];
				int vertexIndex = cubeVertexStartIndex + v;
				vertices[vertexIndex] = vert + position;
				colors[vertexIndex] = color;
			}

			for (int t = 0; t < baseTriangles.Length; t++) {
				int triangleStartIndex = c * trianglesPerCube * 3;
				triangles[triangleStartIndex + t] = baseTriangles[t] + cubeVertexStartIndex;
			}
		}

		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.colors = colors;

		Vector2[] uvs = new Vector2[vertices.Length];

		for (int i = 0; i < uvs.Length; i++) {
			uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
		}
		mesh.uv = uvs;
		mesh.RecalculateNormals();
	}
}
