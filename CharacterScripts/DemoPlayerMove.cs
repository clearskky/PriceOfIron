using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
//[RequireComponent(typeof(Animator))]
public class DemoPlayerMove : MonoBehaviour
{
    public Camera camera;
    public int moveSpeed;
    CharacterController characterController;
    Animator anim;
    RaycastHit hit;

    void Start()
    {
        Vector3 newPosition = new Vector3(transform.position.x, -2.47f, transform.position.z);
        transform.position = newPosition;
        characterController = GetComponent<CharacterController>();
        //anim = GetComponent<Animator>();
        anim = GameObject.Find("ybot").GetComponent<Animator>();
    }
    void Update()
    {
        RotatePlayer();
        AnimatePlayer();
    }

    void FixedUpdate()
    {
        MovePlayer();
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

        //Vector3 Velx = camera.transform.forward * Input.GetAxis("Vertical");
        //anim.SetFloat("velocityX", Velx.x);
        //Vector3 Vely = camera.transform.right * Input.GetAxis("Horizontal");
        //anim.SetFloat("velocityY", Vely.y);
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
