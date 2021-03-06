﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // todo try to grab from actual block in grid. prefab?
    [SerializeField] GameObject leafParent;
    [SerializeField] float blockSize = 5f;
    [SerializeField] int numRows; 
    [SerializeField] int numCols;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileForce = 1000;
    [SerializeField] float projectileY = 1.5f;
    [SerializeField] float projectileX = -1.5f;
    [SerializeField] float projectileZ = 1;
    [SerializeField] Transform position;
    [SerializeField] float despawnTime = 4;
    float maxUps = 0f;
    float maxRight = 0f;

    // Start is called before the first frame update
    void Start()
    {
        maxUps = blockSize * numRows;
        maxRight = blockSize * numCols;
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
            projectileInstance.transform.parent = leafParent.transform;

            projectileInstance.transform.position = new Vector3(transform.localPosition.x + projectileX, transform.localPosition.y + projectileY, transform.localPosition.z + projectileZ);
            Rigidbody projectileRigidBody = projectileInstance.gameObject.GetComponent<Rigidbody>();
            projectileRigidBody.AddForce(0f, 0f, projectileForce);

            StartCoroutine(DespawnLeaf(projectileInstance));
        }
    }

    IEnumerator DespawnLeaf(GameObject projectileInstance)
    {
        yield return new WaitForSeconds(despawnTime);

        Destroy(projectileInstance);
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            float newX = (transform.position.x - blockSize);

            if (newX <= Mathf.Epsilon) return;

            transform.position = new Vector3(
                newX,
                transform.position.y,
                transform.position.z
            );
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            float newX = (transform.position.x + blockSize);

            if (newX >= maxRight) return;

            transform.position = new Vector3(
                newX,
                transform.position.y,
                transform.position.z
            );
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            float newZ = (transform.position.z + blockSize);

            if (newZ >= maxUps) return;

            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                newZ
            );
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            float newZ = (transform.position.z - blockSize);

            if (newZ <= Mathf.Epsilon) return;

            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                newZ
            );
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Friendly") return;

        Destroy(gameObject);

        SceneManager.LoadScene(0);
    }


}
