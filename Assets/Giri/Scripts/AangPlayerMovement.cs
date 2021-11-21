using UnityEngine;

public class AangPlayerMovement : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    private float sprintSpeed = 8f;
    [SerializeField]
    private float rotationSmoothTime = 0.12f;
    [SerializeField]
    private float speedChangeRate = 10f;

    [Space(10)]
    [SerializeField]
    private float jumpHeight = 1.2f;
    [SerializeField]
    private float gravity = -15f;

    [Space(10)]
    [SerializeField]
    private float jumpTimeOut = 0.5f;
    [SerializeField]
    private float fallTimeOut = 0.15f;

    [Header("Player Grounded")]
    public bool isGrounded = true;
    [SerializeField]
    private float groundedOffset = -0.14f;
    [SerializeField]
    private float groundedRadius = 0.4f;
    [SerializeField]
    private LayerMask groundLayers;

    [Header("TPC")]
    [SerializeField]
    private GameObject tpcTarget;
    [SerializeField]
    private float topClamp = 70f;
    [SerializeField]
    private float bottomClamp = -30f;
    [SerializeField]
    private float cameraAngleOverride = 0;
    [SerializeField]
    private bool lockCamPos = false;

    [Header("Character Input Values")]
    [SerializeField]
    private Vector2 move;
    [SerializeField]
    private Vector2 look;
    [SerializeField]
    private bool run;
    [SerializeField]
    private bool jump;

    // Third Person Camera
    private float _tpcTargetYaw;
    private float _tpcTargetPitch;

    // Player
    private float _speed;
    private float _animationBlend;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;

    // Timeout deltaTime
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    // Animation IDs
    private int _animIDSpeed;
    private int _animIDMotionSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private CharacterController _controller;
    [SerializeField]
    private Transform _mainCamera;

    private const float _threshold = 0.01f;

    private Color transparentGreen = new Color(0f, 1f, 0f, 0.35f);
    private Color transparentRed = new Color(1f, 0f, 0f, 0.35f);

    private void Awake()
    {
        _mainCamera = Camera.main.transform;
        InputManager.Controls.Player.Move.performed += ctx => SetMovement(ctx.ReadValue<Vector2>());
        InputManager.Controls.Player.Look.performed += ctx => SetLook(ctx.ReadValue<Vector2>());
        InputManager.Controls.Player.Run.performed += ctx => run = true;
        InputManager.Controls.Player.Jump.performed += ctx => jump = true;

        InputManager.Controls.Player.Move.canceled += ctx => ResetMovement();
        InputManager.Controls.Player.Look.canceled += ctx => ResetLook();
        InputManager.Controls.Player.Run.canceled += ctx => run = false;
        InputManager.Controls.Player.Jump.canceled += ctx => jump = false;
        AssignAnimationIDs();
    }

    private void SetMovement(Vector2 movement) => move = movement;
    
    private void SetLook(Vector2 currentLook) => look = currentLook;

    private void ResetMovement() => move = Vector2.zero;

    private void ResetLook() => look = Vector2.zero;

    private void Update()
    {
        GroundCheck();
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

    private void GroundCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        isGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);

        _animator.SetBool(_animIDGrounded, isGrounded);
    }

    private void CameraRotation()
    {
        if (look.sqrMagnitude >= _threshold && !lockCamPos)
        {
            _tpcTargetYaw += look.x * Time.deltaTime;
            _tpcTargetPitch += look.y * Time.deltaTime;
        }

        _tpcTargetYaw = ClampAngle(_tpcTargetYaw, float.MinValue, float.MaxValue);
        _tpcTargetPitch = ClampAngle(_tpcTargetPitch, bottomClamp, topClamp);

        tpcTarget.transform.rotation = Quaternion.Euler(_tpcTargetPitch + cameraAngleOverride, _tpcTargetYaw, 0.0f);
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
            _fallTimeoutDelta = fallTimeOut;

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
            _jumpTimeoutDelta = jumpTimeOut;

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