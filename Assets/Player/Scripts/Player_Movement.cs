using UnityEngine;
using UnityEngine.UIElements;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] Input_Handler inputHandler;
    
    private CharacterController characterController;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 5f;
    const float gravity = -9.81f;

    [SerializeField] private Transform cam;
    Vector3 velocity;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        if(cam == null && Camera.main != null)
        {
            cam = Camera.main.transform;
        }
    }

    private void FixedUpdate()
    {
        Vector2 moveDir = inputHandler.MoveDir;

        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0;
        camRight.y = 0;


        camForward.Normalize();
        camRight.Normalize();

        Vector3 move = (camForward * moveDir.y + camRight * moveDir.x);

        if (inputHandler.IsJumping > 0 && characterController.isGrounded)
            velocity.y = Mathf.Sqrt(2f * -gravity * jumpForce);

        characterController.Move(move * speed * Time.deltaTime);

        if (characterController.isGrounded)
        {
            if(velocity.y < 0)
            {
                velocity.y = -2f;
            }
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);


    }
}
