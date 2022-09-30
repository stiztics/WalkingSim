using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Transform target;
    private Rigidbody rb;
    public float moveSpeed = 5f;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        Vector3 pos = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(pos);
        transform.LookAt(target);
    }
}