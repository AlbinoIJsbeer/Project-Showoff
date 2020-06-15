using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPile : MonoBehaviour
{
    [SerializeField] private List<GameObject> trashObjects;

    public int numberOfObjects = 62;
    public float spawnRadius = 20;

    private List<Vector3> usedPoints;
    public bool autoUpdate;

    private void Start()
    {
        usedPoints = new List<Vector3>();
    }

    private void GenerateTrashPile(List<GameObject> go, float _spawnRadius, int _numberOfTrash)
    {
        if (go == null) return;

        for (int i = 0; i < _numberOfTrash; i++)
        {
            GameObject temp = Instantiate(go[Random.Range(0, go.Count)]);
            Vector3 rndPos = GetRandomPosition(_spawnRadius);

            usedPoints.Add(rndPos);
            temp.gameObject.transform.position = rndPos;
            temp.transform.eulerAngles = new Vector3(temp.transform.eulerAngles.x, Random.Range(0, 360), temp.transform.eulerAngles.z);
            temp.transform.SetParent(transform);
        }
    }

    private Vector3 GetRandomPosition(float _spawnRadius)
    {
        float xRnd = 0;
        float zRnd = 0;

        xRnd = Random.Range(0, _spawnRadius);
        zRnd = Random.Range(0, _spawnRadius);

        Vector3 tempVec = new Vector3(transform.position.x + xRnd, transform.position.y, transform.position.z + zRnd);

        if (usedPoints.Contains(tempVec)) return GetRandomPosition(_spawnRadius);

        return tempVec;
    }

    public void GeneratePile()
    {
        GenerateTrashPile(trashObjects, spawnRadius, numberOfObjects);
    }

    public void ClearPile()
    {
        for (int i = transform.childCount; i > 0; --i)
            DestroyImmediate(transform.GetChild(0).gameObject);
    }
}
