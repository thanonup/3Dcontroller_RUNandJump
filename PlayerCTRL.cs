using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCTRL : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float gravity;
    [SerializeField] float jumpSpeed;
    [SerializeField] float rotateSpeed;

    float Runspeed = 10;
    float defaultSpeed;


    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private int jumps;

    private void Awake()
    {
        defaultSpeed = speed;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();        
    }
    void Update()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpSpeed;               
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = Runspeed;
            }
            else { speed = defaultSpeed; }

            jumps = 0;
        }
        else
        {
            moveDirection = new Vector3(0, moveDirection.y, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection.x *= speed;
            moveDirection.z *= speed;
            if (Input.GetKeyDown(KeyCode.Space)&& jumps < 1)
            {
                moveDirection.y = jumpSpeed;
                jumps++;
            }
        }

        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
