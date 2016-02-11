using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
[ExecuteInEditMode]
public class VoxelGrid : MonoBehaviour {
    public List<Voxel> voxels;


	void Awake() {
		GetComponent<MeshFilter>().sharedMesh = new Mesh();

        if (voxels == null)
            voxels = new List<Voxel>();
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

		int numVoxels = voxels.Count;
		int verticesPerCube = 8;
		int trianglesPerCube = 6 * 2;

		Vector3[] vertices = new Vector3[numVoxels * verticesPerCube];
		int[] triangles = new int[numVoxels * trianglesPerCube * 3];
		Color[] colors = new Color[vertices.Length];

        int iteration = 0;

        foreach (var voxel in voxels)
        {
            var position = new Vector3(voxel.x, voxel.y, voxel.z);
            var cubeVertexStartIndex = iteration * verticesPerCube;

            for (int v = 0; v < verticesPerCube; v++)
            {
                Vector3 vert = baseVertices[v];
                int vertexIndex = cubeVertexStartIndex + v;
                vertices[vertexIndex] = vert + position;
                colors[vertexIndex] = voxel.color;
            }

            for (int t = 0; t < baseTriangles.Length; t++)
            {
                int triangleStartIndex = iteration * trianglesPerCube * 3;
                triangles[triangleStartIndex + t] = baseTriangles[t] + cubeVertexStartIndex;
            }

            iteration++;
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

        //check for mesh collider we can use, set after creating
        MeshCollider mc = GetComponent<MeshCollider>();

        if (mc != null)
            mc.sharedMesh = mesh;
    }

    public void addVoxel(int xpos, int ypos, int zpos, Color color)
    {
        this.voxels.Add(new Voxel() { x = xpos, y = ypos, z = zpos, color = color });

        GenerateMesh();
    }
}
