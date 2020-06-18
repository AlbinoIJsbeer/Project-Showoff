using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimalEvent : MonoBehaviour
{
    [SerializeField] private GameObject animal;
    [SerializeField] private GameObject sea;
    private BoxCollider col;

    [SerializeField] private float timeInBetweenSpawns;
    private float time;

    public static int numberOfSpawns;
    [SerializeField] private int maxNumberOfSpawns;

    void Start()
    {
        col = sea.GetComponent<BoxCollider>();
        time = timeInBetweenSpawns;
        numberOfSpawns = 0;
    }

    void Update()
    {
        TimeToSpawn();
    }

    private void SpawnAnimal(GameObject _animal)
    {
        if (_animal == null) return;

        float xRandom = Random.Range(col.bounds.min.x + 50, col.bounds.max.x - 50);
        float zRandom = Random.Range(col.bounds.min.z + 125, col.bounds.max.z - 50);

        GameObject temp = Instantiate(_animal);
        Vector3 randomPos = new Vector3(xRandom, transform.position.y, zRandom);

        temp.gameObject.transform.position = randomPos;
        temp.transform.SetParent(transform);
    }

    private void TimeToSpawn()
    {
        time -= Time.deltaTime;

        if (time <= 0 & numberOfSpawns <= maxNumberOfSpawns)
        {
            SpawnAnimal(animal);
            numberOfSpawns++;
            time = timeInBetweenSpawns;
        }
    }
}
