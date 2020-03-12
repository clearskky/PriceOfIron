using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    public Camera camera;
    public float moveSpeed;
    CharacterController characterController;
    Animator anim;
    RaycastHit hit;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }
    void Update()
    {
        //RotatePlayer();
        AnimatePlayer();
    }

    
    void FixedUpdate()
    {
        CorrectPlayerYPosition();
        RotatePlayer();
        MovePlayer();
    }

    private void CorrectPlayerYPosition()
    {
        if (transform.position.y != 0.657)
        {
            Vector3 correctedPos = new Vector3(transform.position.x, 0.657f, transform.position.z);
            transform.position = correctedPos;
        }
    }

    void AnimatePlayer()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(h, 0, v);
        if (moveDirection.magnitude > 1.0f)
        {
            moveDirection = moveDirection.normalized;
        }
        moveDirection = transform.InverseTransformDirection(moveDirection);
        anim.SetFloat("velocityX", moveDirection.x);
        anim.SetFloat("velocityY", moveDirection.z);
    }
    void RotatePlayer()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 horizontalPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(horizontalPoint);
        }
    }
    void MovePlayer()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput * moveSpeed;
        characterController.Move(moveVelocity * Time.fixedDeltaTime);
    }
}
