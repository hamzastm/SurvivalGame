using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Input_Handler inputHandler;
    [SerializeField] private Transform cam;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float acceleration = 60f; 
    [SerializeField] private float jumpForce = 5f;

    private const float gravity = -9.81f;

    private CharacterController characterController;
    private Vector3 velocity;
    private Vector3 currentHorizontalVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        if (cam == null && Camera.main != null)
        {
            cam = Camera.main.transform;
        }
    }

    private void Update()
    {
        bool isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f; 
        }

        Vector2 moveDir = inputHandler.MoveDir;

        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 targetHorizontalVelocity = (camForward * moveDir.y + camRight * moveDir.x) * speed;

        currentHorizontalVelocity = Vector3.MoveTowards(
            currentHorizontalVelocity,
            targetHorizontalVelocity,
            acceleration * Time.deltaTime
        );

        if (inputHandler.IsJumping && isGrounded)
        {
            velocity.y = Mathf.Sqrt(2f * -gravity * jumpForce);
        }

        velocity.y += gravity * Time.deltaTime;

        Vector3 finalMovement = currentHorizontalVelocity + velocity;
        characterController.Move(finalMovement * Time.deltaTime);
    }
}