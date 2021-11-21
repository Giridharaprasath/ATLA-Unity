using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player Movement Controller - NOT USING
public class PMC : MonoBehaviour
{
    public float movementSpeed = 10.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    public float rotationSpeed = 2.0f;

    public CharacterController controller = null;
    public Animator animator = null;

    public Vector2 previousInput;
    public Vector3 playerVelocity;

    public bool playerGrounded;

    public bool isJumping;
    public bool isWalking;
    public bool isRunning;

    public Transform cameraMainTransform;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Transform GroundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private void Awake()
    {
        IM.Controls.Player.Move.performed += ctx => SetMovement(ctx.ReadValue<Vector2>());
        IM.Controls.Player.Jump.performed += ctx => isJumping = true;
        IM.Controls.Player.Run.performed += ctx => isRunning = true;

        IM.Controls.Player.Move.canceled += ctx => ResetMovement();
        IM.Controls.Player.Jump.canceled += ctx => isJumping = false;
        IM.Controls.Player.Run.canceled += ctx => isRunning = false;
        cameraMainTransform = Camera.main.transform;
    }

    private void SetMovement(Vector2 movement) 
    {
        previousInput = movement;
        isWalking = true;
    }

    private void ResetMovement()
    {
        previousInput = Vector2.zero;
        isWalking = false;
    }

    private void Update()
    {
        if (isWalking && !isRunning) movementSpeed = 10.0f;
        else if (isWalking && isRunning) movementSpeed = 15.0f;
        Debug.Log(isRunning);

        // playerGrounded = controller.isGrounded;
        // if (playerGrounded && playerVelocity.y < 0)
        // {
        //     playerVelocity.y = -gravityValue * Time.deltaTime;;
        // }

        playerGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);
        if (playerGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -10f;
        }

        Vector3 InputMove = new Vector3(previousInput.x, 0, previousInput.y).normalized;

        if (InputMove != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(InputMove.x, InputMove.z) * Mathf.Rad2Deg + cameraMainTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * movementSpeed * Time.deltaTime);

            animator.SetBool("isWalking", isWalking);
            animator.SetBool("isRunning", isRunning);
        }

        else
        {
            animator.SetBool("isWalking", isWalking);
            animator.SetBool("isRunning", isRunning);
        }

        if (isJumping && playerGrounded)
        {
            playerVelocity.y += jumpHeight * 10f;
        }
        playerVelocity.y -= gravityValue * Time.deltaTime;
        Vector3 fallV = new Vector3(0, playerVelocity.y, 0);
        controller.Move(fallV * Time.deltaTime);

        // if (isJumping && playerGrounded)
        // {
        //     playerVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravityValue);
        // }

        // playerVelocity.y += gravityValue * Time.deltaTime;
        // controller.Move(playerVelocity * Time.deltaTime);
    }
}
