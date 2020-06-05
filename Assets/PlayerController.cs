using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // todo try to grab from actual block in grid. prefab?
    [SerializeField] float blockSize = 5f;
    [SerializeField] int numRows; 
    [SerializeField] int numCols;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileForce = 1000;

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
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject projectileInstance = Instantiate(projectile);
            projectileInstance.transform.position = new Vector3(transform.localPosition.x - 2f, transform.localPosition.y + 2.5f, transform.localPosition.z + 1);
            Rigidbody projectileRigidBody = projectileInstance.gameObject.GetComponent<Rigidbody>();
            projectileRigidBody.AddForce(0f, 0f, projectileForce);
        }
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            float nextLeftPos = Mathf.Clamp(
                (transform.position.x - blockSize),
                0,
                Mathf.Infinity
            );

            transform.position = new Vector3(
                nextLeftPos,
                transform.position.y,
                transform.position.z
            );
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            float newX = Mathf.Clamp(
                (transform.position.x + blockSize),
                0f,
                maxRight - blockSize
            );

            transform.position = new Vector3(
                newX,
                transform.position.y,
                transform.position.z
            );
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            float newZ = (transform.position.z + blockSize) % maxUp;

            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                newZ
            );
        }
        else if (Input.GetKeyDown(KeyCode.A))
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

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

        SceneManager.LoadScene(0);
    }


}
