using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Win/Lose Game Objects")]
    [SerializeField] GameObject bossElephant;
    [SerializeField] GameObject baseCurly;

    // TODO: Pull into TableController
    [Header("Table")]
    [SerializeField] GameObject tableParent;
    [SerializeField] GameObject tablePrefab;
    [SerializeField] float tableSpawnRate = 3f;
    [SerializeField] int tableSpeed = 1000;

    List<Vector3> notSoRandomPositions = new List<Vector3>();

    void Update()
    {
        ProcessElephantStatus();
        ProcessCrulyStatus();
    }

    private void ProcessElephantStatus()
    {
        if (!bossElephant.gameObject) return;

        bool isBossAlive = bossElephant.GetComponent<ShrinkToDeath>().GetIsAlive();
        if (!isBossAlive)
        {
            Destroy(bossElephant.gameObject);
            RestartGame();
        }
    }
    
    private void ProcessCrulyStatus()
    {
        if (!baseCurly.gameObject) return;

        bool isBaseCurlyAlive = baseCurly.GetComponent<ShrinkToDeath>().GetIsAlive();
        if (!isBaseCurlyAlive)
        {
            Destroy(baseCurly.gameObject);
            RestartGame();
        }
    }

    // Start is called before the first frame update
    void OnMouseUp()
    {
        SetupArrayOfPossiblePositions();
        StartCoroutine(SpawnInRandomPosition());
    }

    private void OnCollisionEnter(Collision collision)
    {
        SetupArrayOfPossiblePositions();
        StartCoroutine(SpawnInRandomPosition());
    }

    private void SetupArrayOfPossiblePositions()
    {
        float flatLandY = -2.46f;

        notSoRandomPositions.Add(new Vector3(-2.16f, flatLandY, 30f));
        notSoRandomPositions.Add(new Vector3(2.84f, flatLandY, 30f));
        notSoRandomPositions.Add(new Vector3(7.54f, flatLandY, 30f));
        notSoRandomPositions.Add(new Vector3(12.25f, flatLandY, 30f));
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
        GameObject instanceOfTable = Instantiate(tablePrefab, pos, Quaternion.identity);
        instanceOfTable.transform.parent = tableParent.transform;
        instanceOfTable.GetComponent<Rigidbody>().AddForce(Vector3.back * tableSpeed);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}

