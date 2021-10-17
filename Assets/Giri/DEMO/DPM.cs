using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPM : MonoBehaviour
{
    [Header("Player")]
	[Tooltip("Move speed of the character in m/s")]
	public float moveSpeed = 2.0f;
	[Tooltip("Sprint speed of the character in m/s")]
	public float sprintSpeed = 5.335f;
	[Tooltip("How fast the character turns to face movement direction")]
	[Range(0.0f, 0.3f)]
	public float rotationSmoothTime = 0.12f;
	[Tooltip("Acceleration and deceleration")]
	public float speedChangeRate = 10.0f;

    [Space(10)]
	[Tooltip("The height the player can jump")]
	public float jumpHeight = 1.2f;
	[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
	public float gravity = -15.0f;

    [Space(10)]
	[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
	public float jumpTimeout = 0.50f;
	[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
	public float fallTimeout = 0.15f;   

    [Header("Player Grounded")]
	[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
	public bool isGrounded = true;
	[Tooltip("Useful for rough ground")]
	public float groundedOffset = -0.14f;
	[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
	public float groundedRadius = 0.4f;
	[Tooltip("What layers the character uses as ground")]
	public LayerMask groundLayers;

    [Header("Cinemachine")]
	[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
	public GameObject cinemachineCameraTarget;
	[Tooltip("How far in degrees can you move the camera up")]
	public float topClamp = 70.0f;
	[Tooltip("How far in degrees can you move the camera down")]
	public float bottomClamp = -30.0f;
	[Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
	public float cameraAngleOverride = 0.0f;
	[Tooltip("For locking the camera position on all axis")]
	public bool lockCameraPosition = false;

    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool run;
    public bool jump;

    // cinemachine
	private float _cinemachineTargetYaw;
	private float _cinemachineTargetPitch;

	// player
	private float _speed;
	private float _animationBlend;
	private float _targetRotation = 0.0f;
	private float _rotationVelocity;
	private float _verticalVelocity;
	private float _terminalVelocity = 53.0f;

    // timeout deltatime
	private float _jumpTimeoutDelta;
	private float _fallTimeoutDelta;

    // animation IDs
    private int _animIDSpeed;
    private int _animIDMotionSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;

    public Animator _animator;
	public CharacterController _controller;
	public Transform _mainCamera;

    private const float _threshold = 0.01f;

    private Color transparentGreen = new Color(0f, 1f, 0f, 0.35f);
    private Color transparentRed = new Color(1f, 0f, 0f, 0.35f);

    private void Awake()
    {
        _mainCamera = Camera.main.transform;
        IM.Controls.Player.Move.performed += ctx => SetMovement(ctx.ReadValue<Vector2>());
        IM.Controls.Player.Look.performed += ctx => SetLook(ctx.ReadValue<Vector2>());
        IM.Controls.Player.Run.performed += ctx => run = true;
        IM.Controls.Player.Jump.performed += ctx => jump = true;

        IM.Controls.Player.Move.canceled += ctx => ResetMovement();
        IM.Controls.Player.Look.canceled += ctx => ResetLook();
        IM.Controls.Player.Run.canceled += ctx => run = false;
        IM.Controls.Player.Jump.canceled += ctx => jump = false;

        AssignAnimationIDs();
    }

    private void SetMovement(Vector2 movement) => move = movement;

    private void SetLook(Vector2 currentLook) => look = currentLook;

    private void ResetMovement() => move = Vector2.zero;

    private void ResetLook() => look = Vector2.zero;

    private void Update()
    {
        GroundedCheck();
        JumpAndGravity();
        Move();
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        _animIDGrounded = Animator.StringToHash("IsGrounded");
        _animIDJump = Animator.StringToHash("ToJump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
    }

    private void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        isGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);

        _animator.SetBool(_animIDGrounded, isGrounded);
    }

    private void CameraRotation()
    {
        if (look.sqrMagnitude >= _threshold && !lockCameraPosition)
        {
            _cinemachineTargetYaw += look.x * Time.deltaTime;
            _cinemachineTargetPitch += look.y * Time.deltaTime;
        }

        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, bottomClamp, topClamp);

        cinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + cameraAngleOverride, _cinemachineTargetYaw, 0.0f);
    }

    private void Move()
    {
        float targetSpeed = run ? sprintSpeed : moveSpeed;

        if (move == Vector2.zero) targetSpeed = 0f;

        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0f, _controller.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = move.magnitude;

        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * speedChangeRate);

            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }
        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * speedChangeRate);

        Vector3 inputDirection = new Vector3(move.x, 0f, move.y).normalized;

        if (move != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, rotationSmoothTime);

            transform.rotation = Quaternion.Euler(0f, rotation, 0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0f, _targetRotation, 0f) * Vector3.forward;

        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime);

        _animator.SetFloat(_animIDSpeed, _animationBlend);
        _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
    }

    private void JumpAndGravity()
    {
        if (isGrounded)
        {
            _fallTimeoutDelta = fallTimeout;

            _animator.SetBool(_animIDJump, false);
            _animator.SetBool(_animIDFreeFall, false);

            if (_verticalVelocity < 0f) _verticalVelocity = -2f;

            if (jump && _jumpTimeoutDelta <= 0f)
            {
                _verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

                _animator.SetBool(_animIDJump, true);
            }

            if (_jumpTimeoutDelta >= 0f) _jumpTimeoutDelta -= Time.deltaTime;
        }
        else
        {
            _jumpTimeoutDelta = jumpTimeout;

            if (_fallTimeoutDelta >= 0f) _fallTimeoutDelta -= Time.deltaTime;
            else _animator.SetBool(_animIDFreeFall, true);
        }

        if (_verticalVelocity < _terminalVelocity) 
        {
            _verticalVelocity += gravity * Time.deltaTime;
        }
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    private void OnDrawGizmos()
    {
        if (isGrounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z), groundedRadius);
    }

}
