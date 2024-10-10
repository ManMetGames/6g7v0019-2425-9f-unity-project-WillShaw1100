using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 720.0f;
    public float jumpSpeed = 8.0f;
    public float jumpGracePeriod = 0.2f;
    private CharacterController controller;
    private Animator animator;

    private float ySpeed;
    private float originalStepOffset;
    private float? lastTimeOnGround; // Nullable type
    private float? lastTimeJumpButtonPressed; // Nullable type

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalStepOffset = controller.stepOffset;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Debug.Log($"Horizontal input: {horizontal}, Vertical input: {vertical}");

        Vector3 movementDirection = new Vector3(horizontal, 0, vertical);
        float magnitude = Mathf.Clamp(movementDirection.magnitude, 0, 1) * movementSpeed;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        //the lastTimeOnGround and lastTimeJumpButtonPressed variables are used to implement a jump grace period if the player hits the jump key a fraction of a seccond too early or late.

        if (controller.isGrounded)
        {
            lastTimeOnGround = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
        {
            lastTimeJumpButtonPressed = Time.time;
        }

        if (Time.time - lastTimeOnGround <= jumpGracePeriod)
        {
            controller.stepOffset = originalStepOffset;
            ySpeed = -0.5f;

            if (Time.time - lastTimeJumpButtonPressed <= jumpGracePeriod)
            {
                ySpeed = jumpSpeed;
                lastTimeJumpButtonPressed = null;
                lastTimeOnGround = null;
            }
        }
        else
        {
            controller.stepOffset = 0.0f;
        }

            Vector3 velocity = movementDirection * magnitude;
            velocity.y = ySpeed;

            controller.Move(velocity * Time.deltaTime);

            if(movementDirection != Vector3.zero)
            {
            animator.SetBool("IsMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        else
        {
            animator.SetBool("IsMoving", false);
        }

    }
}