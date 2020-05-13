using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public GameObject trash;
    public GameObject sea;
    private BoxCollider col;

    public int numberOfTrash;

    private List<Vector3> usedPoints;

    void Start()
    {
        usedPoints = new List<Vector3>();
        col = sea.GetComponent<BoxCollider>();
        GenerateObjects();
    }

    void GenerateObject(GameObject go, int amount)
    {
        if (go == null) return;

        for (int i = 0; i < amount; i++)
        {
            GameObject temp = Instantiate(go);
            Vector3 randomPosition = GetRandomPosition();
            usedPoints.Add(randomPosition);
            temp.gameObject.transform.position = new Vector3(randomPosition.x, 0, randomPosition.z);
        }
    }

    void GenerateObjects()
    {
        GenerateObject(trash, numberOfTrash);
    }

    private Vector3 GetRandomPosition()
    {
        int xRandom = 0;
        int zRandom = 0;

        xRandom = (int)Random.Range(col.bounds.min.x, col.bounds.max.x);
        zRandom = (int)Random.Range(col.bounds.min.z, col.bounds.max.z);

        Vector3 tempVec = new Vector3(xRandom, 0.0f, zRandom);

        if (usedPoints.Contains(tempVec)) return GetRandomPosition();

        return tempVec;
    }
}
