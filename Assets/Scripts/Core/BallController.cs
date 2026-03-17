using UnityEngine;

public class BallController : MonoBehaviour
{
    public Vector3 LastMoveDirection { get; private set; } = Vector3.forward;

    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform cameraTransform;

    [Header("Move")]
    [SerializeField] private float moveForce = 25f;
    [SerializeField] private float maxSpeed = 7f;

    [Header("Jump")]
    [SerializeField] private float jumpImpulse = 6f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.7f;

    [Header("Landing Sound")]
    public AudioClip landingSound;
    private AudioSource audioSource;

    private bool wasGrounded;

    private Vector2 moveInput; // x = left/right, y = forward/back
    private bool jumpQueued;
    public bool controllerEnabled = true;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!controllerEnabled)
            return;

#if UNITY_EDITOR
        HandleKeyboardInput();
#endif
    }

    private void HandleKeyboardInput()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
            jumpQueued = true;
    }

    private void FixedUpdate()
    {
        bool isGrounded = IsGrounded();

        // Detect landing
        if (!wasGrounded && isGrounded && rb.linearVelocity.y < -2f)
        {
            audioSource.PlayOneShot(landingSound);
        }

        wasGrounded = isGrounded;

        if (!controllerEnabled)
            return;

        if (cameraTransform == null)
            return;

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        if (moveInput.sqrMagnitude < 0.0001f && !jumpQueued)
            return;

        Vector3 moveDir = camRight * moveInput.x + camForward * moveInput.y;

        // Keep analog strength for joystick, but prevent values > 1
        moveDir = Vector3.ClampMagnitude(moveDir, 1f);

        if (moveDir.sqrMagnitude > 0.001f)
            LastMoveDirection = moveDir.normalized;

        rb.AddForce(moveDir * moveForce, ForceMode.Force);

        Vector3 v = rb.linearVelocity;
        Vector3 flat = new Vector3(v.x, 0f, v.z);
        if (flat.magnitude > maxSpeed)
        {
            flat = flat.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(flat.x, v.y, flat.z);
        }

        if (moveDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir.normalized, Vector3.up);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, 10f * Time.fixedDeltaTime));
        }

        if (jumpQueued && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
        }

        jumpQueued = false;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    public float getJumpImpulse()
    {
        return jumpImpulse;
    }

    public void setJumpImpulse(float newimpulse)
    {
        jumpImpulse = newimpulse;
    }

    // ----- UI Hold Controls (Buttons) -----
    public void PressLeft() => moveInput.x = -1f;
    public void PressRight() => moveInput.x = 1f;
    public void ReleaseX() => moveInput.x = 0f;

    public void PressUp() => moveInput.y = 1f;
    public void PressDown() => moveInput.y = -1f;
    public void ReleaseY() => moveInput.y = 0f;

    public void PressJump() => jumpQueued = true;

    // ----- Joystick Controls -----
    public void SetMoveInput(Vector2 input)
    {
        moveInput = Vector2.ClampMagnitude(input, 1f);
    }

    // Optional helper if you ever want to force-stop movement
    public void ClearMoveInput()
    {
        moveInput = Vector2.zero;
    }
}