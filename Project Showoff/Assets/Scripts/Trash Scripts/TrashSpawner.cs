using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public List<GameObject> Trash = new List<GameObject>();

    public GameObject sea;
    private BoxCollider col;
    public int numberOfTrash;
    private List<Vector3> usedPoints;

    public float scale;
    public int octaves;
    public int seed;
    [Range(0, 1)]
    public float persistence;
    public float lacunarity; 
    public Vector2 offset;
    public bool autoUpdate;

    void Start()
    {
        usedPoints = new List<Vector3>();
        col = sea.GetComponent<BoxCollider>();
        GenerateObjects();
    }

    void GenerateObject(List<GameObject> go, int amount)
    {
        float[,] noiseMap = Noise.GenerateNoiseMap((int)col.bounds.size.x, (int)col.bounds.size.z, seed, scale, octaves, persistence, lacunarity, offset);

        if (go == null) return;

        for (int i = 0; i < amount; i++)
        {
            GameObject temp = Instantiate(go[Random.Range(0, go.Count)]);
            Vector3 randomPosition = GetRandomPosition();

            while (noiseMap[(int)randomPosition.x + 375, (int)randomPosition.z + 125] < 0.7f)
            {
                randomPosition = GetRandomPosition();
            }

            usedPoints.Add(randomPosition);
            temp.gameObject.transform.position = new Vector3(randomPosition.x, transform.position.y, randomPosition.z);
            temp.transform.eulerAngles = new Vector3(temp.transform.eulerAngles.x, Random.Range(0, 360), temp.transform.eulerAngles.z);
            temp.transform.SetParent(transform);
        }
    }

    void GenerateObjects()
    {
        GenerateObject(Trash, numberOfTrash);
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
        GenerateObjects();
    }

    public void ClearTrash()
    {
        for (int i = transform.childCount; i > 0; --i)
            DestroyImmediate(transform.GetChild(0).gameObject);
    }
}
