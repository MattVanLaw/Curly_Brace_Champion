using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject tablePrefab;

    // TODO: Pull into TableController
    [Header("Table")]
    [SerializeField] float tableSpawnRate = 3f;
    [SerializeField] int tableSpeed = 1000;
    List<Vector3> notSoRandomPositions = new List<Vector3>();

    // Start is called before the first frame update
    void OnMouseUp()
    {
        SetupArrayOfPossiblePositions();
        StartCoroutine(SpawnInRandomPosition());
    }

    private void SetupArrayOfPossiblePositions()
    {
        float flatLandY = -2.46f;

        notSoRandomPositions.Add(new Vector3(0f, flatLandY, 30f));
        notSoRandomPositions.Add(new Vector3(5f, flatLandY, 30f));
        notSoRandomPositions.Add(new Vector3(10f, flatLandY, 30f));
    }

    IEnumerator SpawnInRandomPosition()
    {
        while (true)
        {
            System.Random random = new System.Random();

            int randomIdx = Convert.ToInt32(random.Next(0, notSoRandomPositions.Count));

            print("Random idx" + randomIdx);
            Vector3 pos = notSoRandomPositions[randomIdx];

            yield return new WaitForSeconds(tableSpawnRate);

            SpawnTable(pos);
        }
    }
    private void SpawnTable(Vector3 pos)
    {
        // TODOS
        // get random appropriate position
        // loop over number to spawn at random position
        // apply force to foward at player (-z)
        GameObject instanceOfTable = Instantiate(tablePrefab, pos, Quaternion.identity);

        instanceOfTable.GetComponent<Rigidbody>().AddForce(Vector3.back * tableSpeed);
    }
}

