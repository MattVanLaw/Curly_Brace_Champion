using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // todo try to grab from actual block in grid. prefab?
    [SerializeField] float blockSize = 5f;
    [SerializeField] int numRows; 
    [SerializeField] int numCols;

    float maxRight;
    float maxUp;
    // Start is called before the first frame update
    void Start()
    {
        maxRight = numCols * blockSize;
        maxUp = numRows * blockSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (true)
        {
            Move();
        }
    }



    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            float nextLeftPos = (transform.position.x - blockSize);

            float newX = nextLeftPos < Mathf.Epsilon
                ? 0f
                : nextLeftPos;

            transform.position = new Vector3(
                newX,
                transform.position.y,
                transform.position.z
            );
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            float newX = (transform.position.x + blockSize) % maxRight;

            transform.position = new Vector3(
                newX,
                transform.position.y,
                transform.position.z
            );
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            float newZ = (transform.position.z + blockSize) % maxUp;

            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                newZ
            );
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            float nextZPos = transform.position.z - blockSize;

            float newZ = nextZPos < Mathf.Epsilon
                ? 0f
                : nextZPos;

            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                newZ
            );
        }
    }
}
