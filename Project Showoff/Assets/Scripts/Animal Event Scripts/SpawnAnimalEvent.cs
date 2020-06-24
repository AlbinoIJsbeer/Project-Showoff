using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimalEvent : MonoBehaviour
{
    [SerializeField] private GameObject animal;
    [SerializeField] private GameObject sea;
    private BoxCollider col;

    //[SerializeField] private float timeInBetweenSpawns;
    //private float time;

    [SerializeField] private int numberOfSpawns;
    //[SerializeField] private int maxNumberOfSpawns;

    private List<Vector3> usedPoints;

    void Start()
    {
        usedPoints = new List<Vector3>();
        col = sea.GetComponent<BoxCollider>();
        SpawnAnimals(animal, numberOfSpawns);
        //time = timeInBetweenSpawns;
        //numberOfSpawns = 0;
    }

    void Update()
    {
        //TimeToSpawn();
    }

    //private void SpawnAnimal(GameObject _animal)
    //{
    //    if (_animal == null) return;

    //    float xRandom = Random.Range(col.bounds.min.x + 25, col.bounds.max.x - 25);
    //    float zRandom = Random.Range(col.bounds.min.z + 200, col.bounds.max.z - 25);

    //    GameObject temp = Instantiate(_animal);
    //    Vector3 randomPos = new Vector3(xRandom, transform.position.y, zRandom);

    //    temp.gameObject.transform.position = randomPos;
    //    temp.transform.SetParent(transform);
    //}

    //private void TimeToSpawn()
    //{
    //    time -= Time.deltaTime;

    //    if (time <= 0 & numberOfSpawns <= maxNumberOfSpawns)
    //    {
    //        SpawnAnimal(animal);
    //        numberOfSpawns++;
    //        time = timeInBetweenSpawns;
    //    }
    //}

    private void SpawnAnimals(GameObject _animal, int amount)
    {
        if (_animal == null) return;

        for (int i = 0; i < amount; i++)
        {
            GameObject temp = Instantiate(_animal);
            Vector3 randPosition = GetRandomPosition();

            usedPoints.Add(randPosition);
            temp.gameObject.transform.position = new Vector3(randPosition.x, transform.position.y, randPosition.z);
            temp.transform.SetParent(transform);
        }
    }

    private Vector3 GetRandomPosition()
    {
        float xRandom = Random.Range(col.bounds.min.x + 25, col.bounds.max.x - 25);
        float zRandom = Random.Range(col.bounds.min.z + 200, col.bounds.max.z - 25);

        Vector3 tempVec = new Vector3(xRandom, 0.0f, zRandom);

        if (usedPoints.Contains(tempVec)) return GetRandomPosition();

        return tempVec;
    }
}
