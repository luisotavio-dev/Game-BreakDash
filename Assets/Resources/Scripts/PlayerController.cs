using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private float input;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        // transform.position += input * moveSpeed * Time.deltaTime * Vector3.right;
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = input * moveSpeed * Vector2.right;
    }
}
