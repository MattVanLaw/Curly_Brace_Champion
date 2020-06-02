using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody.isKinematic = true;
    }

    private void OnMouseUp()
    {
        rigidBody.isKinematic = false;
        rigidBody.AddForce(new Vector3(10000f, 10000f, 10000f));
    }
}
