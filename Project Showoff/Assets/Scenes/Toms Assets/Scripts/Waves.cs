using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public int Dimensions = 10;
    public Octave[] Octaves;
    public float UVScale; 

    private protected MeshFilter _meshFilter;
    private protected Mesh _mesh;

    [Serializable]
    public struct Octave
    {
        public Vector2 Speed;
        public Vector2 Scale;
        public float Height;
        public bool Alternate;
    }

    void Start()
    {
        _mesh = new Mesh();
        _mesh.name = gameObject.name;

        _mesh.vertices = GenerateVertices();
        _mesh.triangles = GenerateTriangles();
        _mesh.uv = GenerateUVs();
        _mesh.RecalculateBounds();
        _mesh.RecalculateNormals();

        _meshFilter = gameObject.AddComponent<MeshFilter>();
        _meshFilter.mesh = _mesh;
    }

    void Update()
    {
        var _verts = _mesh.vertices;
        for (int x = 0; x <= Dimensions; ++x)
        {
            for (int z = 0; z < Dimensions; ++z)
            {
                float y = 0f;

                for (int o = 0; o < Octaves.Length; o++)
                {
                    if (Octaves[o].Alternate)
                    {
                        var perl = Mathf.PerlinNoise((x * Octaves[o].Scale.x) / Dimensions, (z * Octaves[o].Scale.y) / Dimensions) * Mathf.PI * 2f;
                        y += Mathf.Cos(perl + Octaves[o].Speed.magnitude * Time.time) * Octaves[o].Height;
                    }
                    else
                    {
                        var perl = Mathf.PerlinNoise((x * Octaves[o].Scale.x + Time.time * Octaves[o].Speed.x) / Dimensions, (z * Octaves[o].Scale.y + Time.time * Octaves[o].Speed.y) / Dimensions) - 0.5f;
                        y += perl * Octaves[o].Height;
                    }
                }

                _verts[index(x, z)] = new Vector3(x, y, z);
            }
        }

        _mesh.vertices = _verts;
        _mesh.RecalculateNormals();
    }

    public float GetHeight(Vector3 position)
    {
        //scale factor and position in local space
        var scale = new Vector3(1 / transform.lossyScale.x, 0, 1 / transform.lossyScale.z);
        var localPos = Vector3.Scale((position - transform.position), scale);

        //get edge points
        var p1 = new Vector3(Mathf.Floor(localPos.x), 0, Mathf.Floor(localPos.z));
        var p2 = new Vector3(Mathf.Floor(localPos.x), 0, Mathf.Ceil(localPos.z));
        var p3 = new Vector3(Mathf.Ceil(localPos.x), 0, Mathf.Floor(localPos.z));
        var p4 = new Vector3(Mathf.Ceil(localPos.x), 0, Mathf.Ceil(localPos.z));

        //clamp if the position is outside the plane
        p1.x = Mathf.Clamp(p1.x, 0, Dimensions);
        p1.z = Mathf.Clamp(p1.z, 0, Dimensions);
        p2.x = Mathf.Clamp(p2.x, 0, Dimensions);
        p2.z = Mathf.Clamp(p2.z, 0, Dimensions);
        p3.x = Mathf.Clamp(p3.x, 0, Dimensions);
        p3.z = Mathf.Clamp(p3.z, 0, Dimensions);
        p4.x = Mathf.Clamp(p4.x, 0, Dimensions);
        p4.z = Mathf.Clamp(p4.z, 0, Dimensions);

        //get the max distance to one of the edges and take that to compute max - dist
        var max = Mathf.Max(Vector3.Distance(p1, localPos), Vector3.Distance(p2, localPos), Vector3.Distance(p3, localPos), Vector3.Distance(p4, localPos) + Mathf.Epsilon);
        var dist = (max - Vector3.Distance(p1, localPos))
                 + (max - Vector3.Distance(p2, localPos))
                 + (max - Vector3.Distance(p3, localPos))
                 + (max - Vector3.Distance(p4, localPos) + Mathf.Epsilon);
        //weighted sum
        var height = _mesh.vertices[index(p1.x, p1.z)].y * (max - Vector3.Distance(p1, localPos))
                   + _mesh.vertices[index(p2.x, p2.z)].y * (max - Vector3.Distance(p2, localPos))
                   + _mesh.vertices[index(p3.x, p3.z)].y * (max - Vector3.Distance(p3, localPos))
                   + _mesh.vertices[index(p4.x, p4.z)].y * (max - Vector3.Distance(p4, localPos));

        //scale
        return height * transform.lossyScale.y / dist;

    }


    private Vector3[] GenerateVertices()
    {
        var verts = new Vector3[(Dimensions + 1) * (Dimensions + 1)];

        //equaly distributed verts
        for (int x = 0; x <= Dimensions; x++)
            for (int z = 0; z <= Dimensions; z++)
                verts[index(x, z)] = new Vector3(x, 0, z);

        return verts;
    }

    private int index(float x, float z)
    {
        return (int)(x * (Dimensions + 1) + z);
    }

    private int[] GenerateTriangles()
    {
        var tries = new int[_mesh.vertices.Length * 6];

        //two triangles are one tile
        for (int x = 0; x < Dimensions; x++)
        {
            for (int z = 0; z < Dimensions; z++)
            {
                tries[index(x, z) * 6 + 0] = index(x, z);
                tries[index(x, z) * 6 + 1] = index(x + 1, z + 1);
                tries[index(x, z) * 6 + 2] = index(x + 1, z);
                tries[index(x, z) * 6 + 3] = index(x, z);
                tries[index(x, z) * 6 + 4] = index(x, z + 1);
                tries[index(x, z) * 6 + 5] = index(x + 1, z + 1);
            }
        }
        return tries;
    }

    private Vector2[] GenerateUVs()
    {
        var uvs = new Vector2[_mesh.vertices.Length];

        for (int x = 0; x <= Dimensions; x++)
        {
            for (int z = 0; z <= Dimensions; z++)
            {
                var vec = new Vector2((x / UVScale) % 2, (z / UVScale) % 2);
                uvs[index(x, z)] = new Vector2(vec.x <= 1 ? vec.x : 2 - vec.x, vec.y <= 1 ? vec.y : 2 - vec.y);
            }
        }

        return uvs;
    }

}