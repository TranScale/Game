using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float runSpeed;
    public float rotationSpeed;
    public KeyCode Run = KeyCode.LeftShift;

    [Header("References")]
    public Transform cam;     // KÉO MAIN CAMERA VÀO ĐÂY
    public Rigidbody rb;
    public Animator animator;

    private float vertical;
    private float horizontal;

    private bool isWalk;
    private bool isRun;
    private float currentMaxSpeed;


    private void Start()
    {
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MyInput();
        HandleAnimation();
    }

    private void FixedUpdate()
    {
        CharacterMove();
        SpeedControl();
    }

    void MyInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        isWalk = (horizontal != 0 || vertical != 0);
        isRun = isWalk && Input.GetKey(Run);
    }
    void HandleAnimation()
    {
        animator.SetBool("isWalk", isWalk);
        animator.SetBool("isRun", isRun);
    }

    void CharacterMove()
    {
        Vector3 forward = cam.forward;
        Vector3 right = cam.right;
        isWalk = (horizontal != 0 || vertical != 0);
        isRun = isWalk && Input.GetKey(Run);

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * vertical + right * horizontal;

        if (moveDirection.magnitude > 0.1f)
        {
            // Xoay nhân vật
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            rb.linearVelocity = new Vector3(
                moveDirection.normalized.x * currentMaxSpeed,
                rb.linearVelocity.y,
                moveDirection.normalized.z * currentMaxSpeed
            );
        }
        else
        {
            // Dừng mượt
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
        }
    }


    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        currentMaxSpeed = isRun ? runSpeed : (isWalk ? moveSpeed : 0f);

        if (flatVel.magnitude > currentMaxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * currentMaxSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }
}
