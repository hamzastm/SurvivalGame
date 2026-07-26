using UnityEngine;

public class Cam_Look : MonoBehaviour
{
    [SerializeField] private Input_Handler inputHandler;

    private float xRotation = 0f;
    private float yRotation = 0f;
    private float clampedY = 90f;

    [SerializeField] private float sensitivity = 100f;

    [SerializeField] private Transform player;


    private void Update()
    {
        float mouseX = inputHandler.LookDir.x * sensitivity * Time.deltaTime;
        float mouseY = inputHandler.LookDir.y * sensitivity * Time.deltaTime;

        xRotation += mouseX;
        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -clampedY, clampedY);

        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        player.rotation = Quaternion.Euler(0f, xRotation, 0f);
    }





}
