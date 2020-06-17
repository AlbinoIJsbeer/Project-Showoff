using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstacles;
    [SerializeField] private GameObject sea;
    private BoxCollider col;

    [SerializeField] private int numberOfObstacles;
    private List<Vector3> usedPoints;
    

    void Start()
    {
        usedPoints = new List<Vector3>();
        col = sea.GetComponent<BoxCollider>();
        GenerateObjects();
    }

    void GenerateObject(List<GameObject> go, int amount)
    {
        if (go == null) return;

        for (int i = 0; i < amount; i++)
        {
            GameObject temp = Instantiate(go[Random.Range(0, go.Count)]);
            Vector3 randomPosition = GetRandomPosition();

            usedPoints.Add(randomPosition);
            temp.gameObject.transform.position = new Vector3(randomPosition.x, transform.position.y, randomPosition.z);
            temp.transform.eulerAngles = new Vector3(temp.transform.eulerAngles.x, Random.Range(0, 360), temp.transform.eulerAngles.z);
            temp.transform.SetParent(transform);
        }
    }

    private void GenerateObjects()
    {
        GenerateObject(obstacles, numberOfObstacles);
    }

    private Vector3 GetRandomPosition()
    {
        float xRandom = 0;
        float zRandom = 0;

        xRandom = Random.Range(col.bounds.min.x, col.bounds.max.x);
        zRandom = Random.Range(col.bounds.min.z + 125, col.bounds.max.z);

        Vector3 tempVec = new Vector3(xRandom, 0.0f, zRandom);

        if (usedPoints.Contains(tempVec)) return GetRandomPosition();

        return tempVec;
    }
}
