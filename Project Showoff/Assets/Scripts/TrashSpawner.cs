using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    int z = 0;
    int x = 0;

    public GameObject trash;
    public GameObject sea;
    private BoxCollider col;

    public float scale;
    public int octaves;
    public int seed;
    [Range(0, 1)]
    public float persistence;
    public float lacunarity;
    
    public Vector2 offset;

    public bool autoUpdate;

    public int numberOfTrash;

    private List<Vector3> usedPoints;

    bool printed = false;

    void Start()
    {
        usedPoints = new List<Vector3>();
        col = sea.GetComponent<BoxCollider>();
        GenerateObjects();
    }

    void GenerateObject(GameObject go, int amount)
    {
        float[,] noiseMap = Noise.GenerateNoiseMap((int)col.bounds.size.x, (int)col.bounds.size.z, seed, scale, octaves, persistence, lacunarity, offset);

        if (go == null) return;

        for (int i = 0; i < amount; i++)
        {
            GameObject temp = Instantiate(go);
            Vector3 randomPosition = GetRandomPosition();

            while (noiseMap[(int)randomPosition.x+250, (int)randomPosition.z+125] < 0.5f)
            {
                randomPosition = GetRandomPosition();
            }

            usedPoints.Add(randomPosition);
            temp.gameObject.transform.position = new Vector3(randomPosition.x, 0, randomPosition.z);
            temp.transform.SetParent(transform);
        }
    }

    void GenerateObjects()
    {
        GenerateObject(trash, numberOfTrash);
    }

    private Vector3 GetRandomPosition()
    {      
        float xRandom = 0;
        float zRandom = 0;

        xRandom = Random.Range(col.bounds.min.x, col.bounds.max.x);
        zRandom = Random.Range(col.bounds.min.z, col.bounds.max.z);

        Vector3 tempVec = new Vector3(xRandom, 0.0f, zRandom);

        if (usedPoints.Contains(tempVec)) return GetRandomPosition();

        return tempVec;
    }

    public void GenerateTrash()
    {
        usedPoints = new List<Vector3>();
        col = sea.GetComponent<BoxCollider>();
        GenerateObjects();
    }

    public void ClearTrash()
    {
        for (int i = transform.childCount; i > 0; --i)
            DestroyImmediate(transform.GetChild(0).gameObject);
    }
    //private float[,] calculateNoiseMap()
    //{
    //    float[,] noiseMap = new float[(int)col.bounds.size.x, (int)col.bounds.size.z];

    //    if (scale <= 0)
    //    {
    //        scale = 0.0001f;
    //    }

    //    for (int z = 0; z < col.bounds.size.z; z++)
    //    {
    //        for (int x = 0; x < col.bounds.size.x; x++)
    //        {
    //            float sampleX = x / scale;
    //            float sampleZ = z / scale;

    //            float perlinValue = Mathf.PerlinNoise(sampleX, sampleZ);
    //            noiseMap[x, z] = perlinValue;
    //        }
    //    }

    //    return noiseMap;
    //}
}
