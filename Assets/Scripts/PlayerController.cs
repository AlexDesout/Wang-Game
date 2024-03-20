using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 moveValue;

    public float speed = 20f;
    public float jumpForce = 10f;
    private bool isJumped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Déplacement horizontal
        float horizontalMovement = moveValue.x * speed * Time.deltaTime;
        float verticalMovement = moveValue.y * speed * Time.deltaTime;
        transform.Translate(new Vector3(horizontalMovement, 0, verticalMovement));

        // Saut
        if (isJumped)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumped = false;
        }
    }

    public void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        isJumped = true;
    }
}
