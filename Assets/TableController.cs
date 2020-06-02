using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    [SerializeField] GameObject tableLeft;
    [SerializeField] GameObject tableRight;
    [SerializeField] GameObject mainCollider;

    [SerializeField] Vector3 explosionLeverRight = new Vector3(1000f, 1000f, 1000f);
    [SerializeField] Vector3 explosionLeverLeft = new Vector3(-1000f, 1000f, 1000f);
    Rigidbody tableLeftRigidBody;
    Rigidbody tableRightRigidBody;

    private void Awake()
    {
        tableLeftRigidBody = tableLeft.GetComponent<Rigidbody>();
        tableRightRigidBody = tableRight.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        tableLeftRigidBody.isKinematic = true;
        tableRightRigidBody.isKinematic = true;
    }

    private void OnMouseUp()
    {
        Destroy(mainCollider);

        tableLeftRigidBody.isKinematic = false;
        tableRightRigidBody.isKinematic = false;

        tableLeftRigidBody.AddForce(explosionLeverLeft);
        tableRightRigidBody.AddForce(explosionLeverRight);
    }
}
