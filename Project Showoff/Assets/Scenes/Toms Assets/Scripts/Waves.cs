using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public int Dimensions = 10;
    public Octave[] Octaves;

    private protected MeshFilter _meshFilter;
    private protected Mesh _mesh;



    void Start()
    {
        _mesh = new Mesh();
        _mesh.name = gameObject.name;

        _mesh.vertices = GenerateVertices();
        _mesh.triangles = GenerateTriangles();
        _mesh.RecalculateBounds();

        _meshFilter = gameObject.AddComponent<MeshFilter>();
        _meshFilter.mesh = _mesh;

    }


    private Vector3[] GenerateVertices()
    {
        var verts = new Vector3[(Dimensions + 1) * (Dimensions + 1)];

        //equaly distributed verts
        for (int x = 0; x <= Dimensions; x++)
            for (int z = 0; z <= Dimensions; z++)
                verts[index(x, z)] = new Vector3(x, 0, z);

        Debug.Log(verts.Length);
        return verts;
    }

    private int index(int x, int z)
    {
        return x * (Dimensions + 1) + z;
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

        Debug.Log(tries.Length);
        return tries;
    }


    void Update()
    {
        var _verts = _mesh.vertices;
        for(int x = 0; x <= Dimensions; ++x)
        {
            for (int z = 0; z < Dimensions; ++z)
            {
                float y = 0f;

                for (int o = 0; o < Octaves.Length; ++o)
                {
                    if (Octaves[o].Alternate)
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
                }

                _verts[index(x, z)] = new Vector3(x, y, z);
            }
        }

        _mesh.vertices = _verts;
    }



    [Serializable]
    public struct Octave
    {
        public Vector2 Speed;
        public Vector2 Scale;
        public float Height;
        public bool Alternate;


    }
}