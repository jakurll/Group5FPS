using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -9.81f;
    public uint health = 100;

    private CharacterController _controller;
    private float _velocity;
    [SerializeField] float jumpHeight = 5f;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        ApplyGravity();
        ApplyMovement();

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void ApplyMovement()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        move = move.z * transform.forward.normalized + move.x * transform.right.normalized;
        move.y = _velocity;

        _controller.Move(move * Time.deltaTime * speed);
    }

    private void ApplyGravity()
    {
        if (_controller.isGrounded)
        {
            _velocity = -1f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _velocity = jumpHeight;
            }
        }

        _velocity -= gravity * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("Enemy"))
      {
        health--;
      }
    }
}

