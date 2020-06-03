using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject tablePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SpawnTable());
    }

    IEnumerator SpawnTable()
    {
        // TODOS
        // get random appropriate position
        // loop over number to spawn at random position
        // apply force to foward at player (-z)
        print("YELP");
        //GameObject instanceOfTable = Instantiate(tablePrefab, transform.position, Quaternion.identity);

        //instanceOfTable.transform.Translate(new Vector3(15f, 15f, 15f));

        yield return new WaitForSeconds(10);
    }
}
